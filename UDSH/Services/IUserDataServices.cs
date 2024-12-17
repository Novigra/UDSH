using UDSH.Model;

namespace UDSH.Services
{
    public interface IUserDataServices
    {
        Session Session { get; }
        string DisplayName { get; set; }
        //string ProjectName { get; set; }

        event EventHandler<string> DisplayNameChanged;
        event EventHandler<string> AddNewProjectTitle;

        Task LoadUserDataAsync();
        Task SaveUserDataAsync();

        Task CreateNewProjectAsync(string NewProjectName, string ProjectVersion, bool IsSecured, string Password);
    }
}
