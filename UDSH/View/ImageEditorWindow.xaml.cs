using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for ImageEditorWindow.xaml
    /// </summary>
    public partial class ImageEditorWindow : Window
    {
        Point InitialMousePosition;
        double InitialMargin;
        bool IsMousePressed = false;
        public ImageEditorWindow(string ImagePath)
        {
            InitializeComponent();

            CurrentImage.Source = new BitmapImage(new Uri(ImagePath));
            OutputImage.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(ImagePath)), Stretch = Stretch.UniformToFill };
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMousePressed = true;
            InitialMousePosition = e.GetPosition(this);
            InitialMargin = ImageBorder.Margin.Right;
            RM.CaptureMouse();
        }

        private void Rectangle_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMousePressed = false;
            RM.ReleaseMouseCapture();
        }

        private void RM_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(IsMousePressed == true)
            {
                Point CurrentMousePosition = e.GetPosition(this);
                double Delta = CurrentMousePosition.X - InitialMousePosition.X;
                Debug.WriteLine($"Change: {Delta}");

                
                if (ImageBorder.Width > 60)
                    ImageBorder.Width += Delta * 2;
                else
                    ImageBorder.Width = 61;
                InitialMousePosition = CurrentMousePosition;
            }
        }

        private void CurrentImage_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBorder.Width = CurrentImage.ActualWidth;
            ImageBorder.Height = CurrentImage.ActualHeight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(CurrentImage) { Stretch = Stretch.UniformToFill };
                context.DrawRectangle(brush, null, new Rect(0, 0, ImageBorder.ActualWidth, ImageBorder.ActualHeight));
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)ImageBorder.ActualWidth,
                (int)ImageBorder.ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (FileStream fileStream = new FileStream("C:\\Users\\Lenovo\\Desktop\\Workflow\\test.png", FileMode.Create))
            {
                encoder.Save(fileStream);
            }

            Debug.WriteLine("Save Complete");
        }
    }
}
