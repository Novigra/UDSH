using UDSH.ViewModel;

namespace UDSH.Services
{
    public class HeaderServices : IHeaderServices
    {
        public event EventHandler<FileStructure> FileStructureSelectionChanged;
        public IUserDataServices UserDataServices { get; }
        public IServiceProvider Services { get; }

        public HeaderServices(IUserDataServices userDataServices, IServiceProvider services)
        {
            UserDataServices = userDataServices;
            Services = services;
        }

        public async Task OnFileSelectionChanged(FileStructure fileStructure)
        {
            FileStructureSelectionChanged?.Invoke(this, fileStructure);
        }
    }
}
