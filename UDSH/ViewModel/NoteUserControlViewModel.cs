using System.Diagnostics;
using System.Windows.Controls;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    class NoteUserControlViewModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        public RelayCommand<TextBox> CheckHeight => new RelayCommand<TextBox>(CheckHeightStatus);

        public NoteUserControlViewModel()
        {
            Title = string.Empty;
            Content = string.Empty;
        }

        private void CheckHeightStatus(TextBox textBox)
        {
            if (textBox != null)
            {
                // TODO: Calculate the height
                Debug.WriteLine(textBox.ClipToBounds);
            }
        }
    }
}
