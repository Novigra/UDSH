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

        public RelayCommand<Canvas> CanvasRMBDown => new RelayCommand<Canvas>(execute => StartRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasRMBUp => new RelayCommand<Canvas>(execute => StopRecordingMouseMovement());
        public RelayCommand<Canvas> CanvasMouseMove => new RelayCommand<Canvas>(execute => UpdateCanvasTransform());

        public RelayCommand<Border> BorderLMBDown => new RelayCommand<Border>(execute => StartRecordingNodeMovement());
        public RelayCommand<Border> BorderLMBUp => new RelayCommand<Border>(execute => StopRecordingNodeMovement());
        public RelayCommand<Border> BorderMouseMove => new RelayCommand<Border>(execute => UpdateNodeLocation());

        public RelayCommand<Canvas> CanvasLoaded => new RelayCommand<Canvas>(SetCurrentCanvas);
        public RelayCommand<EllipseCanvas> EllipseCanvasLoaded => new RelayCommand<EllipseCanvas>(SetEllipseCanvas);

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
            PrepareCanvas();
        }

        private void SetCurrentCanvas(Canvas canvas)
        {
            MainCanvas = canvas;

            DialogueNode dialogueNode = new DialogueNode(BNType.Dialogue);
            MainCanvas.Children.Add(dialogueNode);
            Canvas.SetLeft(dialogueNode, 200);
            Canvas.SetTop(dialogueNode, 300);

            DialogueNode dialogueNode2 = new DialogueNode(BNType.Dialogue);
            MainCanvas.Children.Add(dialogueNode2);
            Canvas.SetLeft(dialogueNode2, 400);
            Canvas.SetTop(dialogueNode2, 400);
        }

        private void PrepareCanvas()
        {
            if (MainWindow != null)
            {
                MainCanvas.Width = MainWindow.Width;
                MainCanvas.Height = MainWindow.Height;

                MainEllipseCanvas.Width = MainCanvas.Width;
                MainEllipseCanvas.Height = MainCanvas.Height;

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
    }
}
