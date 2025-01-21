﻿using System.Collections.ObjectModel;
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

        private string renameNewName;
        public string RenameNewName
        {
            get => renameNewName;
            set { renameNewName = value; OnPropertyChanged(); }
        }

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

        public RelayCommand<SelectionChangedEventArgs> ListSelectionChanged => new RelayCommand<SelectionChangedEventArgs>(ItemSelectionChanged);

        public RelayCommand<object> RenameItem => new RelayCommand<object>(execute => RenameSelectedItem(), canExecute => CanRenameSelectedItem());
        public RelayCommand<object> ConfirmRename => new RelayCommand<object>(execute => ConfirmRenameProcess(), canExecute => OpenRenameTextBox);
        public RelayCommand<object> CancelRename => new RelayCommand<object>(execute => StopRenameProcess(), canExecute => OpenRenameTextBox);

        public ContentWindowViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            SelectedItems = new ObservableCollection<ContentFileStructure>();

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

            IsContentLoading = false;
            IsContentRenameProcess = false;
            IsContentCanceledProcess = false;

            SearchText = string.Empty;
            SelectedSortIndex = (int)ContentSort.FoldersFirst_Ascending;

            IsNormalState = true;
            OpenRenameTextBox = false;
            RenameNewName = string.Empty;

            LoadData();
        }

        private async void LoadData()
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                Project project = _userDataServices.ActiveProject;
                if (project != null)
                {
                    FileStructure structure = new FileStructure();

                    CurrentFiles = new ObservableCollection<ContentFileStructure>();
                    CurrentFiles = structure.CreateCurrentLevelList(project, (ContentSort)SelectedSortIndex);

                    Root = new Node();
                    Root = structure.ContentBuildSideTree(project);

                    //root = structure.ContentBuildSideTree(project);
                    /*foreach (var subNodes in root.SubNodes)
                    {
                        Root.Add(subNodes);
                    }*/
                }
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
            }
        }

        private void OpenFile()
        {
            // TODO: Add folder transition.
            if (SelectedNode.NodeType == DataType.File)
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

        private void RenameSelectedItem()
        {
            Debug.WriteLine($"Rename: {SelectedItem.Name}");

            OpenRenameTextBox = true;
            IsNormalState = false;
            IsContentRenameProcess = true;
        }

        private bool CanRenameSelectedItem()
        {
            if (SelectedItems.Count > 1)
                return false;
            else
                return true;
        }

        private async void ConfirmRenameProcess()
        {
            IsContentLoading = true;
            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                string OldDirectory = string.Empty;
                if (SelectedItem.File == null)
                    OldDirectory = SelectedItem.Directory;
                else
                    OldDirectory = SelectedItem.File.FileDirectory;

                FileStructure fileStructure = new FileStructure();
                fileStructure.RenameFile(SelectedItem, RenameNewName);
                CurrentFiles = new ObservableCollection<ContentFileStructure>(CurrentFiles);

                fileStructure.UpdateTreeItemName(Root, _userDataServices.ActiveProject, SelectedItem, OldDirectory);
                Node Temp = Root;
                Root = new Node();
                Root = Temp;

                _userDataServices.UpdateFileDetailsAsync();

                OpenRenameTextBox = false;
                RenameNewName = string.Empty;

                IsNormalState = true;
                IsContentRenameProcess = false;
            }));

            IsContentLoading = false;
        }

        private void StopRenameProcess()
        {
            OpenRenameTextBox = false;
            RenameNewName = string.Empty;

            IsNormalState = true;
            IsContentRenameProcess = false;
            IsContentCanceledProcess = true;
            IsContentCanceledProcess = false;
        }
    }
}
