using System.Reflection;
using System.Windows.Controls;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for FooterUserControl.xaml
    /// </summary>
    public partial class FooterUserControl : UserControl
    {
        public FooterUserControl()
        {
            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            ApplicationVersion.Text = $"{version.Major}.{version.Minor}.{version.Build}";
        }
    }
}
