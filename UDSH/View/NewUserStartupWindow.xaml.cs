using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace UDSH.View
{
    // There's no need for MVVM approach since the implementation is very simple! (applies to all startup user controls)
    public partial class NewUserStartupWindow : Window
    {
        public string UserDisplayName;
        private bool FirstWindowAnim;
        public NewUserStartupWindow()
        {
            InitializeComponent();

            WelcomeUserControl welcomeUserControl = new WelcomeUserControl(this);
            Main.Content = welcomeUserControl;

            FirstWindowAnim = true;
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
    }
}
