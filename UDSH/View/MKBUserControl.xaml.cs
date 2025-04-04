using System.Windows.Controls;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class MKBUserControl : UserControl
    {
        public MKBUserControl(MKBUserControlViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

        private void BorderGreen_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
