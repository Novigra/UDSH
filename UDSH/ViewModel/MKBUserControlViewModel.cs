using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
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
        //private ObservableCollection<BranchNodeEdge> BranchNodeEdges { get; set; } = new ObservableCollection<BranchNodeEdge>();
        private TranslateTransform CanvasTranslateTransform { get; set; }
        private ScaleTransform CanvasScaleTransform { get; set; }
        private double Scale { get; set; }

        private SolidColorBrush DialogueContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
        private Color DialogueContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#E0911A");
        private SolidColorBrush ChoiceContainerBackgroundSCB { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));
        private Color ChoiceContainerBackgroundColor { get; set; } = (Color)ColorConverter.ConvertFromString("#C41CFF");


        public RelayCommand<Canvas> CanvasRMBDown => new RelayCommand<Canvas>(execute => StartRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasRMBUp => new RelayCommand<Canvas>(execute => StopRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasMouseMove => new RelayCommand<Canvas>(execute => UpdateCanvasTransform());

        public RelayCommand<Border> CanvasLMBDown => new RelayCommand<Border>(execute => HandleCanvasLeftMouseButton());
        /*public RelayCommand<Border> BorderLMBUp => new RelayCommand<Border>(execute => StopRecordingNodeMovement());
        public RelayCommand<Border> BorderMouseMove => new RelayCommand<Border>(execute => UpdateNodeLocation());*/

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
                if (e.KeyEvent.Key == Key.LeftCtrl)
                    CanRemoveConnectedNodesPaths = true;

                if (e.KeyEvent.Key == Key.Add)
                {
                    
                }

                if (e.KeyEvent.Key == Key.Subtract)
                {
                    // TODO: Handle object scaling
                    // I tried zooming in and out by scaling the canvas. IT WAS A NIGHTMARE. The better approach is to scale the objects.
                    // I faced problems with lines start and end positions when updating the node's height.
                    // I DON'T WANT TO LOSE MY SANITY, SO I WILL DO IT LATER.
                    // MOHAMMED PLEASE DON'T FORGET TO HANDLE THIS IT IS NOT NORMAL FOR THE USER TO KEEP SCROLLING THROUGH THESE KINDA BIG NODES!!!

                    /*foreach (var obj in MainCanvas.Children)
                    {
                        if (obj is DialogueNode node)
                        {
                            node.UpdateNodeScale(0.5);
                        }
                    }*/
                }

                if (e.KeyEvent.Key == Key.Delete)
                {
                    DeleteBranchNode();
                }
            }
        }

        private void WorkspaceServices_ControlButtonReleased(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                if (e.KeyEvent.Key == Key.LeftCtrl)
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

        private void HandleCanvasLeftMouseButton()
        {
            if (IsConnecting == true && CanRemoveConnectedNodesPaths == true)
            {
                IsConnecting = false;
                MainCanvas.Children.Remove(CurrentPath);

                foreach (var obj in MainCanvas.Children)
                {
                    if (obj is DialogueNode node)
                    {
                        if (node.ParentsPath.Count == 0)
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
                Scale = CanvasScaleTransform.ScaleX; // ScaleX == ScaleY
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

            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
            dialogueNode.NodeStartedConnectionProcess += DialogueNode_NodeStartedConnectionProcess;
            dialogueNode.ChildNodeRequestedConnection += DialogueNode_ChildNodeRequestedConnection;
            dialogueNode.NodeRequestedConnectionRemoval += DialogueNode_NodeRequestedConnectionRemoval;
            dialogueNode.NotifyPathConnectionColorUpdate += DialogueNode_NotifyPathConnectionColorUpdate;
            dialogueNode.NodeSelectionChanged += DialogueNode_NodeSelectionChanged;
            dialogueNode.NodeRequestedOrderUpdate += DialogueNode_NodeRequestedOrderUpdate;

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
                    if (CanConnectNodes(ParentBranchNode, dialogueNode!) == false)
                    {
                        MainCanvas.Children.Remove(CurrentPath);

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

                line.EndPoint = new Point(dialogueNode!.Position.X + (dialogueNode.ActualWidth / 2), dialogueNode.Position.Y + 55);
                gradient.EndPoint = line.EndPoint;
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

                CurrentDialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Child);
                dialogueNode.UpdateNodeConnectionBackgroundColor(ConnectionType.Parent);

                if (dialogueNode.NodeType == BNType.Choice)
                {
                    //dialogueNode.UpdateNodeOrder(ParentBranchNode.SubBranchNodes.Count);
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

            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
            dialogueNode.NodeStartedConnectionProcess += DialogueNode_NodeStartedConnectionProcess;
            dialogueNode.ChildNodeRequestedConnection += DialogueNode_ChildNodeRequestedConnection;
            dialogueNode.NodeRequestedConnectionRemoval += DialogueNode_NodeRequestedConnectionRemoval;
            dialogueNode.NotifyPathConnectionColorUpdate += DialogueNode_NotifyPathConnectionColorUpdate;
            dialogueNode.NodeSelectionChanged += DialogueNode_NodeSelectionChanged;
            dialogueNode.NodeRequestedOrderUpdate += DialogueNode_NodeRequestedOrderUpdate;

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

            SelectedDialogueNode = null;

            MainCanvas.Children.Remove(CurrentBranchNode.dialogueNode);
            CleanDialogueNode(CurrentBranchNode);
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

            TargetBranchNode.dialogueNode = null;
            TargetBranchNode = null;
        }

        public void UpdateCurrentActiveWorkspaceID()
        {
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);
        }
    }
}
