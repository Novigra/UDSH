using UDSH.Model;

namespace UDSH.Services
{
    public interface IUserDataServices
    {
        Session Session { get; }
        string DisplayName { get; set; }
        //string ProjectName { get; set; }

        event EventHandler<string> DisplayNameChanged;

        Task LoadUserDataAsync();
        Task SaveUserDataAsync();
    }
}
