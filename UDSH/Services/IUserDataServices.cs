using UDSH.Model;

namespace UDSH.Services
{
    public interface IUserDataServices
    {
        Session Session { get; }
        string DisplayName { get; set; }
        Project ActiveProject { get; }
        FileSystem CurrentFile { get; }
        int NumberOfProjects { get; }
        bool IsProfilePictureSet { get; }
        bool IsIconSet { get; }

        event EventHandler<string> DisplayNameChanged;
        event EventHandler<string> AddNewProjectTitle;
        event EventHandler<FileSystem> AddNewFile;
        event EventHandler<FileSystem> AddNewFileToContent;
        event EventHandler<FileDetailsUpdatedEventArgs> FileDetailsUpdated;
        event EventHandler<DirectoriesEventArgs> ItemDeleted;
        event EventHandler<string> ItemDeletedSideContent;
        event EventHandler<DataDragActionUpdateEventArgs> DataDragActionUpdate;
        //event EventHandler<FileSystem> AddFileFromContent;

        Task LoadUserDataAsync();
        Task SaveUserDataAsync();

        Task CreateNewProjectAsync(string NewProjectName, string ProjectVersion, bool IsSecured, string Password);

        Task CreateNewFileAsync(string NewFileName, string FileType, string ProjectDirectory);
        Task UpdateFileDetailsAsync(ContentFileStructure SelectedItem, string OldDirectory, int CurrentLevel);
        Task FileDeletedAsync(string directory, string[] directories, string type);
        Task DataDetailsDragActionUpdateAsync(List<ContentFileStructure> SelectedItems, ContentFileStructure TargetItem, string CurrentDirectory);

        void AddFileToHeader(FileSystem file);
    }
}
