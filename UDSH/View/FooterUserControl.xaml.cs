using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for FooterUserControl.xaml
    /// </summary>
    public partial class FooterUserControl : UserControl
    {
        private FooterUserControlViewModel viewModel;
        private DispatcherTimer timer;
        public FooterUserControl(FooterUserControlViewModel viewModel)
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;

            DataContext = viewModel;
            this.viewModel = viewModel;
            viewModel.PlayConnectionStatusAnim += ViewModel_PlayConnectionStatusAnim;
            ConnectPopup.MouseDown += (s, e) => e.Handled = true;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            timer.Stop();
            ConnectionStatusAnimation(false);
        }

        private void ViewModel_PlayConnectionStatusAnim(object? sender, EventArgs e)
        {
            ConnectionStatusAnimation();
        }

        private void ConnectionStatusAnimation(bool Start = true)
        {
            double opBeginTime = 1;
            double opacity = 1;
            double width = viewModel.ConnectionBorderWidth;

            if (Start != true)
            {
                opBeginTime = 0;
                opacity = 0;
                width = 50;
            }

            Storyboard storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation();
            OpacityAnimation.BeginTime = TimeSpan.FromSeconds(opBeginTime);
            OpacityAnimation.To = opacity;
            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            Storyboard.SetTarget(OpacityAnimation, GameProjectConnection);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);

            DoubleAnimation WidthAnimation = new DoubleAnimation();
            WidthAnimation.To = width;
            WidthAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            WidthAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

            Storyboard.SetTarget(WidthAnimation, ConnectionBackgroundRect);
            Storyboard.SetTargetProperty(WidthAnimation, new PropertyPath("Width"));
            storyboard.Children.Add(WidthAnimation);
            storyboard.Completed += (sender, args) => 
            { 
                if (Start == true)
                    timer.Start();
            };

            storyboard.Begin();
        }
    }
}
