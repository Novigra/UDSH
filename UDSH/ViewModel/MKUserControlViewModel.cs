using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    class MKUserControlViewModel : ViewModelBase
    {
        private bool IsNextToAMarkdown = false;

        private RichTextBox mKRichTextBox;
        public RichTextBox MKRichTextBox
        {
            get { return mKRichTextBox; }
            set { mKRichTextBox = value; OnPropertyChanged(); }
        }
        private Paragraph LastPickedParagraph;


        #region Commands
        public RelayCommand<Button> AlignLeft => new RelayCommand<Button>(execute => AlignParagraphTargetLeft());
        public RelayCommand<Button> AlignCenter => new RelayCommand<Button>(execute => AlignParagraphTargetCenter());
        public RelayCommand<Button> AlignRight => new RelayCommand<Button>(execute => AlignParagraphTargetRight());

        public RelayCommand<Button> BoldText => new RelayCommand<Button>(execute => ChangeBoldText());
        public RelayCommand<Button> ItalicText => new RelayCommand<Button>(execute => ChangeItalicText());
        public RelayCommand<Button> StrikethroughText => new RelayCommand<Button>(execute => ChangeStrikethroughText());
        public RelayCommand<Button> UnderlineText => new RelayCommand<Button>(execute => ChangeUnderlineText());

        public RelayCommand<RichTextBox> RichTextLoaded => new RelayCommand<RichTextBox>(OnRichTextBoxLoaded);
        public RelayCommand<KeyEventArgs> PressedEnter => new RelayCommand<KeyEventArgs>(execute => CreateANewParagraph(), canExecute => CanCreateANewParagraph());

        public RelayCommand<Paragraph> CaptureParagraph => new RelayCommand<Paragraph> (GetParagraph);

        public RelayCommand<RichTextBox> ParagraphFocus => new RelayCommand<RichTextBox>(execute => UpdateLastPickedParagraph());
        #endregion

        public bool SpaceMarkdown()
        {
            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);

            if(textRange.Text.Equals("#"))
            {
                Debug.WriteLine($"Play # Markdown...");
                int FontSize = 40;
                SpaceMarkdown_Header(textRange, FontSize);

                return true;
            }
            else if(textRange.Text.Equals("##"))
            {
                Debug.WriteLine($"Play ## Markdown...");
                int FontSize = 30;
                SpaceMarkdown_Header(textRange, FontSize);

                return true;
            }
            else if(textRange.Text.Equals("###"))
            {
                Debug.WriteLine($"Play ### Markdown...");
                int FontSize = 25;
                SpaceMarkdown_Header(textRange, FontSize);

                return true;
            }
            else if(textRange.Text.Equals("-"))
            {
                Debug.WriteLine($"Play - Markdown...");
                textRange.Text = "";
                ListItem listItem = new ListItem(LastPickedParagraph);
                List list = new List();
                list.ListItems.Add(listItem);

                MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                MKRichTextBox.Document.Blocks.Add(list);
                MKRichTextBox.CaretPosition = list.ContentEnd;
                // TODO: Add functionality so when pressing enter, we add item to the list.

                return true;
            }
            else
            {
                return false;
            }
                
        }

        private void SpaceMarkdown_Header(TextRange textRange, int FontSize)
        {
            textRange.Text = "";
            LastPickedParagraph.FontSize = FontSize;

            Thickness HeaderThickness = new Thickness(0, 0, 0, 0);
            LastPickedParagraph.Margin = HeaderThickness;

            IsNextToAMarkdown = true;
        }

        public void SetLastPickedParagraphAfterDeletion()
        {
            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
        }

        private void UpdateLastPickedParagraph()
        {
            if (MKRichTextBox != null)
            {
                TextPointer caretPosition = MKRichTextBox.CaretPosition;
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null && paragraph.IsKeyboardFocused == false)
                {
                    paragraph.Focus();

                    if (LastPickedParagraph != null)
                        LastPickedParagraph.Focusable = true;

                    LastPickedParagraph = paragraph;
                    LastPickedParagraph.Focusable = false;

                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"MK Paragraph: {textRange.Text}");
                }
            }
        }

        private void CreateANewParagraph()
        {
            Paragraph paragraph = new Paragraph();

            BlockCollection Blocks = MKRichTextBox.Document.Blocks;
            Block CurrentBlock = Blocks.FirstBlock;

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;

            while (CurrentBlock != null)
            {
                if(CurrentBlock == LastPickedParagraph)
                {
                    if(MKRichTextBox.CaretPosition != LastPickedParagraph.ContentEnd)
                    {
                        TextRange textRange = new TextRange(MKRichTextBox.CaretPosition, LastPickedParagraph.ContentEnd);

                        using (var memoryStream = new MemoryStream())
                        {
                            textRange.Save(memoryStream, DataFormats.Xaml);
                            memoryStream.Position = 0;

                            TextRange NewPara = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                            NewPara.Load(memoryStream, DataFormats.Xaml);
                        }

                        /*run = new Run(textRange.Text);
                        paragraph.Inlines.Add(run);*/

                        textRange.Text = string.Empty;
                    }

                    Blocks.InsertAfter(CurrentBlock, paragraph);
                    MKRichTextBox.CaretPosition = paragraph.ContentStart;

                    LastPickedParagraph.Focusable = true;
                    paragraph.Focusable = false;
                    LastPickedParagraph = paragraph;

                    if(IsNextToAMarkdown == true)
                    {
                        Thickness HeaderThickness = new Thickness(0, 10, 0, 0);
                        LastPickedParagraph.Margin = HeaderThickness;

                        IsNextToAMarkdown = false;
                    }

                    return;
                }

                CurrentBlock = CurrentBlock.NextBlock;
            }
        }

        private bool CanCreateANewParagraph()
        {
            if(MKRichTextBox.Selection.Text == "")
                return true;

            ManageSelection();
            return false;
        }
        
        private void ManageSelection()
        {
            if (MKRichTextBox != null)
            {
                Debug.WriteLine($"What is after End :{MKRichTextBox.Selection.End.GetTextInRun(LogicalDirection.Forward)}[stop]");
                
                if(MKRichTextBox.Selection.End.GetTextInRun(LogicalDirection.Forward) == "")
                {
                    MKRichTextBox.Selection.Text = "";
                }
                else
                {
                    TextPointer CurrentPointer = MKRichTextBox.Selection.End;

                    Paragraph Para1 = MKRichTextBox.Selection.Start.Paragraph;

                    MKRichTextBox.Selection.Text = "\n";
                    MKRichTextBox.CaretPosition = CurrentPointer;

                    Paragraph Para2 = MKRichTextBox.Selection.End.Paragraph;
                    LastPickedParagraph = Para2;
                    Para2.Focusable = false;

                    if(Para1 != Para2)
                        Para1.Focusable = true;
                }
            }
        }

        private void AlignParagraphTargetLeft()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Left;
        }
        private void AlignParagraphTargetCenter()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Center;
        }

        private void AlignParagraphTargetRight()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Right;
        }

        private void ChangeBoldText()
        {
            if (MKRichTextBox != null)
            {
                /*TextPointer StartingPoint = MKRichTextBox.Selection.Start;
                TextPointer EndingPoint = MKRichTextBox.Selection.End;*/

                if (MKRichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ChangeItalicText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void ChangeStrikethroughText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Strikethrough))
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
        }

        private void ChangeUnderlineText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }

        private void OnRichTextBoxLoaded(RichTextBox richTextBox)
        {
            if (richTextBox != null)
                MKRichTextBox = richTextBox;
        }

        private void GetParagraph(Paragraph paragraph)
        {
            if (paragraph != null)
            {
                if (LastPickedParagraph != null)
                    LastPickedParagraph.Focusable = true;

                paragraph.Focusable = false;
                LastPickedParagraph = paragraph;

                TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                string ParaText = textRange.Text;

                //Debug.WriteLine($"Text : {ParaText}");
            }
        }
    }
}
