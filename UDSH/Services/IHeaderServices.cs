using UDSH.ViewModel;

namespace UDSH.Services
{
    public interface IHeaderServices
    {
        event EventHandler<FileStructure> FileStructureSelectionChanged;
        IUserDataServices UserDataServices { get; }
        IServiceProvider Services { get; }

        Task OnFileSelectionChanged(FileStructure fileStructure);
    }
}
