using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class MKBUserControlViewModel : ViewModelBase
    {
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

        private double CanvasDimensionsUpdate = 400;
        private double CanvasWidthOffset = 200;

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

        public RelayCommand<object> AddDialogueNode => new RelayCommand<object>(execute => CreateDialogueNode());

        public MKBUserControlViewModel(IWorkspaceServices workspaceServices)
        {
            _workspaceServices = workspaceServices;
            MainWindow = workspaceServices.MainWindow;
            MainWindow.SizeChanged += MainWindow_SizeChanged;

            CanvasEllipseStyle = (Style)Application.Current.FindResource("BNCanvasEllipse");
            IsRightMouseButtonPressed = false;
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

            /*DialogueNode dialogueNode = new DialogueNode(BNType.Dialogue);
            MainCanvas.Children.Add(dialogueNode);
            Canvas.SetLeft(dialogueNode, 200);
            Canvas.SetTop(dialogueNode, 300);

            DialogueNode dialogueNode2 = new DialogueNode(BNType.Dialogue);
            MainCanvas.Children.Add(dialogueNode2);
            Canvas.SetLeft(dialogueNode2, 400);
            Canvas.SetTop(dialogueNode2, 400);*/
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

        private void CreateDialogueNode()
        {
            DialogueNode dialogueNode = new DialogueNode(BNType.Dialogue, new NodePosition { X = InitialCanvasMousePosition.X, Y = InitialCanvasMousePosition.Y});
            MainCanvas.Children.Add(dialogueNode);

            dialogueNode.NodePositionChanged += DialogueNode_NodePositionChanged;
        }

        private void DialogueNode_NodePositionChanged(object? sender, DroppedNodeEventArgs e)
        {
            double NodeWidth = e.Node.ActualWidth;
            double NodeHeight = e.Node.ActualHeight;
            Rect NodeRect = new Rect(e.Position.X, e.Position.Y, NodeWidth, NodeHeight);

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

                e.Node.UpdateNodeLocation(e.Position.X + CanvasWidthOffset, e.Position.Y);

                if (MainCanvas.Width <= MainWindow.Width)
                {
                    MainCanvas.Width += CanvasDimensionsUpdate;
                    MainEllipseCanvas.Width += CanvasDimensionsUpdate;
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

        private void SetLeftCollision(Border border)
        {
            LeftCollisionBorder = border;
        }

        private void SetRightCollision(Border border)
        {
            RightCollisionBorder = border;
        }

        private void SetBottomCollision(Border border)
        {
            BottomCollisionBorder = border;
        }
    }
}
