using UDSH.Model;

namespace UDSH.Services
{
    public interface IUserDataServices
    {
        Session Session { get; }
        string DisplayName { get; set; }
        Project ActiveProject { get; }
        int NumberOfProjects { get; }
        bool IsProfilePictureSet { get; }
        bool IsIconSet { get; }

        event EventHandler<string> DisplayNameChanged;
        event EventHandler<string> AddNewProjectTitle;

        Task LoadUserDataAsync();
        Task SaveUserDataAsync();

        Task CreateNewProjectAsync(string NewProjectName, string ProjectVersion, bool IsSecured, string Password);
    }
}
