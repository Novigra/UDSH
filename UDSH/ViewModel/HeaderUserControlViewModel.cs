using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    public class FileStructure
    {
        public FileSystem file { get; set; } // File itself
        public BitmapImage fileImageNormal { get; set; } // Image Icon - Normal(Not Selected Item)
        public BitmapImage fileImageSelected { get; set; } // Image Icon - Normal(Not Selected Item)
        public MKUserControl UserControl { get; set; } // Associated UserControl
    }

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

        public ObservableCollection<FileStructure> OpenFiles { get; set; }

        private FileStructure PrevSelectedFile;
        private FileStructure selectedFile;
        public FileStructure SelectedFile
        {
            get => selectedFile;
            set
            {
                PrevSelectedFile = selectedFile; selectedFile = value; OnPropertyChanged(); _headerServices.OnFileSelectionChanged(selectedFile);
            }
        }

        private CustomScrollViewer OpenPagesScrollViewer;

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

        public RelayCommand<object> OpenFilesList => new RelayCommand<object>(execute => OpenCurrentFilesList());
        public RelayCommand<object> ScrollRight => new RelayCommand<object>(execute => ScrollToRight());
        public RelayCommand<object> ScrollLeft => new RelayCommand<object>(execute => ScrollToLeft());
        public RelayCommand<CustomScrollViewer> HorizontalScrollViewerLoaded => new RelayCommand<CustomScrollViewer>(AssignScrollViewer);
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

            OpenFiles = new ObservableCollection<FileStructure> { };
            _headerServices.UserDataServices.AddNewFile += UserDataServices_AddNewFile;

            IsRightScrollButtonActive = false;
            IsLeftScrollButtonActive = false;

            //TestScroll();
        }

        private void TestScroll()
        {
            
            FileSystem File = new FileSystem()
            {
                FileName = "First Act"
            };

            for(int i = 0; i < 30; ++i)
            {
                MKUserControl userControl = new MKUserControl(new MKUserControlViewModel(_headerServices.Services.GetRequiredService<IWorkspaceServices>()));
                FileStructure structure = new FileStructure()
                {
                    file = File,
                    UserControl = userControl
                };

                OpenFiles.Add(structure);
                SelectedFile = structure;
            }
        }

        private void UserDataServices_AddNewFile(object? sender, Model.FileSystem e)
        {
            MKUserControl userControl = new MKUserControl(new MKUserControlViewModel(_headerServices.Services.GetRequiredService<IWorkspaceServices>()));

            FileStructure structure = new FileStructure()
            {
                file = e,
                fileImageNormal = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKM.png")),
                fileImageSelected = new BitmapImage(new Uri("pack://application:,,,/Resource/OpenFileMKMSelected.png")),
                UserControl = userControl
            };

            OpenFiles.Add(structure);
            SelectedFile = structure;

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
            MessageBox.Show("Saved MyHouseWad.mkc...");
        }

        private void SaveAllCurrentFiles()
        {
            MessageBox.Show("Saved All Files...");
        }

        private void DeleteCurrentFile()
        {
            MessageBox.Show("Deleted MyHouseWad.mkc...");
        }

        private void OpenLocalization()
        {
            MessageBox.Show("Opening Localization...");
        }

        private void OpenContentFolder()
        {
            MessageBox.Show("Opening Content Folder...");
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

            

            //if(QuickActionDots.)

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
            FileStructure? file = LVItem.DataContext as FileStructure;
            if (file != null)
            {
                
                if (file != SelectedFile)
                    OpenFiles.Remove(file);
                else
                {
                    int CurrentIndex = OpenFiles.IndexOf(file);
                    if (CurrentIndex == 0)
                    {
                        if(OpenFiles.Count > 1)
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
        }

        private void OpenCurrentFilesList()
        {
            
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
        }

        /*
         * TODO List
         * 
         * Pen Tool:
         * - Do each button commands for the pen tool.[IN-PROGRESS]
        */
    }
}
