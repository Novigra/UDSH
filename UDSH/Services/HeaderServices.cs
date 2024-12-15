namespace UDSH.Services
{
    public class HeaderServices : IHeaderServices
    {
        public IUserDataServices UserDataServices { get; }

        public HeaderServices(IUserDataServices userDataServices)
        {
            UserDataServices = userDataServices;
        }
    }
}
