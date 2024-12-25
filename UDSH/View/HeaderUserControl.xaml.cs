using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;
using UDSH.ViewModel;

namespace UDSH.View
{
    enum WindowResizeState
    {
        Normal,
        Max
    }
    public partial class HeaderUserControl : UserControl
    {
        private HeaderUserControlViewModel viewModel;
        private MainWindow OwnerWindow;
        private Rect RestoreBounds;
        private WindowResizeState State;
        private bool IsButtonAction;
        private bool CanResize;
        private bool IsDragAction;
        public HeaderUserControl(HeaderUserControlViewModel viewModel)
        {
            InitializeComponent();
            /*PagesList.Items.Add(new Button());
            PagesList.Items.Add(new Button());
            PagesList.Items.Add(new Button());*/

            //viewModel = new HeaderUserControlViewModel();
            this.viewModel = viewModel;
            DataContext = viewModel;
            /*PagesList.Items.Add(new ListViewItem());
            PagesList.Items.Add(new ListViewItem());
            PagesList.Items.Add(new ListViewItem());*/

            Loaded += OnUserControlLoaded;
            State = WindowResizeState.Normal;

            IsButtonAction = false;
            CanResize = false;
            IsDragAction = false;
        }

        private void OnUserControlLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.MainWindow = Window.GetWindow(this);
            OwnerWindow = (MainWindow)Window.GetWindow(this);
            OwnerWindow.StateChanged += OwnerWindow_StateChanged;
        }

        private void OwnerWindow_StateChanged(object? sender, EventArgs e)
        {
            if (IsButtonAction == false && IsDragAction == true)
            {
                OwnerWindow.MainWindowGrid.Margin = new Thickness(8);
                CanResize = true;
                IsDragAction = false;
            }
        }

        // Buttons to control the window, there's no need to bind and write the code in the ViewModel section.
        private void MinimizeButton(object sender, RoutedEventArgs e)
        {
            OwnerWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton(object sender, RoutedEventArgs e)
        {
            if (OwnerWindow.WindowState == WindowState.Normal)
            {
                OwnerWindow.WindowState = WindowState.Maximized;
                OwnerWindow.MainWindowGrid.Margin = new Thickness(8);

                IsButtonAction = true;
            }
            else
            {
                OwnerWindow.WindowState = WindowState.Normal;
                OwnerWindow.MainWindowGrid.Margin = new Thickness(0);

                IsButtonAction = false;
            } 
        }

        private void CloseButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(viewModel != null)
                viewModel.LockPenToolButtons(e);
        }

        private void HandleKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (viewModel != null)
                viewModel.ReleasePenToolButtons(e);
        }

        private void Rectangle_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (CanResize == true || IsButtonAction == true)
            {
                OwnerWindow.WindowState = WindowState.Normal;
                OwnerWindow.MainWindowGrid.Margin = new Thickness(0);
                
                Point MousePosition = e.GetPosition(OwnerWindow.MainWindowGrid);
                OwnerWindow.Top = MousePosition.Y; // We may need X
                //OwnerWindow.Top = System.Windows.SystemParameters.WorkArea.Top;

                CanResize = false;
                IsButtonAction = false;
            }

            IsDragAction = true;
            OwnerWindow.DragMove();
        }

        private void ProjectNameStoryboard_Completed(object sender, EventArgs e)
        {
            var Translate = ProjectName.RenderTransform as TranslateTransform;
            if (Translate != null)
                Translate.X = 0;

            ProjectName.Opacity = 1.0;
            viewModel.CanPlayProjectAnimation = false;
        }
    }
}
