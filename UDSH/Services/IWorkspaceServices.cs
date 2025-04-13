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
        IUserDataServices UserDataServices { get; }
        Window MainWindow { get; set; }
        string CurrentActiveWorkspaceID { get; }

        void SetCurrentActiveWorkspaceID(string workspaceID);
        void OnControlButtonPressed(System.Windows.Input.KeyEventArgs keyEventArgs);
        void OnControlButtonReleased(System.Windows.Input.KeyEventArgs keyEventArgs);
        void OnReset();
    }
}
