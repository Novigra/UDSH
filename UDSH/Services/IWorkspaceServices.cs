// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;
using UDSH.Model;

namespace UDSH.Services
{
    public interface IWorkspaceServices
    {
        event EventHandler<InputEventArgs> ControlButtonPressed;
        event EventHandler<InputEventArgs> ControlButtonReleased;
        event EventHandler Reset;
        event EventHandler<double> MKCSearchInitAnimFinished;
        event EventHandler<bool> SidebarStatusChanged;
        event EventHandler<MKBFileConnectionUpdateEventArgs> MKBFileConnectionUpdated;

        IUserDataServices UserDataServices { get; }
        Window MainWindow { get; set; }
        string CurrentActiveWorkspaceID { get; }

        void SetCurrentActiveWorkspaceID(string workspaceID);
        void OnControlButtonPressed(System.Windows.Input.KeyEventArgs keyEventArgs);
        void OnControlButtonReleased(System.Windows.Input.KeyEventArgs keyEventArgs);
        void OnReset();
        void OnMKCSearchInitAnimFinished(double Height);
        void OnSidebarStatusChanged(bool IsOpen);
        void OnMKBFileConnectionUpdated(FileSystem MKBFile, FileSystem MKCFile);
    }
}
