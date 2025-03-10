// Copyright (C) 2025 Mohammed Kenawy
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;

namespace UDSH.ViewModel
{
    public class NewFileCreationWindowViewModel : ViewModelBase
    {
        private readonly IUserDataServices _userDataServices;
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

        private Stack<string[]> Temp = new Stack<string[]>();
        private Stack<ObservableCollection<string>> PrevStack;
        private Stack<string> CurrentDirectoriesStack;
        private ObservableCollection<string> PrevDirectories;
        private ObservableCollection<string> directories;
        public ObservableCollection<string> Directories
        {
            get { return directories; }
            set { directories = value; OnPropertyChanged(); }
        }

        private string selectedDirectory;
        public string SelectedDirectory
        {
            get { return selectedDirectory; }
            set { selectedDirectory = value; OnPropertyChanged(); }
        }

        private NewFileNameGenerator newFileNameGenerator;
        private string Text;

        public Window CurrentWindow { get; set; }
        public Grid? GridTarget { get; set; }

        private Project CurrentProject;
        string[] ProjectDirectories;

        private string displayDest;
        public string DisplayDest
        {
            get { return displayDest; }
            set { displayDest = value; OnPropertyChanged(); }
        }

        private string inputDest;
        public string InputDest
        {
            get { return inputDest; }
            set { inputDest = value; OnPropertyChanged(); }
        }

        string CurrentSelectedItem;
        private string FinalDest;
        private string FinalProjectDirectory;

        private bool isLeftScrollButtonActive;
        public bool IsLeftScrollButtonActive
        {
            get { return isLeftScrollButtonActive; }
            set { isLeftScrollButtonActive = value; OnPropertyChanged(); }
        }

        private bool isRightScrollButtonActive;
        public bool IsRightScrollButtonActive
        {
            get { return isRightScrollButtonActive; }
            set { isRightScrollButtonActive = value; OnPropertyChanged(); }
        }

        private bool isDirectoryButtonButtonActive;
        public bool IsDirectoryButtonButtonActive
        {
            get { return isDirectoryButtonButtonActive; }
            set { isDirectoryButtonButtonActive = value; OnPropertyChanged(); }
        }

        // we will check after each input so performance is crucial
        private HashSet<char> InvalidCharacters = new HashSet<char>(@"\:*?""<>|");
        private HashSet<char> InvalidCharactersFile = new HashSet<char>(@"/\:*?""<>|");
        private string InvalidDirectory = "//";
        public TextBlock textBlock;

        private bool canShowWarningMessage;
        public bool CanShowWarningMessage
        {
            get { return canShowWarningMessage; }
            set { canShowWarningMessage = value; OnPropertyChanged(); }
        }

        private bool canShowFileWarningMessage;
        public bool CanShowFileWarningMessage
        {
            get { return canShowFileWarningMessage; }
            set { canShowFileWarningMessage = value; OnPropertyChanged(); }
        }

        private string fileWarningMessage;
        public string FileWarningMessage
        {
            get { return fileWarningMessage; }
            set { fileWarningMessage = value; OnPropertyChanged(); }
        }

        private bool canShowDirectory;
        public bool CanShowDirectory
        {
            get { return canShowDirectory; }
            set { canShowDirectory = value; OnPropertyChanged(); }
        }

        private bool directorystayOpen;
        public bool DirectorystayOpen
        {
            get { return directorystayOpen; }
            set { directorystayOpen = value; OnPropertyChanged(); }
        }

        public RelayCommand<Button> CloseWindow => new RelayCommand<Button>(execute => CloseNewFileProcessWindow());
        public RelayCommand<Grid> LoadedGrid => new RelayCommand<Grid>(OnGridLoaded);
        public RelayCommand<TextBlock> LoadedText => new RelayCommand<TextBlock>(OnTextBlockLoaded);
        public RelayCommand<string> SelectDataType => new RelayCommand<string>(SelectData);
        public RelayCommand<Button> NewFileCreation => new RelayCommand<Button>(execute => CreateNewFile(), canExecute => CanCreateFile);
        public RelayCommand<Button> FileNameChanged => new RelayCommand<Button>(execute => UpdateCreationStatus());

        public RelayCommand<Button> NextPage => new RelayCommand<Button>(execute => EnterNextPage());
        public RelayCommand<Button> PrevPage => new RelayCommand<Button>(execute => GoPrevPage());

        public RelayCommand<CustomScrollViewer> ScrollRight => new RelayCommand<CustomScrollViewer>(ScrollDirectoryRight);
        public RelayCommand<CustomScrollViewer> ScrollLeft => new RelayCommand<CustomScrollViewer>(ScrollDirectoryLeft);
        public RelayCommand<CustomScrollViewer> DirectoryScrollChanged => new RelayCommand<CustomScrollViewer>(UpdateScrollVisuals);

        public RelayCommand<object> DirectoryTextChanged => new RelayCommand<object>(execute => CheckTextInputStatus());

        public RelayCommand<object> OpenDirectoryButton => new RelayCommand<object>(execute => OpenDirectory());
        public RelayCommand<object> MouseEnterDirectoryBorder => new RelayCommand<object>(execute => ChangeDirectoryPopupStatus());
        public RelayCommand<object> MouseLeaveDirectoryBorder => new RelayCommand<object>(execute => ChangeDirectoryPopupStatus());

        public RelayCommand<object> ConfirmButton => new RelayCommand<object>(execute => UpdateFinalDirectory());

        public NewFileCreationWindowViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;
            IsItemSelected = false;
            CanCreateFile = false;
            NewFileName = string.Empty;

            newFileNameGenerator = new NewFileNameGenerator();
            Text = newFileNameGenerator.Generate(Language.English, MediaType.Music);

            PrevStack = new Stack<ObservableCollection<string>>();
            CurrentDirectoriesStack = new Stack<string>();

            PrevDirectories = new ObservableCollection<string>();
            Directories = new ObservableCollection<string>();
            InputDest = string.Empty;

            CurrentProject = _userDataServices.ActiveProject;
            DisplayDest = CurrentProject.ProjectName + '/';
            FinalDest = CurrentProject.ProjectDirectory + '\\';
            FinalProjectDirectory = CurrentProject.ProjectDirectory + '\\';
            ProjectDirectories = Directory.GetDirectories(CurrentProject.ProjectDirectory);
            
            InitializeDirectories();

            IsDirectoryButtonButtonActive = true;
            CanShowWarningMessage = false;
            CanShowFileWarningMessage = false;

            IsLeftScrollButtonActive = false;
            IsRightScrollButtonActive = false;

            CanShowDirectory = false;
            DirectorystayOpen = false;
            // TODO: Change Files Opacity. Use Converter for display text and enter a fixed text for the input text
        }

        private void InitializeDirectories()
        {
            foreach (var directory in ProjectDirectories)
            {
                string[] TextSplit = directory.Split('\\');
                int StartIndex = Array.IndexOf(TextSplit, CurrentProject.ProjectName);
                Directories.Add(TextSplit[StartIndex+1]);
            }

            Directories = new ObservableCollection<string>(Directories.OrderBy(name => name, StringComparer.Ordinal));
        }

        private void OnGridLoaded(Grid grid)
        {
            GridTarget = grid;
        }

        private void OnTextBlockLoaded(TextBlock text)
        {
            TextBlockTarget = text;
        }

        private void CloseNewFileProcessWindow(bool IsCreateNewFile = false)
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

            DoesSameFileTypeExist();
        }

        private void CreateNewFile()
        {
            /*
             * When creating what do we care about?
             * - File name [x]
             * - File type [x]
             * - save file [x]
             * - close creation window [x]
             * - start type window [x]
             * - add it to header list of files [x]
             * - add it to the sidebar [x]
             * - add it to the localization
             * - add it to the content folder
             * - later on can't use the same file name.
             * 
             */

            _userDataServices.CreateNewFileAsync(NewFileName, CurrentDatatype, FinalProjectDirectory);
            CloseNewFileProcessWindow();
            Debug.WriteLine("Create A New File");
        }

        private void UpdateCreationStatus()
        {
            DoesSameFileTypeExist();
        }

        private void DoesSameFileTypeExist()
        {
            if (!FinalProjectDirectory.EndsWith("\\"))
                FinalProjectDirectory += "\\";

            string fileDirectory = FinalProjectDirectory + NewFileName + "." + CurrentDatatype;

            if (File.Exists(fileDirectory))
            {
                CanCreateFile = false;

                CanShowFileWarningMessage = true;
                FileWarningMessage = "A file with the same name and file type already exists.   ";
            }
            else if (NewFileName.Any(c => InvalidCharactersFile.Contains(c)) == true)
            {
                CanCreateFile = false;

                CanShowFileWarningMessage = true;
                FileWarningMessage = "You can’t use these characters: /\\:*?\"<>|   ";
            }
            else if (string.IsNullOrEmpty(NewFileName))
            {
                CanCreateFile = false;

                CanShowFileWarningMessage = false;
            }
            else if (IsItemSelected == true)
            {
                CanCreateFile = true;

                CanShowFileWarningMessage = false;
            }
        }

        private void EnterNextPage()
        {
            PrevDirectories = new ObservableCollection<string>(Directories);
            PrevStack.Push(PrevDirectories);

            CurrentSelectedItem = SelectedDirectory;
            CurrentDirectoriesStack.Push(CurrentSelectedItem);

            FinalDest += CurrentSelectedItem + '\\';
            DisplayDest += CurrentSelectedItem + '/';
            string[] CurrentDirectories = Array.Empty<string>();

            foreach (var directory in ProjectDirectories)
            {
                if(directory.Contains(CurrentSelectedItem))
                {
                    CurrentDirectories = Directory.GetDirectories(directory);
                }
            }
            if(CurrentDirectories.Length > 0)
            {
                ProjectDirectories = CurrentDirectories;
            }
            

            Directories.Clear();
            foreach (var directory in CurrentDirectories)
            {
                string[] TextSplit = directory.Split('\\');
                int StartIndex = Array.IndexOf(TextSplit, CurrentSelectedItem);
                Directories.Add(TextSplit[StartIndex + 1]);
            }

            Directories = new ObservableCollection<string>(Directories.OrderBy(name => name, StringComparer.Ordinal));
        }

        private void GoPrevPage()
        {
            if (CurrentDirectoriesStack.Count > 0)
            {
                string RemovedDir = CurrentDirectoriesStack.Pop();
                int RemoveIndex = FinalDest.IndexOf(RemovedDir);
                if (RemoveIndex != -1)
                {
                    FinalDest = FinalDest.Substring(0, RemoveIndex);
                    ProjectDirectories = Directory.GetDirectories(FinalDest);
                }

                RemoveIndex = DisplayDest.IndexOf(RemovedDir);
                if (RemoveIndex != -1)
                    DisplayDest = DisplayDest.Substring(0, RemoveIndex);
            }

            if (PrevStack.Count > 0)
                Directories = PrevStack.Pop();
        }

        private void ScrollDirectoryRight(CustomScrollViewer scrollViewer)
        {
            scrollViewer.SmoothScrollToHorizontalOffset(scrollViewer, scrollViewer.HorizontalOffset + 300, 0.5);
        }

        private void ScrollDirectoryLeft(CustomScrollViewer scrollViewer)
        {
            scrollViewer.SmoothScrollToHorizontalOffset(scrollViewer, scrollViewer.HorizontalOffset - 300, 0.5);
        }

        private void UpdateScrollVisuals(CustomScrollViewer scrollViewer)
        {
            if (scrollViewer.HorizontalOffset == 0)
                IsLeftScrollButtonActive = false;
            else
                IsLeftScrollButtonActive = true;

            if (scrollViewer.HorizontalOffset == scrollViewer.ScrollableWidth)
                IsRightScrollButtonActive = false;
            else
                IsRightScrollButtonActive = true;
        }

        private void CheckTextInputStatus()
        {
            if (InputDest.Any(c => InvalidCharacters.Contains(c)) == true)
            {
                CanShowWarningMessage = true;
                textBlock.Text = "You can’t use these characters: \\:*?\"<>|\t";
            }
            else if (InputDest.Contains(InvalidDirectory) == true)
            {
                CanShowWarningMessage = true;
                textBlock.Text = "You can’t enter an empty directory. Remove \"//\"\t";
            }
            else
                CanShowWarningMessage = false;

            IsDirectoryButtonButtonActive = !CanShowWarningMessage;
        }

        private void OpenDirectory()
        {
            CanShowDirectory = !CanShowDirectory;
        }

        private void ChangeDirectoryPopupStatus()
        {
            DirectorystayOpen = !DirectorystayOpen;
        }

        private void UpdateFinalDirectory()
        {
            string[] directories = InputDest.Split('/');
            string Result = FinalDest;
            foreach (string directory in directories)
            {
                Result += directory + "\\";
            }

            // Remove whitespaces
            FinalProjectDirectory = string.Join("\\", Result.Split("\\").Select(seq => seq.Trim()).Where(seq => !string.IsNullOrEmpty(seq)));
            CanShowDirectory = false;

            DoesSameFileTypeExist();
        }
    }
}
