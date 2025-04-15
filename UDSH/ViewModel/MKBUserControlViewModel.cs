using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class BranchNodeEdge
    {
        public DialogueNode ParentNode { get; set; }
        public List<DialogueNode> ChildNode { get; set; } = new List<DialogueNode>();
        public Path path { get; set; }
    }

    public class BranchNode
    {
        public DialogueNode dialogueNode { get; set; }
        public ObservableCollection<BranchNode> ParentNodes { get; set; } = new ObservableCollection<BranchNode>();
        //public Path path { get; set; }
        public ObservableCollection<BranchNode> SubBranchNodes { get; set; } = new ObservableCollection<BranchNode>();
    }

    public class MKBUserControlViewModel : ViewModelBase
    {
        private readonly string _workspaceServicesID;
        private readonly IWorkspaceServices _workspaceServices;
        private Window MainWindow;
        private Canvas MainCanvas;
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
        //private ObservableCollection<BranchNodeEdge> BranchNodeEdges { get; set; } = new ObservableCollection<BranchNodeEdge>();
        private TranslateTransform CanvasTranslateTransform { get; set; }

        private SolidColorBrush DialogueContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
        private Color DialogueContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#E0911A");
        private SolidColorBrush ChoiceContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));
        private Color ChoiceContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#C41CFF");


        public RelayCommand<Canvas> CanvasRMBDown => new RelayCommand<Canvas>(execute => StartRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasRMBUp => new RelayCommand<Canvas>(execute => StopRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasMouseMove => new RelayCommand<Canvas>(execute => UpdateCanvasTransform());

        public RelayCommand<Border> BorderLMBDown => new RelayCommand<Border>(execute => StartRecordingNodeMovement());
        public RelayCommand<Border> BorderLMBUp => new RelayCommand<Border>(execute => StopRecordingNodeMovement());
        public RelayCommand<Border> BorderMouseMove => new RelayCommand<Border>(execute => UpdateNodeLocation());

        public RelayCommand<Canvas> CanvasLoaded => new RelayCommand<Canvas>(SetCurrentCanvas);
        public RelayCommand<EllipseCanvas> EllipseCanvasLoaded => new RelayCommand<EllipseCanvas>(SetEllipseCanvas);

        public RelayCommand<Border> LeftCollisionLoaded => new RelayCommand<Border>(SetLeftCollision);
        public RelayCommand<Border> RightCollisionLoaded => new RelayCommand<Border>(SetRightCollision);
        public RelayCommand<Border> BottomCollisionLoaded => new RelayCommand<Border>(SetBottomCollision);

        public RelayCommand<string> AddDialogueNode => new RelayCommand<string>(CreateDialogueNode);

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
        }

        private void WorkspaceServices_ControlButtonPressed(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                CanRemoveConnectedNodesPaths = true;
            }
        }

        private void WorkspaceServices_ControlButtonReleased(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                CanRemoveConnectedNodesPaths = false;
            }
        }

        private void WorkspaceServices_Reset(object? sender, EventArgs e)
        {
            CanRemoveConnectedNodesPaths = false;
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

        private void UpdateCanvasTransform()
        {
            if (IsRightMouseButtonPressed == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);
                double DeltaX = InitialMousePosition.X - CurrentMousePosition.X;
                double DeltaY = InitialMousePosition.Y - CurrentMousePosition.Y;

                if (MainCanvas.RenderTransform is TranslateTransform translateTransform)
                {
                    double NewTranslateX = translateTransform.X - DeltaX;
                    double BoundaryX = MainWindow.Width - MainCanvas.Width;

                    double NewTranslateY = translateTransform.Y - DeltaY;
                    double BoundaryY = MainWindow.Height - MainCanvas.Height;

                    translateTransform.X = Math.Max(BoundaryX, Math.Min(0, NewTranslateX));
                    translateTransform.Y = Math.Max(BoundaryY, Math.Min(0, NewTranslateY));
                }

                InitialMousePosition = CurrentMousePosition;

                MouseMoved = true;
            }

            PathCurrentMouseLocation = Mouse.GetPosition(null);
            if (IsConnecting == true)
            {
                line.EndPoint = new Point(PathCurrentMouseLocation.X - 0 - CanvasTranslateTransform.X, PathCurrentMouseLocation.Y - 110 - CanvasTranslateTransform.Y);
                gradient.EndPoint = new Point(PathCurrentMouseLocation.X - 0 - CanvasTranslateTransform.X, PathCurrentMouseLocation.Y - 110 - CanvasTranslateTransform.Y);
            }
        }

        private void StartRecordingNodeMovement()
        {
            InitialMousePosition = Mouse.GetPosition(null);
            IsLeftMouseButtonPressed = true;
        }

        private void StopRecordingNodeMovement()
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
        }

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

            if (MainCanvas.RenderTransform is TranslateTransform translateTransform)
                CanvasTranslateTransform = translateTransform;
        }

        private void PrepareCanvas()
        {
            if (MainWindow != null)
            {
                MainCanvas.Width = MainWindow.Width;
                MainCanvas.Height = MainWindow.Height;

                MainEllipseCanvas.Width = MainCanvas.Width;
                MainEllipseCanvas.Height = MainCanvas.Height;

                /*if (MainCanvas.RenderTransform is TranslateTransform translateTransform)
                {
                    translateTransform.X = -MainCanvas.Width / 2;
                }*/

                //MainEllipseCanvas.InvalidateVisual();
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

            DialogueNode dialogueNode = new DialogueNode(NodeType, new NodePosition { X = InitialCanvasMousePosition.X, Y = InitialCanvasMousePosition.Y}, this);
            MainCanvas.Children.Add(dialogueNode);

            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
            dialogueNode.NodeStartedConnectionProcess += DialogueNode_NodeStartedConnectionProcess;
            dialogueNode.ChildNodeRequestedConnection += DialogueNode_ChildNodeRequestedConnection;
            dialogueNode.NodeRequestedConnectionRemoval += DialogueNode_NodeRequestedConnectionRemoval;
            dialogueNode.NotifyPathConnectionColorUpdate += DialogueNode_NotifyPathConnectionColorUpdate;

            CheckCanvasBordersCollision(dialogueNode, InitialMousePosition);
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

                if (MainCanvas.RenderTransform is TranslateTransform translateTransform)
                {
                    translateTransform.X -= CanvasWidthOffset;
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

                if (MainCanvas.RenderTransform is TranslateTransform translateTransform)
                {
                    translateTransform.X -= CanvasWidthOffset;
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
                CurrentPath = new Path
                {
                    Fill = (dialogueNode.NodeType == BNType.Choice) ? ChoiceContainerBackgroundSCB : DialogueContainerBackgroundSCB,
                    StrokeThickness = 5,
                    StrokeDashArray = new DoubleCollection { 2, 2 },
                    IsHitTestVisible = false,
                    StrokeDashCap = PenLineCap.Round,
                    StrokeStartLineCap = PenLineCap.Round,
                    StrokeEndLineCap = PenLineCap.Round
                };

                gradient = new LinearGradientBrush
                {
                    MappingMode = BrushMappingMode.Absolute,
                    StartPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - 55)),
                    EndPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - 55)),
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
                    StartPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - 55)),
                    EndPoint = new Point(dialogueNode.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + (dialogueNode.ActualHeight - 55))
                };
                group.Children.Add(line);
                CurrentPath.Data = group;
                MainCanvas.Children.Add(CurrentPath);

                CurrentDialogueNode = dialogueNode;

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node != dialogueNode)
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

                // Temp
                BranchNode ParentBranchNode = GetParentBranchNode();

                if (ParentBranchNode != null)
                {
                    BranchNode branchNode = new BranchNode { dialogueNode = dialogueNode! };
                    if (dialogueNode!.IsSubRootNode == true)
                        branchNode = RemoveSubRootBranchNode(dialogueNode!);

                    branchNode.ParentNodes.Add(ParentBranchNode);

                    ParentBranchNode.SubBranchNodes.Add(branchNode);
                }
                else
                {
                    CurrentDialogueNode.IsSubRootNode = true;
                    ParentBranchNode = new BranchNode { dialogueNode = CurrentDialogueNode };

                    BranchNode branchNode = new BranchNode { dialogueNode = dialogueNode! };
                    branchNode.ParentNodes.Add(ParentBranchNode);

                    ParentBranchNode.SubBranchNodes.Add(branchNode);
                    Roots.Add(ParentBranchNode);
                }

                CurrentDialogueNode.ChildrenPath.Add(CurrentPath);
                dialogueNode!.ParentsPath.Add(CurrentPath);

                line.EndPoint = new Point(dialogueNode!.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + 55);
                gradient.EndPoint = new Point(dialogueNode!.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + 55);
                CurrentPath.StrokeDashArray = new DoubleCollection();

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node.ParentsPath.Count == 0)
                        {
                            node.OpacityAnimation(0, node.ParentNodeCollisionBorder);
                            node.ParentNodeCollisionBorder.IsHitTestVisible = false;
                        }
                    }
                }
            }
        }

        private BranchNode RemoveSubRootBranchNode(DialogueNode dialogueNode)
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
                    ObservableCollection<BranchNode> ParentBranchNodes = CurrentBranchNode.ParentNodes;
                    foreach (BranchNode ParentBranchNode in ParentBranchNodes.ToList())
                    {
                        foreach (Path ParentChildrenPath in ParentBranchNode.dialogueNode.ChildrenPath.ToList())
                        {
                            foreach (Path TargetChildrenPath in TargetDialogueNode!.ParentsPath)
                            {
                                if (ParentChildrenPath == TargetChildrenPath)
                                {
                                    ParentBranchNode.dialogueNode.ChildrenPath.Remove(ParentChildrenPath);
                                    break;
                                }
                            }
                        }

                        foreach (BranchNode ChildBranchNode in ParentBranchNode.SubBranchNodes.ToList())
                        {
                            if (ChildBranchNode.dialogueNode == TargetDialogueNode)
                            {
                                TargetDialogueNode.IsSubRootNode = true;
                                Roots.Add(ChildBranchNode);

                                ParentBranchNode.SubBranchNodes.Remove(ChildBranchNode);
                                break;
                            }
                        }


                        if (ParentBranchNode.SubBranchNodes.Count == 0)
                        {
                            ParentBranchNode.dialogueNode.OpacityAnimation(0, ParentBranchNode.dialogueNode.ChildrenNodeCollisionBorder);
                        }
                    }
                }

                TargetDialogueNode!.OpacityAnimation(0, TargetDialogueNode.ParentNodeCollisionBorder);
                TargetDialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
            }
            else
            {
                if (CurrentBranchNode != null)
                {
                    foreach (BranchNode SubBranchNode in  CurrentBranchNode.SubBranchNodes.ToList())
                    {
                        foreach (Path TargetChildrenPath in CurrentBranchNode.dialogueNode.ChildrenPath.ToList())
                        {
                            foreach (Path ChildrenParentsPath in SubBranchNode.dialogueNode.ParentsPath)
                            {
                                if (TargetChildrenPath == ChildrenParentsPath)
                                {
                                    SubBranchNode.dialogueNode.ParentsPath.Remove(ChildrenParentsPath);
                                    break;
                                }
                            }
                        }

                        if (SubBranchNode.dialogueNode.ParentsPath.Count == 0)
                        {
                            if(SubBranchNode.dialogueNode.ChildrenPath.Count > 0)
                            {
                                SubBranchNode.dialogueNode.IsSubRootNode = true;
                                Roots.Add(SubBranchNode);
                            }

                            SubBranchNode.dialogueNode.OpacityAnimation(0, SubBranchNode.dialogueNode.ParentNodeCollisionBorder);
                            SubBranchNode.dialogueNode.ParentNodeCollisionBorder.IsHitTestVisible = false;
                        }

                        CurrentBranchNode.SubBranchNodes.Remove(SubBranchNode);
                    }
                }
            }
            

            foreach (Path path in e.Paths)
            {
                MainCanvas.Children.Remove(path);
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

            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
            dialogueNode.NodeStartedConnectionProcess += DialogueNode_NodeStartedConnectionProcess;
            dialogueNode.ChildNodeRequestedConnection += DialogueNode_ChildNodeRequestedConnection;
            dialogueNode.NodeRequestedConnectionRemoval += DialogueNode_NodeRequestedConnectionRemoval;
            dialogueNode.NotifyPathConnectionColorUpdate += DialogueNode_NotifyPathConnectionColorUpdate;

            Point point = new Point();
            point.X = MainCanvas.ActualWidth / 2;
            point.Y = 100;
            CheckCanvasBordersCollision(dialogueNode, point);
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

        public void UpdateCurrentActiveWorkspaceID()
        {
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);
        }
    }
}
