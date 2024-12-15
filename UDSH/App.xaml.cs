using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.ViewModel;

namespace UDSH
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ManageServices(serviceCollection);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Session session = new Session();
            /*MainWindow window = new MainWindow();
            window.Show();*/

            MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();


            #if DEBUG
            Console.WriteLine("MODE::DEBUG");
            #else
            Console.WriteLine("MODE::Release");
            #endif
        }

        private void ManageServices(IServiceCollection services)
        {
            services.AddSingleton<Session>();
            services.AddSingleton<IUserDataServices, UserDataServices>();
            services.AddTransient<IHeaderServices>(provider =>
            {
                var userDataServices = provider.GetRequiredService<IUserDataServices>();
                return new HeaderServices(userDataServices);
            });
            services.AddTransient<HeaderUserControlViewModel>();
            services.AddTransient<HeaderUserControl>();
            /*services.AddTransient<NewProjectCreationWindowViewModel>();
            services.AddTransient<NewProjectCreationWindow>();*/
            services.AddTransient<MainWindow>();
        }
    }

}
