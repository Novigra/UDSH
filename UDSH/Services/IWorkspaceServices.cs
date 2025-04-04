// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;

namespace UDSH.Services
{
    public interface IWorkspaceServices
    {
        IUserDataServices UserDataServices { get; }
        Window MainWindow { get; set; }
    }
}
