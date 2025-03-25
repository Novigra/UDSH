using System.Windows;
using System.Windows.Controls;
using UDSH.Model;
using UDSH.ViewModel;

namespace UDSH.View
{
    public partial class TextureCreationWindow : Window
    {
        public TextureCreationWindow(RichTextBox CurrentRTB, FileSystem File)
        {
            InitializeComponent();

            TextureCreationViewModel viewModel = new TextureCreationViewModel(CurrentRTB, File, this);
            DataContext = viewModel;

            viewModel.TextTest = TextTest;
        }
    }
}
