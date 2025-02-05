using UDSH.Model;

namespace UDSH.Services
{
    class UserDataServices : IUserDataServices
    {
        private readonly Session session;
        public event EventHandler<string> DisplayNameChanged;
        public event EventHandler<string> AddNewProjectTitle;
        public event EventHandler<FileSystem> AddNewFile;
        public event EventHandler<FileSystem> AddNewFileToContent;
        public event EventHandler<FileDetailsUpdatedEventArgs> FileDetailsUpdated;
        public event EventHandler<DirectoriesEventArgs> ItemDeleted;
        public event EventHandler<string> ItemDeletedSideContent;
        public event EventHandler<DataDragActionUpdateEventArgs> DataDragActionUpdate;
        //public event EventHandler<FileSystem> AddFileFromContent;

        public string DisplayName
        {
            get => session.User.DisplayName;
            set
            {
                if (session.User.DisplayName != value)
                {
                    session.User.DisplayName = value;
                    SaveUserDataAsync();
                    DisplayNameChanged?.Invoke(this, value);
                }
            }
        }

        public Session Session { get { return session; } }

        public Project ActiveProject
        {
            get => session.CurrentProject;
        }

        public FileSystem CurrentFile
        {
            get => session.CurrentFile;
        }

        public int NumberOfProjects { get => session.NumberOfProjects; }

        public bool IsProfilePictureSet { get => session.User.IsProfilePictureSet; }

        public bool IsIconSet { get => session.User.IsCustomIconSet; }

        public UserDataServices(Session session)
        {
            this.session = session;
        }

        public async Task LoadUserDataAsync()
        {
            await Task.Run(() => session.LoadUserData());
        }

        public async Task SaveUserDataAsync()
        {
            await Task.Run(() => session.SaveUserData());
        }

        public async Task CreateNewProjectAsync(string NewProjectName, string ProjectVersion, bool IsSecured, string Password)
        {
            await Task.Run(() => session.CreateNewProject(NewProjectName, ProjectVersion, IsSecured, Password));
            if (session.NumberOfProjects == 1)
                AddNewProjectTitle?.Invoke(this, NewProjectName);
        }

        public async Task CreateNewFileAsync(string NewFileName, string FileType, string ProjectDirectory)
        {
            await Task.Run(() => session.CreateNewFile(NewFileName, FileType, ProjectDirectory));

            if (CurrentFile != null)
            {
                AddNewFile?.Invoke(this, CurrentFile);
                AddNewFileToContent?.Invoke(this, CurrentFile);
            }
        }

        public async Task UpdateFileDetailsAsync(ContentFileStructure SelectedItem, string OldDirectory, int CurrentLevel)
        {
            await Task.Run(() => session.UpdateFileDetails());
            FileDetailsUpdated?.Invoke(this, new FileDetailsUpdatedEventArgs(SelectedItem, OldDirectory, CurrentLevel));
        }

        public void AddFileToHeader(FileSystem file)
        {
            if (file != null)
                AddNewFile?.Invoke(this, file);
        }

        public async Task FileDeletedAsync(string directory, string[] directories, string type)
        {
            await Task.Run(() => session.UpdateFileDetails());

            if (directory != null)
            {
                ItemDeletedSideContent.Invoke(this, directory);
                ItemDeleted.Invoke(this, new DirectoriesEventArgs(directory, directories, type));
            }
        }

        public async Task DataDetailsDragActionUpdateAsync(List<ContentFileStructure> SelectedItems, ContentFileStructure TargetItem, string CurrentDirectory, Queue<FileSystem> EditFiles)
        {
            await Task.Run(() => session.UpdateFileDetails());

            DataDragActionUpdate.Invoke(this, new DataDragActionUpdateEventArgs(SelectedItems, TargetItem, CurrentDirectory, EditFiles));
        }
    }
}
