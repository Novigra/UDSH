namespace UDSH.Services
{
    public class WorkspaceServices : IWorkspaceServices
    {
        public IUserDataServices UserDataServices { get; }

        public WorkspaceServices(IUserDataServices userDataServices)
        {
            UserDataServices = userDataServices;
        }
    }
}
