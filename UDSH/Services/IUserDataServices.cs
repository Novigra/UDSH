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
        //event EventHandler<FileSystem> AddFileFromContent;

        Task LoadUserDataAsync();
        Task SaveUserDataAsync();

        Task CreateNewProjectAsync(string NewProjectName, string ProjectVersion, bool IsSecured, string Password);

        Task CreateNewFileAsync(string NewFileName, string FileType, string ProjectDirectory);
        void AddFileToHeader(FileSystem file);
    }
}
