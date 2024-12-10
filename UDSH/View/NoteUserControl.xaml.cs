using System.Windows.Controls;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class NoteUserControl : UserControl
    {
        public NoteUserControl()
        {
            InitializeComponent();

            NoteUserControlViewModel viewModel = new NoteUserControlViewModel();
            DataContext = viewModel;

            NoteBorder.Width = 400;
            NoteBorder.Height = 260;
        }
    }
}
