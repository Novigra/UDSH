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
            // Check the UDSH UserData file, not the directory
            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            if (!Directory.Exists(AppData))
            {
                NewUserStartupWindow newUserStartupWindow = serviceProvider.GetRequiredService<NewUserStartupWindow>();
                newUserStartupWindow.Show();
            }
            else
            {
                MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }

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
                return new HeaderServices(userDataServices, serviceProvider);
            });
            services.AddTransient<HeaderUserControlViewModel>();
            services.AddTransient<HeaderUserControl>();
            services.AddTransient<NewUserStartupWindow>();
            services.AddTransient<MainWindow>();
        }
    }

}
