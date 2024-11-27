using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UDSH.Model;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    class NewFileCreationWindowViewModel : ViewModelBase
    {
        private bool isItemSelected;
        public bool IsItemSelected
        {
            get { return isItemSelected; }
            set { isItemSelected = value; OnPropertyChanged(); }
        }

        private bool canCreateFile;
        public bool CanCreateFile
        {
            get { return canCreateFile; }
            set { canCreateFile = value; OnPropertyChanged(); }
        }

        private string currentDatatype;
        public string CurrentDatatype
        {
            get { return currentDatatype; }
            set { currentDatatype = value; OnPropertyChanged(); }
        }

        private string newFileName;
        public string NewFileName
        {
            get { return newFileName; }
            set { newFileName = value; OnPropertyChanged(); }
        }

        private TextBlock textBlockTarget;
        public TextBlock TextBlockTarget
        {
            get { return textBlockTarget; }
            set { textBlockTarget = value; OnPropertyChanged(); }
        }

        private NewFileNameGenerator newFileNameGenerator;
        private string Text;

        public Window CurrentWindow { get; set; }
        public Grid? GridTarget { get; set; }
        public RelayCommand<Button> CloseWindow => new RelayCommand<Button>(execute => CloseNewFileProcessWindow());
        public RelayCommand<Grid> LoadedGrid => new RelayCommand<Grid>(OnGridLoaded);
        public RelayCommand<TextBlock> LoadedText => new RelayCommand<TextBlock>(OnTextBlockLoaded);
        public RelayCommand<string> SelectDataType => new RelayCommand<string>(SelectData);
        public RelayCommand<Button> NewFileCreation => new RelayCommand<Button>(execute => CreateNewFile());
        public RelayCommand<Button> FileNameChanged => new RelayCommand<Button>(execute => UpdateCreationStatus());

        public NewFileCreationWindowViewModel(Window window)
        {
            CurrentWindow = window;
            IsItemSelected = false;
            CanCreateFile = false;
            NewFileName = string.Empty;

            newFileNameGenerator = new NewFileNameGenerator();
            Text = newFileNameGenerator.Generate(Language.English, MediaType.Music);
        }

        private void OnGridLoaded(Grid grid)
        {
            GridTarget = grid;
        }

        private void OnTextBlockLoaded(TextBlock text)
        {
            TextBlockTarget = text;
        }

        private void CloseNewFileProcessWindow()
        {
            var Storyboard = new Storyboard();
            var ClosingTranslateAnimation = new DoubleAnimation
            {
                To = 100.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingTranslateAnimation, GridTarget);
            Storyboard.SetTargetProperty(ClosingTranslateAnimation, new PropertyPath("RenderTransform.(TranslateTransform.Y)"));
            Storyboard.Children.Add(ClosingTranslateAnimation);

            var ClosingOpacityAnimation = new DoubleAnimation
            {
                To = 0.0f,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(ClosingOpacityAnimation, GridTarget);
            Storyboard.SetTargetProperty(ClosingOpacityAnimation, new PropertyPath("Opacity"));
            Storyboard.Children.Add(ClosingOpacityAnimation);

            Storyboard.Completed += (sender, args) => { CurrentWindow.Close(); };
            Storyboard.Begin();
        }

        public void PlayHighlightedText()
        {
            Storyboard story = new Storyboard();
            story.BeginTime = TimeSpan.FromSeconds(1.2);
            story.FillBehavior = FillBehavior.HoldEnd;

            DiscreteStringKeyFrame discreteStringKeyFrame;
            StringAnimationUsingKeyFrames stringAnimationUsingKeyFrames = new StringAnimationUsingKeyFrames();
            stringAnimationUsingKeyFrames.Duration = new Duration(TimeSpan.FromSeconds(1));

            string Conj = string.Empty;
            foreach (char i in Text)
            {
                Conj += i;

                discreteStringKeyFrame = new DiscreteStringKeyFrame();
                discreteStringKeyFrame.KeyTime = KeyTime.Paced;
                discreteStringKeyFrame.Value = Conj;

                stringAnimationUsingKeyFrames.KeyFrames.Add(discreteStringKeyFrame);
            }
            Storyboard.SetTargetName(stringAnimationUsingKeyFrames, TextBlockTarget.Name);
            Storyboard.SetTargetProperty(stringAnimationUsingKeyFrames, new PropertyPath("Text"));
            story.Children.Add(stringAnimationUsingKeyFrames);

            story.Begin(TextBlockTarget);

            Debug.WriteLine("Animation is playing...");
        }

        private void SelectData(string dataType)
        {
            IsItemSelected = true;
            CurrentDatatype = dataType;
        }

        private void CreateNewFile()
        {
            /*
             * When creating what do we care about?
             * - File name
             * - File type
             * - save file
             * - close creation window
             * - start type window
             * - add it to header list of files
             * - add it to the sidebar
             * - add it to the localization
             * - add it to the content folder
             * - later on can't use the same file name.
             * 
             */

            Debug.WriteLine("Create A New File");
        }

        private void UpdateCreationStatus()
        {
            if(string.IsNullOrEmpty(NewFileName))
                CanCreateFile = false;
            else
                CanCreateFile = true;
        }
    }
}
