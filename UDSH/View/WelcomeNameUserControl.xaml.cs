using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for WelcomeNameUserControl.xaml
    /// </summary>
    public partial class WelcomeNameUserControl : UserControl
    {
        private NewUserStartupWindow CurrentWindow;
        private Storyboard storyboard;
        public WelcomeNameUserControl( NewUserStartupWindow window)
        {
            InitializeComponent();

            CurrentWindow = window;
            storyboard = new Storyboard();
            OpeningAnimation();
        }

        private void OpeningAnimation()
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.BeginTime = TimeSpan.FromSeconds(0);
            opacityAnimation.To = 1.0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(opacityAnimation, NameBorder);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityAnimation);


            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.BeginTime = TimeSpan.FromSeconds(4);
            widthAnimation.To = 500.0;
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(widthAnimation, NameBorder);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
            storyboard.Children.Add(widthAnimation);

            TextAnim(5, 1, HighlightText, "My Name Is...");
            TextAnim(2, 1.8, Para, "Life consumed me; all that remains is my name.");

            storyboard.Completed += (sender, args) =>
            {
                storyboard.Remove();
                storyboard = new Storyboard();

                NameBorder.Opacity = 1.0;
                NameBorder.Width = 500.0;
                NameText.IsHitTestVisible = true;

                HighlightText.Text = "My Name Is...";
                Para.Text = "Life consumed me; all that remains is my name.";
            };

            storyboard.Begin();
        }

        private void TextAnim(double start, double duration, TextBlock textBlock, string OutputName)
        {
            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.BeginTime = TimeSpan.FromSeconds(start);
            stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(duration));

            string Conj = string.Empty;

            foreach (char i in OutputName)
            {
                Conj += i;
                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                discreteStringKeyFrame.Value = Conj;

                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }

            Storyboard.SetTarget(stringAnimationUsingKeyFrames, textBlock);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath("Text"));
            storyboard.Children.Add(stringAnimationUsingKeyFrames);
        }

        private void NameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(NameText.Text))
            {
                HighlightText.Text = "";

                NextButton.IsHitTestVisible = true;
                ChangeOpacity(1);
            }
            else
            {
                HighlightText.Text = "My Name Is...";

                NextButton.IsHitTestVisible = false;
                ChangeOpacity(0);
            }
        }

        private void ChangeOpacity(int to)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.BeginTime = TimeSpan.FromSeconds(0);

            opacityAnimation.To = to;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(opacityAnimation, ButtonContainer);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityAnimation);

            storyboard.Begin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("asgdsa");
            // TODO: Close animation for Upper, Pata, and Bottom Text.
            // Add Display name to the New User startup window when hitting Next button.
            // In App, we check directory, that's not good, so check the file itself.

            if (!string.IsNullOrEmpty(NameText.Text))
                CurrentWindow.UserDisplayName = NameText.Text;

            ClosingAnimation(ButtonContainer);
            ClosingAnimation(UpperPara);
            ClosingAnimation(Para);
            ClosingAnimation(BottomPara);
            ClosingAnimation(NameText);
            ClosingBorderAnimation();
            NextButton.IsEnabled = false;

            storyboard.Begin();
        }

        private void ClosingAnimation(UIElement element)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation();
            opacityAnimation.To = 0.0;
            opacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));

            Storyboard.SetTarget(opacityAnimation, element);
            Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));

            storyboard.Children.Add(opacityAnimation);
        }

        private void ClosingBorderAnimation()
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.BeginTime = TimeSpan.FromSeconds(1);
            widthAnimation.To = 0.0;
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(widthAnimation, NameBorder);
            Storyboard.SetTargetProperty(widthAnimation, new PropertyPath("Width"));
            storyboard.Children.Add(widthAnimation);

            storyboard.Completed += (sender, args) =>
            {
                WelcomeProfilePictureUserControl pictureUserControl = new WelcomeProfilePictureUserControl(CurrentWindow);
                CurrentWindow.Main.Content = pictureUserControl;
            };
        }
    }
}
