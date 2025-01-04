using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using UDSH.Services;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for DefaultUserControl.xaml
    /// </summary>
    public partial class DefaultUserControl : UserControl
    {
        public DefaultUserControl(IUserDataServices userDataServices)
        {
            InitializeComponent();

            /*SideContentUserControl sideContentUserControl = new SideContentUserControl(new SideContentUserControlViewModel(userDataServices));
            SideContentGrid.Children.Add(sideContentUserControl);*/
        }

        private void ObjectFocus(object sender, MouseButtonEventArgs e)
        {
            if(sender is Rectangle)
            {
                Rectangle? rectangle = sender as Rectangle;
                rectangle!.Focus();
            }
            else if (sender is Image)
            {
                Image? image = sender as Image;
                image!.Focus();
            }
        }
    }
}
