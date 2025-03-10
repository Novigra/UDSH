// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using static System.Net.Mime.MediaTypeNames;

namespace UDSH.ViewModel
{
    public class NewProjectCreationWindowViewModel : ViewModelBase
    {
        private readonly IUserDataServices _userDataServices;
        public Window AssociatedWindow;

        private string projectVersion;
        public string ProjectVersion
        {
            get { return projectVersion; }
            set { projectVersion = value; OnPropertyChanged(); }
        }

        private string newProjectName;
        public string NewProjectName
        {
            get { return newProjectName; }
            set { newProjectName = value; OnPropertyChanged(); }
        }

        private string newPassword;
        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; OnPropertyChanged(); }
        }

        private bool canSecure;
        public bool CanSecure
        {
            get { return canSecure; }
            set { canSecure = value; OnPropertyChanged(); }
        }

        private bool containsTitleText;
        public bool ContainsTitleText
        {
            get { return containsTitleText; }
            set { containsTitleText = value; OnPropertyChanged(); }
        }

        private bool containsPassText;
        public bool ContainsPassText
        {
            get { return containsPassText; }
            set { containsPassText = value; OnPropertyChanged(); }
        }

        public RelayCommand<TextBlock> GridLoaded => new RelayCommand<TextBlock>(PlayUserAnimation);
        public RelayCommand<TextBlock> TitleLoaded => new RelayCommand<TextBlock>(PlayTitleAnimation);
        public RelayCommand<TextBlock> CheckboxMouseLeftButtonDown => new RelayCommand<TextBlock>(StartPassAnim);

        public RelayCommand<TextBox> ProjectNameChanged => new RelayCommand<TextBox>(UpdateVisuals);
        public RelayCommand<TextBox> ProjectPassChanged => new RelayCommand<TextBox>(UpdateVisuals);

        public RelayCommand<Grid> CloseWindow => new RelayCommand<Grid>(Close);
        public RelayCommand<Grid> NewProjectCreation => new RelayCommand<Grid>(CreateNewProject);



        public NewProjectCreationWindowViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            NewProjectName = string.Empty;
            NewPassword = string.Empty;

            CanSecure = false;
            ContainsTitleText = false;
            ContainsPassText = false;

            Version version = Assembly.GetExecutingAssembly().GetName().Version!;
            if (version != null)
                ProjectVersion = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private void PlayUserAnimation(TextBlock textBlock)
        {
            string UserDisplayName = _userDataServices.DisplayName;
            string[] NameCollection = UserDisplayName.Split(' ');
            string OutputName = NameCollection[0] + "!";

            PlayHighlightAnimation(OutputName, 1.0, textBlock);
        }

        private void PlayTitleAnimation(TextBlock textBlock)
        {
            string text = "Enter Project Name...";
            double start = 1.5;
            PlayHighlightAnimation(text, start, textBlock);
        }
        private void StartPassAnim(TextBlock textBlock)
        {
            if (CanSecure == false)
            {
                string Pass = "Enter Password...";
                double Start = 1.5;
                PlayHighlightAnimation(Pass, Start, textBlock);
            }
            else
            {
                PlayHighlightAnimation("", 0.2, textBlock);
            }
        }

        private void PlayHighlightAnimation(string TargetText, double BeginTime, TextBlock TargetTextBlock)
        {
            string OutputName = TargetText;

            Storyboard story = new Storyboard();
            story.BeginTime = TimeSpan.FromSeconds(BeginTime);
            story.FillBehavior = FillBehavior.HoldEnd;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(1));

            string Conj = string.Empty;
            foreach (char i in OutputName)
            {
                Conj += i;

                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                discreteStringKeyFrame.Value = Conj;

                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, TargetTextBlock.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath("Text"));
            story.Children.Add(stringAnimationUsingKeyFrames);
            if (CanSecure == true)
                story.Completed += (sender, args) => { NewPassword = string.Empty; };

            story.Begin(TargetTextBlock);
        }

        private void UpdateVisuals(TextBox textBlock)
        {
            ContainsTitleText = (NewProjectName == "") ? false : true;
            ContainsPassText = (NewPassword == "") ? false : true;
        }

        private void Close(Grid grid)
        {
            var Storyboard = new Storyboard();
            var ClosingTranslateAnimation = new DoubleAnimation
            {
                To = 100.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingTranslateAnimation, grid);
            Storyboard.SetTargetProperty(ClosingTranslateAnimation, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            Storyboard.Children.Add(ClosingTranslateAnimation);

            var ClosingOpacityAnimation = new DoubleAnimation
            {
                To = 0.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingOpacityAnimation, grid);
            Storyboard.SetTargetProperty(ClosingOpacityAnimation, new PropertyPath("Opacity"));
            Storyboard.Children.Add(ClosingOpacityAnimation);

            Storyboard.Completed += (sender, args) => { AssociatedWindow.Close(); };
            Storyboard.Begin();
        }

        private void CreateNewProject(Grid grid)
        {
            _userDataServices.CreateNewProjectAsync(NewProjectName, ProjectVersion, CanSecure, NewPassword);
            Close(grid);
        }
    }
}
