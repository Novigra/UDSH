using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for SideContentUserControl.xaml
    /// </summary>
    public partial class SideContentUserControl : UserControl
    {
        public SideContentUserControl()
        {
            InitializeComponent();

            SideContentUserControlViewModel ViewModel = new SideContentUserControlViewModel();
            DataContext = ViewModel;
            //this.Loaded += SideContentUserControl_Loaded;
        }
        void SideContentUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            foreach (InputBinding ib in this.InputBindings)
            {
                window.InputBindings.Add(ib);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Focus();
        }
    }
}
