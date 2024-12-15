using System.Windows;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for NewProjectCreationWindow.xaml
    /// </summary>
    public partial class NewProjectCreationWindow : Window
    {
        public NewProjectCreationWindow(NewProjectCreationWindowViewModel viewModel, Window window)
        {
            Owner = window;
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
