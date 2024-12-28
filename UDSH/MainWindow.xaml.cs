using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.ViewModel;

namespace UDSH
{
    public partial class MainWindow : Window
    {
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            var session = serviceProvider.GetRequiredService<Session>();
            session.mainWindow = this;

            var header = serviceProvider.GetRequiredService<IHeaderServices>();
            HeaderUserControl headerUserControl = new HeaderUserControl(new HeaderUserControlViewModel(header));
            HeaderUserControl.Children.Add(headerUserControl);

            var MK = serviceProvider.GetRequiredService<IWorkspaceServices>();
            MKUserControl mKUserControl = new MKUserControl(new MKUserControlViewModel(MK));
            DefaultUserControl defaultUserControl = new DefaultUserControl();
            TestContent.Content = defaultUserControl;

            Debug.WriteLine($"Screen Width: {System.Windows.SystemParameters.WorkArea.Width}");

            header.FileStructureSelectionChanged += Header_FileStructureSelectionChanged;
        }

        private void Header_FileStructureSelectionChanged(object? sender, FileStructure e)
        {
            if (e != null)
                TestContent.Content = e.UserControl;
            else
                TestContent.Content = new DefaultUserControl();
        }

        private void HeaderMovement(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}