// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using UDSH.ViewModel;

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
