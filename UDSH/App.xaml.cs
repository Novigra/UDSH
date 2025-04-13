// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.View.TestGround;
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
            string FilePath = Path.Combine(AppData, "UserData.json");
            if (!File.Exists(FilePath))
            {
                NewUserStartupWindow newUserStartupWindow = serviceProvider.GetRequiredService<NewUserStartupWindow>();
                newUserStartupWindow.Show();
            }
            else
            {
                MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();

                /*TestWindow testWindow = new TestWindow();
                testWindow.Show();*/
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
            services.AddSingleton<IHeaderServices>(provider =>
            {
                var userDataServices = provider.GetRequiredService<IUserDataServices>();
                return new HeaderServices(userDataServices, serviceProvider);
            });
            services.AddSingleton<IWorkspaceServices>(provider =>
            {
                var userDataServices = provider.GetRequiredService<IUserDataServices>();
                return new WorkspaceServices(userDataServices);
            });
            services.AddTransient<HeaderUserControlViewModel>();
            services.AddTransient<HeaderUserControl>();
            services.AddTransient<NewUserStartupWindow>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnExit(ExitEventArgs exit)
        {
            base.OnExit(exit);

            string AppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            string TempPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Temp");

            if(Directory.Exists(TempPath))
            {
                foreach(string file  in Directory.GetFiles(TempPath))
                    File.Delete(file);
            }
        }
    }

}
