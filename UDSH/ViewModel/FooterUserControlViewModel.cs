using Microsoft.Win32;
using System.IO;
using System.Reflection;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;

namespace UDSH.ViewModel
{
    public class FooterUserControlViewModel : ViewModelBase
    {
        private readonly IUserDataServices _userDataServices;
        public event EventHandler PlayConnectionStatusAnim;

        private string appVersionLabel;
        public string AppVersionLabel
        {
            get => appVersionLabel;
            set { appVersionLabel = value; OnPropertyChanged(); }
        }

        private string connectionLabel;
        public string ConnectionLabel
        {
            get => connectionLabel;
            set { connectionLabel = value; OnPropertyChanged(); }
        }

        private string gameProjectName;
        public string GameProjectName
        {
            get => gameProjectName;
            set { gameProjectName = value; OnPropertyChanged(); }
        }

        private const double ConnectedWidth = 155;
        private const double DisconnectedWidth = 190;
        private double connectionBorderWidth;
        public double ConnectionBorderWidth
        {
            get => connectionBorderWidth;
            set { connectionBorderWidth = value; OnPropertyChanged(); }
        }

        private bool isConnectPopupOpen;
        public bool IsConnectPopupOpen
        {
            get => isConnectPopupOpen;
            set { isConnectPopupOpen = value; OnPropertyChanged(); }
        }

        private bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set { isConnected = value; OnPropertyChanged(); }
        }

        private bool isConnectionEnabled;
        public bool IsConnectionEnabled
        {
            get => isConnectionEnabled;
            set { isConnectionEnabled = value; OnPropertyChanged(); }
        }

        private bool isMouseOverButton;
        public bool IsMouseOverButton
        {
            get => isMouseOverButton;
            set { isMouseOverButton = value; OnPropertyChanged(); }
        }

        public RelayCommand<object> ConnectButton => new RelayCommand<object>(execute => ChangeConnectPopup());
        public RelayCommand<object> ConnectProjectButton => new RelayCommand<object>(execute => ConnectGameProject());
        public RelayCommand<object> ConnectButtonMouseEnter => new RelayCommand<object>(execute => ChangeConnectBorderStatus());
        public RelayCommand<object> ConnectButtonMouseLeave => new RelayCommand<object>(execute => ChangeConnectBorderStatus());
        public RelayCommand<object> SendButton => new RelayCommand<object>(execute => SendDataToGameProject());

        public FooterUserControlViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            IsConnectPopupOpen = false;
            IsMouseOverButton = false;

            CheckConnectionStatus();
            UpdateVersion();
            // Show the connection when changing projects or starting one.
        }

        private void CheckConnectionStatus()
        {
            if (_userDataServices.ActiveProject != null)
            {
                IsConnected = _userDataServices.ActiveProject.IsConnectedToGameProject;
                IsConnectionEnabled = true;

                if (IsConnected == true)
                {
                    ConnectionLabel = "CONNECTED";
                    ConnectionBorderWidth = ConnectedWidth;

                    GameProjectName = Path.GetFileNameWithoutExtension(_userDataServices.ActiveProject.GameProjectDirectory);
                }
                else
                {
                    ConnectionLabel = "NOT CONNECTED";
                    ConnectionBorderWidth = DisconnectedWidth;
                    GameProjectName = string.Empty;
                }
            }
            else
            {
                IsConnected = false;
                IsConnectionEnabled = false;

                ConnectionLabel = "NOT CONNECTED";
                ConnectionBorderWidth = DisconnectedWidth;
                GameProjectName = string.Empty;
            }
        }

        private void UpdateVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            AppVersionLabel = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private void ChangeConnectPopup()
        {
            IsConnectPopupOpen = !IsConnectPopupOpen;
        }

        private void ConnectGameProject()
        {
            Project project = _userDataServices.ActiveProject;

            if (IsConnected == true)
            {
                project.GameProjectDirectory = string.Empty;
                project.IsConnectedToGameProject = false;
                IsConnected = false;
                ConnectionBorderWidth = DisconnectedWidth;
                ConnectionLabel = "NOT CONNECTED";
                GameProjectName = string.Empty;
                PlayConnectionStatusAnim.Invoke(this, EventArgs.Empty);

                _userDataServices.SaveUserDataAsync();
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please pick the game project...";
            openFileDialog.Filter = "Project Files|*.uproject";
            openFileDialog.Multiselect = false;
            bool? success = openFileDialog.ShowDialog();
            
            if (success == true)
            {
                project.GameProjectDirectory = openFileDialog.FileName;
                project.IsConnectedToGameProject = true;
                IsConnected = true;
                ConnectionBorderWidth = ConnectedWidth;
                ConnectionLabel = "CONNECTED";
                GameProjectName = Path.GetFileNameWithoutExtension(_userDataServices.ActiveProject.GameProjectDirectory);
                PlayConnectionStatusAnim.Invoke(this, EventArgs.Empty);

                _userDataServices.SaveUserDataAsync();
            }
        }

        private void ChangeConnectBorderStatus()
        {
            IsMouseOverButton = !IsMouseOverButton;
        }

        private void SendDataToGameProject()
        {
            IsConnectPopupOpen = false;
            // TODO: Send Data to project [Content Folder].
        }
    }
}
