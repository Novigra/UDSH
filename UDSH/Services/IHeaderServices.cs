namespace UDSH.Services
{
    public interface IHeaderServices
    {
        IUserDataServices UserDataServices { get; }
        IServiceProvider Services { get; }
    }
}
