using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;

namespace UDSH.ViewModel
{
    public class ContentWindowViewModel : ViewModelBase
    {
        private readonly IUserDataServices _userDataServices;

        private DispatcherTimer Timer;
        private bool TimerCompleted;
        private Point InitialMousePosition;
        private TextBox textBox;

        public event EventHandler WindowDragStart;
        public event EventHandler WindowDragEnd;
        public event EventHandler<bool> MousePressed;
        public event EventHandler<bool> MouseEnterCollision;
        public Window AssociatedWindow {  get; private set; }

        private bool canRegisterDragMode;
        public bool CanRegisterDragMode
        {
            get { return canRegisterDragMode; }
            set { canRegisterDragMode = value; OnPropertyChanged(); }
        }

        private bool canRegisterMouseOver;
        public bool CanRegisterMouseOver
        {
            get { return canRegisterMouseOver; }
            set { canRegisterMouseOver = value; OnPropertyChanged(); }
        }

        private string collapseResizeButtonState;
        public string CollapseResizeButtonState
        {
            get { return collapseResizeButtonState; }
            set { collapseResizeButtonState = value; OnPropertyChanged(); }
        }

        private string collapseResetButtonState;
        public string CollapseResetButtonState
        {
            get { return collapseResetButtonState; }
            set { collapseResetButtonState = value; OnPropertyChanged(); }
        }

        private Thickness gridContentMargin;
        public Thickness GridContentMargin
        {
            get { return gridContentMargin; }
            set { gridContentMargin = value; OnPropertyChanged(); }
        }

        private Thickness windowChromeMargin;
        public Thickness WindowChromeMargin
        {
            get { return windowChromeMargin; }
            set { windowChromeMargin = value; OnPropertyChanged(); }
        }

        private bool IsTextBoxFocused;
        private bool isSearchBoxBorderOpen;
        public bool IsSearchBoxBorderOpen
        {
            get { return isSearchBoxBorderOpen; }
            set { isSearchBoxBorderOpen = value; OnPropertyChanged(); }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ContentFileStructure> currentFiles;
        public ObservableCollection<ContentFileStructure> CurrentFiles
        {
            get => currentFiles;
            set { currentFiles = value; OnPropertyChanged(); }
        }

        private int selectedSortIndex;
        public int SelectedSortIndex
        {
            get => selectedSortIndex;
            set { selectedSortIndex = value; OnPropertyChanged(); }
        }

        private CustomScrollViewer OuterScrollViewer;
        private CustomScrollViewer ContentScrollViewer;
        private bool reachedContentBottom;
        public bool ReachedContentBottom
        {
            get => reachedContentBottom;
            set { reachedContentBottom = value; OnPropertyChanged(); }
        }

        private double MinWidth = 50;

        private double colWidth1;
        public double ColWidth1
        {
            get => colWidth1;
            set
            {
                if (value <= MinWidth)
                    colWidth1 = MinWidth;
                else
                    colWidth1 = value;
                OnPropertyChanged();
            }
        }

        private double colWidth2;
        public double ColWidth2
        {
            get => colWidth2;
            set
            {
                if (value <= MinWidth)
                    colWidth2 = MinWidth;
                else
                    colWidth2 = value;
                OnPropertyChanged();
            }
        }

        private double colWidth3;
        public double ColWidth3
        {
            get => colWidth3;
            set
            {
                if (value <= MinWidth)
                    colWidth3 = MinWidth;
                else
                    colWidth3 = value;
                OnPropertyChanged();
            }
        }

        private double colWidth4;
        public double ColWidth4
        {
            get => colWidth4;
            set
            {
                if (value <= MinWidth)
                    colWidth4 = MinWidth;
                else
                    colWidth4 = value;
                OnPropertyChanged();
            }
        }

        private double colWidth5;
        public double ColWidth5
        {
            get => colWidth5;
            set
            {
                if (value <= MinWidth)
                    colWidth5 = MinWidth;
                else
                    colWidth5 = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Node> Root { get; set; }
        private Node root;
        private Node SelectedNode;

        public RelayCommand<MouseButtonEventArgs> DirectoryHitCollisionLMB => new RelayCommand<MouseButtonEventArgs>(RecordMouseDown);
        public RelayCommand<MouseButtonEventArgs> DirectoryHitCollisionLMBUp => new RelayCommand<MouseButtonEventArgs>(StopMouseDownRecord);
        public RelayCommand<TextBox> DirectoryTextBoxLoaded => new RelayCommand<TextBox>(AssignDirectoryRef);

        public RelayCommand<object> DirectoryHitCollisionMouseEnter => new RelayCommand<object>(execute => UpdateDirectoryBorder());
        public RelayCommand<object> DirectoryHitCollisionMouseLeave => new RelayCommand<object>(execute => ResetDirectoryBorder());

        public RelayCommand<object> CloseWindow => new RelayCommand<object>(execute => CloseCurrentWindow());
        public RelayCommand<object> MaximizeWindow => new RelayCommand<object>(execute => UpdatedCurrentWindowSize());
        public RelayCommand<object> NormalizeWindow => new RelayCommand<object>(execute => UpdatedCurrentWindowSize());
        public RelayCommand<object> MinimizeWindow => new RelayCommand<object>(execute => MinimizeCurrentWindowSize());

        public RelayCommand<object> SearchBoxMouseEnter => new RelayCommand<object>(execute => ExpandSearchBoxBorder());
        public RelayCommand<object> SearchBoxMouseLeave => new RelayCommand<object>(execute => CollapseSearchBoxBorder());
        public RelayCommand<TextBox> SearchBoxGotFocus => new RelayCommand<TextBox>(SearchBoxBorderGotFocused);


        public RelayCommand<CustomScrollViewer> ContentScrollViewerLoaded => new RelayCommand<CustomScrollViewer>(OnContentScrollViewerLoaded);
        public RelayCommand<CustomScrollViewer> VerticalContentScrollViewerLoaded => new RelayCommand<CustomScrollViewer>(OnVerticalContentScrollViewerLoaded);


        public RelayCommand<RoutedPropertyChangedEventArgs<object>> TreeViewSelectionChanged => new RelayCommand<RoutedPropertyChangedEventArgs<object>>(TreeSelectionChanged);
        public RelayCommand<object> TreeViewMouseDoubleClick => new RelayCommand<object>(execute => OpenFile());

        public ContentWindowViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.205) };
            Timer.Tick += Timer_Tick;

            TimerCompleted = false;
            CanRegisterDragMode = true;
            CanRegisterMouseOver = true;

            CollapseResizeButtonState = "Collapsed";
            CollapseResetButtonState = "Visible";

            GridContentMargin = new Thickness(0);
            WindowChromeMargin = new Thickness(0);

            IsSearchBoxBorderOpen = false;
            IsTextBoxFocused = false;

            ReachedContentBottom = false;

            ColWidth1 = 110;
            ColWidth2 = 223;
            ColWidth3 = 70;
            ColWidth4 = 70;
            ColWidth5 = 200;

            SearchText = string.Empty;
            SelectedSortIndex = (int)ContentSort.FoldersFirst_Ascending;

            Project project = userDataServices.ActiveProject;
            if (project != null)
            {
                FileStructure structure = new FileStructure();

                CurrentFiles = new ObservableCollection<ContentFileStructure>();
                CurrentFiles = structure.CreateCurrentLevelList(project, (ContentSort)SelectedSortIndex);

                Root = new ObservableCollection<Node>();
                root = structure.ContentBuildSideTree(project);

                foreach (var subNodes in root.SubNodes)
                {
                    Root.Add(subNodes);
                }
            }
        }

        private void RecordMouseDown(MouseButtonEventArgs e)
        {
            Timer.Start();
            InitialMousePosition = Mouse.GetPosition(null);
            Debug.WriteLine($"Initial pos: {InitialMousePosition}");
        }

        private void StopMouseDownRecord(MouseButtonEventArgs e)
        {
            Timer.Stop();
            Point CurrentMousePosition = Mouse.GetPosition(null);
            Debug.WriteLine($"New Pos: {CurrentMousePosition}");

            if(InitialMousePosition == CurrentMousePosition)
            {
                textBox.Focus();
                textBox.LostFocus += TextBox_LostFocus;
                CanRegisterDragMode = false;
                MousePressed.Invoke(this, CanRegisterDragMode);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Timer.Stop();
            Debug.WriteLine($"Timesss: {Timer}");
            if (CanRegisterDragMode == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);

                if (AssociatedWindow.WindowState == WindowState.Maximized)
                {
                    AssociatedWindow.WindowState = WindowState.Normal;

                    double mouseX = CurrentMousePosition.X - (AssociatedWindow.Width / 2);
                    double mouseY = CurrentMousePosition.Y - 30;

                    AssociatedWindow.Left = mouseX;
                    AssociatedWindow.Top = mouseY;
                }

                try
                {
                    WindowDragStart.Invoke(sender, e);
                    AssociatedWindow.DragMove();
                    WindowDragEnd.Invoke(sender, e);
                }
                catch { }
                TimerCompleted = false;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CanRegisterDragMode = true;
            MousePressed.Invoke(this, CanRegisterDragMode);
        }

        private void AssignDirectoryRef(TextBox textBox)
        {
            this.textBox = textBox;
        }

        private void UpdateDirectoryBorder()
        {
            if (CanRegisterDragMode == true)
                MouseEnterCollision.Invoke(this, true);
        }

        private void ResetDirectoryBorder()
        {
            if (CanRegisterDragMode == true)
                MouseEnterCollision.Invoke(this, false);
        }

        private void CloseCurrentWindow()
        {
            AssociatedWindow.Close();
        }

        private void UpdatedCurrentWindowSize()
        {
            if (AssociatedWindow.WindowState == WindowState.Normal)
            {
                AssociatedWindow.WindowState = WindowState.Maximized;
                CollapseResizeButtonState = "Visible";
                CollapseResetButtonState = "Collapsed";
            }
            else
            {
                AssociatedWindow.WindowState = WindowState.Normal;
                CollapseResizeButtonState = "Collapsed";
                CollapseResetButtonState = "Visible";
            }

        }

        private void MinimizeCurrentWindowSize()
        {
            AssociatedWindow.WindowState = WindowState.Minimized;
        }

        public void SetAssociatedWindow(Window window)
        {
            AssociatedWindow = window;
            AssociatedWindow.StateChanged += AssociatedWindow_StateChanged;
        }

        private void AssociatedWindow_StateChanged(object? sender, EventArgs e)
        {
            if (AssociatedWindow.WindowState == WindowState.Normal)
            {
                CollapseResizeButtonState = "Collapsed";
                CollapseResetButtonState = "Visible";

                GridContentMargin = new Thickness(0);
                WindowChromeMargin = new Thickness(0);
            }
            else
            {
                CollapseResizeButtonState = "Visible";
                CollapseResetButtonState = "Collapsed";

                GridContentMargin = new Thickness(10, 0, 10, 0);
                WindowChromeMargin = new Thickness(10, 10, 10, 0);
            }
        }

        private void ExpandSearchBoxBorder()
        {
            IsSearchBoxBorderOpen = true;
        }

        private void CollapseSearchBoxBorder()
        {
            if (IsTextBoxFocused == false)
                IsSearchBoxBorderOpen = false;
        }

        private void SearchBoxBorderGotFocused(TextBox textBox)
        {
            IsTextBoxFocused = true;
            textBox.LostFocus += SearchBoxTextBox_LostFocus;
        }

        private void SearchBoxTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            IsTextBoxFocused = false;
            IsSearchBoxBorderOpen = false;
        }

        private void OnContentScrollViewerLoaded(CustomScrollViewer customScrollViewer)
        {
            OuterScrollViewer = customScrollViewer;
        }

        private void OnVerticalContentScrollViewerLoaded(CustomScrollViewer customScrollViewer)
        {
            ContentScrollViewer = customScrollViewer;
            ContentScrollViewer.ScrollChanged += VerticalScrollViewer_ScrollChanged;
        }

        private void VerticalScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            OuterScrollViewer.ScrollToHorizontalOffset(e.HorizontalOffset);

            if (ContentScrollViewer.VerticalOffset == ContentScrollViewer.ScrollableHeight)
                ReachedContentBottom = true;
            else
                ReachedContentBottom = false;
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
            // TODO: Add folder transition. Don't forget to animate grid splitter
            if (SelectedNode.NodeType == DataType.File)
            {
                _userDataServices.AddFileToHeader(SelectedNode.NodeFile);
            }
        }
    }
}
