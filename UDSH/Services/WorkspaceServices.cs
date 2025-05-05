// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;
using UDSH.Model;

namespace UDSH.Services
{
    public class WorkspaceServices : IWorkspaceServices
    {
        public event EventHandler<InputEventArgs> ControlButtonPressed;
        public event EventHandler<InputEventArgs> ControlButtonReleased;
        public event EventHandler Reset;
        public event EventHandler<double> MKCSearchInitAnimFinished;
        public event EventHandler<bool> SidebarStatusChanged;

        public IUserDataServices UserDataServices { get; }
        public Window MainWindow { get; set; }
        public string CurrentActiveWorkspaceID { get; set; }

        public WorkspaceServices(IUserDataServices userDataServices)
        {
            UserDataServices = userDataServices;
            MainWindow = UserDataServices.Session.mainWindow;
        }

        public void SetCurrentActiveWorkspaceID(string workspaceID)
        {
            CurrentActiveWorkspaceID = workspaceID;
        }

        public void OnControlButtonPressed(System.Windows.Input.KeyEventArgs keyEventArgs)
        {
            ControlButtonPressed?.Invoke(this, new InputEventArgs(CurrentActiveWorkspaceID, keyEventArgs));
        }

        public void OnControlButtonReleased(System.Windows.Input.KeyEventArgs keyEventArgs)
        {
            ControlButtonReleased?.Invoke(this, new InputEventArgs(CurrentActiveWorkspaceID, keyEventArgs));
        }

        public void OnReset()
        {
            Reset?.Invoke(this, EventArgs.Empty);
        }

        public void OnMKCSearchInitAnimFinished(double Height)
        {
            MKCSearchInitAnimFinished?.Invoke(this, Height);
        }

        public void OnSidebarStatusChanged(bool IsOpen)
        {
            SidebarStatusChanged?.Invoke(this, IsOpen);
        }
    }
}
