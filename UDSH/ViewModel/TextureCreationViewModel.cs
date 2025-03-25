using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UDSH.Model;
using UDSH.MVVM;

namespace UDSH.ViewModel
{
    public class TextureCreationViewModel : ViewModelBase
    {
        private FileSystem File {  get; set; }
        private RichTextBox CurrentRTB { get; set; }
        private Window CurrentWindow { get; set; }
        public Run TextTest { get; set; }

        private string associateFileName;
        public string AssociateFileName
        {
            get => associateFileName;
            set { associateFileName = value; OnPropertyChanged(); }
        }

        private string fontFamily;
        public string FontFamily
        {
            get => fontFamily;
            set { fontFamily = value; OnPropertyChanged(); }
        }

        public RelayCommand<Button> FontFamilyButton => new RelayCommand<Button>(execute => PickFontFamily());
        public RelayCommand<Button> CloseButton => new RelayCommand<Button>(execute => CloseWindow());

        public TextureCreationViewModel(RichTextBox currentRTB, FileSystem file, Window currentWindow)
        {
            File = file;
            CurrentRTB = currentRTB;
            CurrentWindow = currentWindow;

            AssociateFileName = File.FileName;
            FontFamily = "Pick Font Family";
        }

        private void PickFontFamily()
        {
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Please pick a font for your font family...";
            openFileDialog.Filter = "Font Files|*.ttf;*.otf";
            openFileDialog.Multiselect = false;

            bool? success = openFileDialog.ShowDialog();
            if (success == true)
            {
                string FileName = openFileDialog.SafeFileName;
                string[] Collection = FileName.Split("-");
                string FontName = Collection[0].TrimEnd();
                string directory = openFileDialog.

            }*/


            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Title = "Please pick a folder that contains fonts...";
            openFolderDialog.Multiselect = false;

            bool? success = openFolderDialog.ShowDialog();
            if (success == true)
            {
                string directory = openFolderDialog.FolderName;
                // TODO: Get Font Name From the File.
            }
        }

        private void CopyRTBContent(RichTextBox richTextBox, RichTextBox targetRichTextBox)
        {
            TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            string DocumentData = string.Empty;
            using (var memoryStream = new MemoryStream())
            {
                textRange.Save(memoryStream, DataFormats.Xaml);
                DocumentData = Encoding.UTF8.GetString(memoryStream.ToArray());
            }

            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(DocumentData)))
            {
                TextRange targetTextRange = new TextRange(targetRichTextBox.Document.ContentStart, targetRichTextBox.Document.ContentEnd);
                targetTextRange.Load(memoryStream, DataFormats.Xaml);
            }
        }

        private void CloseWindow()
        {
            CurrentWindow.Close();
        }
    }
}
