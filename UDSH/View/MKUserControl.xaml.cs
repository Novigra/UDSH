// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using UDSH.ViewModel;
using System.Xml.Linq;
using System.Xml;
using UDSH.Model;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for MKUserControl.xaml
    /// </summary>
    public partial class MKUserControl : UserControl
    {
        private MKUserControlViewModel viewModel;

        public event EventHandler<int> MouseScroll;

        public MKUserControl(MKUserControlViewModel viewModel)
        {
            InitializeComponent();

            this.viewModel = viewModel;
            DataContext = viewModel;

            viewModel.MKCurrentUserControl = this;
        }

        // Pick the previous paragraph
        private void MKContentLayout_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                // Get the caret position
                TextPointer caretPosition = richTextBox.CaretPosition;

                // Find the containing paragraph
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null && paragraph.IsKeyboardFocused == false)
                {
                    // Set focus to the paragraph
                    paragraph.Focus();

                    // Optional: Debug output
                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"Paragraph Focused: {textRange.Text}");
                }
            }
        }

        // Also, pick previous paragraph
        private void MKContentLayout_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                TextPointer caretPosition = richTextBox.CaretPosition;
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null)
                {
                    paragraph.Focus();
                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"Paragraph Focused: {textRange.Text}");
                }
            }
        }

        // Best solution 
        private void MKContentLayout_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                TextPointer caretPosition = richTextBox.CaretPosition;
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null && paragraph.IsKeyboardFocused == false)
                {
                    paragraph.Focus();

                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"Paragraph Focused: {textRange.Text}");
                }
            }
        }

        private async void MKContentLayout_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                bool MarkdownHandled = viewModel.SpaceMarkdown();

                if (MarkdownHandled)
                {
                    e.Handled = true;
                }
                else
                {
                    Debug.WriteLine("Normal Space will be initiated...");
                    e.Handled = false;
                }

                viewModel.ChangeSaveStatus();
            }
            else if (e.Key == Key.Back)
            {
                bool BackResult = await viewModel.SetLastPickedParagraphAfterParagraphUpdate();

                if(BackResult)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }

                viewModel.ChangeSaveStatus();
            }
            else if(e.Key == Key.Up || e.Key == Key.Down)
            {
                viewModel.NavigateListItem(e.Key);
                e.Handled = false;
            }
            else
            {
                viewModel.ChangeSaveStatus();
            }
        }

        private void ScrollViewer_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("huh");
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("aaaaa");
        }

        private void NoteButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine($"Button Name: {e.ButtonState}");
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Start Now");
        }

        private void OnMouseScroll(object sender, MouseWheelEventArgs e)
        {
            MouseScroll?.Invoke(sender, e.Delta);
            //Debug.WriteLine($"Delta = {e.Delta}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*MessageBox.Show("Create a picture");

            Border PrintBorder = new Border();
            *//*PrintBorder.Width = 400;
            PrintBorder.Height = 800;*//*
            PrintBorder.Background = new SolidColorBrush(Colors.Transparent);


            string fontFolder = @"file:///C:/Users/Lenovo/Desktop/PrintFont/";
            FontFamily courierPrime = new FontFamily(new Uri(fontFolder, UriKind.Absolute), "./#Bytesized");


            RichTextBox richTextBox = new RichTextBox();
            richTextBox.Background = new SolidColorBrush(Colors.Transparent);
            richTextBox.BorderThickness = new Thickness(0);
            viewModel.TempTest(viewModel.MKRichTextBox, richTextBox);
            richTextBox.FontFamily = courierPrime;
            richTextBox.Foreground = new SolidColorBrush(Colors.Red);
            richTextBox.Width = viewModel.MKRichTextBox.ActualWidth;
            richTextBox.Height = viewModel.MKRichTextBox.ActualHeight;

            FlowDocument document = richTextBox.Document;
            // Iterate through each block in the document.
            foreach (Block block in document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            // Determine which custom font to use based on the style.
                            if (run.FontWeight == FontWeights.Bold && run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = courierPrime;
                            }
                            else if (run.FontWeight == FontWeights.Bold)
                            {
                                run.FontFamily = courierPrime;
                            }
                            else if (run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = courierPrime;
                            }
                            else
                            {
                                run.FontFamily = courierPrime;
                            }
                        }
                    }
                }
                else if (block is List list)
                {
                    foreach (var item in list.ListItems)
                    {
                        if (item.Blocks.FirstBlock is Paragraph listParagraph)
                        {
                            foreach (var inline in listParagraph.Inlines)
                            {
                                if (inline is Run run)
                                {
                                    if (run.FontWeight == FontWeights.Bold && run.FontStyle == FontStyles.Italic)
                                    {
                                        run.FontFamily = courierPrime;
                                    }
                                    else if (run.FontWeight == FontWeights.Bold)
                                    {
                                        run.FontFamily = courierPrime;
                                    }
                                    else if (run.FontStyle == FontStyles.Italic)
                                    {
                                        run.FontFamily = courierPrime;
                                    }
                                    else
                                    {
                                        run.FontFamily = courierPrime;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            PrintBorder.Child = richTextBox;

            PrintBorder.Measure(new Size(richTextBox.Width, richTextBox.Height));
            PrintBorder.Arrange(new Rect(0, 0, richTextBox.Width, richTextBox.Height));

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(PrintBorder) { Stretch = Stretch.UniformToFill };
                Rect Canvas = new Rect();
                Canvas.Width = PrintBorder.ActualWidth;
                Canvas.Height = PrintBorder.ActualHeight;
                Canvas.Location = new Point(0, 0);

                context.DrawRectangle(brush, null, Canvas);
            }

            double desiredDpi = 600;
            double scale = desiredDpi / 96.0;

            // Calculate new pixel dimensions based on the scale factor.
            int pixelWidth = (int)(PrintBorder.ActualWidth * scale);
            int pixelHeight = (int)(PrintBorder.ActualHeight * scale);

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                pixelWidth,
                pixelHeight,
                desiredDpi, desiredDpi, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(drawingVisual);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            string ImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MyImage.png");

            using (FileStream fileStream = new FileStream(ImagePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }*/
        }

        /*private void RichTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Paragraph para = sender as Paragraph;
            if(para != null)
            {
                TextRange textRange = new TextRange(para.ContentStart, para.ContentEnd);
                MessageBox.Show(textRange.Text);
            }
            // Try mouse over and hover and maybe visual states!!!
        }*/


    }
}
