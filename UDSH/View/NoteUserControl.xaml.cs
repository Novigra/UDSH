using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class NoteUserControl : UserControl
    {
        private NoteUserControlViewModel viewModel;

        public event EventHandler? RemoveCurrentNote;

        // NOTE: DECOUPLE THE CODE WHEN YOU UNDERSTAND THE DATA FLOW
        public NoteUserControl(UserControl ParentControl)
        {
            InitializeComponent();

            viewModel = new NoteUserControlViewModel(this, ParentControl);
            DataContext = viewModel;
        }

        private void Control_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.LeftCtrl)
            {
                viewModel.CanStartEditing = true;
                Debug.WriteLine($"Key: {e.Key}");
            }
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                viewModel.CanStartEditing = false;
                Debug.WriteLine($"Stop");
            }
        }

        private void OnRemoveCurrentNote(object sender, System.Windows.RoutedEventArgs e)
        {
            RemoveCurrentNote?.Invoke(this, EventArgs.Empty);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Debug.WriteLine($"Can Record");
        }
    }
}
