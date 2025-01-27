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
        
        private Node root;
        public Node Root
        {
            get => root;
            set { root = value; OnPropertyChanged(); }
        }
        public ObservableCollection<Node> RootSearch { get; set; }

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

        private bool canChooseFromSearch;
        public bool CanChooseFromSearch
        {
            get { return canChooseFromSearch; }
            set { canChooseFromSearch = value; OnPropertyChanged(); }
        }

        private bool CanStartCounting;
        private Dictionary<int, List<Structure>> structureDic = new Dictionary<int, List<Structure>>();

        //private Node root;
        private Node SearchRoot;
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
            
            CanChooseFromSearch = false;
            RootSearch = new ObservableCollection<Node>();
            BuildStructure();

            _userDataServices.FileDetailsUpdated += _userDataServices_FileDetailsUpdated;
            _userDataServices.ItemDeletedSideContent += _userDataServices_ItemDeleted;
        }

        private void _userDataServices_ItemDeleted(object? sender, string directory)
        {
            FileStructure fileStructure = new FileStructure();
            fileStructure.UpdateTreeAfterDeletion(Root, directory);
            fileStructure.RemoveEmptyFolderNodes(Root);
        }

        private async void _userDataServices_FileDetailsUpdated(object? sender, FileDetailsUpdatedEventArgs e)
        {
            await Task.Run(() =>
            {
                FileStructure fileStructure = new FileStructure();
                fileStructure.UpdateTreeItemName(Root, _userDataServices.ActiveProject, e.FileStructure, e.OldDirectory, e.CurrentLevel);
            });

            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                Node temp = Root;
                Root = new Node();
                Root = temp;
            });
        }

        private void _userDataServices_AddNewFileToContent(object? sender, FileSystem e)
        {
            FileStructure fileStructure = new FileStructure();
            Node AddedNode = fileStructure.AddNewFile(_userDataServices.ActiveProject, Root, e);
            RootSearch.Add(AddedNode);
        }

        private async void BuildStructure()
        {
            Project project = _userDataServices.ActiveProject;

            if (project != null)
            {
                await System.Windows.Application.Current.Dispatcher.InvokeAsync((Action)(delegate
                {
                    FileStructure fileStructure = new FileStructure();
                    Node root = fileStructure.BuildSideContentTree(project);
                    Root = new Node();
                    Root = root;

                    fileStructure.BuildSearchTree(Root, RootSearch);
                }));
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
        private async void OnSearchBoxTextChanged(TextBox textBox)
        {
            if(textBox != null)
            {
                SearchText = textBox.Text;
                if (!string.IsNullOrEmpty(SearchText))
                    CanChooseFromSearch = true;
                else
                    CanChooseFromSearch = false;

                // UI is lagging, so only calculate text when stopping typing
                await Task.Delay(500);

                List<Node> nodes = new List<Node>(RootSearch);
                //nodes = nodes.OrderBy(node => SearchCalculations(node)).ToList();
                nodes = nodes.OrderBy(node => IsMatch(node, SearchText) ? 0 : 1).ThenBy(node => SearchCalculations(node)).ToList();

                RootSearch.Clear();
                foreach (Node node in nodes)
                {
                    RootSearch.Add(node);
                }
            }
        }

        private bool IsMatch(Node node, string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            return node.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
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
                Debug.WriteLine($"{SelectedNode.NodeDirectory}");
            }
        }

        private void OpenFile()
        {
            if (SelectedNode != null && SelectedNode.NodeType == DataType.File)
            {
                _userDataServices.AddFileToHeader(SelectedNode.NodeFile);
            }
        }

        private int SearchCalculations(Node node)
        {
            int SearchLength = SearchText.Length;
            int NodesLength = node.Name.Length;
            var Matrix = new int[SearchLength + 1, NodesLength + 1];

            if (NodesLength == 0)
                return SearchLength;
            else if (SearchLength == 0)
                return NodesLength;

            for (int i = 0; i <= SearchLength; ++i)
                Matrix[i, 0] = i;
            for (int j = 0; j <= NodesLength; ++j)
                Matrix[0, j] = j;

            for (int i = 1; i <= SearchLength; ++i)
            {
                for (int j = 1; j <= NodesLength; ++j)
                {
                    //int cost = (SearchText[i - 1] == node.Name[j - 1]) ? 0 : 1;
                    int cost = (string.Compare(SearchText[i - 1].ToString(), node.Name[j - 1].ToString(), StringComparison.OrdinalIgnoreCase) == 0) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }
            }

            return Matrix[SearchLength, NodesLength];
        }
    }
}
