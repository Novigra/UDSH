using System.Reflection;
using UDSH.MVVM;
using UDSH.Services;

namespace UDSH.ViewModel
{
    public class FooterUserControlViewModel : ViewModelBase
    {
        private readonly IUserDataServices _userDataServices;

        private string appVersionLabel;
        public string AppVersionLabel
        {
            get => appVersionLabel;
            set { appVersionLabel = value; OnPropertyChanged(); }
        }

        private bool isConnectPopupOpen;
        public bool IsConnectPopupOpen
        {
            get => isConnectPopupOpen;
            set { isConnectPopupOpen = value; OnPropertyChanged(); }
        }

        public RelayCommand<object> ConnectButton => new RelayCommand<object>(execute => ChangeConnectPopup());

        public FooterUserControlViewModel(IUserDataServices userDataServices)
        {
            _userDataServices = userDataServices;

            IsConnectPopupOpen = false;

            UpdateVersion();
        }

        private void UpdateVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            AppVersionLabel = $"{version.Major}.{version.Minor}.{version.Build}";
        }

        private void ChangeConnectPopup()
        {
            // TODO: work on the project popup
        }
    }
}
