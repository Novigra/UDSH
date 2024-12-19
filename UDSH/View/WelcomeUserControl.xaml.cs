using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for WelcomeUserControl.xaml
    /// </summary>
    public partial class WelcomeUserControl : UserControl
    {
        private NewUserStartupWindow CurrentWindow;
        private Storyboard storyboard;
        public WelcomeUserControl(NewUserStartupWindow window)
        {
            InitializeComponent();

            storyboard = new Storyboard();
            CurrentWindow = window;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ClosingAnimation(WelcomeTitle);
            ClosingAnimation(WelcomeSubTitle);
            ClosingAnimation(UpperPara);
            ClosingAnimation(Para);
            ClosingAnimation(BottomPara);
            ClosingAnimation(ButtonContainer, true);

            NextButton.IsEnabled = false;

            storyboard.Begin();
            /*WelcomeNameUserControl welcomeName = new WelcomeNameUserControl();
            CurrentWindow.Main.Content = welcomeName;*/
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            string OutputName = "I embarked on a journey hoping to find myself.";

            Storyboard story = new Storyboard();
            story.BeginTime = TimeSpan.FromSeconds(2);
            story.FillBehavior = FillBehavior.HoldEnd;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(1.8));

            string Conj = string.Empty;
            foreach (char i in OutputName)
            {
                Conj += i;

                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                discreteStringKeyFrame.Value = Conj;

                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, Para.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath("Text"));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(Para);
        }

        private void ClosingAnimation(UIElement element, bool isLast = false)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.To = 0.0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            Storyboard.SetTarget(opacityAnimation, element);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            storyboard.Children.Add(opacityAnimation);
            if (isLast == true)
                storyboard.Completed += (sender, args) =>
                {
                    WelcomeNameUserControl welcomeName = new WelcomeNameUserControl(CurrentWindow);
                    CurrentWindow.Main.Content = welcomeName;
                };
        }
    }
}
