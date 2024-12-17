namespace UDSH.Services
{
    public class HeaderServices : IHeaderServices
    {
        public IUserDataServices UserDataServices { get; }
        public IServiceProvider Services { get; }

        public HeaderServices(IUserDataServices userDataServices, IServiceProvider services)
        {
            UserDataServices = userDataServices;
            Services = services;
        }
    }
}
