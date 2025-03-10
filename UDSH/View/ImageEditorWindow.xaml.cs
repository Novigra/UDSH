// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UDSH.View
{
    /*
     * Important Notes:
     *  
     *  1- The modification stops when both corners collisions record the border.
     *  2- Speed is not bad, but it can be better. discover a better approach.
     *     was thinking of using multi-threading, but it can be hard to implement,
     *     as tasks can pile up and a 'race' may happen resulting in wrong data or
     *     crashings.
     */
    enum Movement
    {
        LR,
        UB,
        M
    }
    public partial class ImageEditorWindow : Window
    {
        private double InitialWidth;
        private double InitialHeight;
        private double XOffset = 0;
        private double YOffset = 0;
        private double ChangeRate = 0.485;
        private int DeltaRateChange;
        private Movement CurrentMovement;
        private Point InitialMousePosition;
        private double InitialMargin;
        private bool IsMousePressed = false;

        private Thickness LastMargins;

        private string ImagePath;
        private string tempFilePath;

        private List<string> RealTimeTempFiles = new List<string>();
        public ImageEditorWindow(string ImagePath)
        {
            InitializeComponent();

            string AppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            string TempPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Temp");

            if(!Directory.Exists(TempPath))
                Directory.CreateDirectory(TempPath);

            /*tempFilePath = System.IO.Path.GetTempFileName();
            tempFilePath = System.IO.Path.ChangeExtension(tempFilePath, ".png");*/

            string TempFilePath = System.IO.Path.Combine(TempPath, Guid.NewGuid().ToString() + ".png");
            try
            {
                File.Copy(ImagePath, TempFilePath, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

            CurrentImage.Source = new BitmapImage(new Uri(TempFilePath));
            OutputImage.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(TempFilePath)), Stretch = Stretch.UniformToFill };

            this.ImagePath = ImagePath;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Debug.WriteLine($"CurrentMargin = {ImageBorder.Margin.Left}");
            IsMousePressed = true;
            InitialMousePosition = e.GetPosition(this);
            if (sender is Rectangle currentTarget)
            {
                switch (currentTarget.Name)
                {
                    case "RM":
                        CurrentMovement = Movement.LR;
                        DeltaRateChange = 2;
                        RM.CaptureMouse();
                        break;
                    case "LM":
                        CurrentMovement = Movement.LR;
                        DeltaRateChange = -2;
                        LM.CaptureMouse();
                        break;
                    case "UM":
                        CurrentMovement = Movement.UB;
                        DeltaRateChange = -2;
                        UM.CaptureMouse();
                        break;
                    case "BM":
                        CurrentMovement = Movement.UB;
                        DeltaRateChange = 2;
                        BM.CaptureMouse();
                        break;
                    case "MM":
                        CurrentMovement = Movement.M;
                        MM.CaptureMouse();
                        break;
                    default:
                        break;
                }
            }
            //InitialMargin = ImageBorder.Margin.Right;
            //LM.CaptureMouse();
        }

        private void Rectangle_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsMousePressed = false;
            if (sender is Rectangle currentTarget)
            {
                switch (currentTarget.Name)
                {
                    case "RM":
                        RM.ReleaseMouseCapture();
                        break;
                    case "LM":
                        LM.ReleaseMouseCapture();
                        break;
                    case "UM":
                        UM.ReleaseMouseCapture();
                        break;
                    case "BM":
                        BM.ReleaseMouseCapture();
                        break;
                    case "MM":
                        MM.ReleaseMouseCapture();
                        break;
                    default:
                        break;
                }
            }
        }

        private void RM_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(IsMousePressed == true)
            {
                Point CurrentMousePosition = e.GetPosition(this);

                if(CurrentMovement == Movement.LR)
                {
                    double Delta = CurrentMousePosition.X - InitialMousePosition.X;
                    Debug.WriteLine($"Change: {Delta}");
                    double Update = ImageBorder.Width + (Delta * DeltaRateChange);
                    ImageBorder.Margin = new Thickness(0);

                    if (Update > CurrentImage.ActualWidth)
                        ImageBorder.Width = CurrentImage.ActualWidth;
                    else if (Update > 60)
                        ImageBorder.Width = Update;
                    else
                        ImageBorder.Width = 61;

                        XOffset = ChangeRate * (ImageBorder.ActualWidth - InitialWidth);
                }
                else if(CurrentMovement == Movement.UB)
                {
                    double Delta = CurrentMousePosition.Y - InitialMousePosition.Y;
                    Debug.WriteLine($"Change: {Delta}");
                    double Update = ImageBorder.Height + (Delta * DeltaRateChange);
                    ImageBorder.Margin = new Thickness(0);

                    if (Update > CurrentImage.ActualHeight)
                        ImageBorder.Height = CurrentImage.ActualHeight;
                    else if (Update > 60)
                        ImageBorder.Height = Update;
                    else
                        ImageBorder.Height = 61;

                    YOffset = ChangeRate * (ImageBorder.ActualHeight - InitialHeight);
                }
                else
                {
                    double DeltaX = CurrentMousePosition.X - InitialMousePosition.X;
                    double DeltaY = CurrentMousePosition.Y - InitialMousePosition.Y;

                    double ResultX = XOffset;
                    ResultX -= DeltaX;

                    double ResultY = YOffset;
                    ResultY -= DeltaY;

                    double TopMargin = ImageBorder.Margin.Top + (DeltaY * 2);
                    double MaxTopMargin = ImageBorder.ActualHeight - InitialHeight;

                    double LeftMargin = ImageBorder.Margin.Left + (DeltaX * 2);
                    double MaxLeftMargin = (ImageBorder.ActualWidth - InitialWidth);


                    if (LeftMargin >= Math.Abs(MaxLeftMargin))
                    {
                        ImageBorder.Margin = new Thickness(Math.Abs(MaxLeftMargin), ImageBorder.Margin.Top, ImageBorder.Margin.Right, ImageBorder.Margin.Bottom);
                    }
                    else if (TopMargin >= Math.Abs(MaxTopMargin))
                    {
                        ImageBorder.Margin = new Thickness(ImageBorder.Margin.Left, Math.Abs(MaxTopMargin), ImageBorder.Margin.Right, ImageBorder.Margin.Bottom);
                    }
                    else if (ResultX >= 0)
                    {
                        ImageBorder.Margin = new Thickness(MaxLeftMargin, ImageBorder.Margin.Top, ImageBorder.Margin.Right, ImageBorder.Margin.Bottom);
                        XOffset = 0;
                    }
                    else if (ResultY >= 0)
                    {
                        ImageBorder.Margin = new Thickness(ImageBorder.Margin.Left, MaxTopMargin, ImageBorder.Margin.Right, ImageBorder.Margin.Bottom);
                        YOffset = 0;
                    }
                    else if (ResultX <= 0 && ResultY <= 0 && LeftMargin < Math.Abs(MaxLeftMargin) && TopMargin < Math.Abs(MaxLeftMargin))
                    {
                        ImageBorder.Margin = new Thickness(LeftMargin, TopMargin, ImageBorder.Margin.Right, ImageBorder.Margin.Bottom);
                        LastMargins = ImageBorder.Margin; // Currently useless
                        XOffset = ResultX;
                        YOffset = ResultY;
                    }

                    Debug.WriteLine($"TopMargin = {TopMargin}");
                }

                InitialMousePosition = CurrentMousePosition;
                ProcessImage(false, true);
            }
        }

        private void CurrentImage_Loaded(object sender, RoutedEventArgs e)
        {
            ImageBorder.Width = CurrentImage.ActualWidth;
            ImageBorder.Height = CurrentImage.ActualHeight;

            InitialWidth = ImageBorder.Width;
            InitialHeight = ImageBorder.Height;

            MM.Width = ImageBorder.Width;
            MM.Height = ImageBorder.Height;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessImage(true, false);
            Debug.WriteLine("Save Complete");
            DialogResult = true;

            CurrentImage.Source = null;
            OutputImage.Fill = null;

            ImagePath = string.Empty;
            tempFilePath = string.Empty;

            if (RealTimeTempFiles.Count > 0)
            {
                foreach (var file in RealTimeTempFiles)
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
            }

            Close();
        }

        private void ProcessImage(bool IsLastOutput = false, bool RealTimeUpdate = false)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(CurrentImage) { Stretch = Stretch.UniformToFill };
                Rect Canvas = new Rect();
                Canvas.Width = CurrentImage.ActualWidth;
                Canvas.Height = CurrentImage.ActualHeight;
                Canvas.Location = new Point(XOffset, YOffset); // -333.4 at border width = 448

                //context.DrawRectangle(brush, null, new Rect(0, 0, CurrentImage.ActualWidth, CurrentImage.ActualHeight));
                context.DrawRectangle(brush, null, Canvas);
            }

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)ImageBorder.ActualWidth,
                (int)ImageBorder.ActualHeight,
                96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            if(IsLastOutput == true)
            {
                using (FileStream fileStream = new FileStream(ImagePath, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
            else
            {
                if(RealTimeUpdate == true)
                {
                    string AppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
                    string TempPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Temp");
                    string TempFilePath = System.IO.Path.Combine(TempPath, Guid.NewGuid().ToString() + ".png");

                    using (FileStream fileStream = new FileStream(TempFilePath, FileMode.Create))
                    {
                        encoder.Save(fileStream);
                    }

                    OutputImage.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(TempFilePath))
                        {
                            CacheOption = BitmapCacheOption.OnLoad
                        },
                        Stretch = Stretch.UniformToFill
                    };
                }
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            Close();
        }
    }
}
