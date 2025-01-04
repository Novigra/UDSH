using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using static System.Net.Mime.MediaTypeNames;

namespace UDSH.ViewModel
{
    public enum DataType
    {
        Folder,
        File
    }

    public class Node
    {
        public string Name { get; set; }
        public BitmapImage NodeImage { get; set; }
        public DataType NodeType { get; set; }
        public string NodeDirectory { get; set; }
        public FileSystem NodeFile { get; set; }
        public ObservableCollection<Node> SubNodes { get; set; } = new ObservableCollection<Node>();
    }

    public class Structure
    {
        public int Level { get; set; }
        public DataType ObjectDataType { get; set; }
        public string Name { get; set; }
    }

    public class SideContentUserControlViewModel : ViewModelBase
    {
        #region Side Content Properties
        private readonly IUserDataServices _userDataServices;
        public ObservableCollection<Node> Root {  get; set; }

        private double CurrentAnimationNumber = 0.0f;
        private double ShowOpacityTarget = 0.55f;
        private double HideOpacityTarget = 0.0f;

        private bool CanRecordMouse = false;

        private Point InitialMousePos;
        private bool IsMousePressed = false;
        private double InitialRectangleWidth;

        private bool canExpandSideContent;
        public bool CanExpandSideContent
        {
            get { return canExpandSideContent; }
            set { canExpandSideContent = value; OnPropertyChanged(); }
        }

        private double sideContentWidth = 202;
        public double SideContentWidth
        {
            get { return sideContentWidth; }
            set { sideContentWidth = value; OnPropertyChanged(); }
        }

        private bool canPinSideContent = false;
        public bool CanPinSideContent
        {
            get { return canExpandSideContent; }
            set { canExpandSideContent = value; OnPropertyChanged(); }
        }

        private double BorderWidth;

        private Border? TargetControl;
        private Grid? TargetGrid;

        /*
         * Search Box
        */
        private bool canSearchBoxTextBeFocusable;
        public bool CanSearchBoxTextBeFocusable
        {
            get { return canSearchBoxTextBeFocusable; }
            set { canSearchBoxTextBeFocusable = value; OnPropertyChanged(); }
        }

        private bool searchGotFocused = false;
        public bool SearchGotFocused
        {
            get { return searchGotFocused; }
            set { searchGotFocused = value; OnPropertyChanged(); }
        }

        private bool resetSearchBox = false;
        public bool ResetSearchBox
        {
            get { return resetSearchBox; }
            set { resetSearchBox = value; OnPropertyChanged(); }
        }

        private float searchIconOpacity;
        public float SearchIconOpacity
        {
            get { return searchIconOpacity; }
            set { searchIconOpacity = value; OnPropertyChanged(); }
        }

        private string highlightText = "Search...";
        public string HighlightText
        {
            get { return highlightText; }
            set { highlightText = value; OnPropertyChanged(); }
        }

        private string searchText = "";
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged(); }
        }

        private float textBoxWidth = 175;
        public float TextBoxWidth
        {
            get { return textBoxWidth; }
            set { textBoxWidth = value; OnPropertyChanged(); }
        }

        private bool CanStartCounting;
        private Dictionary<int, List<Structure>> structureDic = new Dictionary<int, List<Structure>>();

        private Node root;
        private Node SelectedNode;
        #endregion

        #region Commands
        public RelayCommand<Grid> SideContentLoad => new RelayCommand<Grid>(OnSideContentLoad);
        public RelayCommand<Border> BorderMouseEnter => new RelayCommand<Border>(BorderHitCollision);
        public RelayCommand<Border> BorderMouseLeave => new RelayCommand<Border>(BorderLeaveCollision);
        public RelayCommand<MouseButtonEventArgs> BorderMouseButtonDown => new RelayCommand<MouseButtonEventArgs>(BorderMouseRecord, canExecute=>CanRecordMouse);
        public RelayCommand<Border> BorderMouseButtonUp => new RelayCommand<Border>(BorderMouseStopRecord);
        public RelayCommand<MouseEventArgs> BorderMouseMove => new RelayCommand<MouseEventArgs>(BorderWidthChange, canExecute => CanRecordMouse);
        public RelayCommand<Grid> SideContentMouseLeave => new RelayCommand<Grid>(SideContentCollapse, canExecute => CanPinSideContent);

        public RelayCommand<Object> SearchBoxFocus => new RelayCommand<Object>(execute => OnSearchBoxGotFocus());
        public RelayCommand<TextBox> SearchBoxTextChange => new RelayCommand<TextBox>(OnSearchBoxTextChanged);
        public RelayCommand<TextBox> SearchBoxLeftMouseButtonDown => new RelayCommand<TextBox>(SetSearchBoxTextFocus);
        public RelayCommand<Grid> SideContentBackgroundLeftMouseButtonDown => new RelayCommand<Grid>(LoseSearchBoxFocus);
        public RelayCommand<Button> PinSideContent => new RelayCommand<Button>(PinSidebar);


        public RelayCommand<RoutedPropertyChangedEventArgs<object>> TreeViewSelectionChanged => new RelayCommand<RoutedPropertyChangedEventArgs<object>>(TreeSelectionChanged);
        public RelayCommand<object> TreeViewMouseDoubleClick => new RelayCommand<object>(execute => OpenFile());
        #endregion

        public SideContentUserControlViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;
            _userDataServices.AddNewFileToContent += _userDataServices_AddNewFileToContent;
            Project project = _userDataServices.ActiveProject;

            Root = new ObservableCollection<Node>();

            if (project != null)
                BuildStructure();
        }

        private void _userDataServices_AddNewFileToContent(object? sender, FileSystem e)
        {
            Project project = _userDataServices.ActiveProject;
            string[] TextSplit = e.FileDirectory.Split('\\');

            int index = Array.IndexOf(TextSplit, project.ProjectName);

            if (root == null)
                root = new Node { Name = project.ProjectName, NodeType = DataType.Folder };

            Node CurrentNode = root;
            for (int i = index + 1; i < TextSplit.Length; ++i)
            {
                string CurrentFileNodeDirectory = e.FileDirectory;
                BitmapImage CurrentImage;
                DataType CurrentType;
                bool IsFile = false;
                if (TextSplit[i].Contains(".mkm"))
                {
                    IsFile = true;
                    CurrentType = DataType.File;
                    CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKM.png"));
                }
                else if (TextSplit[i].Contains(".mkc"))
                {
                    IsFile = true;
                    CurrentType = DataType.File;
                    CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKC.png"));
                }
                else if (TextSplit[i].Contains(".mkb"))
                {
                    IsFile = true;
                    CurrentType = DataType.File;
                    CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKB.png"));
                }
                else
                {
                    CurrentFileNodeDirectory = string.Empty;
                    CurrentType = DataType.Folder;
                    CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderSidebar.png"));
                }

                // Avoid replicas
                var matchingNodes = CurrentNode.SubNodes.Where(n => n.Name == TextSplit[i]).ToList();
                Node? subNode = matchingNodes.Count > 0 ? matchingNodes[0] : null;

                if (subNode == null)
                {
                    subNode = new Node { Name = TextSplit[i], NodeImage = CurrentImage, NodeType = CurrentType, NodeDirectory = CurrentFileNodeDirectory, NodeFile = (IsFile == true) ? e : null };
                    CurrentNode.SubNodes.Add(subNode);
                }

                CurrentNode = subNode;
            }

            SortNodes(root);
            Root.Clear();
            foreach (var subNodes in root.SubNodes)
            {
                Root.Add(subNodes);
            }
        }

        private void BuildStructure()
        {
            Project project = _userDataServices.ActiveProject;
            root = new Node { Name = project.ProjectName, NodeType = DataType.Folder };
            //string[] files = Directory.GetFiles(project.ProjectDirectory, "*", SearchOption.AllDirectories);
            foreach (var file in project.Files)
            {
                string[] TextSplit = file.FileDirectory.Split('\\');

                // Start at the project root as the user could be storing the app files deep inside.
                int index = Array.IndexOf(TextSplit, project.ProjectName);

                Node CurrentNode = root;
                for(int i = index + 1;  i < TextSplit.Length; ++i)
                {
                    string CurrentFileNodeDirectory = file.FileDirectory;
                    BitmapImage CurrentImage;
                    DataType CurrentType;
                    bool IsFile = false;
                    if (TextSplit[i].Contains(".mkm"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKM.png"));
                    }
                    else if (TextSplit[i].Contains(".mkc"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKC.png"));
                    }
                    else if (TextSplit[i].Contains(".mkb"))
                    {
                        IsFile = true;
                        CurrentType = DataType.File;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/SidebarMKB.png"));
                    }
                    else
                    {
                        CurrentFileNodeDirectory = string.Empty;
                        CurrentType = DataType.Folder;
                        CurrentImage = new BitmapImage(new Uri("pack://application:,,,/Resource/FolderSidebar.png"));
                    }

                    // Avoid replicas
                    var matchingNodes = CurrentNode.SubNodes.Where(n => n.Name == TextSplit[i]).ToList();
                    Node? subNode = matchingNodes.Count > 0 ? matchingNodes[0] : null;

                    if (subNode == null)
                    {
                        subNode = new Node{ Name = TextSplit[i], NodeImage = CurrentImage, NodeType = CurrentType, NodeDirectory = CurrentFileNodeDirectory, NodeFile = (IsFile == true) ? file : null };
                        CurrentNode.SubNodes.Add(subNode);
                    }

                    CurrentNode = subNode;
                }
            }

            // Sorting
            SortNodes(root);

            foreach (var subNodes  in root.SubNodes)
            {
                Root.Add(subNodes);
            }
        }

        private void SortNodes(Node root)
        {
            root.SubNodes = new ObservableCollection<Node>(root.SubNodes.OrderBy(d => d.NodeType == DataType.File).ThenBy(n => n.Name, StringComparer.Ordinal));

            foreach (var subNode in root.SubNodes)
            {
                SortNodes(subNode);
            }
        }

        // TODO: Pin button command, and textbox width modification.

        /// <summary>
        /// Get the grid reference that holds the sidebar content
        /// </summary>
        /// <param name="grid"></param>
        private void OnSideContentLoad(Grid grid)
        {
            TargetGrid = grid;
        }


        /// <summary>
        /// Record Mouse when entering the border of the sidebar, and two things may happen:<br/><br/>
        /// 1- if user holds left ctrl button, the user can modify the width of the sidebar.<br/>
        /// 2- if no key has been recorded, then open the sidebar.
        /// </summary>
        /// <param name="border"></param>
        private void BorderHitCollision(Border border)
        {
            if(Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                Debug.WriteLine($"CurrentAnimNumber = {CurrentAnimationNumber}");
                EnableModification(border);
            }
            else if (Keyboard.IsKeyDown(Key.LeftAlt)) // Maybe add options whether the user wants to use Alt or not.
            {
                Debug.WriteLine($"OPEN SIDE CONTENT");
                Debug.WriteLine($"SideContentWidth = {SideContentWidth}");
                OpenSideContent();
            }
        }

        /// <summary>
        /// Record mouse leaving the border of the sidebar.
        /// </summary>
        /// <param name="border"></param>
        private void BorderLeaveCollision(Border border)
        {
            if (border != null)
            {
                BorderAnimation(border, HideOpacityTarget);
                CanRecordMouse = false;
            }
        }

        /// <summary>
        /// Start Border opacity animation, and allow left mouse button input.
        /// </summary>
        /// <param name="border"></param>
        private void EnableModification(Border border)
        {
            BorderAnimation(border, ShowOpacityTarget);
            TargetControl = border;
            CanRecordMouse = true;
        }
        
        /// <summary>
        /// Get initial mouse hit location relative to the object and keep recording left mouse button input.
        /// </summary>
        /// <param name="e"></param>
        private void BorderMouseRecord(MouseButtonEventArgs e)
        {
            if(e != null && TargetControl != null)
            {
                Debug.WriteLine($"Started Recording");
                InitialMousePos = e.GetPosition(TargetControl);
                Debug.WriteLine($"InitialMousePos = {InitialMousePos}");
                InitialRectangleWidth = TargetControl.Width;
                IsMousePressed = true;
                TargetControl.CaptureMouse();
            }
            else
            {
                MessageBox.Show("e is empty");
            }
            
        }

        /// <summary>
        /// Record mouse movement and calculate the difference between the initial hit position <br/>
        /// and the new hit position, which will decide whether the mouse moves left or right on <br/>
        /// the X axis, and assign the new width.
        /// </summary>
        /// <param name="e"></param>
        private void BorderWidthChange(MouseEventArgs e)
        {
            if (IsMousePressed && TargetControl != null)
            {
                Point CurrentMousePos = e.GetPosition(TargetControl);

                double DeltaX = (CurrentMousePos.X - InitialMousePos.X);
                BorderWidth = InitialRectangleWidth + DeltaX;

                if (BorderWidth > 65)
                {
                    TargetControl.Width = BorderWidth;
                    SideContentWidth = TargetControl.Width + 2;
                    TextBoxWidth = (float)TargetControl.Width - 26;
                    Debug.WriteLine($"Side Content Width = {SideContentWidth}");
                }
            }
        }

        /// <summary>
        /// Stop recording mouse input when stopping pressing left mouse button.
        /// </summary>
        /// <param name="border"></param>
        private void BorderMouseStopRecord(Border border)
        {
            IsMousePressed = false;
            border.ReleaseMouseCapture();
        }

        /// <summary>
        /// Open Side content and play animation.
        /// </summary>
        private void OpenSideContent()
        {
            CanExpandSideContent = true;

            var Storyboard = new Storyboard();
            var WidthAnimation = new DoubleAnimation
            {
                From = 0.0f,
                To = SideContentWidth,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(WidthAnimation, TargetGrid);
            Storyboard.SetTargetProperty(WidthAnimation, new PropertyPath(Grid.WidthProperty));
            Storyboard.Children.Add(WidthAnimation);
            Storyboard.Begin();
        }

        /// <summary>
        /// Border Show/Hide Opacity Animaiton.
        /// </summary>
        /// <param name="border"></param>
        /// <param name="Target"></param>
        private void BorderAnimation(Border border, double Target)
        {
            CurrentAnimationNumber = border.Opacity;
            var Storyboard = new Storyboard();
            var OpacityAnimation = new DoubleAnimation
            {
                From = CurrentAnimationNumber,
                To = Target,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(OpacityAnimation, border);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath(Border.OpacityProperty));
            Storyboard.Children.Add(OpacityAnimation);
            Storyboard.Begin();
        }

        /// <summary>
        /// Closing side content animation.
        /// </summary>
        /// <param name="grid"></param>
        private void SideContentCollapse(Grid grid)
        {
            var Storyboard = new Storyboard();
            var WidthAnimation = new DoubleAnimation
            {
                From = SideContentWidth,
                To = 0.0f,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(WidthAnimation, grid);
            Storyboard.SetTargetProperty(WidthAnimation, new PropertyPath(Grid.WidthProperty));
            Storyboard.Children.Add(WidthAnimation);
            Storyboard.Completed += (sender, args) => { CanExpandSideContent = false; CanSearchBoxTextBeFocusable = false; };
            Storyboard.Begin();
        }

        // May remove it.
        private void OnSearchBoxGotFocus()
        {
            SearchGotFocused = true;
        }

        /// <summary>
        /// Update Search Text when typing.
        /// </summary>
        /// <param name="textBox"></param>
        private void OnSearchBoxTextChanged(TextBox textBox)
        {
            if(textBox != null)
            {
                SearchText = textBox.Text;
            }
        }

        /// <summary>
        /// Set Focus when pressing on the search bar.
        /// </summary>
        /// <param name="textBox"></param>
        private void SetSearchBoxTextFocus(TextBox textBox)
        {
            if (textBox != null)
            {
                CanSearchBoxTextBeFocusable = true;
                ResetSearchBox = false;
                textBox.Focus();
            }
        }

        /// <summary>
        /// Lose Search Text Box focus when pressing on the background of the sidebar.
        /// </summary>
        /// <param name="grid"></param>
        private void LoseSearchBoxFocus(Grid grid)
        {
            SearchGotFocused = false;
            CanSearchBoxTextBeFocusable = false;
            ResetSearchBox = true;
        }

        /// <summary>
        /// Pin the sidebar, so when the user leave it, the sidebar won't collapse.
        /// </summary>
        /// <param name="btn"></param>
        private void PinSidebar(Button btn)
        {
            CanPinSideContent = !CanPinSideContent;
        }

        private void TreeSelectionChanged(RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Node node)
            {
                SelectedNode = node;
            }
        }

        private void OpenFile()
        {
            if (SelectedNode.NodeType == DataType.File)
            {
                _userDataServices.AddFileToHeader(SelectedNode.NodeFile);
            }
        }
    }
}
