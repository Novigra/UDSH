// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;
using System.Windows.Controls;

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
        private ProcessType type;

        public PopupWindow(ProcessType ProcessType, string ItemName)
        {
            type = ProcessType;
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

        private void Title_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
            {
                switch (type)
                {
                    case ProcessType.Save:
                        textBlock.Text = "Save";
                        break;
                    case ProcessType.Delete:
                        textBlock.Text = "Delete";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
