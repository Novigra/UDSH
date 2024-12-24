using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.Model;

namespace UDSH.View
{
    // There's no need for MVVM approach since the implementation is very simple! (applies to all startup user controls)
    public partial class NewUserStartupWindow : Window
    {
        private readonly IServiceProvider serviceProvider;
        public string UserDisplayName;
        private bool FirstWindowAnim;
        public bool UserSetProfilePicture;
        public bool UserSetCustomIcon;
        public NewUserStartupWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            WelcomeUserControl welcomeUserControl = new WelcomeUserControl(this);
            Main.Content = welcomeUserControl;

            FirstWindowAnim = true;

            UserDisplayName = string.Empty;
            UserSetProfilePicture = false;
            UserSetCustomIcon = false;

            this.serviceProvider = serviceProvider;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.From = 0.0;
            opacityAnimation.To = 1.0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(opacityAnimation, BackGrid);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            TranslateTransform translateTransform = new TranslateTransform();
            BackGrid.RenderTransform = translateTransform;

            DoubleAnimation translateYAnimation = new DoubleAnimation();
            translateYAnimation.From = 150.0;
            translateYAnimation.To = 0.0;
            translateYAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(translateYAnimation, BackGrid);
            Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));

            storyboard.Children.Add(opacityAnimation);
            storyboard.Children.Add(translateYAnimation);

            storyboard.Begin();
        }

        public void StartSession()
        {
            Debug.WriteLine("###### SESSION STARTED ######");

            User user = new User()
            {
                DisplayName = UserDisplayName,
                IsProfilePictureSet = UserSetProfilePicture,
                IsCustomIconSet = UserSetCustomIcon,
                Projects = new List<Project> { }
            };

            string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            
            if (!Directory.Exists(AppData))
                Directory.CreateDirectory(AppData);

            string UserDataFilePath = Path.Combine(AppData, "UserData.json");
            string JsonFile = JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(UserDataFilePath, JsonFile);

            MainWindow mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            Close();

            /*
             * TODO:
             * 
             *  - Main window startup animation.
             *  - Header Custom Icons.
             *  - Header Profile Picture.
             *  - Header Drag Movement.
             *  - Delete all temp files, when closing the application.
             */
        }
    }
}
