// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for WelcomeProfilePictureUserControl.xaml
    /// </summary>
    public partial class WelcomeProfilePictureUserControl : UserControl
    {
        //private string TempFilePath;
        private NewUserStartupWindow CurrentWindow;
        private Storyboard storyboard;
        public WelcomeProfilePictureUserControl(NewUserStartupWindow window)
        {
            InitializeComponent();

            //TempFilePath = string.Empty;
            CurrentWindow = window;
            storyboard = new Storyboard();

            TextAnim("I cried a lot, I began to see my reflection.", 1.6, 1.8, Para);
            storyboard.Begin();

            TextGuidance.Text = "Custom icons are displayed next to your name at the top of the screen. " +
                "They allow you to express yourself through a selection of images, such as flags, emojis, and more. The choice is entirely yours, pick what represents you best!";

            TipGuidance.Text = "Tip: The lesser the detail, the better.";
            StartCount();
        }

        private async void StartCount()
        {
            await AllowSkipAction();
        }

        private async Task AllowSkipAction()
        {
            await Task.Delay(5500);

            NextButton.IsHitTestVisible = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.Close();
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

        private void ProfilePictureHitCollision_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            /*
             * Steps: -
             *  1- Open a file dialog to select a file.
             *  2- we have the directory, copy the file to AppData/UDSH/Resources/Images/ProfilePicture/ (Create one if it doesn't exist)
             *  3- Add the profile picture
             */

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please pick an Image for your profile picture...";
            openFileDialog.Filter = "Image Files|*.bmp;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.wdp;*.tiff";
            openFileDialog.Multiselect = false;
            
            bool? success = openFileDialog.ShowDialog();
            if(success == true)
            {
                string SelectedImagePath = openFileDialog.FileName;
                string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
                string ProfilePicturePath = Path.Combine(AppData, "Resources", "Images", "Profile Picture");

                if (!Directory.Exists(ProfilePicturePath))
                    Directory.CreateDirectory(ProfilePicturePath);

                string[] Files = Directory.GetFiles(ProfilePicturePath);
                if (Files.Length > 0)
                    File.Delete(Files[0]);

                FileInfo ProfilePicture = new FileInfo(SelectedImagePath);
                string FinalDest = Path.Combine(ProfilePicturePath, ProfilePicture.Name);
                ProfilePicture.CopyTo(FinalDest);

                ImageEditorWindow imageEditorWindow = new ImageEditorWindow(FinalDest);
                bool? Confirm = imageEditorWindow.ShowDialog();

                if(Confirm == true)
                {
                    ProfilePictureHitCollision.IsHitTestVisible = false;
                    PickImageBorder.IsHitTestVisible = false;

                    PickedProfileCollision.IsHitTestVisible = true;

                    string TempPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Temp");

                    if (!Directory.Exists(TempPath))
                        Directory.CreateDirectory(TempPath);

                    string TempFilePath = System.IO.Path.Combine(TempPath, Guid.NewGuid().ToString() + ".png");
                    try
                    {
                        File.Copy(FinalDest, TempFilePath, true);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"ERROR: {ex.Message}");
                    }
                    PickedImage.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(TempFilePath)), Stretch = Stretch.UniformToFill };

                    if(sender is not System.Windows.Shapes.Ellipse ellipse)
                    {
                        PlaySetImageAnimation();
                    }

                    NextTextBlock.Text = "Next";
                    CurrentWindow.UserSetProfilePicture = true;
                }
            }
        }

        private void PlaySetImageAnimation()
        {
            storyboard = new Storyboard();

            DoubleAnimation opacityFadeAnimation = new DoubleAnimation();
            opacityFadeAnimation.BeginTime = TimeSpan.FromSeconds(0);
            opacityFadeAnimation.To = 0.0;
            opacityFadeAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(opacityFadeAnimation, PickImageBorder);
            Storyboard.SetTargetProperty(opacityFadeAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityFadeAnimation);

            DoubleAnimation opacityShowAnimation = new DoubleAnimation();
            opacityShowAnimation.BeginTime = TimeSpan.FromSeconds(0.5);
            opacityShowAnimation.To = 1.0;
            opacityShowAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(opacityShowAnimation, PickedImage);
            Storyboard.SetTargetProperty(opacityShowAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(opacityShowAnimation);

            storyboard.Begin();
        }

        private void Icons_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please pick an Image for your profile picture...";
            openFileDialog.Filter = "Image Files|*.bmp;*.gif;*.ico;*.jpg;*.jpeg;*.png;*.wdp;*.tiff";

            bool? success = openFileDialog.ShowDialog();
            if(success == true)
            {
                string SelectedImagePath = openFileDialog.FileName;
                string AppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
                string IconPath = Path.Combine(AppData, "Resources", "Images", "Icons");

                if (!Directory.Exists(IconPath))
                    Directory.CreateDirectory(IconPath);

                FileInfo Icon = new FileInfo(SelectedImagePath);
                string IconName = "Icon";
                string Extension = Icon.Extension;

                if (sender is System.Windows.Shapes.Rectangle rectangle)
                {
                    switch (rectangle.Name)
                    {
                        case "IconHitColl1":
                            IconName = "Icon1";
                            break;
                        case "IconHitColl2":
                            IconName = "Icon2";
                            break;
                        case "IconHitColl3":
                            IconName = "Icon3";
                            break;
                        default:
                            break;
                    }
                }

                string FinalDest = Path.Combine(IconPath, IconName + Extension);
                Icon.CopyTo(FinalDest, true);

                string TempPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Temp");

                if (!Directory.Exists(TempPath))
                    Directory.CreateDirectory(TempPath);

                string TempFilePath = System.IO.Path.Combine(TempPath, Guid.NewGuid().ToString() + ".png");
                try
                {
                    File.Copy(FinalDest, TempFilePath, true);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"ERROR: {ex.Message}");
                }

                if(sender is System.Windows.Shapes.Rectangle rect)
                {
                    switch (rect.Name)
                    {
                        case "IconHitColl1":
                            IconOne.Source = new BitmapImage(new Uri(TempFilePath));
                            break;
                        case "IconHitColl2":
                            IconTwo.Source = new BitmapImage(new Uri(TempFilePath));
                            break;
                        case "IconHitColl3":
                            IconThree.Source = new BitmapImage(new Uri(TempFilePath));
                            break;
                        default:
                            break;
                    }
                }

                NextTextBlock.Text = "Next";
                CurrentWindow.UserSetCustomIcon = true;
            }
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            GuidPopup.IsOpen = true;
        }

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            GuidPopup.IsOpen = false;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            storyboard = new Storyboard();
            CloseAnimation(UpperPara, 0.0);
            CloseAnimation(Para, 0.0);
            CloseAnimation(BottomPara, 0.0);

            CloseAnimation(LeftGrid, 0.5);
            CloseAnimation(RightGrid, 0.8);

            CloseAnimation(ButtonContainer, 1.1);
            CloseAnimation(M_Border, 1.3, true);
            storyboard.Begin();
        }

        private void CloseAnimation(DependencyObject Control, double Start, bool IsLast = false)
        {
            if (IsLast == true)
            {
                DoubleAnimation HeightAnimation = new DoubleAnimation();
                HeightAnimation.BeginTime = TimeSpan.FromSeconds(Start);
                HeightAnimation.To = 0.0;
                HeightAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

                Storyboard.SetTarget(HeightAnimation, Control);
                Storyboard.SetTargetProperty(HeightAnimation, new PropertyPath("Height"));
                storyboard.Children.Add(HeightAnimation);

                storyboard.Completed += (sender, args) =>
                {
                    WelcomeLastUserControl welcomeLastUserControl = new WelcomeLastUserControl(CurrentWindow);
                    CurrentWindow.Main.Content = welcomeLastUserControl;
                };
                return;
            }
            DoubleAnimation OpacityAnimation = new DoubleAnimation();
            OpacityAnimation.BeginTime = TimeSpan.FromSeconds(Start);
            OpacityAnimation.To = 0.0;
            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(OpacityAnimation, Control);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);
        }
    }
}
