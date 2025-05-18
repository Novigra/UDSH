// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class MKCUserControl : UserControl
    {
        MKCUserControlViewModel viewModel;
        public MKCUserControl(MKCUserControlViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private async void MKContentLayout_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
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

                if (BackResult)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }

                viewModel.ChangeSaveStatus();
            }
            else if (e.Key == Key.Up || e.Key == Key.Down || e.Key == Key.Left || e.Key == Key.Right)
            {
                viewModel.NavigateParagraphs(e.Key);
                e.Handled = false;
            }
            else
            {
                viewModel.ChangeSaveStatus();
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void MKContentLayout_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            e.Handled = true;
        }

        private void MKContentLayout_PreviewDrop(object sender, System.Windows.DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
