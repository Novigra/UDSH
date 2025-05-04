// Copyright (C) 2025 Mohammed Kenawy
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.Model;
using UDSH.Services;
using UDSH.View;
using UDSH.ViewModel;

namespace UDSH
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IWorkspaceServices _workspaceServices;
        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            
            var session = serviceProvider.GetRequiredService<Session>();
            session.mainWindow = this;

            _workspaceServices = _serviceProvider.GetRequiredService<IWorkspaceServices>();

            var header = serviceProvider.GetRequiredService<IHeaderServices>();
            HeaderUserControl headerUserControl = new HeaderUserControl(new HeaderUserControlViewModel(header));
            HeaderUserControl.Children.Add(headerUserControl);

            /*var MK = serviceProvider.GetRequiredService<IWorkspaceServices>();
            MK.MainWindow = this;
            MKUserControl mKUserControl = new MKUserControl(new MKUserControlViewModel(MK));*/

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
            {
                TestContent.Content = e.userControl;

                if (e.userControl is MKBUserControl control)
                {
                    BNButtonSpaceGrid.IsHitTestVisible = true;
                    BNButtonSpaceGrid.Opacity = 1;

                    DataContext = control.DataContext;
                }
                else
                {
                    BNButtonSpaceGrid.IsHitTestVisible = false;
                    BNButtonSpaceGrid.Opacity = 0;
                }
            }
            else
            {
                TestContent.Content = new DefaultUserControl(_serviceProvider.GetRequiredService<IUserDataServices>());

                BNButtonSpaceGrid.IsHitTestVisible = false;
                BNButtonSpaceGrid.Opacity = 0;
            }
        }

        private void HeaderMovement(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _workspaceServices.OnControlButtonPressed(e);
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            _workspaceServices.OnControlButtonReleased(e);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            _workspaceServices.OnReset();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            _workspaceServices.OnReset();
        }

        private void MKCSearchOpenStoryboard_Completed(object sender, EventArgs e)
        {
            _workspaceServices.OnMKCSearchInitAnimFinished(400);
        }

        private void MKCSearchCloseStoryboard_Completed(object sender, EventArgs e)
        {
            _workspaceServices.OnMKCSearchInitAnimFinished(0);
        }
    }
}