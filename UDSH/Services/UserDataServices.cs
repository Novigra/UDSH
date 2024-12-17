using UDSH.Model;

namespace UDSH.Services
{
    class UserDataServices : IUserDataServices
    {
        private readonly Session session;
        public event EventHandler<string> DisplayNameChanged;
        public event EventHandler<string> AddNewProjectTitle;

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

        /*public string ProjectName
        {
            get => session.User.Projects[0].ProjectName;
        }*/

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
            AddNewProjectTitle?.Invoke(this, NewProjectName);
        }
    }
}
