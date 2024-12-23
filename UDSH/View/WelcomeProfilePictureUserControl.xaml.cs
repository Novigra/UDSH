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
        private string TempFilePath;
        private NewUserStartupWindow CurrentWindow;
        private Storyboard storyboard;
        public WelcomeProfilePictureUserControl(NewUserStartupWindow window)
        {
            InitializeComponent();

            TempFilePath = string.Empty;
            CurrentWindow = window;
            storyboard = new Storyboard();

            TextAnim("I cried a lot, I began to see my reflection.", 1.6, 1.8, Para);
            storyboard.Begin();
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

                    TempFilePath = System.IO.Path.GetTempFileName();
                    TempFilePath = System.IO.Path.ChangeExtension(TempFilePath, ".png");
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
    }
}
