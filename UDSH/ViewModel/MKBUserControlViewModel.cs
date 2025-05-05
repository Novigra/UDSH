// Copyright (C) 2025 Mohammed Kenawy
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class BranchNode
    {
        public DialogueNode dialogueNode { get; set; }
        public ObservableCollection<BranchNode> ParentNodes { get; set; } = new ObservableCollection<BranchNode>();
        public ObservableCollection<BranchNode> SubBranchNodes { get; set; } = new ObservableCollection<BranchNode>();
    }

    public class MKBUserControlViewModel : ViewModelBase
    {
        public event EventHandler DialogueNodeAddedInMultiSelectionBox;

        private readonly string _workspaceServicesID;
        private readonly IWorkspaceServices _workspaceServices;
        private Window MainWindow;
        public Canvas MainCanvas { get; set; }
        private Style CanvasEllipseStyle;
        private EllipseCanvas MainEllipseCanvas;
        private bool IsRightMouseButtonPressed;
        private bool IsLeftMouseButtonPressed;
        private Point InitialMousePosition;
        private Point InitialCanvasMousePosition;

        private Border LeftCollisionBorder;
        private Border RightCollisionBorder;
        private Border BottomCollisionBorder;

        private double CanvasDimensionsUpdate = 1200;
        private double CanvasWidthOffset = 600;

        public Path CurrentPath;
        public DialogueNode CurrentDialogueNode { get; set; }
        public DialogueNode SelectedDialogueNode { get; set; }
        public BranchNode SelectedBranchNode { get; set; }

        public Point PathCurrentMouseLocation;
        public LineGeometry line;
        LinearGradientBrush gradient;
        public bool IsConnecting = false;

        private bool MouseMoved { get; set; } = false;
        private bool canOpenCanvasContextMenu;
        public bool CanOpenCanvasContextMenu
        {
            get => canOpenCanvasContextMenu;
            set { canOpenCanvasContextMenu = value; OnPropertyChanged(); }
        }

        private event Action CanvasBordersLoaded;
        private int loadedCanvasBorders = 0;
        public int LoadedCanvasBorders
        {
            get => loadedCanvasBorders;
            set
            {
                loadedCanvasBorders = value;
                if (loadedCanvasBorders == 3)
                    CanvasBordersLoaded.Invoke();
            }
        }

        public bool CanRemoveConnectedNodesPaths { get; set; } = false;

        private List<BranchNode> Roots { get; set; } = new List<BranchNode>();
        private TranslateTransform CanvasTranslateTransform { get; set; }
        private ScaleTransform CanvasScaleTransform { get; set; }
        public double Scale { get; set; }
        public bool CanReceiveCanvasMouseInput { get; set; } = true;

        private SolidColorBrush DialogueContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
        private Color DialogueContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#E0911A");
        private SolidColorBrush ChoiceContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));
        private Color ChoiceContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#C41CFF");


        #region Dialogue Node Properties
        private double collisionBorderWidth;
        public double CollisionBorderWidth
        {
            get => collisionBorderWidth;
            set { collisionBorderWidth = value; OnPropertyChanged(); }
        }

        private double collisionBorderHeight;
        public double CollisionBorderHeight
        {
            get => collisionBorderHeight;
            set { collisionBorderHeight = value; OnPropertyChanged(); }
        }

        private Thickness collisionBorderPadding;
        public Thickness CollisionBorderPadding
        {
            get => collisionBorderPadding;
            set { collisionBorderPadding = value; OnPropertyChanged(); }
        }

        private Thickness contentStackPanelMargin;
        public Thickness ContentStackPanelMargin
        {
            get => contentStackPanelMargin;
            set { contentStackPanelMargin = value; OnPropertyChanged(); }
        }

        private CornerRadius containerBorderCornerRadius;
        public CornerRadius ContainerBorderCornerRadius
        {
            get => containerBorderCornerRadius;
            set { containerBorderCornerRadius = value; OnPropertyChanged(); }
        }

        private Thickness containerBorderThickness;
        public Thickness ContainerBorderThickness
        {
            get => containerBorderThickness;
            set { containerBorderThickness = value; OnPropertyChanged(); }
        }

        private DoubleCollection containerBorderBrushStrokeDashArray;
        public DoubleCollection ContainerBorderBrushStrokeDashArray
        {
            get => containerBorderBrushStrokeDashArray;
            set { containerBorderBrushStrokeDashArray = value; OnPropertyChanged(); }
        }

        private double containerBorderBrushStrokeThickness;
        public double ContainerBorderBrushStrokeThickness
        {
            get => containerBorderBrushStrokeThickness;
            set { containerBorderBrushStrokeThickness = value; OnPropertyChanged(); }
        }

        private double containerBorderBrushRadiusX;
        public double ContainerBorderBrushRadiusX
        {
            get => containerBorderBrushRadiusX;
            set { containerBorderBrushRadiusX = value; OnPropertyChanged(); }
        }

        private double containerBorderBrushRadiusY;
        public double ContainerBorderBrushRadiusY
        {
            get => containerBorderBrushRadiusY;
            set { containerBorderBrushRadiusY = value; OnPropertyChanged(); }
        }

        private double nodeCollisionBorderWidth;
        public double NodeCollisionBorderWidth
        {
            get => nodeCollisionBorderWidth;
            set { nodeCollisionBorderWidth = value; OnPropertyChanged(); }
        }

        private double nodeCollisionBorderHeight;
        public double NodeCollisionBorderHeight
        {
            get => nodeCollisionBorderHeight;
            set { nodeCollisionBorderHeight = value; OnPropertyChanged(); }
        }

        private CornerRadius nodeCollisionBorderCornerRadius;
        public CornerRadius NodeCollisionBorderCornerRadius
        {
            get => nodeCollisionBorderCornerRadius;
            set { nodeCollisionBorderCornerRadius = value; OnPropertyChanged(); }
        }

        private Thickness nodeCollisionBorderParentMargin;
        public Thickness NodeCollisionBorderParentMargin
        {
            get => nodeCollisionBorderParentMargin;
            set { nodeCollisionBorderParentMargin = value; OnPropertyChanged(); }
        }

        private Thickness nodeCollisionBorderChildMargin;
        public Thickness NodeCollisionBorderChildMargin
        {
            get => nodeCollisionBorderChildMargin;
            set { nodeCollisionBorderChildMargin = value; OnPropertyChanged(); }
        }

        private Thickness nodeCollisionBorderThickness;
        public Thickness NodeCollisionBorderThickness
        {
            get => nodeCollisionBorderThickness;
            set { nodeCollisionBorderThickness = value; OnPropertyChanged(); }
        }

        private DoubleCollection nodeCollisionBorderStrokeDashArray;
        public DoubleCollection NodeCollisionBorderStrokeDashArray
        {
            get => nodeCollisionBorderStrokeDashArray;
            set { nodeCollisionBorderStrokeDashArray = value; OnPropertyChanged(); }
        }

        private double nodeCollisionBorderStrokeThickness;
        public double NodeCollisionBorderStrokeThickness
        {
            get => nodeCollisionBorderStrokeThickness;
            set { nodeCollisionBorderStrokeThickness = value; OnPropertyChanged(); }
        }

        private double nodeCollisionBorderRadiusX;
        public double NodeCollisionBorderRadiusX
        {
            get => nodeCollisionBorderRadiusX;
            set { nodeCollisionBorderRadiusX = value; OnPropertyChanged(); }
        }

        private double nodeCollisionBorderRadiusY;
        public double NodeCollisionBorderRadiusY
        {
            get => nodeCollisionBorderRadiusY;
            set { nodeCollisionBorderRadiusY = value; OnPropertyChanged(); }
        }

        private double nodeTitleIconWidth;
        public double NodeTitleIconWidth
        {
            get => nodeTitleIconWidth;
            set { nodeTitleIconWidth = value; OnPropertyChanged(); }
        }

        private double nodeTitleIconHeight;
        public double NodeTitleIconHeight
        {
            get => nodeTitleIconHeight;
            set { nodeTitleIconHeight = value; OnPropertyChanged(); }
        }

        private double nodeTitleDialogueTextBlockWidth;
        public double NodeTitleDialogueTextBlockWidth
        {
            get => nodeTitleDialogueTextBlockWidth;
            set { nodeTitleDialogueTextBlockWidth = value; OnPropertyChanged(); }
        }

        private double nodeTitleFontSize;
        public double NodeTitleFontSize
        {
            get => nodeTitleFontSize;
            set { nodeTitleFontSize = value; OnPropertyChanged(); }
        }

        private Thickness nodeTitleMargin;
        public Thickness NodeTitleMargin
        {
            get => nodeTitleMargin;
            set { nodeTitleMargin = value; OnPropertyChanged(); }
        }

        private double rTBFontSize;
        public double RTBFontSize
        {
            get => rTBFontSize;
            set { rTBFontSize = value; OnPropertyChanged(); }
        }

        private Thickness nodeChoiceMargin;
        public Thickness NodeChoiceMargin
        {
            get => nodeChoiceMargin;
            set { nodeChoiceMargin = value; OnPropertyChanged(); }
        }

        private double nodeChoiceRTBWidth;
        public double NodeChoiceRTBWidth
        {
            get => nodeChoiceRTBWidth;
            set { nodeChoiceRTBWidth = value; OnPropertyChanged(); }
        }

        private Thickness nodeCharacterMargin;
        public Thickness NodeCharacterMargin
        {
            get => nodeCharacterMargin;
            set { nodeCharacterMargin = value; OnPropertyChanged(); }
        }

        private double nodeCharacterIconWidth;
        public double NodeCharacterIconWidth
        {
            get => nodeCharacterIconWidth;
            set { nodeCharacterIconWidth = value; OnPropertyChanged(); }
        }

        private double nodeCharacterIconHeight;
        public double NodeCharacterIconHeight
        {
            get => nodeCharacterIconHeight;
            set { nodeCharacterIconHeight = value; OnPropertyChanged(); }
        }

        private double nodeRTBWidth;
        public double NodeRTBWidth
        {
            get => nodeRTBWidth;
            set { nodeRTBWidth = value; OnPropertyChanged(); }
        }

        private double nodeMarkFontSize;
        public double NodeMarkFontSize
        {
            get => nodeMarkFontSize;
            set { nodeMarkFontSize = value; OnPropertyChanged(); }
        }

        private Thickness nodeMarkMargin;
        public Thickness NodeMarkMargin
        {
            get => nodeMarkMargin;
            set { nodeMarkMargin = value; OnPropertyChanged(); }
        }

        private Thickness nodeDialogueMargin;
        public Thickness NodeDialogueMargin
        {
            get => nodeDialogueMargin;
            set { nodeDialogueMargin = value; OnPropertyChanged(); }
        }

        private double nodeDialogueIconWidth;
        public double NodeDialogueIconWidth
        {
            get => nodeDialogueIconWidth;
            set { nodeDialogueIconWidth = value; OnPropertyChanged(); }
        }

        private double nodeDialogueIconHeight;
        public double NodeDialogueIconHeight
        {
            get => nodeDialogueIconHeight;
            set { nodeDialogueIconHeight = value; OnPropertyChanged(); }
        }

        private double nodeDialogueRTBWidth;
        public double NodeDialogueRTBWidth
        {
            get => nodeDialogueRTBWidth;
            set { nodeDialogueRTBWidth = value; OnPropertyChanged(); }
        }

        private double nodeOrderTextBlockFontSize;
        public double NodeOrderTextBlockFontSize
        {
            get => nodeOrderTextBlockFontSize;
            set { nodeOrderTextBlockFontSize = value; OnPropertyChanged(); }
        }

        private Thickness nodeOrderTextBlockMargin;
        public Thickness NodeOrderTextBlockMargin
        {
            get => nodeOrderTextBlockMargin;
            set { nodeOrderTextBlockMargin = value; OnPropertyChanged(); }
        }

        private Thickness rootCollisionBorderPadding;
        public Thickness RootCollisionBorderPadding
        {
            get => rootCollisionBorderPadding;
            set { rootCollisionBorderPadding = value; OnPropertyChanged(); }
        }

        private double rootContainerBorderHeight;
        public double RootContainerBorderHeight
        {
            get => rootContainerBorderHeight;
            set { rootContainerBorderHeight = value; OnPropertyChanged(); }
        }

        private double rootContainerImageWidth;
        public double RootContainerImageWidth
        {
            get => rootContainerImageWidth;
            set { rootContainerImageWidth = value; OnPropertyChanged(); }
        }

        private double rootContainerImageHeight;
        public double RootContainerImageHeight
        {
            get => rootContainerImageHeight;
            set { rootContainerImageHeight = value; OnPropertyChanged(); }
        }
        #endregion


        #region Selection Border Properties
        private SelectionBorderBox CurrentSelectionBorderBox { get; set; }

        private double selectionBorderWidth = 20;
        public double SelectionBorderWidth
        {
            get => selectionBorderWidth;
            set { selectionBorderWidth = value; OnPropertyChanged(); }
        }

        private double selectionBorderHeight = 20;
        public double SelectionBorderHeight
        {
            get => selectionBorderHeight;
            set { selectionBorderHeight = value; OnPropertyChanged(); }
        }

        private bool StartMultiSelectionProcess { get; set; } = false;
        private Point InitialLeftMouseMultiSelectionPosition { get; set; }
        public List<BranchNode> MultiSelectedBranchNodes { get; set; } = new List<BranchNode>();
        private bool CanMoveMultiSelectedBranchNodes { get; set; } = false;
        #endregion

        private int selectedResizeIndex;
        public int SelectedResizeIndex
        {
            get => selectedResizeIndex;
            set { selectedResizeIndex = value; OnPropertyChanged(); }
        }

        private string selectedResizeText;
        public string SelectedResizeText
        {
            get => selectedResizeText;
            set { selectedResizeText = value; OnPropertyChanged(); }
        }

        private bool mkcConnectionProcessStarted = false;
        public bool MkcConnectionProcessStarted
        {
            get => mkcConnectionProcessStarted;
            set { mkcConnectionProcessStarted = value; OnPropertyChanged(); }
        }

        private string searchText = string.Empty;
        public string SearchText
        {
            get => searchText;
            set { searchText = value; OnPropertyChanged(); }
        }

        private bool canResizeConnectionWindow = false;
        public bool CanResizeConnectionWindow
        {
            get => canResizeConnectionWindow;
            set { canResizeConnectionWindow = value; OnPropertyChanged(); }
        }

        private double resizeConnectionWindowHeight = 0;
        public double ResizeConnectionWindowHeight
        {
            get => resizeConnectionWindowHeight;
            set { resizeConnectionWindowHeight = value; OnPropertyChanged(); }
        }

        private bool isMouseInsideResizeControl = false;
        public bool IsMouseInsideResizeControl
        {
            get => isMouseInsideResizeControl;
            set { isMouseInsideResizeControl = value; OnPropertyChanged(); }
        }

        private bool isConnectedToMKCFile = false;
        public bool IsConnectedToMKCFile
        {
            get => isConnectedToMKCFile;
            set { isConnectedToMKCFile = value; OnPropertyChanged(); }
        }

        private bool isConnectToMKCButtonActive;
        public bool IsConnectToMKCButtonActive
        {
            get => isConnectToMKCButtonActive;
            set { isConnectToMKCButtonActive = value; OnPropertyChanged(); }
        }

        private bool isConnectedToMKCButtonActive;
        public bool IsConnectedToMKCButtonActive
        {
            get => isConnectedToMKCButtonActive;
            set { isConnectedToMKCButtonActive = value; OnPropertyChanged(); }
        }

        private bool showConnectMKCButton = false;
        public bool ShowConnectMKCButton
        {
            get => showConnectMKCButton;
            set { showConnectMKCButton = value; OnPropertyChanged(); }
        }

        private bool showConnectedMKCButton = false;
        public bool ShowConnectedMKCButton
        {
            get => showConnectedMKCButton;
            set { showConnectedMKCButton = value; OnPropertyChanged(); }
        }

        private bool canRemoveConnectedMKCFile = false;
        public bool CanRemoveConnectedMKCFile
        {
            get => canRemoveConnectedMKCFile;
            set { canRemoveConnectedMKCFile = value; OnPropertyChanged(); }
        }

        private bool resetMKCConnectionButtons = false;
        public bool ResetMKCConnectionButtons
        {
            get => resetMKCConnectionButtons;
            set { resetMKCConnectionButtons = value; OnPropertyChanged(); }
        }

        private Point InitialResizeConnectionMousePosition;

        private Project CurrentProject { get; set; }
        private FileSystem AssociatedFile { get; set; }

        private ObservableCollection<FileSystem> mKCFiles = new ObservableCollection<FileSystem>();
        public ObservableCollection<FileSystem> MKCFiles
        {
            get => mKCFiles;
            set { mKCFiles = value; OnPropertyChanged(); }
        }

        private FileSystem selectedMKCFile;
        public FileSystem SelectedMKCFile
        {
            get => selectedMKCFile;
            set { selectedMKCFile = value; OnPropertyChanged(); }
        }

        private string connectedMKCFileName;
        public string ConnectedMKCFileName
        {
            get => connectedMKCFileName;
            set { connectedMKCFileName = value; OnPropertyChanged(); }
        }

        public RelayCommand<Canvas> CanvasRMBDown => new RelayCommand<Canvas>(execute => StartRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasRMBUp => new RelayCommand<Canvas>(execute => StopRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasMouseMove => new RelayCommand<Canvas>(execute => HandleCanvasMouseMove());

        public RelayCommand<MouseButtonEventArgs> CanvasLMBDown => new RelayCommand<MouseButtonEventArgs>(HandleCanvasLeftMouseButtonDown);
        public RelayCommand<MouseButtonEventArgs> CanvasLMBUp => new RelayCommand<MouseButtonEventArgs>(HandleCanvasLeftMouseButtonUp);

        /*public RelayCommand<Border> BorderLMBUp => new RelayCommand<Border>(execute => StopRecordingNodeMovement());
        public RelayCommand<Border> BorderMouseMove => new RelayCommand<Border>(execute => UpdateNodeLocation());*/

        public RelayCommand<Canvas> CanvasLoaded => new RelayCommand<Canvas>(SetCurrentCanvas);
        public RelayCommand<EllipseCanvas> EllipseCanvasLoaded => new RelayCommand<EllipseCanvas>(SetEllipseCanvas);

        public RelayCommand<Border> LeftCollisionLoaded => new RelayCommand<Border>(SetLeftCollision);
        public RelayCommand<Border> RightCollisionLoaded => new RelayCommand<Border>(SetRightCollision);
        public RelayCommand<Border> BottomCollisionLoaded => new RelayCommand<Border>(SetBottomCollision);

        public RelayCommand<string> AddDialogueNode => new RelayCommand<string>(CreateDialogueNode);
        public RelayCommand<object> ResizeSelectionChanged => new RelayCommand<object>(execute => UpdateElementsSize());

        public RelayCommand<MouseButtonEventArgs> StartResizingConnectionHeight => new RelayCommand<MouseButtonEventArgs>(HandleResizeControlLeftMouseButtonDown);
        public RelayCommand<MouseButtonEventArgs> StopResizingConnectionHeight => new RelayCommand<MouseButtonEventArgs>(HandleResizeControlLeftMouseButtonUp);
        public RelayCommand<MouseEventArgs> ResizingConnectionMouseMove => new RelayCommand<MouseEventArgs>(HandleResizeControlMouseMove);
        public RelayCommand<object> MouseEnteredResizeControl => new RelayCommand<object>(execute => UpdateResizeControlStatus());
        public RelayCommand<object> MouseLeavedResizeControl => new RelayCommand<object>(execute => UpdateResizeControlStatus());
        public RelayCommand<object> CloseConnection => new RelayCommand<object>(execute => StopConnectionProcess());
        public RelayCommand<object> SearchTextChange => new RelayCommand<object>(execute => UpdateSearchList());
        public RelayCommand<object> ConnectMKCButtonClicked => new RelayCommand<object>(execute => HandleConnectMKCButtonClicked());
        public RelayCommand<MouseButtonEventArgs> ConnectBNToMKC => new RelayCommand<MouseButtonEventArgs>(HandleBNToMKCConnection, canExecute => !IsConnectedToMKCFile);
        public RelayCommand<Button> ConnectedMKCButtonMouseEnter => new RelayCommand<Button>(UpdateMKCButtonState);
        public RelayCommand<Button> ConnectedMKCButtonMouseLeave => new RelayCommand<Button>(ResetMKCButtonState);
        public RelayCommand<object> ConnectedMKCButtonClicked => new RelayCommand<object>(execute => HandleConnectedMKCButton());
        public RelayCommand<object> UserControlLoaded => new RelayCommand<object>(execute => UpdateDetailsOnUserControlLoaded());

        public MKBUserControlViewModel(IWorkspaceServices workspaceServices)
        {
            _workspaceServices = workspaceServices;

            _workspaceServicesID = Guid.NewGuid().ToString();
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);

            MainWindow = workspaceServices.MainWindow;
            MainWindow.SizeChanged += MainWindow_SizeChanged;

            CanvasEllipseStyle = (Style)Application.Current.FindResource("BNCanvasEllipse");
            IsRightMouseButtonPressed = false;

            CanvasBordersLoaded += MKBUserControlViewModel_CanvasBordersLoaded;
            workspaceServices.ControlButtonPressed += WorkspaceServices_ControlButtonPressed;
            workspaceServices.ControlButtonReleased += WorkspaceServices_ControlButtonReleased;
            workspaceServices.Reset += WorkspaceServices_Reset;
            workspaceServices.MKCSearchInitAnimFinished += WorkspaceServices_MKCSearchInitAnimFinished;

            Scale = 1.0;
            SetDefaultValues();

            SelectedResizeIndex = 0;
            SelectedResizeText = "100%";

            LoadMKCFiles();
        }

        private void WorkspaceServices_ControlButtonPressed(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                if (e.KeyEvent.Key == Key.LeftCtrl)
                {
                    CanRemoveConnectedNodesPaths = true;
                    CanRemoveConnectedMKCFile = true;
                }

                if (e.KeyEvent.Key == Key.Add)
                {
                    if (Scale < 1)
                    {
                        Scale += 0.1;
                        UpdateResizeBoxData();
                        SetDefaultValues();
                        UpdatePathsThickness();
                        _ = UpdatePathsLocation();
                    }
                }

                if (e.KeyEvent.Key == Key.Subtract)
                {
                    if (Math.Round(Scale, 1) > 0.5)
                    {
                        Scale -= 0.1;
                        UpdateResizeBoxData();
                        SetDefaultValues();
                        UpdatePathsThickness();
                        _ = UpdatePathsLocation();
                    }
                }

                if (e.KeyEvent.Key == Key.Delete)
                {
                    DeleteBranchNode();
                    DeleteMultiSelectedNodes();
                }
            }
        }

        private void UpdatePathsThickness()
        {
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();
            BranchNode? CurrentBranchNode = null;

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                CurrentBranchNode = BranchNodeStack.Pop();
                
                foreach (Path CurrentChildPath in CurrentBranchNode.dialogueNode.ChildrenPath)
                {
                    CurrentChildPath.StrokeThickness = 5 * Scale;
                }

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }
        }

        private async Task UpdatePathsLocation()
        {
            await Task.Delay(60);

            foreach (var obj in MainCanvas.Children)
            {
                if (obj is DialogueNode node)
                {
                    node.UpdatePathsLocation();
                }
            }
        }

        private void WorkspaceServices_ControlButtonReleased(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                if (e.KeyEvent.Key == Key.LeftCtrl)
                {
                    CanRemoveConnectedNodesPaths = false;
                    CanRemoveConnectedMKCFile = false;
                }
            }
        }

        private void WorkspaceServices_Reset(object? sender, EventArgs e)
        {
            CanRemoveConnectedNodesPaths = false;
            CanRemoveConnectedMKCFile = false;
        }

        private void StartRecordingMouseMovement()
        {
            InitialMousePosition = Mouse.GetPosition(null);
            InitialCanvasMousePosition = Mouse.GetPosition(MainCanvas);
            Mouse.Capture(MainCanvas);
            IsRightMouseButtonPressed = true;
        }

        private void StopRecordingMouseMovement()
        {
            IsRightMouseButtonPressed = false;
            Mouse.Capture(null);

            Point CurrentMousePosition = Mouse.GetPosition(null);

            if (MouseMoved == false)
                CanOpenCanvasContextMenu = true;
            else
                CanOpenCanvasContextMenu = false;
           
            MouseMoved = false;
        }

        private void HandleCanvasMouseMove()
        {
            UpdateCanvasTransform();

            if (StartMultiSelectionProcess == true)
            {
                UpdateSelectionBoxSize();
            }

            if (CanMoveMultiSelectedBranchNodes == true)
            {
                UpdateMultiSelectedNodes();
            }
        }

        private void UpdateCanvasTransform()
        {
            if (IsRightMouseButtonPressed == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);
                double DeltaX = InitialMousePosition.X - CurrentMousePosition.X;
                double DeltaY = InitialMousePosition.Y - CurrentMousePosition.Y;

                if (CanvasTranslateTransform != null)
                {
                    double NewTranslateX = CanvasTranslateTransform.X - DeltaX;
                    double BoundaryX = MainWindow.Width - MainCanvas.Width;

                    double NewTranslateY = CanvasTranslateTransform.Y - DeltaY;
                    double BoundaryY = MainWindow.Height - MainCanvas.Height;

                    CanvasTranslateTransform.X = Math.Max(BoundaryX, Math.Min(0, NewTranslateX));
                    CanvasTranslateTransform.Y = Math.Max(BoundaryY, Math.Min(0, NewTranslateY));
                }

                InitialMousePosition = CurrentMousePosition;

                MouseMoved = true;
            }

            PathCurrentMouseLocation = Mouse.GetPosition(null);
            if (IsConnecting == true)
            {
                line.EndPoint = new Point(PathCurrentMouseLocation.X - CanvasTranslateTransform.X, PathCurrentMouseLocation.Y - CanvasTranslateTransform.Y - 110);
                gradient.EndPoint = line.EndPoint;
            }
        }

        private void HandleCanvasLeftMouseButtonDown(MouseButtonEventArgs e)
        {
            InitialLeftMouseMultiSelectionPosition = Mouse.GetPosition(MainCanvas);

            if (IsConnecting == true && CanRemoveConnectedNodesPaths == true)
            {
                IsConnecting = false;
                MainCanvas.Children.Remove(CurrentPath);

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node.ParentsPath.Count == 0 && node.IsRootNode == false)
                        {
                            node.OpacityAnimation(0, node.ParentNodeCollisionBorder);
                            node.ParentNodeCollisionBorder.IsHitTestVisible = false;
                        }

                        if (node.ChildrenPath.Count == 0)
                        {
                            node.OpacityAnimation(0, node.ChildrenNodeCollisionBorder);
                        }
                    }
                }
            }

            if (e.ClickCount == 2 && CanReceiveCanvasMouseInput == true)
            {
                CreateSelectionBorderBox();

                Debug.WriteLine($"Number Of Clicks: {e.ClickCount}");
            }

            if (CanReceiveCanvasMouseInput == true)
                RemoveSelectedNode();
        }

        private void HandleCanvasLeftMouseButtonUp(MouseButtonEventArgs e)
        {
            StartMultiSelectionProcess = false;

            Point CurrentMultiSelectionMousePosition = Mouse.GetPosition(MainCanvas);

            if (CurrentSelectionBorderBox != null && InitialLeftMouseMultiSelectionPosition != CurrentMultiSelectionMousePosition)
            {
                GetDialogueNodesInSelectionBorder();

                SelectionBorderWidth = 0;
                SelectionBorderHeight = 0;
                MainCanvas.Children.Remove(CurrentSelectionBorderBox);
                CurrentSelectionBorderBox = null;
            }
            else if (CurrentSelectionBorderBox == null && InitialLeftMouseMultiSelectionPosition == CurrentMultiSelectionMousePosition && CanReceiveCanvasMouseInput == true)
            {
                RemoveSelectedBranchNodes();
            }
        }

        private void CreateSelectionBorderBox()
        {
            CurrentSelectionBorderBox = new SelectionBorderBox(this);
            MainCanvas.Children.Add(CurrentSelectionBorderBox);

            InitialLeftMouseMultiSelectionPosition = Mouse.GetPosition(MainCanvas);
            Canvas.SetLeft(CurrentSelectionBorderBox, InitialLeftMouseMultiSelectionPosition.X);
            Canvas.SetTop(CurrentSelectionBorderBox, InitialLeftMouseMultiSelectionPosition.Y);

            StartMultiSelectionProcess = true;
        }

        private void UpdateSelectionBoxSize()
        {
            Point CurrentMultiSelectionMousePosition = Mouse.GetPosition(MainCanvas);

            SelectionBorderWidth = Math.Abs(CurrentMultiSelectionMousePosition.X - InitialLeftMouseMultiSelectionPosition.X);
            SelectionBorderHeight = Math.Abs(CurrentMultiSelectionMousePosition.Y - InitialLeftMouseMultiSelectionPosition.Y);

            Canvas.SetLeft(CurrentSelectionBorderBox, Math.Min(CurrentMultiSelectionMousePosition.X, InitialLeftMouseMultiSelectionPosition.X));
            Canvas.SetTop(CurrentSelectionBorderBox, Math.Min(CurrentMultiSelectionMousePosition.Y, InitialLeftMouseMultiSelectionPosition.Y));
        }

        private void GetDialogueNodesInSelectionBorder()
        {
            MultiSelectedBranchNodes.Clear();
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                BranchNode CurrentBranchNode = BranchNodeStack.Pop();

                double NodeWidth = CurrentBranchNode.dialogueNode.ActualWidth;
                double NodeHeight = CurrentBranchNode.dialogueNode.ActualHeight;
                Rect NodeRect = new Rect(CurrentBranchNode.dialogueNode.Position.X, CurrentBranchNode.dialogueNode.Position.Y, NodeWidth, NodeHeight);

                Rect SelectionBorderBoxRect = new Rect(Canvas.GetLeft(CurrentSelectionBorderBox), Canvas.GetTop(CurrentSelectionBorderBox), SelectionBorderWidth, SelectionBorderHeight);

                if (NodeRect.IntersectsWith(SelectionBorderBoxRect))
                {
                    if (MultiSelectedBranchNodes.Contains(CurrentBranchNode) == false)
                    {
                        MultiSelectedBranchNodes.Add(CurrentBranchNode);
                        CurrentBranchNode.dialogueNode.IsSelectedInMultiSelectionProcess = true;
                        CurrentBranchNode.dialogueNode.UpdateNodeMultiSelectionStatus(true);
                    }
                }

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }
        }

        private void UpdateMultiSelectedNodes()
        {
            Point CurrentMousePosition = Mouse.GetPosition(MainCanvas);

            double DeltaX = InitialLeftMouseMultiSelectionPosition.X - CurrentMousePosition.X;
            double DeltaY = InitialLeftMouseMultiSelectionPosition.Y - CurrentMousePosition.Y;

            foreach (BranchNode branchNode in MultiSelectedBranchNodes)
            {
                branchNode.dialogueNode.UpdateNodeLocation(branchNode.dialogueNode.Position.X - DeltaX, branchNode.dialogueNode.Position.Y - DeltaY);
            }

            InitialLeftMouseMultiSelectionPosition = CurrentMousePosition;
        }

        private void DialogueNode_NodeRequestedSelectedNodesToStop(object? sender, EventArgs e)
        {
            CanMoveMultiSelectedBranchNodes = false;
        }

        private void RemoveSelectedBranchNodes()
        {
            foreach(BranchNode branchNode in MultiSelectedBranchNodes)
            {
                branchNode.dialogueNode.UpdateNodeMultiSelectionStatus(false);
            }

            MultiSelectedBranchNodes.Clear();
        }

        private void RemoveSelectedNode()
        {
            if (SelectedBranchNode != null)
            {
                SelectedBranchNode.dialogueNode.ResetSelectedNodeVisual();
                SelectedBranchNode = null;
                SelectedDialogueNode = null;
            }
        }

        private void DialogueNode_NodeRequestedSelectedNodesRemoval(object? sender, EventArgs e)
        {
            RemoveSelectedBranchNodes();
        }

        /*private void StopRecordingNodeMovement()
        {
            IsLeftMouseButtonPressed = false;
        }

        private void UpdateNodeLocation()
        {
            if (IsLeftMouseButtonPressed == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);
                double DeltaX = InitialMousePosition.X - CurrentMousePosition.X;
                double DeltaY = InitialMousePosition.Y - CurrentMousePosition.Y;
            }
        }*/

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //PrepareCanvas();

            if (MainCanvas.Width < MainWindow.Width)
            {
                MainCanvas.Width = MainWindow.Width;
                MainEllipseCanvas.Width = MainWindow.Width;
            }

            if (MainCanvas.Height < MainWindow.Height)
            {
                MainCanvas.Height = MainWindow.Height;
                MainEllipseCanvas.Height = MainWindow.Height;
            }
        }

        private void SetCurrentCanvas(Canvas canvas)
        {
            MainCanvas = canvas;

            TransformGroup? group = MainCanvas.RenderTransform as TransformGroup;
            if (group!.Children[0] is TranslateTransform translateTransform)
            {
                CanvasTranslateTransform = translateTransform;
            }

            if (group!.Children[1] is ScaleTransform scaleTransform)
            {
                CanvasScaleTransform = scaleTransform;
                //Scale = CanvasScaleTransform.ScaleX; // ScaleX == ScaleY
            }
        }

        private void PrepareCanvas()
        {
            if (MainWindow != null)
            {
                MainCanvas.Width = MainWindow.Width;
                MainCanvas.Height = MainWindow.Height;

                MainEllipseCanvas.Width = MainCanvas.Width;
                MainEllipseCanvas.Height = MainCanvas.Height;
            }
        }

        private void DrawEllipseBackground()
        {
            /*
             * This modification depends on Canvas width and height and those depend on the window [REMOVE WINDOW DEPENDENCE] <- Maybe I'm wrong
             *      - Canvas width and height should rely on:
             *              * Node collision.
             *              * Window width and height [We need the window]
             *      - Add Border and its width and height equal to canvas for debugging.
             */

            
        }

        private void SetEllipseCanvas(EllipseCanvas ellipseCanvas)
        {
            MainEllipseCanvas = ellipseCanvas;
            PrepareCanvas();
        }

        private void CreateDialogueNode(string type)
        {
            BNType NodeType;
            if (type.Equals("Dialogue"))
                NodeType = BNType.Dialogue;
            else
                NodeType = BNType.Choice;

            DialogueNode dialogueNode = new DialogueNode(NodeType, new NodePosition { X = InitialCanvasMousePosition.X, Y = InitialCanvasMousePosition.Y}, this, false, true);
            Roots.Add(new BranchNode { dialogueNode = dialogueNode});
            MainCanvas.Children.Add(dialogueNode);

            SetDialogueNodeEvents(dialogueNode);

            CheckCanvasBordersCollision(dialogueNode, InitialMousePosition);
        }

        private void DialogueNode_NodeRequestedSelectedNodesToMove(object? sender, EventArgs e)
        {
            /*foreach (BranchNode branchNode in MultiSelectedBranchNodes)
            {
                branchNode.dialogueNode.UpdateSelectedNodeLocation();
            }*/
            CanMoveMultiSelectedBranchNodes = true;
        }

        private void DialogueNode_NodePositionChanged(object? sender, DroppedNodeEventArgs e)
        {
            CheckCanvasBordersCollision(e.Node, e.Position);
        }

        private void CheckCanvasBordersCollision(DialogueNode dialogueNode, Point Position)
        {
            double NodeWidth = dialogueNode.ActualWidth;
            double NodeHeight = dialogueNode.ActualHeight;
            Rect NodeRect = new Rect(Position.X, Position.Y, NodeWidth, NodeHeight);

            double LeftBorderWidth = LeftCollisionBorder.ActualWidth;
            double LeftBorderHeight = LeftCollisionBorder.ActualHeight;
            Rect LeftBorderRect = new Rect(0, 0, LeftBorderWidth, LeftBorderHeight);

            double RightBorderWidth = RightCollisionBorder.ActualWidth;
            double RightBorderHeight = RightCollisionBorder.ActualHeight;
            Rect RightBorderRect = new Rect(Canvas.GetLeft(RightCollisionBorder), 0, RightBorderWidth, RightBorderHeight);

            double BottomBorderWidth = BottomCollisionBorder.ActualWidth;
            double BottomBorderHeight = BottomCollisionBorder.ActualHeight;
            Rect BottomBorderRect = new Rect(0, Canvas.GetTop(BottomCollisionBorder), BottomBorderWidth, BottomBorderHeight);

            if (NodeRect.IntersectsWith(LeftBorderRect))
            {
                // Update Canvas X offset and check if node exceeded the right collision and if so update the width of both the Main Canvas and Ellipse Canvas

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        node.UpdateNodeLocation(node.Position.X + CanvasWidthOffset, node.Position.Y);

                        if (MainCanvas.Width <= MainWindow.Width)
                        {
                            MainCanvas.Width += CanvasDimensionsUpdate;
                            MainEllipseCanvas.Width += CanvasDimensionsUpdate;
                        }
                    }
                }

                if (CanvasTranslateTransform != null)
                {
                    CanvasTranslateTransform.X -= CanvasWidthOffset;
                }
            }

            if (NodeRect.IntersectsWith(RightBorderRect))
            {
                MainCanvas.Width += CanvasDimensionsUpdate;
                MainEllipseCanvas.Width += CanvasDimensionsUpdate;
            }

            if (NodeRect.IntersectsWith(BottomBorderRect))
            {
                MainCanvas.Height += CanvasDimensionsUpdate;
                MainEllipseCanvas.Height += CanvasDimensionsUpdate;
            }
        }

        private void CheckCanvasBordersCollision(DialogueNode dialogueNode, NodePosition Position)
        {
            double NodeWidth = dialogueNode.ActualWidth;
            double NodeHeight = dialogueNode.ActualHeight;
            Rect NodeRect = new Rect(Position.X, Position.Y, NodeWidth, NodeHeight);

            double LeftBorderWidth = LeftCollisionBorder.ActualWidth;
            double LeftBorderHeight = LeftCollisionBorder.ActualHeight;
            Rect LeftBorderRect = new Rect(0, 0, LeftBorderWidth, LeftBorderHeight);

            double RightBorderWidth = RightCollisionBorder.ActualWidth;
            double RightBorderHeight = RightCollisionBorder.ActualHeight;
            Rect RightBorderRect = new Rect(Canvas.GetLeft(RightCollisionBorder), 0, RightBorderWidth, RightBorderHeight);

            double BottomBorderWidth = BottomCollisionBorder.ActualWidth;
            double BottomBorderHeight = BottomCollisionBorder.ActualHeight;
            Rect BottomBorderRect = new Rect(0, Canvas.GetTop(BottomCollisionBorder), BottomBorderWidth, BottomBorderHeight);

            if (NodeRect.IntersectsWith(LeftBorderRect))
            {
                // Update Canvas X offset and check if node exceeded the right collision and if so update the width of both the Main Canvas and Ellipse Canvas

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        node.UpdateNodeLocation(node.Position.X + CanvasWidthOffset, node.Position.Y);

                        if (MainCanvas.Width <= MainWindow.Width)
                        {
                            MainCanvas.Width += CanvasDimensionsUpdate;
                            MainEllipseCanvas.Width += CanvasDimensionsUpdate;
                        }
                    }
                }

                if (CanvasTranslateTransform != null)
                {
                    CanvasTranslateTransform.X -= CanvasWidthOffset;
                }
            }

            if (NodeRect.IntersectsWith(RightBorderRect))
            {
                MainCanvas.Width += CanvasDimensionsUpdate;
                MainEllipseCanvas.Width += CanvasDimensionsUpdate;
            }

            if (NodeRect.IntersectsWith(BottomBorderRect))
            {
                MainCanvas.Height += CanvasDimensionsUpdate;
                MainEllipseCanvas.Height += CanvasDimensionsUpdate;
            }
        }

        private void DialogueNode_NodeStartedConnectionProcess(object? sender, EventArgs e)
        {
            if (sender is DialogueNode dialogueNode)
            {
                double YOffset = 55;
                if (dialogueNode.IsRootNode == true)
                    YOffset = 15;

                CurrentPath = new Path
                {
                    Fill = (dialogueNode.NodeType == BNType.Choice) ? ChoiceContainerBackgroundSCB : DialogueContainerBackgroundSCB,
                    StrokeThickness = 5 * Scale,
                    StrokeDashArray = new DoubleCollection { 2 * Scale, 2 * Scale },
                    IsHitTestVisible = false,
                    StrokeDashCap = PenLineCap.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };

                gradient = new LinearGradientBrush
                {
                    MappingMode = BrushMappingMode.Absolute,
                    StartPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - YOffset * Scale)),
                    EndPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - YOffset * Scale)),
                    GradientStops = new GradientStopCollection
                    {
                        new GradientStop((dialogueNode.NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor, 0),
                        new GradientStop((dialogueNode.NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor, 1)
                    },
                };
                CurrentPath.Stroke = gradient;

                GeometryGroup group = new GeometryGroup();

                line = new LineGeometry
                {
                    StartPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - YOffset * Scale)),
                    EndPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - YOffset * Scale))
                };
                group.Children.Add(line);
                CurrentPath.Data = group;
                MainCanvas.Children.Add(CurrentPath);

                CurrentDialogueNode = dialogueNode;

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node != dialogueNode && node.IsRootNode == false)
                        {
                            node.OpacityAnimation(1, node.ParentNodeCollisionBorder);
                            node.ParentNodeCollisionBorder.IsHitTestVisible = true;
                        }
                    }
                }

                IsConnecting = true;
            }
        }

        private void DialogueNode_ChildNodeRequestedConnection(object? sender, EventArgs e)
        {
            if (IsConnecting == true)
            {
                IsConnecting = false;

                DialogueNode? dialogueNode = sender as DialogueNode;

                BranchNode ParentBranchNode = GetParentBranchNode();

                if (ParentBranchNode != null)
                {
                    if (CanConnectNodes(ParentBranchNode, dialogueNode!) == false)
                    {
                        MainCanvas.Children.Remove(CurrentPath);

                        foreach (var obj in MainCanvas.Children)
                        {
                            if (obj is DialogueNode node)
                            {
                                if (node.ParentsPath.Count == 0 && node.IsRootNode == false)
                                {
                                    node.OpacityAnimation(0, node.ParentNodeCollisionBorder);
                                    node.ParentNodeCollisionBorder.IsHitTestVisible = false;
                                }
                            }
                        }

                        return;
                    }

                    BranchNode branchNode = GetBranchNodeForConnection(dialogueNode!);

                    branchNode.ParentNodes.Add(ParentBranchNode);

                    ParentBranchNode.SubBranchNodes.Add(branchNode);
                }
                else
                {
                    CurrentDialogueNode.IsSubRootNode = true;
                    ParentBranchNode = new BranchNode { dialogueNode = CurrentDialogueNode };

                    BranchNode branchNode = GetChildBranchNode(dialogueNode!);
                    if (branchNode == null)
                        branchNode = new BranchNode { dialogueNode = dialogueNode! };

                    branchNode.ParentNodes.Add(ParentBranchNode);

                    ParentBranchNode.SubBranchNodes.Add(branchNode);
                    Roots.Add(ParentBranchNode);
                }

                CurrentDialogueNode.ChildrenPath.Add(CurrentPath);
                dialogueNode!.ParentsPath.Add(CurrentPath);

                line.EndPoint = new Point(dialogueNode!.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + 55 * Scale);
                gradient.EndPoint = line.EndPoint;
                CurrentPath.StrokeDashArray = new DoubleCollection();

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node.ParentsPath.Count == 0 && node.IsRootNode == false)
                        {
                            node.OpacityAnimation(0, node.ParentNodeCollisionBorder);
                            node.ParentNodeCollisionBorder.IsHitTestVisible = false;
                        }
                    }
                }

                CurrentDialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);

                if (dialogueNode.NodeType == BNType.Choice)
                {
                    UpdateChoiceNodesOrder(ParentBranchNode);
                }
            }
        }

        private bool CanConnectNodes(BranchNode ParentBranchNode, DialogueNode TargetDialogueNode)
        {
            if (ParentBranchNode.SubBranchNodes.Count == 0)
                return true;

            BranchNode ChildBranchNode = ParentBranchNode.SubBranchNodes[0];
            if (ChildBranchNode.dialogueNode.NodeType == TargetDialogueNode.NodeType)
                return true;
            else
                return false;
        }

        private BranchNode GetBranchNodeForConnection(DialogueNode dialogueNode)
        {
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                BranchNode CurrentBranchNode = BranchNodeStack.Pop();

                if (CurrentBranchNode.dialogueNode == dialogueNode)
                {
                    dialogueNode.IsSubRootNode = false;
                    Roots.Remove(CurrentBranchNode);
                    return CurrentBranchNode;
                }

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }

            return new BranchNode { dialogueNode = dialogueNode! };
        }

        private BranchNode GetChildBranchNode(DialogueNode dialogueNode)
        {
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                BranchNode CurrentBranchNode = BranchNodeStack.Pop();

                if (CurrentBranchNode.dialogueNode == dialogueNode)
                    return CurrentBranchNode;

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }

            return null;
        }

        private void DialogueNode_NodeRequestedConnectionRemoval(object? sender, BranchNodeRemovalEventArgs e)
        {
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();
            BranchNode? CurrentBranchNode = null;
            DialogueNode? TargetDialogueNode = sender as DialogueNode;

            foreach(BranchNode node in Roots)
                BranchNodeStack.Push(node);
            

            while (BranchNodeStack.Count > 0)
            {
                CurrentBranchNode = BranchNodeStack.Pop();

                if (CurrentBranchNode.dialogueNode == TargetDialogueNode)
                    break;

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }

            if (e.NodeConnectionType == ConnectionType.Parent)
            {
                if (CurrentBranchNode != null)
                {
                    foreach (BranchNode ParentBranchNode in CurrentBranchNode!.ParentNodes)
                    {
                        ParentBranchNode.SubBranchNodes.Remove(CurrentBranchNode);

                        foreach (Path ParentBranchNodeChildrenPath in ParentBranchNode.dialogueNode.ChildrenPath.ToList())
                        {
                            foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ParentsPath)
                            {
                                if (CurrentBranchNodeParentsPath == ParentBranchNodeChildrenPath)
                                {
                                    ParentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);
                                    CurrentBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);

                                    MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                                    break;
                                }
                            }
                        }

                        if (ParentBranchNode.SubBranchNodes.Count == 0)
                        {
                            ParentBranchNode.dialogueNode.OpacityAnimation(0, ParentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
                            ParentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                        }
                    }
                }

                CurrentBranchNode!.ParentNodes.Clear();

                CurrentBranchNode.dialogueNode.IsSubRootNode = true;
                Roots.Add(CurrentBranchNode);

                CurrentBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                CurrentBranchNode.dialogueNode.OpacityAnimation(0, CurrentBranchNode.dialogueNode.ParentNodeCollisionBorder);
                CurrentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);
            }
            else
            {
                if (CurrentBranchNode != null)
                {
                    foreach (BranchNode ChildBranchNode in CurrentBranchNode!.SubBranchNodes)
                    {
                        ChildBranchNode.ParentNodes.Remove(CurrentBranchNode);

                        foreach (Path ChildBranchNodeChildrenPath in ChildBranchNode.dialogueNode.ParentsPath.ToList())
                        {
                            foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ChildrenPath)
                            {
                                if (CurrentBranchNodeParentsPath == ChildBranchNodeChildrenPath)
                                {
                                    ChildBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);
                                    CurrentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);

                                    MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                                    break;
                                }
                            }
                        }

                        if (ChildBranchNode.ParentNodes.Count == 0)
                        {
                            ChildBranchNode.dialogueNode.IsSubRootNode = true;
                            Roots.Add(ChildBranchNode);

                            ChildBranchNode.dialogueNode.OpacityAnimation(0, ChildBranchNode.dialogueNode.ParentNodeCollisionBorder);
                            ChildBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);
                            ChildBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                        }
                    }

                    CurrentBranchNode.SubBranchNodes.Clear();

                    CurrentBranchNode.dialogueNode.OpacityAnimation(0, CurrentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
                    CurrentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                }
            }
        }

        private BranchNode GetParentBranchNode()
        {
            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                BranchNode CurrentBranchNode = BranchNodeStack.Pop();

                if (CurrentBranchNode.dialogueNode == CurrentDialogueNode)
                    return CurrentBranchNode;

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }

            return null;
        }

        private void SetLeftCollision(Border border)
        {
            LeftCollisionBorder = border;
            LoadedCanvasBorders++;
        }

        private void SetRightCollision(Border border)
        {
            RightCollisionBorder = border;
            LoadedCanvasBorders++;
        }

        private void SetBottomCollision(Border border)
        {
            BottomCollisionBorder = border;
            LoadedCanvasBorders++;
        }

        private void MKBUserControlViewModel_CanvasBordersLoaded()
        {
            DialogueNode dialogueNode = new DialogueNode(BNType.Dialogue, new NodePosition { X = MainCanvas.ActualWidth / 2, Y = 100 }, this, true);
            BranchNode branchNode = new BranchNode { dialogueNode =  dialogueNode };
            Roots.Add(branchNode);

            MainCanvas.Children.Add(dialogueNode);

            SetDialogueNodeEvents(dialogueNode);

            Point point = new Point();
            point.X = MainCanvas.ActualWidth / 2;
            point.Y = 100;
            CheckCanvasBordersCollision(dialogueNode, point);
        }

        private void DialogueNode_NodeRequestedOrderUpdate(object? sender, EventArgs e)
        {
            if (SelectedBranchNode != null && SelectedBranchNode.ParentNodes.Count > 0)
                UpdateChoiceNodesOrder(SelectedBranchNode.ParentNodes[0]);
        }

        private void DialogueNode_NodeSelectionChanged(object? sender, EventArgs e)
        {
            SelectedBranchNode = GetChildBranchNode(SelectedDialogueNode);
        }

        private void DialogueNode_NotifyPathConnectionColorUpdate(object? sender, bool e)
        {
            DialogueNode? dialogueNode = sender as DialogueNode;
            
            if (IsConnecting == true)
            {
                if (e == true)
                {
                    AnimatePathColor(dialogueNode!);
                }
                else
                {
                    AnimatePathColor(CurrentDialogueNode);
                }
            }
        }

        private void AnimatePathColor(DialogueNode dialogueNode)
        {
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.To = (dialogueNode.NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor;
            colorAnimation.Duration = TimeSpan.FromSeconds(0.3);
            gradient.GradientStops[1].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
        }

        private void UpdateChoiceNodesOrder(BranchNode ParentBranchNode)
        {
            for (int i = 0; i < ParentBranchNode.SubBranchNodes.Count; ++i)
            {
                int MinBranchNodeIndex = i;

                for (int j = i + 1; j < ParentBranchNode.SubBranchNodes.Count; ++j)
                {
                    if (ParentBranchNode.SubBranchNodes[MinBranchNodeIndex].dialogueNode.Position.X > ParentBranchNode.SubBranchNodes[j].dialogueNode.Position.X)
                    {
                        MinBranchNodeIndex = j;
                    }
                }

                if (MinBranchNodeIndex != i)
                {
                    BranchNode temp = ParentBranchNode.SubBranchNodes[i];
                    ParentBranchNode.SubBranchNodes[i] = ParentBranchNode.SubBranchNodes[MinBranchNodeIndex];
                    ParentBranchNode.SubBranchNodes[MinBranchNodeIndex] = temp;
                }
            }

            for (int i = 0; i < ParentBranchNode.SubBranchNodes.Count; ++i)
            {
                ParentBranchNode.SubBranchNodes[i].dialogueNode.UpdateNodeOrder(i + 1);
            }
        }

        private void DeleteBranchNode()
        {
            if (SelectedDialogueNode == null || SelectedDialogueNode.IsRootNode == true)
                return;

            Stack<BranchNode> BranchNodeStack = new Stack<BranchNode>();
            BranchNode? CurrentBranchNode = null;

            foreach (BranchNode branchNode in Roots)
                BranchNodeStack.Push(branchNode);

            while (BranchNodeStack.Count > 0)
            {
                CurrentBranchNode = BranchNodeStack.Pop();

                if (CurrentBranchNode.dialogueNode == SelectedDialogueNode)
                    break;

                foreach (BranchNode branchNode in CurrentBranchNode.SubBranchNodes)
                    BranchNodeStack.Push(branchNode);
            }

            foreach (BranchNode ParentBranchNode in CurrentBranchNode!.ParentNodes)
            {
                ParentBranchNode.SubBranchNodes.Remove(CurrentBranchNode);

                foreach (Path ParentBranchNodeChildrenPath in ParentBranchNode.dialogueNode.ChildrenPath.ToList())
                {
                    foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ParentsPath)
                    {
                        if (CurrentBranchNodeParentsPath == ParentBranchNodeChildrenPath)
                        {
                            ParentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);
                            CurrentBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);

                            MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                            break;
                        }
                    }
                }

                if (ParentBranchNode.SubBranchNodes.Count == 0)
                {
                    ParentBranchNode.dialogueNode.OpacityAnimation(0, ParentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
                    ParentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                }
            }

            foreach (BranchNode ChildBranchNode in CurrentBranchNode!.SubBranchNodes)
            {
                ChildBranchNode.ParentNodes.Remove(CurrentBranchNode);

                foreach (Path ChildBranchNodeChildrenPath in ChildBranchNode.dialogueNode.ParentsPath.ToList())
                {
                    foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ChildrenPath)
                    {
                        if (CurrentBranchNodeParentsPath == ChildBranchNodeChildrenPath)
                        {
                            ChildBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);
                            CurrentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);

                            MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                            break;
                        }
                    }
                }

                if (ChildBranchNode.ParentNodes.Count == 0)
                {
                    ChildBranchNode.dialogueNode.IsSubRootNode = true;
                    Roots.Add(ChildBranchNode);

                    ChildBranchNode.dialogueNode.OpacityAnimation(0, ChildBranchNode.dialogueNode.ParentNodeCollisionBorder);
                    ChildBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);
                    ChildBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                }
            }

            if (SelectedDialogueNode.NodeType == BNType.Choice)
            {
                if (CurrentBranchNode != null && CurrentBranchNode.ParentNodes.Count > 0)
                    UpdateChoiceNodesOrder(CurrentBranchNode.ParentNodes[0]);
            }

            Roots.Remove(CurrentBranchNode);
            SelectedDialogueNode = null;

            MainCanvas.Children.Remove(CurrentBranchNode.dialogueNode);
            CleanDialogueNode(CurrentBranchNode);
        }

        // Combine Single and Multi-Select Deletion the algo is similar!!!
        private void DeleteMultiSelectedNodes()
        {
            if (MultiSelectedBranchNodes.Count == 0)
                return;

            foreach (BranchNode CurrentBranchNode in MultiSelectedBranchNodes)
            {
                CurrentBranchNode.dialogueNode.UpdateNodeMultiSelectionStatus(false);

                if (CurrentBranchNode.dialogueNode.IsRootNode == true)
                    continue;

                foreach (BranchNode ParentBranchNode in CurrentBranchNode!.ParentNodes)
                {
                    ParentBranchNode.SubBranchNodes.Remove(CurrentBranchNode);

                    foreach (Path ParentBranchNodeChildrenPath in ParentBranchNode.dialogueNode.ChildrenPath.ToList())
                    {
                        foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ParentsPath)
                        {
                            if (CurrentBranchNodeParentsPath == ParentBranchNodeChildrenPath)
                            {
                                ParentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);
                                CurrentBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);

                                MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                                break;
                            }
                        }
                    }

                    if (ParentBranchNode.SubBranchNodes.Count == 0)
                    {
                        ParentBranchNode.dialogueNode.OpacityAnimation(0, ParentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
                        ParentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                    }
                }

                foreach (BranchNode ChildBranchNode in CurrentBranchNode!.SubBranchNodes)
                {
                    ChildBranchNode.ParentNodes.Remove(CurrentBranchNode);

                    foreach (Path ChildBranchNodeChildrenPath in ChildBranchNode.dialogueNode.ParentsPath.ToList())
                    {
                        foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ChildrenPath)
                        {
                            if (CurrentBranchNodeParentsPath == ChildBranchNodeChildrenPath)
                            {
                                ChildBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);
                                CurrentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);

                                MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                                break;
                            }
                        }
                    }

                    if (ChildBranchNode.ParentNodes.Count == 0)
                    {
                        ChildBranchNode.dialogueNode.IsSubRootNode = true;
                        Roots.Add(ChildBranchNode);

                        ChildBranchNode.dialogueNode.OpacityAnimation(0, ChildBranchNode.dialogueNode.ParentNodeCollisionBorder);
                        ChildBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);
                        ChildBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                    }
                }

                if (CurrentBranchNode.dialogueNode.NodeType == BNType.Choice)
                {
                    if (CurrentBranchNode != null && CurrentBranchNode.ParentNodes.Count > 0)
                        UpdateChoiceNodesOrder(CurrentBranchNode.ParentNodes[0]);
                }

                Roots.Remove(CurrentBranchNode);
                MainCanvas.Children.Remove(CurrentBranchNode.dialogueNode);
                CleanDialogueNode(CurrentBranchNode);
            }

            MultiSelectedBranchNodes.Clear();
        }

        private void CleanDialogueNode(BranchNode TargetBranchNode)
        {
            TargetBranchNode.dialogueNode.NodePositionChanged -= DialogueNode_NodePositionChanged;
            TargetBranchNode.dialogueNode.NodeStartedConnectionProcess -= DialogueNode_NodeStartedConnectionProcess;
            TargetBranchNode.dialogueNode.ChildNodeRequestedConnection -= DialogueNode_ChildNodeRequestedConnection;
            TargetBranchNode.dialogueNode.NodeRequestedConnectionRemoval -= DialogueNode_NodeRequestedConnectionRemoval;
            TargetBranchNode.dialogueNode.NotifyPathConnectionColorUpdate -= DialogueNode_NotifyPathConnectionColorUpdate;
            TargetBranchNode.dialogueNode.NodeSelectionChanged -= DialogueNode_NodeSelectionChanged;
            TargetBranchNode.dialogueNode.NodeRequestedOrderUpdate -= DialogueNode_NodeRequestedOrderUpdate;
            TargetBranchNode.dialogueNode.NodeRequestedSelectedNodesToMove -= DialogueNode_NodeRequestedSelectedNodesToMove;
            TargetBranchNode.dialogueNode.NodeRequestedSelectedNodesToStop -= DialogueNode_NodeRequestedSelectedNodesToStop;
            TargetBranchNode.dialogueNode.NodeRequestedSelectedNodesRemoval -= DialogueNode_NodeRequestedSelectedNodesRemoval;

            TargetBranchNode.dialogueNode = null;
            TargetBranchNode = null;
        }

        private void RemoveRootConnections()
        {
            BranchNode? CurrentBranchNode = null;

            foreach (BranchNode branchNode in Roots)
            {
                if (branchNode.dialogueNode.IsRootNode == true)
                {
                    CurrentBranchNode = branchNode;
                    break;
                }
            }

            foreach (BranchNode ChildBranchNode in CurrentBranchNode!.SubBranchNodes)
            {
                ChildBranchNode.ParentNodes.Remove(CurrentBranchNode);

                foreach (Path ChildBranchNodeChildrenPath in ChildBranchNode.dialogueNode.ParentsPath.ToList())
                {
                    foreach (Path CurrentBranchNodeParentsPath in CurrentBranchNode.dialogueNode.ChildrenPath)
                    {
                        if (CurrentBranchNodeParentsPath == ChildBranchNodeChildrenPath)
                        {
                            ChildBranchNode.dialogueNode.ParentsPath.Remove(CurrentBranchNodeParentsPath);
                            CurrentBranchNode.dialogueNode.ChildrenPath.Remove(CurrentBranchNodeParentsPath);

                            MainCanvas.Children.Remove(CurrentBranchNodeParentsPath);
                            break;
                        }
                    }
                }

                if (ChildBranchNode.ParentNodes.Count == 0)
                {
                    ChildBranchNode.dialogueNode.IsSubRootNode = true;
                    Roots.Add(ChildBranchNode);

                    ChildBranchNode.dialogueNode.OpacityAnimation(0, ChildBranchNode.dialogueNode.ParentNodeCollisionBorder);
                    ChildBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);
                    ChildBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                }
            }

            CurrentBranchNode.SubBranchNodes.Clear();
            CurrentBranchNode.dialogueNode.OpacityAnimation(0, CurrentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
            CurrentBranchNode.dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
        }

        public void UpdateCurrentActiveWorkspaceID()
        {
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);
        }

        private void SetDialogueNodeEvents(DialogueNode dialogueNode)
        {
            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
            dialogueNode.NodeStartedConnectionProcess += DialogueNode_NodeStartedConnectionProcess;
            dialogueNode.ChildNodeRequestedConnection += DialogueNode_ChildNodeRequestedConnection;
            dialogueNode.NodeRequestedConnectionRemoval += DialogueNode_NodeRequestedConnectionRemoval;
            dialogueNode.NotifyPathConnectionColorUpdate += DialogueNode_NotifyPathConnectionColorUpdate;
            dialogueNode.NodeSelectionChanged += DialogueNode_NodeSelectionChanged;
            dialogueNode.NodeRequestedOrderUpdate += DialogueNode_NodeRequestedOrderUpdate;
            dialogueNode.NodeRequestedSelectedNodesToMove += DialogueNode_NodeRequestedSelectedNodesToMove;
            dialogueNode.NodeRequestedSelectedNodesToStop += DialogueNode_NodeRequestedSelectedNodesToStop;
            dialogueNode.NodeRequestedSelectedNodesRemoval += DialogueNode_NodeRequestedSelectedNodesRemoval;
        }

        private void SetDefaultValues()
        {
            CollisionBorderWidth = 600 * Scale;
            CollisionBorderHeight = 300 * Scale;
            CollisionBorderPadding = new Thickness(40 * Scale);
            RootCollisionBorderPadding = new Thickness(260 * Scale, 0, 260 * Scale, 0);

            ContentStackPanelMargin = new Thickness(10 * Scale);

            RootContainerBorderHeight = 80 * Scale;
            ContainerBorderCornerRadius = new CornerRadius(10 * Scale);
            ContainerBorderThickness = new Thickness(5 * Scale);
            ContainerBorderBrushStrokeDashArray = new DoubleCollection { 4 * Scale, 2 * Scale };
            ContainerBorderBrushStrokeThickness = 5 * Scale;
            ContainerBorderBrushRadiusX = 10 * Scale;
            ContainerBorderBrushRadiusY = 10 * Scale;

            NodeCollisionBorderWidth = 30 * Scale;
            NodeCollisionBorderHeight = 30 * Scale;
            NodeCollisionBorderCornerRadius = new CornerRadius(30 * Scale);
            NodeCollisionBorderParentMargin = new Thickness(0, 0, 0, 10 * Scale);
            NodeCollisionBorderChildMargin = new Thickness(0, 10 * Scale, 0, 0);
            NodeCollisionBorderThickness = new Thickness(2 * Scale);
            NodeCollisionBorderStrokeDashArray = new DoubleCollection { 2 * Scale, 2 * Scale };
            NodeCollisionBorderStrokeThickness = 2 * Scale;
            NodeCollisionBorderRadiusX = 30 * Scale;
            NodeCollisionBorderRadiusY = 30 * Scale;

            NodeTitleIconWidth = 40 * Scale;
            NodeTitleIconHeight = 40 * Scale;
            NodeTitleDialogueTextBlockWidth = 430 * Scale;
            NodeTitleFontSize = 18 * Scale;
            NodeTitleMargin = new Thickness(10 * Scale, 0, 0, 0);

            RTBFontSize = 18 * Scale;
            NodeChoiceMargin = new Thickness(0, 0, 0, 0);
            NodeChoiceRTBWidth = 450 * Scale;

            NodeCharacterMargin = new Thickness(0, 10 * Scale, 0, 0);
            NodeCharacterIconWidth = 20 * Scale;
            NodeCharacterIconHeight = 20 * Scale;
            NodeRTBWidth = 450 * Scale;
            NodeMarkFontSize = 15 * Scale;
            NodeMarkMargin = new Thickness(10 * Scale, 0, 0, 0);

            NodeDialogueMargin = new Thickness(0, 10 * Scale, 0, 0);
            NodeDialogueIconWidth = 20 * Scale;
            NodeDialogueIconHeight = 20 * Scale;
            NodeDialogueRTBWidth = 450 * Scale;

            NodeOrderTextBlockFontSize = 20 * Scale;
            NodeOrderTextBlockMargin = new Thickness(0, 0, 10 * Scale, 0);

            RootContainerImageWidth = 40 * Scale;
            RootContainerImageHeight = 40 * Scale;
        }

        private void UpdateElementsSize()
        {
            switch (SelectedResizeIndex)
            {
                case 0:
                    SelectedResizeText = "100%";
                    Scale = 1.0;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
                case 1:
                    SelectedResizeText = "90%";
                    Scale = 0.9;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
                case 2:
                    SelectedResizeText = "80%";
                    Scale = 0.8;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
                case 3:
                    SelectedResizeText = "70%";
                    Scale = 0.7;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
                case 4:
                    SelectedResizeText = "60%";
                    Scale = 0.6;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
                case 5:
                    SelectedResizeText = "50%";
                    Scale = 0.5;
                    SetDefaultValues();
                    UpdatePathsThickness();
                    _ = UpdatePathsLocation();
                    break;
            }
        }

        private void UpdateResizeBoxData()
        {
            SelectedResizeText = ((int)(Scale * 100)).ToString() + "%";

            switch (Math.Round(Scale, 1))
            {
                case 1:
                    SelectedResizeIndex = 0;
                    break;
                case 0.9:
                    SelectedResizeIndex = 1;
                    break;
                case 0.8:
                    SelectedResizeIndex = 2;
                    break;
                case 0.7:
                    SelectedResizeIndex = 3;
                    break;
                case 0.6:
                    SelectedResizeIndex = 4;
                    break;
                case 0.5:
                    SelectedResizeIndex = 5;
                    break;
            }
        }

        private void HandleConnectMKCButtonClicked()
        {
            if (IsConnectedToMKCFile == false)
                MkcConnectionProcessStarted = true;
        }

        private void HandleResizeControlLeftMouseButtonDown(MouseButtonEventArgs e)
        {
            CanResizeConnectionWindow = true;
            InitialResizeConnectionMousePosition = Mouse.GetPosition((IInputElement)e.Source);
            Mouse.Capture((IInputElement)e.Source);
        }

        private void HandleResizeControlLeftMouseButtonUp(MouseButtonEventArgs e)
        {
            if (_workspaceServices.CurrentActiveWorkspaceID != _workspaceServicesID)
                return;

            CanResizeConnectionWindow = false;
            Mouse.Capture(null);
        }

        private void HandleResizeControlMouseMove(MouseEventArgs e)
        {
            if (CanResizeConnectionWindow == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition((IInputElement)e.Source);
                double DeltaY = InitialResizeConnectionMousePosition.Y - CurrentMousePosition.Y;
                double NewHeight = ResizeConnectionWindowHeight + DeltaY;
                
                if (NewHeight < MainWindow.Height - 300 && NewHeight > 200)
                    ResizeConnectionWindowHeight = NewHeight;
            }
        }

        private void UpdateResizeControlStatus()
        {
            IsMouseInsideResizeControl = !IsMouseInsideResizeControl;
        }

        private void StopConnectionProcess()
        {
            MkcConnectionProcessStarted = false;
        }

        private void LoadMKCFiles()
        {
            CurrentProject = _workspaceServices.UserDataServices.ActiveProject;
            foreach(var file in CurrentProject.Files)
            {
                if (file.FileType.Equals("mkc"))
                    MKCFiles.Add(file);
            }

            AssociatedFile = _workspaceServices.UserDataServices.CurrentSelectedFile;
            if (AssociatedFile == null)
                return;

            if (AssociatedFile.ConnectedMKCFile != null)
            {
                IsConnectedToMKCFile = true;
                IsConnectedToMKCButtonActive = true;
                IsConnectToMKCButtonActive = false;

                SelectedMKCFile = AssociatedFile.ConnectedMKCFile;
                ConnectedMKCFileName = AssociatedFile.ConnectedMKCFile.FileName;
            }
            else
            {
                IsConnectedToMKCFile = false;
                IsConnectedToMKCButtonActive = false;
                IsConnectToMKCButtonActive = true;

                ConnectedMKCFileName = string.Empty;
            }
        }

        private void UpdateSearchList()
        {
            List<FileSystem> files = new List<FileSystem>(MKCFiles);
            files = files.OrderBy(file => IsMatch(file, SearchText) ? 0 : 1).ThenBy(file => SearchCalculations(file)).ToList();

            MKCFiles.Clear();
            foreach (FileSystem file in files)
            {
                MKCFiles.Add(file);
            }
        }

        private bool IsMatch(FileSystem file, string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;

            return file.FileName.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private int SearchCalculations(FileSystem file)
        {
            int SearchLength = SearchText.Length;
            int NodesLength = file.FileName.Length;
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
                    int cost = (string.Compare(SearchText[i - 1].ToString(), file.FileName[j - 1].ToString(), StringComparison.OrdinalIgnoreCase) == 0) ? 0 : 1;
                    Matrix[i, j] = Math.Min(Math.Min(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1), Matrix[i - 1, j - 1] + cost);
                }
            }

            return Matrix[SearchLength, NodesLength];
        }

        private void HandleBNToMKCConnection(MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Border border && border.Name.Equals("BorderItem"))
            {
                Debug.WriteLine($"Item Name:{SelectedMKCFile}");

                MkcConnectionProcessStarted = false;
                AssociatedFile.ConnectedMKCFile = SelectedMKCFile;
                IsConnectedToMKCFile = true;

                IsConnectedToMKCButtonActive = true;
                IsConnectToMKCButtonActive = false;
                ConnectedMKCFileName = AssociatedFile.ConnectedMKCFile.FileName;

                RemoveRootConnections();
                _workspaceServices.UserDataServices.SaveUserDataAsync();
            }
        }

        private void WorkspaceServices_MKCSearchInitAnimFinished(object? sender, double Height)
        {
            if (_workspaceServices.CurrentActiveWorkspaceID != _workspaceServicesID)
                return;

            ResizeConnectionWindowHeight = Height;
        }

        private void UpdateMKCButtonState(Button button)
        {
            double ConnectToLength = 185;
            double TotalWidth = GetTextBlockWidth(ConnectedMKCFileName, "Segoe UI", 20) + ConnectToLength;
            PlayAnimation(TotalWidth, button);
        }

        private void ResetMKCButtonState(Button button)
        {
            PlayAnimation(50, button);
        }

        private void PlayAnimation(double target, DependencyObject dependencyObject)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation ControlWidthAnimation = new DoubleAnimation();
            ControlWidthAnimation.BeginTime = TimeSpan.FromSeconds(0);
            ControlWidthAnimation.To = target;
            ControlWidthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(ControlWidthAnimation, dependencyObject);
            Storyboard.SetTargetProperty(ControlWidthAnimation, new PropertyPath("Width"));
            storyboard.Children.Add(ControlWidthAnimation);

            storyboard.Begin();
        }

        private double GetTextBlockWidth(string BoundText, string FontFamily, double FontSize)
        {
            FormattedText formattedText = new FormattedText(BoundText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface(new FontFamily(FontFamily), FontStyles.Normal, FontWeights.DemiBold, FontStretches.Normal),
                FontSize, Brushes.Black, new NumberSubstitution(), 1);

            return formattedText.Width;
        }

        private void HandleConnectedMKCButton()
        {
            if (CanRemoveConnectedMKCFile == true)
            {
                AssociatedFile.ConnectedMKCFile = null;

                IsConnectedToMKCFile = false;
                IsConnectedToMKCButtonActive = false;
                IsConnectToMKCButtonActive = true;

                ResetMKCConnectionButtons = true;
                ResetMKCConnectionButtons = false;

                ConnectedMKCFileName = string.Empty;
                _workspaceServices.UserDataServices.SaveUserDataAsync();
            }
            else
            {
                _workspaceServices.UserDataServices.AddFileToHeader(SelectedMKCFile);
            }
        }

        private void UpdateDetailsOnUserControlLoaded()
        {
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);

            if (IsConnectedToMKCFile == true)
            {
                ShowConnectMKCButton = false;
                ShowConnectedMKCButton = true;
            }
            else
            {
                ShowConnectMKCButton = true;
                ShowConnectedMKCButton = false;
            }
        }
    }
}
