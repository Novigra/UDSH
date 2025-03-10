// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class HeaderUserControlViewModel : ViewModelBase
    {
        #region Properties
        //private readonly IUserDataServices userDataServices;
        //private readonly Session session;
        private readonly IHeaderServices _headerServices;

        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { if(displayName != value)
                {
                    displayName = value; OnPropertyChanged(); //_headerServices.UserDataServices.DisplayName = value; (do it in a function instead of real-time update in settings)
                }
            }
        }

        private BitmapImage profilePicture;
        public BitmapImage ProfilePicture
        {
            get { return profilePicture; }
            set { profilePicture = value; OnPropertyChanged(); }
        }

        private float blankProfilePictureOpacity;
        public float BlankProfilePictureOpacity
        {
            get { return blankProfilePictureOpacity; }
            set { blankProfilePictureOpacity = value; OnPropertyChanged(); }
        }

        private BitmapImage icon1;
        public BitmapImage Icon1
        {
            get { return icon1; }
            set { icon1 = value; OnPropertyChanged(); }
        }

        private BitmapImage icon2;
        public BitmapImage Icon2
        {
            get { return icon2; }
            set { icon2 = value; OnPropertyChanged(); }
        }

        private BitmapImage icon3;
        public BitmapImage Icon3
        {
            get { return icon3; }
            set { icon3 = value; OnPropertyChanged(); }
        }

        private string projectName;
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; OnPropertyChanged(); }
        }

        private bool canPlayProjectAnimation;
        public bool CanPlayProjectAnimation
        {
            get { return canPlayProjectAnimation; }
            set
            {
                canPlayProjectAnimation = value; OnPropertyChanged();
            }
        }

        private bool isPenToolButtonClicked;
        public bool IsPenToolButtonClicked
        {
            get { return isPenToolButtonClicked; }
            set { isPenToolButtonClicked = value; OnPropertyChanged(); }
        }

        private bool canClosePopup;
        public bool CanClosePopup
        {
            get { return canClosePopup; }
            set { canClosePopup = value; OnPropertyChanged(); }
        }

        // Holds all the available commands.
        private ObservableCollection<int> quickActionsList;
        public ObservableCollection<int> QuickActionsList
        {
            get { return quickActionsList; }
            set { quickActionsList = value; OnPropertyChanged(); }
        }

        #region Quick Action Button One Properties
        private RelayCommand<Button> quickActionButton1;
        public RelayCommand<Button> QuickActionButton1
        {
            get { return quickActionButton1; }
            set { quickActionButton1 = value; OnPropertyChanged(); }
        }

        private int qButtonImage1;
        public int QButtonImage1
        {
            get { return qButtonImage1; }
            set { qButtonImage1 = value; OnPropertyChanged(); }
        }

        private bool isQAButtonEnabled1;
        public bool IsQAButtonEnabled1
        {
            get { return isQAButtonEnabled1; }
            set { isQAButtonEnabled1 = value; OnPropertyChanged(); }
        }

        private bool canPlayAssignAnim1;
        public bool CanPlayAssignAnim1
        {
            get { return canPlayAssignAnim1; }
            set { canPlayAssignAnim1 = value; OnPropertyChanged(); }
        }

        private bool canPlayReassignAnim1;
        public bool CanPlayReassignAnim1
        {
            get { return canPlayReassignAnim1; }
            set { canPlayReassignAnim1 = value; OnPropertyChanged(); }
        }

        private bool canPlayRemoveAnim1;
        public bool CanPlayRemoveAnim1
        {
            get { return canPlayRemoveAnim1; }
            set { canPlayRemoveAnim1 = value; OnPropertyChanged(); }
        }
        #endregion

        #region Quick Action Button Two Properties
        private RelayCommand<Button> quickActionButton2;
        public RelayCommand<Button> QuickActionButton2
        {
            get { return quickActionButton2; }
            set { quickActionButton2 = value; OnPropertyChanged(); }
        }

        private int qButtonImage2;
        public int QButtonImage2
        {
            get { return qButtonImage2; }
            set { qButtonImage2 = value; OnPropertyChanged(); }
        }

        private bool isQAButtonEnabled2;
        public bool IsQAButtonEnabled2
        {
            get { return isQAButtonEnabled2; }
            set { isQAButtonEnabled2 = value; OnPropertyChanged(); }
        }

        private bool canPlayAssignAnim2;
        public bool CanPlayAssignAnim2
        {
            get { return canPlayAssignAnim2; }
            set { canPlayAssignAnim2 = value; OnPropertyChanged(); }
        }

        private bool canPlayReassignAnim2;
        public bool CanPlayReassignAnim2
        {
            get { return canPlayReassignAnim2; }
            set { canPlayReassignAnim2 = value; OnPropertyChanged(); }
        }

        private bool canPlayRemoveAnim2;
        public bool CanPlayRemoveAnim2
        {
            get { return canPlayRemoveAnim2; }
            set { canPlayRemoveAnim2 = value; OnPropertyChanged(); }
        }
        #endregion

        #region Quick Action Button Three Properties
        private RelayCommand<Button> quickActionButton3;
        public RelayCommand<Button> QuickActionButton3
        {
            get { return quickActionButton3; }
            set { quickActionButton3 = value; OnPropertyChanged(); }
        }
        
        private int qButtonImage3;
        public int QButtonImage3
        {
            get { return qButtonImage3; }
            set { qButtonImage3 = value; OnPropertyChanged(); }
        }

        private bool isQAButtonEnabled3;
        public bool IsQAButtonEnabled3
        {
            get { return isQAButtonEnabled3; }
            set { isQAButtonEnabled3 = value; OnPropertyChanged(); }
        }

        private bool canPlayAssignAnim3;
        public bool CanPlayAssignAnim3
        {
            get { return canPlayAssignAnim3; }
            set { canPlayAssignAnim3 = value; OnPropertyChanged(); }
        }

        private bool canPlayReassignAnim3;
        public bool CanPlayReassignAnim3
        {
            get { return canPlayReassignAnim3; }
            set { canPlayReassignAnim3 = value; OnPropertyChanged(); }
        }

        private bool canPlayRemoveAnim3;
        public bool CanPlayRemoveAnim3
        {
            get { return canPlayRemoveAnim3; }
            set { canPlayRemoveAnim3 = value; OnPropertyChanged(); }
        }
        #endregion


        public Dictionary<int, bool> QuickActionDots { get; set; }

        private int currentDot;
        public int CurrentDot
        {
            get { return currentDot; }
            set { currentDot = value; OnPropertyChanged(); }
        }

        private bool playDotAnim;
        public bool PlayDotAnim
        {
            get { return playDotAnim; }
            set { playDotAnim = value; OnPropertyChanged(); }
        }

        private bool reverseDotAnim;
        public bool ReverseDotAnim
        {
            get { return reverseDotAnim; }
            set { reverseDotAnim = value; OnPropertyChanged(); }
        }

        private bool isPenToolButtonDisabled;
        public bool IsPenToolButtonDisabled
        {
            get { return isPenToolButtonDisabled; }
            set { isPenToolButtonDisabled = value; OnPropertyChanged(); }
        }

        private bool canEnablePenToolButton;
        public bool CanEnablePenToolButton
        {
            get { return canEnablePenToolButton; }
            set { canEnablePenToolButton = value; OnPropertyChanged(); }
        }

        // Holds the content layout for animation. Yes we will be breaking some MVVM rules in a sneaky way!
        private Grid targetGrid;
        public Grid TargetGrid
        {
            get { return targetGrid; }
            set { targetGrid = value; OnPropertyChanged(); }
        }

        private Window mainWindow;
        public Window MainWindow
        {
            get { return mainWindow; }
            set { mainWindow = value; }
        }

        private ObservableCollection<FileSystem> openFiles;
        public ObservableCollection<FileSystem> OpenFiles
        {
            get => openFiles;
            set { openFiles = value; OnPropertyChanged(); }
        }

        private FileSystem PrevSelectedFile;
        private FileSystem selectedFile;
        public FileSystem SelectedFile
        {
            get => selectedFile;
            set
            {
                PrevSelectedFile = selectedFile; selectedFile = value; OnPropertyChanged(); _headerServices.OnFileSelectionChanged(selectedFile);
            }
        }

        private CustomScrollViewer OpenPagesScrollViewer;
        private CustomScrollViewer OpenFilesListScrollViewer;

        private bool isRightScrollButtonActive;
        public bool IsRightScrollButtonActive
        {
            get => isRightScrollButtonActive;
            set { isRightScrollButtonActive = value; OnPropertyChanged(); }
        }

        private bool isLeftScrollButtonActive;
        public bool IsLeftScrollButtonActive
        {
            get => isLeftScrollButtonActive;
            set { isLeftScrollButtonActive = value; OnPropertyChanged(); }
        }

        private bool canOpenFilesList;
        public bool CanOpenFilesList
        {
            get => canOpenFilesList;
            set { canOpenFilesList = value; OnPropertyChanged(); }
        }

        private bool isOpenFilesListPopupOpen;
        public bool IsOpenFilesListPopupOpen
        {
            get => isOpenFilesListPopupOpen;
            set { isOpenFilesListPopupOpen = value; OnPropertyChanged(); }
        }

        private string saveFileText;
        public string SaveFileText
        {
            get => saveFileText;
            set { saveFileText = value; OnPropertyChanged(); }
        }

        private string deleteFileText;
        public string DeleteFileText
        {
            get => deleteFileText;
            set { deleteFileText = value; OnPropertyChanged(); }
        }

        private string projectTitleSection;
        public string ProjectTitleSection
        {
            get => projectTitleSection;
            set { projectTitleSection = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands
        public RelayCommand<Button> PlaceholderCommand => new RelayCommand<Button>(execute => { });
        public RelayCommand<Grid> LoadedContentLayoutGrid => new RelayCommand<Grid>(AssignGrid);
        public RelayCommand<Button> PenToolLeftMouseButtonDown => new RelayCommand<Button>(execute => ClosePenTool());
        public RelayCommand<object> PenToolPopupClosed => new RelayCommand<object>(execute => OnPenToolPopupClosed());

        public RelayCommand<Button> NewFile => new RelayCommand<Button>(execute => CreateNewFile());
        public RelayCommand<Button> SaveFile => new RelayCommand<Button>(execute => SaveCurrentOpenedFile());
        public RelayCommand<Button> SaveAllFiles => new RelayCommand<Button>(execute => SaveAllCurrentFiles());
        public RelayCommand<Button> DeleteFile => new RelayCommand<Button>(execute => DeleteCurrentFile());
        public RelayCommand<Button> Localization => new RelayCommand<Button>(execute => OpenLocalization());
        public RelayCommand<Button> ContentFolder => new RelayCommand<Button>(execute => OpenContentFolder());

        public RelayCommand<Button> NewProject => new RelayCommand<Button>(execute => CreateNewProject());

        public RelayCommand<string> QNewFile => new RelayCommand<string>(SetQuickAction);
        public RelayCommand<string> QSaveFile => new RelayCommand<string>(SetQuickAction);
        public RelayCommand<string> QSaveAllFiles => new RelayCommand<string>(SetQuickAction);
        public RelayCommand<string> QDeleteFile => new RelayCommand<string>(SetQuickAction);
        public RelayCommand<string> QLocalization => new RelayCommand<string>(SetQuickAction);
        public RelayCommand<string> QContentFolder => new RelayCommand<string>(SetQuickAction);

        private List<RelayCommand<Button>> ListOfQuickActionCommands;

        public RelayCommand<StackPanel> MouseEnterPenToolPopup => new RelayCommand<StackPanel>(EnableQuickActionEdit);

        public RelayCommand<ListViewItem> CloseOpenedFile => new RelayCommand<ListViewItem>(CloseFile);

        public RelayCommand<object> OpenFilesList => new RelayCommand<object>(execute => OpenCurrentFilesList(), canExecute => !IsOpenFilesListPopupOpen);
        public RelayCommand<object> ScrollRight => new RelayCommand<object>(execute => ScrollToRight());
        public RelayCommand<object> ScrollLeft => new RelayCommand<object>(execute => ScrollToLeft());
        public RelayCommand<CustomScrollViewer> HorizontalScrollViewerLoaded => new RelayCommand<CustomScrollViewer>(AssignScrollViewer);
        public RelayCommand<CustomScrollViewer> VerticalOpenFilesListScrollViewerLoaded => new RelayCommand<CustomScrollViewer>(AssignVerticalScrollViewer);

        public RelayCommand<object> OpenFilesSelectionChanged => new RelayCommand<object>(execute => PageSelectionChanged());
        #endregion

        public HeaderUserControlViewModel(IHeaderServices headerServices)
        {
            _headerServices = headerServices;
            DisplayName = _headerServices.UserDataServices.DisplayName;
            _headerServices.UserDataServices.DisplayNameChanged += UserDataServices_DisplayNameChanged;

            if (_headerServices.UserDataServices.NumberOfProjects != 0)
                ProjectName = _headerServices.UserDataServices.ActiveProject.ProjectName;

            _headerServices.UserDataServices.AddNewProjectTitle += UserDataServices_AddNewProjectTitle;

            if (_headerServices.UserDataServices.IsIconSet == true)
                LoadIcons();

            BlankProfilePictureOpacity = 0.0f;

            if (_headerServices.UserDataServices.IsProfilePictureSet == true)
                LoadProfilePicture();
            else
                BlankProfilePictureOpacity = 1.0f;

            IsPenToolButtonClicked = false;
            CanClosePopup = false;
            QuickActionsList = new ObservableCollection<int>();

            ListOfQuickActionCommands = new List<RelayCommand<Button>> { NewFile, SaveFile, SaveAllFiles, DeleteFile, Localization, ContentFolder };
            QButtonImage1 = -1;
            QButtonImage2 = -1;
            QButtonImage3 = -1;

            IsQAButtonEnabled1 = false;
            IsQAButtonEnabled2 = false;
            IsQAButtonEnabled3 = false;

            CanPlayAssignAnim1 = false;
            CanPlayReassignAnim1 = false;
            CanPlayRemoveAnim1 = false;

            CanPlayAssignAnim2 = false;
            CanPlayReassignAnim2 = false;
            CanPlayRemoveAnim2 = false;

            CanPlayAssignAnim3 = false;
            CanPlayReassignAnim3 = false;
            CanPlayRemoveAnim3 = false;

            CurrentDot = -1;
            PlayDotAnim = false;
            ReverseDotAnim = false;

            IsPenToolButtonDisabled = false;
            CanEnablePenToolButton = true;

            CanPlayProjectAnimation = false;

            OpenFiles = new ObservableCollection<FileSystem> { };
            _headerServices.UserDataServices.AddNewFile += UserDataServices_AddNewFile;

            IsRightScrollButtonActive = false;
            IsLeftScrollButtonActive = false;

            CanOpenFilesList = false;
            IsOpenFilesListPopupOpen = false;

            SaveFileText = string.Empty;
            DeleteFileText = string.Empty;
            
            if (_headerServices.UserDataServices.ActiveProject != null)
                ProjectTitleSection = _headerServices.UserDataServices.ActiveProject.ProjectName;
            else
                ProjectTitleSection = string.Empty;

            //TestScroll();

            _headerServices.UserDataServices.ItemDeleted += UserDataServices_ItemDeleted;
            _headerServices.UserDataServices.FileDetailsUpdated += UserDataServices_FileDetailsUpdated;
            _headerServices.UserDataServices.DataDragActionUpdate += UserDataServices_DataDragActionUpdate;
            _headerServices.UserDataServices.FileQuickDelete += UserDataServices_FileQuickDelete;
        }

        private void UserDataServices_FileQuickDelete(object? sender, FileSystem e)
        {
            RemoveItemFromList(e);
        }

        private void UserDataServices_DataDragActionUpdate(object? sender, DataDragActionUpdateEventArgs e)
        {
            ObservableCollection<FileSystem> Temp = new ObservableCollection<FileSystem>(OpenFiles);
            FileSystem TempSelected = SelectedFile;
            OpenFiles.Clear();

            OpenFiles = Temp;
            SelectedFile = TempSelected;
        }

        private void UserDataServices_FileDetailsUpdated(object? sender, FileDetailsUpdatedEventArgs e)
        {
            ObservableCollection<FileSystem> Temp = new ObservableCollection<FileSystem>(OpenFiles);
            FileSystem TempSelected = SelectedFile;
            OpenFiles.Clear();

            OpenFiles = Temp;
            SelectedFile = TempSelected;
        }

        private void UserDataServices_ItemDeleted(object? sender, DirectoriesEventArgs e)
        {
            string directory = e.directory;

            if (e.type.Equals("Folder"))
            {
                if (!directory.EndsWith("\\"))
                    directory += "\\";
            }
            
            string FileName = Path.GetFileNameWithoutExtension(directory);

            if (!string.IsNullOrEmpty(FileName))
            {
                foreach (var OpenFile in OpenFiles)
                {
                    if (OpenFile.FileName.Equals(FileName))
                        OpenFiles.Remove(OpenFile);
                }
            }
            else
            {
                foreach (var FileDirectory in e.directories)
                {
                    FileName = Path.GetFileNameWithoutExtension(FileDirectory);
                    foreach (var OpenFile in OpenFiles.ToList())
                    {
                        if (OpenFile.FileName.Equals(FileName))
                            OpenFiles.Remove(OpenFile);
                    }
                }

                Debug.WriteLine("I'm Here");
            }
        }

        private void TestScroll()
        {
            for(int i = 0; i < 30; ++i)
            {
                MKUserControl userControl = new MKUserControl(new MKUserControlViewModel(_headerServices.Services.GetRequiredService<IWorkspaceServices>()));
                FileSystem structure = new FileSystem()
                {
                    FileName = "First Act",
                    fileImageNormal = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKM.png")),
                    fileImageSelected = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKMSelected.png")),
                    userControl = userControl
                };

                OpenFiles.Add(structure);
                SelectedFile = structure;
            }
        }

        private void UserDataServices_AddNewFile(object? sender, Model.FileSystem e)
        {
            var file = OpenFiles.FirstOrDefault(f => f.FileID == e.FileID);

            if(file != null)
            {
                SelectedFile = file;
            }
            else
            {
                if (e.userControl == null)
                {
                    e.userControl = new DefaultUserControl(_headerServices.UserDataServices);
                    e.fileImageNormal = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));
                    e.fileImageSelected = new BitmapImage(new Uri("pack://application:,,,/Resource/Placeholder.png"));

                    switch (e.FileType)
                    {
                        case "mkm":
                            e.userControl = new MKUserControl(new MKUserControlViewModel(_headerServices.Services.GetRequiredService<IWorkspaceServices>()));
                            e.fileImageNormal = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKM.png"));
                            e.fileImageSelected = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKMSelected.png"));
                            break;
                        default:
                            break;
                    }
                }

                OpenFiles.Add(e);
                SelectedFile = e;
            }

            _headerServices.OnFileSelectionChanged(SelectedFile);
        }

        private void UserDataServices_AddNewProjectTitle(object? sender, string e)
        {
            ProjectName = e;
            CanPlayProjectAnimation = true;
        }

        private void UserDataServices_DisplayNameChanged(object? sender, string NewDisplayName)
        {
            DisplayName = NewDisplayName;
        }

        private void LoadIcons()
        {
            string AppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            string IconPath = System.IO.Path.Combine(AppData, "Resources", "Images", "Icons");


            string IconImage1 = Directory.EnumerateFiles(IconPath, "Icon1.*").First();
            string IconImage2 = Directory.EnumerateFiles(IconPath, "Icon2.*").First();
            string IconImage3 = Directory.EnumerateFiles(IconPath, "Icon3.*").First();

            if (File.Exists(IconImage1))
                Icon1 = new BitmapImage(new Uri(IconImage1));

            if (File.Exists(IconImage2))
                Icon2 = new BitmapImage(new Uri(IconImage2));

            if (File.Exists(IconImage3))
                Icon3 = new BitmapImage(new Uri(IconImage3));
        }

        private void LoadProfilePicture()
        {
            string AppData = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UDSH");
            string ProfilePicturePath = Path.Combine(AppData, "Resources", "Images", "Profile Picture");

            string File = Directory.EnumerateFiles(ProfilePicturePath).First();

            if (File != null)
                ProfilePicture = new BitmapImage(new Uri(File));
            else
                BlankProfilePictureOpacity = 1.0f;
        }

        private void AssignGrid(Grid grid)
        {
            TargetGrid = grid;
        }

        private void ClosePenTool()
        {
            IsPenToolButtonClicked = !IsPenToolButtonClicked;
            CanClosePopup = IsPenToolButtonClicked;
        }

        // A "Race" happens when pressing on the pen tool to close the popup, so there're two ways of doing it:
        // 1- Make the function work async and add delay, so when closing, the bool would be already false.
        // 2- We can add a new logic to execute/cancel (canExecute{bool}), but it would make it harder to follow the code.
        private async void OnPenToolPopupClosed()
        {
            //IsPenToolButtonClicked = false;
            await DelaySetting();
        }

        private async Task DelaySetting()
        {
            await Task.Delay(100);
            IsPenToolButtonClicked = false;
        }

        private void EnableQuickActionEdit(StackPanel rectangle)
        {
            rectangle.Focus();
        }

        public void LockPenToolButtons(KeyEventArgs e)
        {
            Debug.WriteLine("Key View Working!!!");
            if(e.Key == Key.LeftCtrl)
            {
                IsPenToolButtonDisabled = true;
                CanEnablePenToolButton = !IsPenToolButtonDisabled;
            }
        }

        public void ReleasePenToolButtons(KeyEventArgs e)
        {
            Debug.WriteLine("##### Key Stopped #####");
            if (e.Key == Key.LeftCtrl)
            {
                IsPenToolButtonDisabled = false;
                CanEnablePenToolButton = !IsPenToolButtonDisabled;
            }
        }

        private void CreateNewFile()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            Window mainWindowRef = _headerServices.UserDataServices.Session.mainWindow;
            var NewFileService = _headerServices.Services.GetRequiredService<IUserDataServices>();

            NewFileCreationWindow newFileCreationWindow = new NewFileCreationWindow(new NewFileCreationWindowViewModel(NewFileService), mainWindowRef);
            newFileCreationWindow.ShowDialog();

            Debug.WriteLine("Create a new file...");
        }

        private void SaveCurrentOpenedFile()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            if (SelectedFile == null)
                return;

            _ = SaveContent(SelectedFile);
            SelectedFile.OpenSaveMessage = false;
        }

        private async void SaveAllCurrentFiles()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            foreach (var file in OpenFiles)
            {
                await CopyRTBData(file.CurrentRichTextBox, file.InitialRichTextBox);
                _ = SaveContent(file);
                file.OpenSaveMessage = false;
            }
        }

        private void DeleteCurrentFile()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            if (SelectedFile == null)
                return;

            PopupWindow popupWindow = new PopupWindow(ProcessType.Delete, SelectedFile.FileName + "." + SelectedFile.FileType);
            bool? CanDelete = popupWindow.ShowDialog();

            if (CanDelete == true)
            {
                _headerServices.UserDataServices.FileQuickDeleteAction(SelectedFile);
            }
        }

        private void OpenLocalization()
        {
            MessageBox.Show("Opening Localization...");
        }

        private void OpenContentFolder()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            ContentWindow contentWindow = new ContentWindow(new ContentWindowViewModel(_headerServices.UserDataServices));
            contentWindow.Show();
        }

        private void CreateNewProject()
        {
            IsPenToolButtonClicked = false;
            CanClosePopup = false;

            Window mainWindowRef = _headerServices.UserDataServices.Session.mainWindow;
            var NewProjService = _headerServices.Services.GetRequiredService<IUserDataServices>();
            NewProjectCreationWindow newProjectCreationWindow = new NewProjectCreationWindow(new NewProjectCreationWindowViewModel(NewProjService), mainWindowRef);
            newProjectCreationWindow.ShowDialog();
        }

        private void SetQuickAction(string index)
        {
            int CurrentIndex = Int32.Parse(index);

            if (QuickActionsList.Count < 3 && !QuickActionsList.Contains(CurrentIndex))
            {
                CurrentDot = CurrentIndex;
                PlayDotAnim = true;
                PlayDotAnim = false;

                switch (QuickActionsList.Count)
                {
                    case 0:
                        QuickActionsList.Add(CurrentIndex);
                        QuickActionButton1 = ListOfQuickActionCommands[CurrentIndex];
                        QButtonImage1 = CurrentIndex;
                        
                        CanPlayRemoveAnim1 = false;
                        CanPlayAssignAnim1 = true;

                        IsQAButtonEnabled1 = true;
                        VisualStateManager.GoToElementState(targetGrid, "State1", true);
                        break;
                    case 1:
                        QuickActionsList.Add(CurrentIndex);
                        QuickActionButton2 = ListOfQuickActionCommands[CurrentIndex];
                        QButtonImage2 = CurrentIndex;

                        CanPlayAssignAnim2 = true;
                        CanPlayRemoveAnim2 = false;

                        IsQAButtonEnabled2 = true;
                        VisualStateManager.GoToElementState(targetGrid, "State2", true);
                        break;
                    case 2:
                        QuickActionsList.Add(CurrentIndex);
                        QuickActionButton3 = ListOfQuickActionCommands[CurrentIndex];
                        QButtonImage3 = CurrentIndex;

                        CanPlayAssignAnim3 = true;
                        CanPlayRemoveAnim3 = false;

                        IsQAButtonEnabled3 = true;
                        VisualStateManager.GoToElementState(targetGrid, "State3", true);
                        break;
                }
            }
            else if(QuickActionsList.Contains(CurrentIndex))
            {
                QuickActionsList.Remove(CurrentIndex);

                CurrentDot = CurrentIndex;
                ReverseDotAnim = true;
                ReverseDotAnim = false;

                switch (QuickActionsList.Count)
                {
                    case 0:
                        QuickActionButton1 = PlaceholderCommand;
                        //QButtonImage1 = -1;
                        IsQAButtonEnabled1 = false;

                        CanPlayRemoveAnim1 = true;
                        CanPlayAssignAnim1 = false;
                        VisualStateManager.GoToElementState(targetGrid, "State0", true);
                        break;
                    case 1:
                        if (QuickActionButton1 != ListOfQuickActionCommands[QuickActionsList[0]])
                        {
                            QuickActionButton1 = ListOfQuickActionCommands[QuickActionsList[0]];
                            QButtonImage1 = QuickActionsList[0];
                            CanPlayReassignAnim1 = true;
                            CanPlayReassignAnim1 = false;
                        }

                        QuickActionButton2 = PlaceholderCommand;
                        //QButtonImage2 = -1;
                        CanPlayRemoveAnim2 = true;
                        CanPlayAssignAnim2 = false;

                        IsQAButtonEnabled2 = false;
                        VisualStateManager.GoToElementState(targetGrid, "State1", true);
                        break;
                    case 2:
                        if (QuickActionButton1 != ListOfQuickActionCommands[QuickActionsList[0]])
                        {
                            QuickActionButton1 = ListOfQuickActionCommands[QuickActionsList[0]];
                            QButtonImage1 = QuickActionsList[0];
                            CanPlayReassignAnim1 = true;
                            CanPlayReassignAnim1 = false;
                        }

                        if (QuickActionButton2 != ListOfQuickActionCommands[QuickActionsList[1]])
                        {
                            QuickActionButton2 = ListOfQuickActionCommands[QuickActionsList[1]];
                            QButtonImage2 = QuickActionsList[1];
                            CanPlayReassignAnim2 = true;
                            CanPlayReassignAnim2 = false;
                        }

                        QuickActionButton3 = PlaceholderCommand;
                        //QButtonImage3 = -1;
                        CanPlayRemoveAnim3 = true;
                        CanPlayAssignAnim3 = false;

                        IsQAButtonEnabled3 = false;
                        VisualStateManager.GoToElementState(targetGrid, "State2", true);
                        break;
                }
            }
        }

        private void CloseFile(ListViewItem LVItem)
        {
            FileSystem? file = LVItem.DataContext as FileSystem;
            if (file != null)
            {
                CheckSaveStatus(file);

                RemoveItemFromList(file);
            }
        }

        private void RemoveItemFromList(FileSystem file)
        {
            if (file != SelectedFile)
                OpenFiles.Remove(file);
            else
            {
                int CurrentIndex = OpenFiles.IndexOf(file);
                if (CurrentIndex == 0)
                {
                    if (OpenFiles.Count > 1)
                    {
                        SelectedFile = OpenFiles[CurrentIndex + 1];
                    }
                }
                else
                {
                    SelectedFile = OpenFiles[CurrentIndex - 1];
                }

                OpenFiles.Remove(file);
            }
        }

        private void OpenCurrentFilesList()
        {
            Debug.WriteLine("Pressed on the button");
            IsOpenFilesListPopupOpen = !IsOpenFilesListPopupOpen;
        }

        private void ScrollToRight()
        {
            OpenPagesScrollViewer.SmoothScrollToHorizontalOffset(OpenPagesScrollViewer, OpenPagesScrollViewer.HorizontalOffset + 300, 0.5);
        }

        private void ScrollToLeft()
        {
            OpenPagesScrollViewer.SmoothScrollToHorizontalOffset(OpenPagesScrollViewer, OpenPagesScrollViewer.HorizontalOffset - 300, 0.5);
        }

        private void AssignScrollViewer(CustomScrollViewer scrollViewer)
        {
            OpenPagesScrollViewer = scrollViewer;
            //OpenPagesScrollViewer.ReachedMaxValue += OpenPagesScrollViewer_ReachedMaxValue; //better than scroll changed, but is it worth it?
            OpenPagesScrollViewer.ScrollChanged += OpenPagesScrollViewer_ScrollChanged;
        }

        private void AssignVerticalScrollViewer(CustomScrollViewer scrollViewer)
        {
            OpenFilesListScrollViewer = scrollViewer;
        }

        private void OpenPagesScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (OpenPagesScrollViewer.HorizontalOffset == 0)
                IsLeftScrollButtonActive = false;
            else
                IsLeftScrollButtonActive = true;

            if (OpenPagesScrollViewer.HorizontalOffset == OpenPagesScrollViewer.ScrollableWidth)
                IsRightScrollButtonActive = false;
            else
                IsRightScrollButtonActive = true;

            if (OpenPagesScrollViewer.HorizontalOffset == 0 && OpenPagesScrollViewer.HorizontalOffset == OpenPagesScrollViewer.ScrollableWidth)
                CanOpenFilesList = false;
            else
                CanOpenFilesList = true;
        }

        private void PageSelectionChanged()
        {
            if (SelectedFile != null)
            {
                SaveFileText = SelectedFile.FileName + "." + SelectedFile.FileType;
                DeleteFileText = SaveFileText;
            }
            else
            {
                SaveFileText = string.Empty;
                DeleteFileText = string.Empty;
            }
        }

        private async void CheckSaveStatus(FileSystem file)
        {
            if (file.OpenSaveMessage == true)
            {
                PopupWindow popupWindow = new PopupWindow(ProcessType.Save, file.FileName+"."+file.FileType);
                bool? CanSave = popupWindow.ShowDialog();

                if (CanSave == true)
                {
                    _ = CopyRTBData(file.CurrentRichTextBox, file.InitialRichTextBox);
                    _ = SaveContent(file);
                }
                else
                    _ = CopyRTBData(file.InitialRichTextBox, file.CurrentRichTextBox);

                file.OpenSaveMessage = false;
            }
        }

        private async Task CopyRTBData(RichTextBox richTextBox, RichTextBox targetRichTextBox)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string DocumentData = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    textRange.Save(memoryStream, DataFormats.Xaml);
                    DocumentData = Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(DocumentData)))
                {
                    TextRange targetTextRange = new TextRange(targetRichTextBox.Document.ContentStart, targetRichTextBox.Document.ContentEnd);
                    targetTextRange.Load(memoryStream, DataFormats.Xaml);
                }
            });
        }

        private async Task SaveContent(FileSystem file)
        {
            if (file == null)
                return;

            FileManager fileManager = new FileManager();
            BlockCollection Blocks = file.CurrentRichTextBox.Document.Blocks;

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(file.FileDirectory, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    fileManager.UpdateFileData(xmlWriter, file, Blocks);
                }

                FileStructure fileStructure = new FileStructure();
                fileStructure.UpdateFileSize(file);
                _headerServices.UserDataServices.SaveUserDataAsync();
            });
        }

        /*
         * TODO List
         * 
         * Pen Tool:
         * - Do each button commands for the pen tool.[IN-PROGRESS]
        */
    }
}
