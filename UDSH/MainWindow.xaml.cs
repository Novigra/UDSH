using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.ViewModel;

namespace UDSH
{
    public partial class MainWindow : Window
    {
        //private readonly IUserDataServices userDataServices;
        //private IServiceProvider serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            /*this.serviceProvider = serviceProvider;
            var header = this.serviceProvider.GetRequiredService<HeaderUserControl>();*/
            //userDataServices = userService;

            var session = serviceProvider.GetRequiredService<Session>();
            session.mainWindow = this;

            var header = serviceProvider.GetRequiredService<IHeaderServices>();
            HeaderUserControl headerUserControl = new HeaderUserControl(new HeaderUserControlViewModel(header));
            HeaderUserControl.Children.Add(headerUserControl);

            MKUserControl mKUserControl = new MKUserControl();
            DefaultUserControl defaultUserControl = new DefaultUserControl();
            TestContent.Content = defaultUserControl;
        }

        private void HeaderMovement(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}