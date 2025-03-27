// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UDSH.Model;
using UDSH.MVVM;
using System.Drawing.Text;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace UDSH.ViewModel
{
    public class TextureCreationViewModel : ViewModelBase
    {
        private FileSystem File {  get; set; }
        private RichTextBox CurrentRTB { get; set; }
        private Window CurrentWindow { get; set; }
        public RichTextBox PrePreviewRTB { get; set; } = new RichTextBox();
        public RichTextBox PreviewRTB { get; set; }

        private string associateFileName;
        public string AssociateFileName
        {
            get => associateFileName;
            set { associateFileName = value; OnPropertyChanged(); }
        }

        private string fontFamily;
        public string FontFamily
        {
            get => fontFamily;
            set { fontFamily = value; OnPropertyChanged(); }
        }

        private bool canOpenForegroundPopup;
        public bool CanOpenForegroundPopup
        {
            get => canOpenForegroundPopup;
            set { canOpenForegroundPopup = value; OnPropertyChanged(); }
        }

        private string foregroundHexColor;
        public string ForegroundHexColor
        {
            get => foregroundHexColor;
            set { foregroundHexColor = value; OnPropertyChanged(); }
        }

        private string foregroundColor;
        public string ForegroundColor
        {
            get => foregroundColor;
            set { foregroundColor = value; OnPropertyChanged(); }
        }

        private string finalForegroundColor;
        public string FinalForegroundColor
        {
            get => finalForegroundColor;
            set { finalForegroundColor = value; OnPropertyChanged(); }
        }

        private string dpiNumber;
        public string DpiNumber
        {
            get => dpiNumber;
            set { dpiNumber = value; OnPropertyChanged(); }
        }

        private string dpiBorderColor;
        public string DpiBorderColor
        {
            get => dpiBorderColor;
            set { dpiBorderColor = value; OnPropertyChanged(); }
        }

        private string resolutionMessage;
        public string ResolutionMessage
        {
            get => resolutionMessage;
            set { resolutionMessage = value; OnPropertyChanged(); }
        }

        private string resolutionMessageFontColor;
        public string ResolutionMessageFontColor
        {
            get => resolutionMessageFontColor;
            set { resolutionMessageFontColor = value; OnPropertyChanged(); }
        }

        private bool canOpenBackgroundPopup;
        public bool CanOpenBackgroundPopup
        {
            get => canOpenBackgroundPopup;
            set { canOpenBackgroundPopup = value; OnPropertyChanged(); }
        }

        private string backgroundHexColor;
        public string BackgroundHexColor
        {
            get => backgroundHexColor;
            set { backgroundHexColor = value; OnPropertyChanged(); }
        }

        private string backgroundColor;
        public string BackgroundColor
        {
            get => backgroundColor;
            set { backgroundColor = value; OnPropertyChanged(); }
        }

        private string finalBackgroundColor;
        public string FinalBackgroundColor
        {
            get => finalBackgroundColor;
            set { finalBackgroundColor = value; OnPropertyChanged(); }
        }

        private string outputNameMark;
        public string OutputNameMark
        {
            get => outputNameMark;
            set { outputNameMark = value; OnPropertyChanged(); }
        }

        private string outputName;
        public string OutputName
        {
            get => outputName;
            set { outputName = value; OnPropertyChanged(); }
        }

        private string outputBorderColor;
        public string OutputBorderColor
        {
            get => outputBorderColor;
            set { outputBorderColor = value; OnPropertyChanged(); }
        }

        private string outputWarningMessage;
        public string OutputWarningMessage
        {
            get => outputWarningMessage;
            set { outputWarningMessage = value; OnPropertyChanged(); }
        }

        private double outputWarningMessageOpacity;
        public double OutputWarningMessageOpacity
        {
            get => outputWarningMessageOpacity;
            set { outputWarningMessageOpacity = value; OnPropertyChanged(); }
        }

        // Checkbox
        private bool regularCheck;
        public bool RegularCheck
        {
            get => regularCheck;
            set { regularCheck = value; OnPropertyChanged(); }
        }

        private bool regularCheckCollision;
        public bool RegularCheckCollision
        {
            get => regularCheckCollision;
            set { regularCheckCollision = value; OnPropertyChanged(); }
        }

        private bool boldCheck;
        public bool BoldCheck
        {
            get => boldCheck;
            set { boldCheck = value; OnPropertyChanged(); }
        }

        private bool boldCheckCollision;
        public bool BoldCheckCollision
        {
            get => boldCheckCollision;
            set { boldCheckCollision = value; OnPropertyChanged(); }
        }

        private bool italicCheck;
        public bool ItalicCheck
        {
            get => italicCheck;
            set { italicCheck = value; OnPropertyChanged(); }
        }

        private bool underlineCheck;
        public bool UnderlineCheck
        {
            get => underlineCheck;
            set { underlineCheck = value; OnPropertyChanged(); }
        }

        private bool strikethroughCheck;
        public bool StrikethroughCheck
        {
            get => strikethroughCheck;
            set { strikethroughCheck = value; OnPropertyChanged(); }
        }

        private bool ComesFromPickFontFamily;
        private bool fontCheck;
        public bool FontCheck
        {
            get => fontCheck;
            set { fontCheck = value; OnPropertyChanged(); }
        }

        private bool ComesFromForegroundConfirmButton;
        private bool foregroundCheck;
        public bool ForegroundCheck
        {
            get => foregroundCheck;
            set { foregroundCheck = value; OnPropertyChanged(); }
        }

        private bool backgroundCheck;
        public bool BackgroundCheck
        {
            get => backgroundCheck;
            set { backgroundCheck = value; OnPropertyChanged(); }
        }

        private double backgroundGridOpacity;
        public double BackgroundGridOpacity
        {
            get => backgroundGridOpacity;
            set { backgroundGridOpacity = value; OnPropertyChanged(); }
        }

        private bool canExport;
        public bool CanExport
        {
            get => canExport;
            set { canExport = value; OnPropertyChanged(); }
        }

        private double exportRedWarningOutput;
        public double ExportRedWarningOutput
        {
            get => exportRedWarningOutput;
            set { exportRedWarningOutput = value; OnPropertyChanged(); }
        }

        private string OutputPath;
        private string FontDirectory;

        private HashSet<char> InvalidCharactersFile = new HashSet<char>(@"/\:*?""<>|");

        public RelayCommand<Button> FontFamilyButton => new RelayCommand<Button>(execute => PickFontFamily());
        public RelayCommand<Button> ForegroundButton => new RelayCommand<Button>(execute => PickForeground());
        public RelayCommand<TextBox> ForegroundHexColorChanged => new RelayCommand<TextBox>(execute => UpdateForeground());
        public RelayCommand<Button> ForegroundConfirmButton => new RelayCommand<Button>(execute => ApplyForeground());
        public RelayCommand<Button> DefaultForegroundButton => new RelayCommand<Button>(execute => ApplyDefaultForeground());
        public RelayCommand<TextBox> ResolutionChanged => new RelayCommand<TextBox>(execute => CheckResolutionState());
        public RelayCommand<Button> BackgroundButton => new RelayCommand<Button>(execute => PickBackground());
        public RelayCommand<TextBox> BackgroundHexColorChanged => new RelayCommand<TextBox>(execute => UpdateBackground());
        public RelayCommand<Button> BackgroundConfirmButton => new RelayCommand<Button>(execute => ApplyBackground());
        public RelayCommand<Button> DefaultBackgroundButton => new RelayCommand<Button>(execute => ApplyDefaultBackground());
        public RelayCommand<TextBox> OutputNameChanged => new RelayCommand<TextBox>(execute => CheckOutputNameStatus());

        public RelayCommand<CheckBox> RegularCheckChanged => new RelayCommand<CheckBox>(execute => ChangeRegularStatus());
        public RelayCommand<CheckBox> BoldCheckChanged => new RelayCommand<CheckBox>(execute => ChangeBoldStatus());
        public RelayCommand<CheckBox> ItalicCheckChanged => new RelayCommand<CheckBox>(execute => ChangeItalicStatus());
        public RelayCommand<CheckBox> UnderlineCheckChanged => new RelayCommand<CheckBox>(execute => ChangeUnderlineStatus());
        public RelayCommand<CheckBox> StrikethroughCheckChanged => new RelayCommand<CheckBox>(execute => ChangeStrikethroughStatus());
        public RelayCommand<CheckBox> FontCheckChanged => new RelayCommand<CheckBox>(execute => ChangeFontFamily());
        public RelayCommand<CheckBox> ForegroundCheckChanged => new RelayCommand<CheckBox>(execute => ChangeFontForeground());
        public RelayCommand<CheckBox> BackgroundCheckChanged => new RelayCommand<CheckBox>(execute => ChangeBackground());

        public RelayCommand<Button> ExportButton => new RelayCommand<Button>(execute => CreateTexture());
        public RelayCommand<Button> ExportDirectoryButton => new RelayCommand<Button>(execute => OpenExportDirectory());

        public RelayCommand<Button> CloseButton => new RelayCommand<Button>(execute => CloseWindow());
        public RelayCommand<RichTextBox> PreviewRTBLoaded => new RelayCommand<RichTextBox>(SetPreviewRTB);

        public TextureCreationViewModel(RichTextBox currentRTB, FileSystem file, Window currentWindow)
        {
            File = file;
            CurrentRTB = currentRTB;
            CurrentWindow = currentWindow;

            AssociateFileName = File.FileName;
            FontFamily = "Pick Font Family";

            OutputNameMark = File.FileName;
            OutputName = string.Empty;
            OutputBorderColor = "#FFFFFF";
            OutputWarningMessage = "You can’t use these characters: /\\:*?\"<>|";
            OutputWarningMessageOpacity = 0.0;
            ExportRedWarningOutput = 0.0;

            CanOpenForegroundPopup = false;
            CanOpenBackgroundPopup = false;

            ComesFromPickFontFamily = false;
            ComesFromForegroundConfirmButton = false;

            RegularCheck = true;
            RegularCheckCollision = false;

            BoldCheck = false;
            BoldCheckCollision = true;

            ItalicCheck = false;

            UnderlineCheck = false;
            StrikethroughCheck = false;

            ForegroundCheck = false;
            BackgroundCheck = false;
            BackgroundGridOpacity = 0.0;

            ForegroundHexColor = "000000";
            ForegroundColor = "#000000";
            FinalForegroundColor = "#000000";

            BackgroundHexColor = "Transparent";
            BackgroundColor = "Transparent";
            FinalBackgroundColor = "Transparent";

            ResolutionMessage = "Recommended: Below 600";
            ResolutionMessageFontColor = "#666666";

            DpiNumber = string.Empty;
            DpiBorderColor = "#FFFFFF";

            CanExport = true;

            OutputPath = DoesDirectoryExists();
        }

        private void PickFontFamily()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please pick a font for your font family...";
            openFileDialog.Filter = "Font Files|*.ttf;*.otf";
            openFileDialog.Multiselect = false;

            bool? success = openFileDialog.ShowDialog();
            if (success == true)
            {
                string directory = openFileDialog.FileName;
                string FontName = String.Empty;

                using (PrivateFontCollection PFC = new PrivateFontCollection())
                {
                    PFC.AddFontFile(directory);

                    if (PFC.Families.Length > 0)
                    {
                        FontName = PFC.Families[0].Name;

                        FontCheck = false;
                        UpdateRTBFontFamily(PreviewRTB, directory, FontName);

                        ComesFromPickFontFamily = true;
                        FontCheck = true;

                        FontFamily = FontName;
                        FontDirectory = directory;
                    }
                }
            }
        }

        private void PickForeground()
        {
            CanOpenForegroundPopup = !CanOpenForegroundPopup;
        }

        private void UpdateForeground()
        {
            ForegroundColor = "#" + ForegroundHexColor;
        }

        private void ApplyForeground()
        {
            FinalForegroundColor = ForegroundColor;

            ForegroundCheck = true;
            ChangeFontForeground();
        }

        private void ApplyDefaultForeground()
        {
            ForegroundHexColor = "000000";
            ForegroundColor = "#000000";
            FinalForegroundColor = "#000000";
        }

        private void CheckResolutionState()
        {
            if (IsDigitsOnly(DpiNumber) == true)
            {
                DpiBorderColor = "#FFFFFF";

                if (DpiNumber.Length == 0)
                    return;

                int number = Int32.Parse(DpiNumber); // Important note: clamp if exceeded 2000 when exporting
                if (number > 2000)
                {
                    ResolutionMessage = "Warning: App may crash, please reduce the number";
                    ResolutionMessageFontColor = "#F83030";
                }
                else if (number > 600)
                {
                    ResolutionMessage = "Warning: It will take longer export the file";
                    ResolutionMessageFontColor = "#F83030";
                }
                else
                {
                    ResolutionMessage = "Recommended: Below 600";
                    ResolutionMessageFontColor = "#666666";
                }
            }
            else
            {
                DpiBorderColor = "#F83030";
            }
        }

        private bool IsDigitsOnly(string target)
        {
            foreach (char c in target)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void PickBackground()
        {
            CanOpenBackgroundPopup = !CanOpenBackgroundPopup;
        }

        private void UpdateBackground()
        {
            BackgroundColor = "#" + BackgroundHexColor;
        }

        private void ApplyBackground()
        {
            FinalBackgroundColor = BackgroundColor;

            BackgroundCheck = true;
            ChangeBackground();
        }

        private void ApplyDefaultBackground()
        {
            BackgroundHexColor = "Transparent";
            BackgroundColor = "Transparent";
            FinalBackgroundColor = "Transparent";
        }

        private void CheckOutputNameStatus()
        {
            if (OutputName.Any(c => InvalidCharactersFile.Contains(c)) == true)
            {
                OutputBorderColor = "#F83030";
                OutputWarningMessageOpacity = 1.0;

                CanExport = false;
            }
            else
            {
                OutputBorderColor = "#FFFFFF";
                OutputWarningMessageOpacity = 0.0;

                CanExport = true;
                ExportRedWarningOutput = 0.0;
            }
        }

        private void ChangeRegularStatus()
        {
            BoldCheck = false;
            BoldCheckCollision = true;
            RegularCheckCollision = false;

            UpdateFontWeight(FontWeights.Regular);
        }

        private void ChangeBoldStatus()
        {
            RegularCheck = false;
            RegularCheckCollision = true;
            BoldCheckCollision = false;

            UpdateFontWeight(FontWeights.Bold);
        }

        private void ChangeItalicStatus()
        {
            TextRange textRange = new TextRange(PreviewRTB.Document.ContentStart, PreviewRTB.Document.ContentEnd);
            if (ItalicCheck == true)
                textRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            else
                textRange.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
        }

        private void ChangeUnderlineStatus()
        {
            StrikethroughCheck = false;

            TextRange textRange = new TextRange(PreviewRTB.Document.ContentStart, PreviewRTB.Document.ContentEnd);
            if (UnderlineCheck == true)
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        private void ChangeStrikethroughStatus()
        {
            UnderlineCheck = false;

            TextRange textRange = new TextRange(PreviewRTB.Document.ContentStart, PreviewRTB.Document.ContentEnd);
            if (StrikethroughCheck == true)
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            else
                textRange.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
        }

        private void ChangeFontFamily()
        {
            if (ComesFromPickFontFamily == true)
            {
                ComesFromPickFontFamily = false;

                PrePreviewRTB.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/Lora/#Lora");
                UpdateRTBFontFamily(PrePreviewRTB);
            }
            else
            {
                RichTextBox TempRTB = new RichTextBox();
                CopyRTBContent(PreviewRTB, TempRTB);
                CopyRTBContent(PrePreviewRTB, PreviewRTB);
                CopyRTBContent(TempRTB, PrePreviewRTB);

                UpdateRTBFontFamily(PreviewRTB);
            }
        }

        private void ChangeFontForeground()
        {
            if (ForegroundCheck == true)
            {
                UpdateRTBForeground(PreviewRTB, FinalForegroundColor);
                UpdateRTBForeground(PrePreviewRTB, FinalForegroundColor);
            }
            else
            {
                UpdateRTBForeground(PreviewRTB, "#FFFFFF");
                UpdateRTBForeground(PrePreviewRTB, "#FFFFFF");
            }
        }

        private void ChangeBackground()
        {
            if (BackgroundCheck == true)
            {
                BackgroundGridOpacity = 1.0;
            }
            else
            {
                BackgroundGridOpacity = 0.0;
            }
        }

        private void UpdateRTBFontFamily(RichTextBox RichTextBoxTarget, string directory, string FontName)
        {
            FontFamily fontFamily = new FontFamily(new Uri(directory, UriKind.Absolute), "./#" + FontName);
            RichTextBoxTarget.FontFamily = fontFamily;

            if (ForegroundCheck == true)
                RichTextBoxTarget.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));

            FlowDocument Document = RichTextBoxTarget.Document;
            foreach (Block block in Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            if (run.FontWeight == FontWeights.Bold && run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else if (run.FontWeight == FontWeights.Bold)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else if (run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else
                            {
                                run.FontFamily = fontFamily;
                            }

                            if (ForegroundCheck == true)
                                run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
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
                                        run.FontFamily = fontFamily;
                                    }
                                    else if (run.FontWeight == FontWeights.Bold)
                                    {
                                        run.FontFamily = fontFamily;
                                    }
                                    else if (run.FontStyle == FontStyles.Italic)
                                    {
                                        run.FontFamily = fontFamily;
                                    }
                                    else
                                    {
                                        run.FontFamily = fontFamily;
                                    }

                                    if (ForegroundCheck == true)
                                        run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateRTBFontFamily(RichTextBox RichTextBoxTarget)
        {
            if (ForegroundCheck == true)
                RichTextBoxTarget.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));

            FlowDocument Document = RichTextBoxTarget.Document;
            foreach (Block block in Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            if (run.FontWeight == FontWeights.Bold && run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = RichTextBoxTarget.FontFamily;
                            }
                            else if (run.FontWeight == FontWeights.Bold)
                            {
                                run.FontFamily = RichTextBoxTarget.FontFamily;
                            }
                            else if (run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = RichTextBoxTarget.FontFamily;
                            }
                            else
                            {
                                run.FontFamily = RichTextBoxTarget.FontFamily;
                            }

                            if (ForegroundCheck == true)
                                run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
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
                                        run.FontFamily = RichTextBoxTarget.FontFamily;
                                    }
                                    else if (run.FontWeight == FontWeights.Bold)
                                    {
                                        run.FontFamily = RichTextBoxTarget.FontFamily;
                                    }
                                    else if (run.FontStyle == FontStyles.Italic)
                                    {
                                        run.FontFamily = RichTextBoxTarget.FontFamily;
                                    }
                                    else
                                    {
                                        run.FontFamily = RichTextBoxTarget.FontFamily;
                                    }

                                    if (ForegroundCheck == true)
                                        run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateRTBForeground(RichTextBox RichTextBoxTarget, string Color)
        {
            RichTextBoxTarget.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Color));

            FlowDocument Document = RichTextBoxTarget.Document;
            foreach (Block block in Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Color));
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
                                    run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Color));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void CopyRTBContent(RichTextBox richTextBox, RichTextBox targetRichTextBox)
        {
            FontFamily RichfontFamily = richTextBox.FontFamily;
            targetRichTextBox.FontFamily = RichfontFamily;

            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string DocumentData = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                textRange.Save(memoryStream, DataFormats.Xaml);
                DocumentData = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(DocumentData)))
            {
                TextRange targetTextRange = new TextRange(targetRichTextBox.Document.ContentStart, targetRichTextBox.Document.ContentEnd);
                targetTextRange.Load(memoryStream, DataFormats.Xaml);
            }
        }

        private void UpdateFontWeight(FontWeight fontWeight)
        {
            TextRange textRange = new TextRange(PreviewRTB.Document.ContentStart, PreviewRTB.Document.ContentEnd);
            textRange.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
        }

        private void CloseWindow()
        {
            CurrentWindow.Close();
        }

        private void SetPreviewRTB(RichTextBox richTextBox)
        {
            PreviewRTB = richTextBox;

            if (PreviewRTB != null)
            {
                PrePreviewRTB.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/Lora/#Lora");
                CopyRTBContent(PreviewRTB, PrePreviewRTB);
                UpdateRTBFontFamily(PrePreviewRTB);
            }
        }

        private void CreateTexture()
        {
            if (CanExport == false)
            {
                ExportRedWarningOutput = 1.0;
                return;
            }

            if (CurrentRTB != null)
            {
                RichTextBox OutputRTB = new RichTextBox();
                InitializeOutputRTB(OutputRTB);

                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext context = drawingVisual.RenderOpen())
                {
                    VisualBrush brush = new VisualBrush(OutputRTB) { Stretch = Stretch.UniformToFill };
                    Rect Canvas = new Rect();
                    Canvas.Width = OutputRTB.ActualWidth;
                    Canvas.Height = OutputRTB.ActualHeight;
                    Canvas.Location = new Point(0, 0);

                    context.DrawRectangle(brush, null, Canvas);
                }

                double OutputDpi = 96.0;
                if (!string.IsNullOrEmpty(DpiNumber))
                    OutputDpi = Int32.Parse(DpiNumber);

                double scale = OutputDpi / 96.0;
                int pixelWidth = (int)(OutputRTB.ActualWidth * scale);
                int pixelHeight = (int)(OutputRTB.ActualHeight * scale);

                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(pixelWidth, pixelHeight, OutputDpi, OutputDpi, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(drawingVisual);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                string ImageName = OutputNameMark;
                if (!string.IsNullOrEmpty(OutputName))
                    ImageName = OutputName;

                string ImagePath = Path.Combine(OutputPath, ImageName + ".png");
                int index = 0;
                while (System.IO.File.Exists(ImagePath))
                {
                    index++;
                    ImagePath = Path.Combine(OutputPath, ImageName + "_" + index + ".png");
                }

                using (FileStream fileStream = new FileStream(ImagePath, FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }

        private void InitializeOutputRTB(RichTextBox RichTextBoxTarget)
        {
            CopyRTBContent(CurrentRTB, RichTextBoxTarget);

            RichTextBoxTarget.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalBackgroundColor));
            

            FontFamily fontFamily;
            if (FontFamily.Equals("Pick Font Family"))
                fontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/Lora/#Lora");
            else
                fontFamily = new FontFamily(new Uri(FontDirectory, UriKind.Absolute), "./#" + FontFamily);


            RichTextBoxTarget.FontFamily = fontFamily;
            RichTextBoxTarget.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));

            FlowDocument Document = RichTextBoxTarget.Document;
            foreach (Block block in Document.Blocks)
            {
                if (block is Paragraph paragraph)
                {
                    foreach (Inline inline in paragraph.Inlines)
                    {
                        if (inline is Run run)
                        {
                            if (run.FontWeight == FontWeights.Bold && run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else if (run.FontWeight == FontWeights.Bold)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else if (run.FontStyle == FontStyles.Italic)
                            {
                                run.FontFamily = fontFamily;
                            }
                            else
                            {
                                run.FontFamily = fontFamily;
                            }

                            run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
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
                                        run.FontFamily = fontFamily;
                                    }
                                    else if (run.FontWeight == FontWeights.Bold)
                                    {
                                        run.FontFamily = fontFamily;
                                    }
                                    else if (run.FontStyle == FontStyles.Italic)
                                    {
                                        run.FontFamily = fontFamily;
                                    }
                                    else
                                    {
                                        run.FontFamily = fontFamily;
                                    }

                                    run.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(FinalForegroundColor));
                                }
                            }
                        }
                    }
                }
            }

            RichTextBoxTarget.BorderThickness = new Thickness(0);
            RichTextBoxTarget.Measure(new Size(CurrentRTB.ActualWidth, CurrentRTB.ActualHeight));
            RichTextBoxTarget.Arrange(new Rect(0, 0, CurrentRTB.ActualWidth, CurrentRTB.ActualHeight));
        }

        private void OpenExportDirectory()
        {
            if (Directory.Exists(OutputPath))
                Process.Start(new ProcessStartInfo { FileName = OutputPath, UseShellExecute = true});
        }

        private string DoesDirectoryExists()
        {
            string ApplicationPath = AppContext.BaseDirectory;

            string ResourcesPath = Path.Combine(ApplicationPath, "Resources");
            if (!Directory.Exists(ResourcesPath))
                Directory.CreateDirectory(ResourcesPath);

            string TextureOutputsPath = Path.Combine(ResourcesPath, "Texture Outputs");
            if (!Directory.Exists(TextureOutputsPath))
                Directory.CreateDirectory(TextureOutputsPath);

            return TextureOutputsPath;
        }
    }
}
