using System.Windows;

namespace UDSH.View
{
    public enum ProcessType
    {
        Save,
        Delete
    }

    /// <summary>
    /// Interaction logic for PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Window
    {
        private const string SaveSentence = "Do you want to save";
        private const string DeleteSentence = "Do you want to delete";
        private const double AddedWidth = 200.0;
        private Window window;

        public PopupWindow(ProcessType ProcessType, string ItemName)
        {
            InitializeComponent();

            window = GetWindow(this);
            InitiateMessage(ProcessType, ItemName);
        }

        private void InitiateMessage(ProcessType ProcessType, string ItemName)
        {
            switch (ProcessType)
            {
                case ProcessType.Save:
                    Message.Text = SaveSentence + " " + $"\"{ItemName}\"" + "?";
                    break;
                case ProcessType.Delete:
                    Message.Text = DeleteSentence + " " + $"\"{ItemName}\"" + "?";
                    break;
                default:
                    break;
            }

            Dispatcher.BeginInvoke(() =>
            {
                double InitialWidth = window.Width;
                window.Width = Message.ActualWidth + AddedWidth;
                window.Left -= (window.Width - InitialWidth) / 2;
            }, System.Windows.Threading.DispatcherPriority.Render);
        }

        private void ConfirmAction(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelAction(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
