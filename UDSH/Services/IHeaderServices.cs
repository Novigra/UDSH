// Copyright (C) 2025 Mohammed Kenawy
using UDSH.Model;

namespace UDSH.Services
{
    public interface IHeaderServices
    {
        event EventHandler<FileSystem> FileStructureSelectionChanged;
        IUserDataServices UserDataServices { get; }
        IServiceProvider Services { get; }

        Task OnFileSelectionChanged(FileSystem file);
    }
}
