using System.Windows.Controls;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class MKBUserControl : UserControl
    {
        private MKBUserControlViewModel viewModel;
        public MKBUserControl(MKBUserControlViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            this.viewModel = viewModel;
        }

        private void MainCanvas_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (viewModel.CanOpenCanvasContextMenu == false)
            {
                e.Handled = true;
            }
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            viewModel.UpdateCurrentActiveWorkspaceID();
        }
    }
}
