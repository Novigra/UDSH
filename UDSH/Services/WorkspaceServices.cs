// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;

namespace UDSH.Services
{
    public class WorkspaceServices : IWorkspaceServices
    {
        public IUserDataServices UserDataServices { get; }
        public Window MainWindow { get; set; }

        public WorkspaceServices(IUserDataServices userDataServices)
        {
            UserDataServices = userDataServices;
            MainWindow = UserDataServices.Session.mainWindow;
        }
    }
}
