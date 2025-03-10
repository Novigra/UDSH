// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for NewFileCreationWindow.xaml
    /// </summary>
    public partial class NewFileCreationWindow : Window
    {
        private NewFileCreationWindowViewModel viewModel;
        public NewFileCreationWindow(NewFileCreationWindowViewModel viewModel, Window ParentWindow)
        {
            Owner = ParentWindow;
            InitializeComponent();

            DirectoryPopup.MouseDown += (s, e) => e.Handled = true;

            this.viewModel = viewModel;
            DataContext = viewModel;

            this.viewModel.CurrentWindow = this;

            viewModel.textBlock = WarningMessage;
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            viewModel.PlayHighlightedText();
        }

        private void Border_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //MoveImageOpacityAnimation(1.0);
        }

        private void Border_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //MoveImageOpacityAnimation(0.0);
        }

        private void SetModalWindowActivation(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!this.IsActive)
                this.Activate();
        }

        /*private void MoveImageOpacityAnimation(double Target)
        {
            Storyboard Storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation
            {
                To = Target,
                Duration = TimeSpan.FromSeconds(0.2),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(OpacityAnimation, MoveImage);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath(Image.OpacityProperty));
            Storyboard.Children.Add(OpacityAnimation);
            Storyboard.Begin();
        }*/
    }
}
