// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace UDSH.View
{
    public class CharacterLinkButton : Button
    {
        public Paragraph CharacterParagraph { get; set; }
        public Button AssociatedDialogueButton { get; set; }
        public InlineUIContainer Placeholder { get; set; }

        private Border ButtonBorder { get; set; }
        private Border ConnectedButtonBorder { get; set; }

        public CharacterLinkButton()
        {
            Construct();
        }

        private void Construct()
        {
            Style = (Style)Application.Current.FindResource("BNCharacterConnectionButton");

            MouseEnter += CharacterLinkButton_MouseEnter;
            MouseLeave += CharacterLinkButton_MouseLeave;
            PreviewMouseLeftButtonDown += CharacterLinkButton_PreviewMouseLeftButtonDown;
            PreviewMouseLeftButtonUp += CharacterLinkButton_PreviewMouseLeftButtonUp;
            //Click += CharacterLinkButton_Click;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ButtonBorder = GetTemplateChild("ButtonBorder") as Border;
            ConnectedButtonBorder = GetTemplateChild("ConnectedButtonBorder") as Border;
        }

        private void CharacterLinkButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (AssociatedDialogueButton == null)
                ButtonBackgroundAnimation("#88E0911A", 0.1, ButtonBorder);
            else
                ButtonBackgroundAnimation("#D88623", 0.1, ConnectedButtonBorder);
        }

        private void CharacterLinkButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (AssociatedDialogueButton == null)
                ButtonBackgroundAnimation("#66E0911A", 0.1, ButtonBorder);
            else
                ButtonBackgroundAnimation("#E0911A", 0.1, ConnectedButtonBorder);
        }

        private void CharacterLinkButton_Click(object sender, RoutedEventArgs e)
        {
            // Preview works much better for animation
            if (AssociatedDialogueButton == null)
                ButtonBackgroundAnimation("#E0911A", 0.1, ButtonBorder, true, "#66E0911A", 0.1, 0.1);
        }

        private void CharacterLinkButton_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AssociatedDialogueButton == null)
                ButtonBackgroundAnimation("#E0911A", 0.1, ButtonBorder);
        }

        private void CharacterLinkButton_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (AssociatedDialogueButton == null)
                ButtonBackgroundAnimation("#66E0911A", 0.1, ButtonBorder);
        }

        public void UpdateButtonVisuals(bool IsConnected)
        {
            if (IsConnected == true)
            {
                ButtonBackgroundSwitchAnimation(ButtonBorder, 0);
                ButtonBackgroundSwitchAnimation(ConnectedButtonBorder, 1);
            }
            else
            {
                ButtonBackgroundSwitchAnimation(ButtonBorder, 1);
                ButtonBackgroundSwitchAnimation(ConnectedButtonBorder, 0);
            }
        }

        private void ButtonBackgroundAnimation(string ColorHexCode, double duration, DependencyObject dependencyObject, bool CanReverse = false,
            string ReverseColorHexCode = "", double ReverseBeginTime = 0, double ReverseDuration = 0)
        {
            Storyboard storyboard = new Storyboard();
            ColorAnimation colorAnimation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString(ColorHexCode),
                Duration = TimeSpan.FromSeconds(duration),
            };

            Storyboard.SetTarget(colorAnimation, dependencyObject);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
            storyboard.Children.Add(colorAnimation);

            if (CanReverse == true)
            {
                ColorAnimation colorToOriginal = new ColorAnimation
                {
                    To = (Color)ColorConverter.ConvertFromString(ReverseColorHexCode),
                    Duration = TimeSpan.FromSeconds(ReverseDuration),
                    BeginTime = TimeSpan.FromSeconds(ReverseBeginTime),
                };

                Storyboard.SetTarget(colorToOriginal, dependencyObject);
                Storyboard.SetTargetProperty(colorToOriginal, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
                storyboard.Children.Add(colorToOriginal);
            }

            storyboard.Begin();
        }

        private void ButtonBackgroundSwitchAnimation(DependencyObject dependencyObject, double Target)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation
            {
                To = Target,
                Duration = TimeSpan.FromSeconds(0.1),
            };

            Storyboard.SetTarget(OpacityAnimation, dependencyObject);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);

            storyboard.Begin();
        }
    }
}
