using System.Reflection;
using System.Windows.Controls;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for FooterUserControl.xaml
    /// </summary>
    public partial class FooterUserControl : UserControl
    {
        public FooterUserControl(FooterUserControlViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            ConnectPopup.MouseDown += (s, e) => e.Handled = true;
        }
    }
}
