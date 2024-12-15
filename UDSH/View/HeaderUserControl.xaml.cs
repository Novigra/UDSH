using System.Windows;
using System.Windows.Controls;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class HeaderUserControl : UserControl
    {
        private HeaderUserControlViewModel viewModel;

        public HeaderUserControl(HeaderUserControlViewModel viewModel)
        {
            InitializeComponent();
            /*PagesList.Items.Add(new Button());
            PagesList.Items.Add(new Button());
            PagesList.Items.Add(new Button());*/

            //viewModel = new HeaderUserControlViewModel();
            this.viewModel = viewModel;
            DataContext = viewModel;
            PagesList.Items.Add(new ListViewItem());
            PagesList.Items.Add(new ListViewItem());
            PagesList.Items.Add(new ListViewItem());

            Loaded += OnUserControlLoaded;
        }

        private void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.MainWindow = Window.GetWindow(this);
        }

        // Buttons to control the window, there's no need to bind and write the code in the ViewModel section.
        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            Window OwnerWindow = Window.GetWindow(this);
            OwnerWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton(object sender, RoutedEventArgs e)
        {
            Window OwnerWindow = Window.GetWindow(this);

            if (OwnerWindow.WindowState == WindowState.Maximized)
                OwnerWindow.WindowState = WindowState.Normal;
            else
                OwnerWindow.WindowState = WindowState.Maximized;
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(viewModel != null)
                viewModel.LockPenToolButtons(e);
        }

        private void HandleKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (viewModel != null)
                viewModel.ReleasePenToolButtons(e);
        }
    }
}
