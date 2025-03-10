// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for WelcomeLastUserControl.xaml
    /// </summary>
    public partial class WelcomeLastUserControl : UserControl
    {
        private NewUserStartupWindow CurrentWindow;
        Storyboard storyboard;
        public WelcomeLastUserControl(NewUserStartupWindow window)
        {
            InitializeComponent();

            CurrentWindow = window;
            storyboard = new Storyboard();
            TextAnim("But I will speak, for my words are my last weapon.", 1.6, 1.8, Para);
            storyboard.Begin();
        }

        private void TextAnim(string OutputName, double start, double duration, TextBlock textBlock)
        {
            storyboard.BeginTime = TimeSpan.FromSeconds(start);
            storyboard.FillBehavior = FillBehavior.HoldEnd;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.Close();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.StartSession();
        }
    }
}
