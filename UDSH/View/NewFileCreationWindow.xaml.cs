using System.Windows;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for NewFileCreationWindow.xaml
    /// </summary>
    public partial class NewFileCreationWindow : Window
    {
        private NewFileCreationWindowViewModel viewModel;
        public NewFileCreationWindow(NewFileCreationWindowViewModel viewModel, Window ParentWindow)
        {
            Owner = ParentWindow;
            InitializeComponent();

            this.viewModel = viewModel;
            DataContext = viewModel;

            this.viewModel.CurrentWindow = this;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            viewModel.PlayHighlightedText();
        }
    }
}
