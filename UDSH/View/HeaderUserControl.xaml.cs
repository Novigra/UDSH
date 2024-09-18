using System.Windows;
using System.Windows.Controls;

namespace UDSH.View
{
    public partial class HeaderUserControl : UserControl
    {
        public HeaderUserControl()
        {
            InitializeComponent();
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

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
        }
    }
}
