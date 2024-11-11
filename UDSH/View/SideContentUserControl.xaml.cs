using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
