using UDSH.Model;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    public class NewProjectCreationWindowViewModel : ViewModelBase
    {
        private readonly Session _session;
        public NewProjectCreationWindowViewModel(Session session)
        {
            _session = session;
        }
    }
}
