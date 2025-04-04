// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.ViewModel;

namespace UDSH
{
    public partial class MainWindow : Window
    {
        private IServiceProvider _serviceProvider;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;

            var session = serviceProvider.GetRequiredService<Session>();
            session.mainWindow = this;

            var header = serviceProvider.GetRequiredService<IHeaderServices>();
            HeaderUserControl headerUserControl = new HeaderUserControl(new HeaderUserControlViewModel(header));
            HeaderUserControl.Children.Add(headerUserControl);

            var MK = serviceProvider.GetRequiredService<IWorkspaceServices>();
            MK.MainWindow = this;
            MKUserControl mKUserControl = new MKUserControl(new MKUserControlViewModel(MK));

            var userDataServices = serviceProvider.GetRequiredService<IUserDataServices>();
            DefaultUserControl defaultUserControl = new DefaultUserControl(userDataServices);
            TestContent.Content = defaultUserControl;

            FooterUserControl footerUserControl = new FooterUserControl(new FooterUserControlViewModel(userDataServices));
            FooterUserControl.Children.Add(footerUserControl);

            Debug.WriteLine($"Screen Width: {System.Windows.SystemParameters.WorkArea.Width}");

            header.FileStructureSelectionChanged += Header_FileStructureSelectionChanged;

            SideContentUserControl sideContentUserControl = new SideContentUserControl(new SideContentUserControlViewModel(userDataServices));
            SideGrid.Children.Add(sideContentUserControl);
        }

        private void Header_FileStructureSelectionChanged(object? sender, FileSystem e)
        {
            if (e != null)
                TestContent.Content = e.userControl;
            else
                TestContent.Content = new DefaultUserControl(_serviceProvider.GetRequiredService<IUserDataServices>());
        }

        private void HeaderMovement(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}