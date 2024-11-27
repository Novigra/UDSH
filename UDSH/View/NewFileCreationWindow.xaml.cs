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
        public NewFileCreationWindow(Window ParentWindow)
        {
            Owner = ParentWindow;
            InitializeComponent();
            viewModel = new NewFileCreationWindowViewModel(this);
            DataContext = viewModel;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            viewModel.PlayHighlightedText();
        }
    }
}
