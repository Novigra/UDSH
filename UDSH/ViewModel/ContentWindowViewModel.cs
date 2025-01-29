using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class ContentWindowViewModel : ViewModelBase
    {
        #region Properties
        private readonly IUserDataServices _userDataServices;

        private DispatcherTimer Timer;
        private bool TimerCompleted;
        private Point InitialMousePosition;
        private TextBox textBox;

        public event EventHandler WindowDragStart;
        public event EventHandler WindowDragEnd;
        public event EventHandler<bool> MousePressed;
        public event EventHandler<bool> MouseEnterCollision;
        public event EventHandler<bool> DirectoryWarningMessage;
        public event EventHandler WrongDirectoryNotification;
        public event EventHandler PageChanged;
        public Window AssociatedWindow {  get; private set; }

        private string projectName;
        public string ProjectName
        {
            get => projectName;
            set { projectName = value; OnPropertyChanged(); }
        }

        private string BaseDirectory;
        private string CurrentDirectoryBeforeEdit;

        private string currentDirectory;
        public string CurrentDirectory
        {
            get => currentDirectory;
            set { currentDirectory = value; OnPropertyChanged(); }
        }

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

        private ObservableCollection<ContentFileStructure> DefaultCurrentFiles;

        private ObservableCollection<ContentFileStructure> currentFiles;
        public ObservableCollection<ContentFileStructure> CurrentFiles
        {
            get => currentFiles;
            set { currentFiles = value; OnPropertyChanged(); }
        }

        private ContentFileStructure selectedItem;
        public ContentFileStructure SelectedItem
        {
            get => selectedItem;
            set { selectedItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ContentFileStructure> selectedItems;
        public ObservableCollection<ContentFileStructure> SelectedItems
        {
            get => selectedItems;
            set { selectedItems = value; OnPropertyChanged(); }
        }

        private int selectedAddIndex;
        public int SelectedAddIndex
        {
            get => selectedAddIndex;
            set { selectedAddIndex = value; OnPropertyChanged(); }
        }

        private int selectedSortIndex;
        public int SelectedSortIndex
        {
            get => selectedSortIndex;
            set { selectedSortIndex = value; OnPropertyChanged(); }
        }

        private int selectedFilterIndex;
        public int SelectedFilterIndex
        {
            get => selectedFilterIndex;
            set { selectedFilterIndex = value; OnPropertyChanged(); }
        }

        private int selectedViewIndex;
        public int SelectedViewIndex
        {
            get => selectedViewIndex;
            set { selectedViewIndex = value; OnPropertyChanged(); }
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

        private bool isContentLoading;
        public bool IsContentLoading
        {
            get { return isContentLoading; }
            set { isContentLoading = value; OnPropertyChanged(); }
        }

        private bool isContentRenameProcess;
        public bool IsContentRenameProcess
        {
            get { return isContentRenameProcess; }
            set { isContentRenameProcess = value; OnPropertyChanged(); }
        }

        private bool isContentCanceledProcess;
        public bool IsContentCanceledProcess
        {
            get { return isContentCanceledProcess; }
            set { isContentCanceledProcess = value; OnPropertyChanged(); }
        }

        private Node root;
        public Node Root
        {
            get => root;
            set { root = value; OnPropertyChanged(); }
        }
        //private Node root;
        private Node SelectedNode;


        private bool isNormalState;
        public bool IsNormalState
        {
            get => isNormalState;
            set { isNormalState = value; OnPropertyChanged(); }
        }

        private bool openRenameTextBox;
        public bool OpenRenameTextBox
        {
            get => openRenameTextBox;
            set { openRenameTextBox = value; OnPropertyChanged(); }
        }

        private bool openNameTextBox;
        public bool OpenNameTextBox
        {
            get => openNameTextBox;
            set { openNameTextBox = value; OnPropertyChanged(); }
        }

        private string renameNewName;
        public string RenameNewName
        {
            get => renameNewName;
            set { renameNewName = value; OnPropertyChanged(); }
        }

        private string itemNewName;
        public string ItemNewName
        {
            get => itemNewName;
            set { itemNewName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ContentFileStructure> DefaultDirectory;
        private Stack<string> CurrentDirectoryStack;
        private Stack<ObservableCollection<ContentFileStructure>> MemoryDirectoriesStack;

        private Stack<ObservableCollection<ContentFileStructure>> ReturnPageStack;
        private Stack<string> ReturnDirectoryStack;

        private HashSet<char> InvalidCharactersFile = new HashSet<char>(@"/\:*?""<>|");
        private HashSet<char> InvalidCharactersDirectory = new HashSet<char>(@"\:*?""<>|");
        private string InvalidDirectory = "//";

        private bool DirectoryUpdated;
        private bool canGoToDirectory;
        public bool CanGoToDirectory
        {
            get => canGoToDirectory;
            set { canGoToDirectory = value; OnPropertyChanged(); }
        }

        private bool showWarningMessage;
        public bool ShowWarningMessage
        {
            get => showWarningMessage;
            set { showWarningMessage = value; OnPropertyChanged(); }
        }

        private string directoryWrongInputMessage;
        public string DirectoryWrongInputMessage
        {
            get => directoryWrongInputMessage;
            set { directoryWrongInputMessage = value; OnPropertyChanged(); }
        }

        private double largeListViewOpacity;
        public double LargeListViewOpacity
        {
            get => largeListViewOpacity;
            set { largeListViewOpacity = value; OnPropertyChanged(); }
        }

        private bool largeListViewHitTest;
        public bool LargeListViewHitTest
        {
            get => largeListViewHitTest;
            set { largeListViewHitTest = value; OnPropertyChanged(); }
        }

        private double detailsListViewOpacity;
        public double DetailsListViewOpacity
        {
            get => detailsListViewOpacity;
            set { detailsListViewOpacity = value; OnPropertyChanged(); }
        }

        private bool detailsListViewHitTest;
        public bool DetailsListViewHitTest
        {
            get => detailsListViewHitTest;
            set { detailsListViewHitTest = value; OnPropertyChanged(); }
        }

        private bool isContentDeleteProcess;
        public bool IsContentDeleteProcess
        {
            get => isContentDeleteProcess;
            set { isContentDeleteProcess = value; OnPropertyChanged(); }
        }

        private string nameInputMessage;
        public string NameInputMessage
        {
            get => nameInputMessage;
            set { nameInputMessage = value; OnPropertyChanged(); }
        }

        private bool showNameInputWarningMessage;
        public bool ShowNameInputWarningMessage
        {
            get => showNameInputWarningMessage;
            set { showNameInputWarningMessage = value; OnPropertyChanged(); }
        }

        private bool isRenameItemConfirmButtonEnabled;
        public bool IsRenameItemConfirmButtonEnabled
        {
            get => isRenameItemConfirmButtonEnabled;
            set { isRenameItemConfirmButtonEnabled = value; OnPropertyChanged(); }
        }

        private bool isNameItemConfirmButtonEnabled;
        public bool IsNameItemConfirmButtonEnabled
        {
            get => isNameItemConfirmButtonEnabled;
            set { isNameItemConfirmButtonEnabled = value; OnPropertyChanged(); }
        }

        private int CurrentLevel;

        private Border FocusTarget;

        public ListView LargeListView { get; set; }
        public ListView DetailsListView { get; set; }
        #endregion

        #region Commands
        public RelayCommand<Border> StatusBorderLoaded => new RelayCommand<Border>(AssignFocusBorder);

        public RelayCommand<MouseButtonEventArgs> DirectoryHitCollisionLMB => new RelayCommand<MouseButtonEventArgs>(RecordMouseDown);
        public RelayCommand<MouseButtonEventArgs> DirectoryHitCollisionLMBUp => new RelayCommand<MouseButtonEventArgs>(StopMouseDownRecord);
        public RelayCommand<TextBox> DirectoryTextBoxLoaded => new RelayCommand<TextBox>(AssignDirectoryRef);
        public RelayCommand<object> DirectoryTextBoxTextChanged => new RelayCommand<object>(execute => UpdateTextBoxStatus());
        public RelayCommand<KeyEventArgs> PressedEnter => new RelayCommand<KeyEventArgs>(execute => GoToDirectory(), canExecute => CanExecuteGoToDirectory());

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

        public RelayCommand<SelectionChangedEventArgs> ListSelectionChanged => new RelayCommand<SelectionChangedEventArgs>(ItemSelectionChanged);
        public RelayCommand<object> ListViewMouseDoubleClick => new RelayCommand<object>(execute => ExecuteItem(), canExecute => CanExecuteSelectedItem());

        public RelayCommand<object> DeleteItem => new RelayCommand<object>(execute => DeleteSelectedItem(), canExecute => CanRenameSelectedItem());
        public RelayCommand<object> ConfirmDelete => new RelayCommand<object>(execute => ConfirmDeleteProcess(), canExecute => IsContentDeleteProcess);
        public RelayCommand<object> CancelDelete => new RelayCommand<object>(execute => StopDeleteProcess(), canExecute => IsContentDeleteProcess);


        public RelayCommand<object> NameTextBoxChanged => new RelayCommand<object>(execute => NameChanged());
        public RelayCommand<object> RenameItem => new RelayCommand<object>(execute => RenameSelectedItem(), canExecute => CanRenameSelectedItem());
        public RelayCommand<object> ConfirmRename => new RelayCommand<object>(execute => ConfirmRenameProcess(), canExecute => OpenRenameTextBox);
        public RelayCommand<object> CancelRename => new RelayCommand<object>(execute => StopRenameProcess(), canExecute => OpenRenameTextBox);

        public RelayCommand<object> BackButton => new RelayCommand<object>(execute => GoBack());
        public RelayCommand<object> ReturnButton => new RelayCommand<object>(execute => GoForward());

        public RelayCommand<object> AddSelectionChanged => new RelayCommand<object>(execute => UpdateAdd());
        public RelayCommand<object> FilterSelectionChanged => new RelayCommand<object>(execute => UpdateFilter());
        public RelayCommand<object> SortSelectionChanged => new RelayCommand<object>(execute => UpdateSorting());
        public RelayCommand<object> ViewSelectionChanged => new RelayCommand<object>(execute => UpdateView());

        public RelayCommand<object> ConfirmNewItemName => new RelayCommand<object>(execute => ConfirmNewItemNameProcess(), canExecute => OpenNameTextBox);
        public RelayCommand<object> CancelNewItemName => new RelayCommand<object>(execute => CancelNewItemNameProcess(), canExecute => OpenNameTextBox);

        public RelayCommand<MouseEventArgs> ListMouseMove => new RelayCommand<MouseEventArgs>(ListMouseMoveProcess);
        public RelayCommand<DragEventArgs> ListDragOver => new RelayCommand<DragEventArgs>(ListDragOverProcess);
        public RelayCommand<DragEventArgs> ListDrop => new RelayCommand<DragEventArgs>(ListDropProcess);
        #endregion

        public ContentWindowViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            SelectedItems = new ObservableCollection<ContentFileStructure>();

            Timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.205) };
            Timer.Tick += Timer_Tick;

            if (_userDataServices.ActiveProject != null)
            {
                ProjectName = _userDataServices.ActiveProject.ProjectName + "/";
            }
            else
            {
                ProjectName = string.Empty;
            }

            CurrentDirectory = string.Empty;

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

            ColWidth1 = 220;
            ColWidth2 = 223;
            ColWidth3 = 70;
            ColWidth4 = 70;
            ColWidth5 = 200;

            IsContentLoading = false;
            IsContentRenameProcess = false;
            IsContentDeleteProcess = false;
            IsContentCanceledProcess = false;

            SearchText = string.Empty;

            SelectedAddIndex = -1;
            SelectedSortIndex = (int)ContentSort.FoldersFirst_Ascending;
            SelectedFilterIndex = (int)ContentFilter.None;
            SelectedViewIndex = (int)ContentView.LargeIcons;

            LargeListViewOpacity = 1.0;
            LargeListViewHitTest = true;
            DetailsListViewOpacity = 0.0;
            DetailsListViewHitTest = false;

            IsNormalState = true;
            OpenRenameTextBox = false;
            OpenNameTextBox = false;
            RenameNewName = string.Empty;
            ItemNewName = string.Empty;
            CurrentDirectoryBeforeEdit = string.Empty;

            LoadData();

            _userDataServices.AddNewFileToContent += _userDataServices_AddNewFileToContent;

            if (_userDataServices.ActiveProject != null)
                BaseDirectory = _userDataServices.ActiveProject.ProjectDirectory + "\\";
            else
                BaseDirectory = string.Empty;

            DefaultDirectory = new ObservableCollection<ContentFileStructure>();
            CurrentDirectoryStack = new Stack<string>();
            MemoryDirectoriesStack = new Stack<ObservableCollection<ContentFileStructure>>();

            ReturnPageStack = new Stack<ObservableCollection<ContentFileStructure>>();
            ReturnDirectoryStack = new Stack<string>();

            DirectoryUpdated = false;
            CanGoToDirectory = true;
            ShowWarningMessage = false;

            DirectoryWrongInputMessage = "You Can’t Add Empty Directories Or Use \\ : * ? \" < > |";
            ShowNameInputWarningMessage = false;

            DefaultCurrentFiles = new ObservableCollection<ContentFileStructure>();
            CurrentLevel = 0;

            IsRenameItemConfirmButtonEnabled = true;
            IsNameItemConfirmButtonEnabled = true;
        }

        private void ListMouseMoveProcess(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && ((FrameworkElement)e.OriginalSource).DataContext is ContentFileStructure)
            {
                if (SelectedItem != null)
                {
                    if (LargeListViewHitTest == true)
                        DragDrop.DoDragDrop(LargeListView, SelectedItem, DragDropEffects.Move);
                    else
                        DragDrop.DoDragDrop(DetailsListView, SelectedItem, DragDropEffects.Move);
                }
            }
        }

        private void ListDragOverProcess(DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        private void ListDropProcess(DragEventArgs e)
        {
            var sourceItem = e.Data.GetData(typeof(ContentFileStructure)) as ContentFileStructure;
            ListView CurrentListView;

            if (LargeListViewHitTest == true)
                CurrentListView = LargeListView;
            else
                CurrentListView = DetailsListView;

            var position = e.GetPosition(CurrentListView);
            var targetItem = CurrentListView.Items.OfType<ContentFileStructure>().FirstOrDefault(c => CurrentListView.ItemContainerGenerator.ContainerFromItem(c) is ListViewItem container &&
                        container.TransformToVisual(CurrentListView)
                        .TransformBounds(new Rect(0, 0, container.ActualWidth, container.ActualHeight))
                        .Contains(position));

            if (sourceItem != null && targetItem != null && sourceItem != targetItem)
            {
                // TODO: Update File Directory
                /*
                 * - if file exists with similar name, add _1 at the end of the file(of course do iterations)
                 * - Update file's directory
                 * - Update file in side tree, and content tree
                 * - Update header, as the file name could change
                 */
            }
        }

        private async void LoadData()
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                Project project = _userDataServices.ActiveProject;
                if (project != null)
                {
                    CurrentDirectoryStack.Push(CurrentDirectory);

                    FileStructure structure = new FileStructure();

                    string SubDirectory = CurrentDirectory;
                    string ReadableDirectory = BaseDirectory + SubDirectory.Replace("/", "\\");

                    if (!ReadableDirectory.EndsWith('\\'))
                        ReadableDirectory += "\\";

                    CurrentFiles = new ObservableCollection<ContentFileStructure>();
                    CurrentFiles = structure.CreateCurrentLevelList(project, ReadableDirectory, (ContentSort)SelectedSortIndex);

                    DefaultDirectory = CurrentFiles;
                    MemoryDirectoriesStack.Push(CurrentFiles);

                    Root = new Node();
                    Root = structure.ContentBuildSideTree(project);

                    string[] directories = Directory.GetDirectories(_userDataServices.ActiveProject.ProjectDirectory, "*", SearchOption.AllDirectories);
                    List<string> EmptyDirectories = new List<string>();
                    foreach (string directory in directories)
                    {
                        if (!Directory.EnumerateFileSystemEntries(directory).Any())
                            EmptyDirectories.Add(directory);
                    }

                    foreach (string directory in EmptyDirectories)
                        structure.AddEmptyFolderTreeItemName(Root, project, directory);

                    Node temp = Root;
                    Root = new Node();
                    Root = temp;

                    UpdateDefaultList();
                }
            }));

            IsContentLoading = false;
        }

        private async void _userDataServices_AddNewFileToContent(object? sender, FileSystem e)
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                FileStructure fileStructure = new FileStructure();
                Node AddedNode = fileStructure.AddNewFile(_userDataServices.ActiveProject, Root, e, true);

                string SubDirectory = CurrentDirectory;
                string ReadableDirectory = BaseDirectory + SubDirectory.Replace("/", "\\");
                if (!ReadableDirectory.EndsWith('\\'))
                    ReadableDirectory += "\\";

                ObservableCollection<ContentFileStructure> UpdatedList = fileStructure.UpdateCurrentLevelList(CurrentFiles, _userDataServices.ActiveProject, e, ReadableDirectory, (ContentSort)SelectedSortIndex);
                CurrentFiles = UpdatedList;
                UpdateDefaultList();
            }));

            IsContentLoading = false;
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
                CurrentDirectoryBeforeEdit = CurrentDirectory;

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
            if(DirectoryUpdated == false)
                CurrentDirectory = CurrentDirectoryBeforeEdit;

            DirectoryUpdated = false;
            CanRegisterDragMode = true;
            MousePressed.Invoke(this, CanRegisterDragMode);
        }

        private void AssignDirectoryRef(TextBox textBox)
        {
            this.textBox = textBox;
        }

        private void UpdateTextBoxStatus()
        {
            if (CurrentDirectory.Any(c => InvalidCharactersDirectory.Contains(c)) == true || CurrentDirectory.Contains(InvalidDirectory) == true || CurrentDirectory.Equals("/"))
            {
                ShowWarningMessage = true;
                CanGoToDirectory = false;
                DirectoryWarningMessage.Invoke(this, ShowWarningMessage);
            }
            else
            {
                ShowWarningMessage = false;
                CanGoToDirectory = true;
                DirectoryWarningMessage.Invoke(this, ShowWarningMessage);
            }
        }

        private async void GoToDirectory()
        {
            string SubDirectory = CurrentDirectory;
            string Reset = CurrentDirectory;
            string ReadableDirectory = BaseDirectory + SubDirectory.Replace("/", "\\");

            if (!ReadableDirectory.EndsWith('\\'))
                ReadableDirectory += "\\";

            if (Directory.Exists(ReadableDirectory))
            {
                SelectedFilterIndex = 0;
                CurrentFiles = DefaultCurrentFiles;
                IsContentLoading = true;

                await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
                {
                    if (!CurrentDirectory.EndsWith('/'))
                        CurrentDirectory += "/";

                    Project project = _userDataServices.ActiveProject;
                    if (project != null)
                    {
                        FileStructure structure = new FileStructure();

                        if (CurrentFiles != DefaultDirectory)
                            CurrentDirectoryStack.Push(CurrentDirectoryBeforeEdit);

                        if (CurrentFiles != DefaultDirectory)
                            MemoryDirectoriesStack.Push(CurrentFiles);
                        CurrentFiles = structure.CreateCurrentLevelList(project, ReadableDirectory, (ContentSort)SelectedSortIndex);

                        ReturnPageStack.Clear();
                        ReturnDirectoryStack.Clear();
                    }

                    DirectoryUpdated = true;
                    FocusTarget.Focus();
                }));

                if (!Reset.EndsWith('/') && !string.IsNullOrEmpty(Reset))
                    Reset += "/";
                CurrentDirectory = Reset;
                IsContentLoading = false;

                DefaultCurrentFiles = CurrentFiles;
            }
            else
                WrongDirectoryNotification.Invoke(this, EventArgs.Empty);
        }

        private bool CanExecuteGoToDirectory()
        {
            if (CurrentDirectory.Equals(CurrentDirectoryBeforeEdit))
                return false;

            if (CanGoToDirectory == true)
                return true;
            else
                return false;
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

                GridContentMargin = new Thickness(10, 0, 10, 10);
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

        private void ItemSelectionChanged(SelectionChangedEventArgs e)
        {
            if(SelectedItems.Count > 0)
            {
                if (SelectedItems[0] != SelectedItem)
                {
                    SelectedItems.Clear();
                    SelectedItems.Add(SelectedItem); // First selection isn't added in Added Items
                }

                if (SelectedItems[0] == SelectedItem && e.AddedItems.Count == 0)
                {
                    SelectedItems.Clear();
                    SelectedItems.Add(SelectedItem);
                }
            }
            
            foreach (var AddedItem in e.AddedItems)
            {
                if (!SelectedItems.Contains(AddedItem))
                    SelectedItems.Add((ContentFileStructure)AddedItem);
            }
        }

        private async void ExecuteItem()
        {
            if (SelectedItem.File != null)
            {
                _userDataServices.AddFileToHeader(SelectedItem.File);
            }
            else
            {
                IsContentLoading = true;
                await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
                {
                    Project project = _userDataServices.ActiveProject;
                    if (project != null)
                    {
                        FileStructure structure = new FileStructure();

                        if (CurrentFiles != DefaultDirectory)
                            CurrentDirectoryStack.Push(CurrentDirectory);
                        CurrentDirectory += SelectedItem.Name + "/";

                        string SubDirectory = CurrentDirectory;
                        string ReadableDirectory = BaseDirectory + SubDirectory.Replace("/", "\\");

                        if (!ReadableDirectory.EndsWith('\\'))
                            ReadableDirectory += "\\";

                        if (CurrentFiles != DefaultDirectory)
                            MemoryDirectoriesStack.Push(CurrentFiles);
                        CurrentFiles = structure.CreateCurrentLevelList(project, ReadableDirectory, (ContentSort)SelectedSortIndex);

                        ReturnPageStack.Clear();
                        ReturnDirectoryStack.Clear();

                        PageChanged.Invoke(this, EventArgs.Empty);

                        DefaultCurrentFiles = CurrentFiles;
                    }
                }));

                IsContentLoading = false;
            }
        }

        private bool CanExecuteSelectedItem()
        {
            if (SelectedItems.Count > 1)
                return false;
            else
                return true;
        }

        private void DeleteSelectedItem()
        {
            IsContentDeleteProcess = true;
            IsNormalState = false;
        }

        private async void ConfirmDeleteProcess()
        {
            ReturnDirectoryStack.Clear();
            ReturnPageStack.Clear();

            IsContentDeleteProcess = false;
            IsNormalState = true;
            IsContentLoading = true;

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                FileStructure fileStructure = new FileStructure();

                string Directory = string.Empty;
                string[] Directories = Array.Empty<string>();
                ContentFileStructure TempSelectedItem = SelectedItem;

                if (SelectedItem.File != null)
                    Directory = SelectedItem.File.FileDirectory;
                else
                {
                    Directory = SelectedItem.Directory;
                    Directories = System.IO.Directory.GetFiles(Directory, "*", SearchOption.AllDirectories);
                }

                fileStructure.UpdateTreeAfterDeletion(Root, Directory);

                Node temp = Root;
                Root = new Node();
                Root = temp;

                fileStructure.DeleteItem(CurrentFiles, _userDataServices.ActiveProject, SelectedItem);
                _userDataServices.FileDeletedAsync(Directory, Directories, TempSelectedItem.Type);
            });

            IsContentLoading = false;
        }

        private void StopDeleteProcess()
        {
            IsContentDeleteProcess = false;
            IsNormalState = true;
            ShowNameInputWarningMessage = false;
        }

        private void NameChanged()
        {
            if (RenameNewName.Any(c => InvalidCharactersFile.Contains(c)) == true || ItemNewName.Any(c => InvalidCharactersFile.Contains(c)) == true)
            {
                NameInputMessage = "You Can’t Enter / \\ : * ? \" < > | ";
                ShowNameInputWarningMessage = true;

                IsRenameItemConfirmButtonEnabled = false;
                IsNameItemConfirmButtonEnabled = false;
            }
            else
            {
                if (SelectedAddIndex == -1)
                    IsRenameItemConfirmButtonEnabled = true;
                else
                    IsNameItemConfirmButtonEnabled = true;

                ShowNameInputWarningMessage = false;
            }

            foreach (var item in CurrentFiles)
            {
                if (string.Equals(item.Name, RenameNewName, StringComparison.OrdinalIgnoreCase) && string.Equals(SelectedItem.Type, item.Type, StringComparison.OrdinalIgnoreCase))
                {
                    NameInputMessage = "An Item With That Name Already Exists ";
                    ShowNameInputWarningMessage = true;

                    IsRenameItemConfirmButtonEnabled = false;
                    IsNameItemConfirmButtonEnabled = false;
                }
                else if (string.Equals(item.Name, ItemNewName, StringComparison.OrdinalIgnoreCase) && string.Equals(item.Type, Enum.GetName(typeof(ContentAdd), SelectedAddIndex), StringComparison.OrdinalIgnoreCase))
                {
                    NameInputMessage = "An Item With That Name Already Exists ";
                    ShowNameInputWarningMessage = true;

                    IsRenameItemConfirmButtonEnabled = false;
                    IsNameItemConfirmButtonEnabled = false;
                }
            }
        }

        private void RenameSelectedItem()
        {
            Debug.WriteLine($"Rename: {SelectedItem.Name}");

            OpenRenameTextBox = true;
            IsNormalState = false;
            IsContentRenameProcess = true;
        }

        private bool CanRenameSelectedItem()
        {
            if (SelectedItem == null)
                return false;

            if (SelectedItems.Count > 1)
                return false;
            else
                return true;
        }

        private async void ConfirmRenameProcess()
        {
            IsContentRenameProcess = false;
            IsContentLoading = true;

            OpenRenameTextBox = false;

            string OldDirectory = string.Empty;
            if (SelectedItem.File == null)
            {
                OldDirectory = SelectedItem.Directory;

                string[] TextSplit = OldDirectory.Split("\\");
                if (!string.IsNullOrEmpty(TextSplit.Last()))
                    CurrentLevel = TextSplit.Length - 1;
                else
                    CurrentLevel = TextSplit.Length - 2;
            }
            else
                OldDirectory = SelectedItem.File.FileDirectory;

            await Task.Run(() =>
            {
                FileStructure fileStructure = new FileStructure();
                fileStructure.RenameItem(SelectedItem, RenameNewName);

                fileStructure.UpdateTreeItemName(Root, _userDataServices.ActiveProject, SelectedItem, OldDirectory, CurrentLevel);
            });

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                FileStructure fileStructure = new FileStructure();
                CurrentFiles = fileStructure.SortListItems(CurrentFiles, (ContentSort)SelectedSortIndex);

                Node temp = Root;
                Root = new Node();
                Root = temp;

                _userDataServices.UpdateFileDetailsAsync(SelectedItem, OldDirectory, CurrentLevel);
            });

            RenameNewName = string.Empty;

            IsNormalState = true;
            IsContentLoading = false;

            UpdateDefaultList();
        }

        private void StopRenameProcess()
        {
            OpenRenameTextBox = false;
            RenameNewName = string.Empty;

            IsNormalState = true;
            IsContentRenameProcess = false;
            IsContentCanceledProcess = true;
            IsContentCanceledProcess = false;

            ShowNameInputWarningMessage = false;
        }

        private async void GoBack()
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                if (CurrentFiles != DefaultDirectory)
                {
                    CurrentFiles = DefaultCurrentFiles;

                    ReturnPageStack.Push(CurrentFiles);
                    ReturnDirectoryStack.Push(CurrentDirectory);

                    CurrentFiles = MemoryDirectoriesStack.Pop();
                    CurrentDirectory = CurrentDirectoryStack.Pop();

                    if (MemoryDirectoriesStack.Count == 0)
                        MemoryDirectoriesStack.Push(DefaultDirectory);
                    if (CurrentDirectoryStack.Count == 0)
                        CurrentDirectoryStack.Push("");
                }

                PageChanged.Invoke(this, EventArgs.Empty);

                // Reset Filter
                SelectedFilterIndex = 0;
                DefaultCurrentFiles = CurrentFiles;
            }));

            IsContentLoading = false;
        }

        private void GoForward()
        {
            if (ReturnPageStack.Count > 0)
            {
                CurrentFiles = DefaultCurrentFiles;

                MemoryDirectoriesStack.Push(CurrentFiles);
                CurrentDirectoryStack.Push(CurrentDirectory);

                CurrentFiles = ReturnPageStack.Pop();
                CurrentDirectory = ReturnDirectoryStack.Pop();

                PageChanged.Invoke(this, EventArgs.Empty);

                // Reset Filter
                SelectedFilterIndex = 0;
                DefaultCurrentFiles = CurrentFiles;
            }
        }

        private void AssignFocusBorder(Border border)
        {
            FocusTarget = border;
        }

        private async void UpdateFilter()
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                FileStructure fileStructure = new FileStructure();
                ObservableCollection<ContentFileStructure> FilteredFiles = fileStructure.FilterListItems(DefaultCurrentFiles, (ContentFilter)SelectedFilterIndex);
                if (FilteredFiles.Count > 0)
                    CurrentFiles = FilteredFiles;
                else
                    CurrentFiles = DefaultCurrentFiles;
            }));

            IsContentLoading = false;
        }

        private void UpdateAdd()
        {
            //MessageBox.Show($"{(ContentAdd)SelectedAddIndex}");

            if (SelectedAddIndex != -1)
            {
                OpenNameTextBox = true;
                IsNormalState = false;
                IsContentRenameProcess = true;
            }
        }

        private void UpdateDefaultList()
        {
            if ((ContentFilter)SelectedFilterIndex == ContentFilter.None)
                DefaultCurrentFiles = CurrentFiles;
        }

        private void UpdateSorting()
        {
            FileStructure fileStructure = new FileStructure();
            CurrentFiles = fileStructure.SortListItems(CurrentFiles, (ContentSort)SelectedSortIndex);
        }

        private void UpdateView()
        {
            switch ((ContentView)SelectedViewIndex)
            {
                case ContentView.LargeIcons:
                    LargeListViewHitTest = true;
                    LargeListViewOpacity = 1.0;

                    DetailsListViewHitTest = false;
                    DetailsListViewOpacity = 0.0;
                    break;
                case ContentView.Details:
                    DetailsListViewHitTest = true;
                    DetailsListViewOpacity = 1.0;

                    LargeListViewHitTest = false;
                    LargeListViewOpacity = 0.0;
                    break;
                default:
                    break;
            }
        }

        private void ConfirmNewItemNameProcess()
        {
            switch ((ContentAdd)SelectedAddIndex)
            {
                case ContentAdd.Folder:
                    AddItem("Folder");
                    break;
                case ContentAdd.MKB:
                    AddItem("mkb");
                    break;
                case ContentAdd.MKC:
                    AddItem("mkc");
                    break;
                case ContentAdd.MKM:
                    AddItem("mkm");
                    break;
                default:
                    break;
            }
        }

        private async void AddItem(string type)
        {
            IsContentRenameProcess = false;
            IsContentLoading = true;

            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                string SubDirectory = CurrentDirectory;
                string ReadableDirectory = BaseDirectory + SubDirectory.Replace("/", "\\");
                if (!ReadableDirectory.EndsWith('\\'))
                    ReadableDirectory += "\\";

                if (type.Equals("Folder"))
                {
                    ReadableDirectory += ItemNewName + "\\";
                    FileStructure fileStructure = new FileStructure();
                    CurrentFiles = fileStructure.CreateFolder(CurrentFiles, _userDataServices.ActiveProject, ReadableDirectory, (ContentSort)SelectedSortIndex);
                    fileStructure.AddEmptyFolderTreeItemName(Root, _userDataServices.ActiveProject, ReadableDirectory);

                    Node temp = Root;
                    Root = new Node();
                    Root = temp;
                }
                else
                    _userDataServices.CreateNewFileAsync(ItemNewName, type, ReadableDirectory);
            }));
            ResetNewItemNameBox(true);
        }

        private void CancelNewItemNameProcess()
        {
            ResetNewItemNameBox();
        }

        private void ResetNewItemNameBox(bool IsConfirmProcess = false)
        {
            OpenNameTextBox = false;
            ItemNewName = string.Empty;

            IsNormalState = true;

            if (IsConfirmProcess == true)
            {
                IsContentLoading = false;
            }
            else
            {
                IsContentRenameProcess = false;
                IsContentCanceledProcess = true;
                IsContentCanceledProcess = false;
            }

            SelectedAddIndex = -1;
        }
    }
}
