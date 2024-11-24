using System.Windows;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for NewFileCreationWindow.xaml
    /// </summary>
    public partial class NewFileCreationWindow : Window
    {
        public NewFileCreationWindow(Window ParentWindow)
        {
            Owner = ParentWindow;
            InitializeComponent();
            NewFileCreationWindowViewModel viewModel = new NewFileCreationWindowViewModel(this);
            DataContext = viewModel;
        }
    }
}
