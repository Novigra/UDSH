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

        private string foreground;
        public string Foreground
        {
            get => foreground;
            set { foreground = value; OnPropertyChanged(); }
        }

        private bool canOpenForegroundPopup;
        public bool CanOpenForegroundPopup
        {
            get => canOpenForegroundPopup;
            set { canOpenForegroundPopup = value; OnPropertyChanged(); }
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

        public RelayCommand<Button> FontFamilyButton => new RelayCommand<Button>(execute => PickFontFamily());
        public RelayCommand<Button> ForegroundButton => new RelayCommand<Button>(execute => PickForeground());

        public RelayCommand<CheckBox> RegularCheckChanged => new RelayCommand<CheckBox>(execute => ChangeRegularStatus());
        public RelayCommand<CheckBox> BoldCheckChanged => new RelayCommand<CheckBox>(execute => ChangeBoldStatus());
        public RelayCommand<CheckBox> ItalicCheckChanged => new RelayCommand<CheckBox>(execute => ChangeItalicStatus());
        public RelayCommand<CheckBox> UnderlineCheckChanged => new RelayCommand<CheckBox>(execute => ChangeUnderlineStatus());
        public RelayCommand<CheckBox> StrikethroughCheckChanged => new RelayCommand<CheckBox>(execute => ChangeStrikethroughStatus());
        public RelayCommand<CheckBox> FontCheckChanged => new RelayCommand<CheckBox>(execute => ChangeFontFamily());

        public RelayCommand<Button> CloseButton => new RelayCommand<Button>(execute => CloseWindow());
        public RelayCommand<RichTextBox> PreviewRTBLoaded => new RelayCommand<RichTextBox>(SetPreviewRTB);

        public TextureCreationViewModel(RichTextBox currentRTB, FileSystem file, Window currentWindow)
        {
            File = file;
            CurrentRTB = currentRTB;
            CurrentWindow = currentWindow;

            AssociateFileName = File.FileName;
            FontFamily = "Pick Font Family";
            Foreground = "Pick Foreground";

            CanOpenForegroundPopup = false;

            ComesFromPickFontFamily = false;

            RegularCheck = true;
            RegularCheckCollision = false;

            BoldCheck = false;
            BoldCheckCollision = true;

            ItalicCheck = false;

            UnderlineCheck = false;
            StrikethroughCheck = false;
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
                    }
                }
            }
        }

        private void PickForeground()
        {
            CanOpenForegroundPopup = !CanOpenForegroundPopup;
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

        private void UpdateRTBFontFamily(RichTextBox RichTextBoxTarget, string directory, string FontName)
        {
            FontFamily fontFamily = new FontFamily(new Uri(directory, UriKind.Absolute), "./#" + FontName);
            RichTextBoxTarget.FontFamily = fontFamily;

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
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateRTBFontFamily(RichTextBox RichTextBoxTarget)
        {
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
    }
}
