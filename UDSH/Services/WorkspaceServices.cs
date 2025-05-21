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
        public event EventHandler<MKBFileConnectionUpdateEventArgs> MKBFileConnectionUpdated;
        public event EventHandler<MKBFileConnectionUpdateEventArgs> MKCFileConnectionUpdated;
        public event EventHandler<string> MKRequestedConnectionRemoval;
        public event EventHandler<MKBFileConnectionUpdateEventArgs> MKBRequestedConnectionRemoval;

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

        public void OnMKBFileConnectionUpdated(FileSystem MKBFile, FileSystem MKCFile)
        {
            MKBFileConnectionUpdated?.Invoke(this, new MKBFileConnectionUpdateEventArgs(MKBFile, MKCFile));

            if (string.IsNullOrEmpty(MKBFile.ConnectedMKCFileID))
            {
                MKBFile.ConnectedMKCFileID = MKCFile.FileID;
                UserDataServices.SaveUserDataAsync();
            }
        }

        public void OnMKCFileConnectionUpdated(FileSystem MKBFile, FileSystem MKCFile)
        {
            MKCFileConnectionUpdated?.Invoke(this, new MKBFileConnectionUpdateEventArgs(MKBFile, MKCFile));

            if (!MKCFile.ConnectedMKMFilesID.Contains(MKBFile.FileID))
            {
                MKCFile.ConnectedMKMFilesID.Add(MKBFile.FileID);
                UserDataServices.SaveUserDataAsync();
            }
        }

        public void OnMKRequestedConnectionRemoval(string MKFileID)
        {
            // The deletion depends on who requesting it.
            // MKC -> Remove the Link and if all links removed, then remove the connection between the files.
            // MKB -> Remove all the links. [Already done in "OnMKBRequestedConnectionRemoval"]
            MKRequestedConnectionRemoval?.Invoke(this, MKFileID);
        }

        public void OnMKBRequestedConnectionRemoval(FileSystem MKBFile, FileSystem MKCFile)
        {
            MKBRequestedConnectionRemoval?.Invoke(this, new MKBFileConnectionUpdateEventArgs(MKBFile, MKCFile));

            MKBFile.ConnectedMKCFileID = string.Empty;
            MKCFile.ConnectedMKMFilesID.Remove(MKBFile.FileID);
            UserDataServices.SaveUserDataAsync();
        }
    }
}
