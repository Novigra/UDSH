using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.ViewModel;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        private ContentWindowViewModel viewModel;
        private TextBlock TextContainer;
        private Border DragBorder;
        private Border BorderContainer;

        private ColumnDefinition FirstColumn;
        private double MinWidth = 200;
        private double LastWidthBeforeResize = 0;
        private double WindowedWidth = 0;
        private double FullScreenWidth = 0;
        private bool WindowedWidthChanged = false;
        private bool FullScreenWidthChanged = false;
        public ContentWindow(ContentWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            this.viewModel = viewModel;
            this.viewModel.SetAssociatedWindow(this);
            this.viewModel.WindowDragStart += StartDragModeAnimation;
            this.viewModel.WindowDragEnd += ResetDragModeAnimation;
            this.viewModel.MousePressed += ViewModel_MousePressed;
            this.viewModel.MouseEnterCollision += ViewModel_MouseEnterCollision;
            this.viewModel.DirectoryWarningMessage += ViewModel_DirectoryWarningMessage;
            this.viewModel.WrongDirectoryNotification += ViewModel_WrongDirectoryNotification;
            this.viewModel.PageChanged += ViewModel_PageChanged;

            StateChanged += ContentWindow_StateChanged;
            SizeChanged += ContentWindow_SizeChanged;
        }

        private void ViewModel_PageChanged(object? sender, EventArgs e)
        {
            BorderContainer.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C5C5C5"));
        }

        private void ViewModel_WrongDirectoryNotification(object? sender, EventArgs e)
        {
            ColorTargetAnimation("#F83030", BorderContainer, true);
        }

        private void ViewModel_DirectoryWarningMessage(object? sender, bool e)
        {
            if (e == true)
            {
                ColorTargetAnimation("#F83030", BorderContainer);
            }
            else
            {
                ColorTargetAnimation("#FFFFFF", BorderContainer);
            }
        }

        private void ContentWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                if (WindowedWidth > GridContent.ActualWidth)
                {
                    WindowedWidth = GridContent.ActualWidth - 200;
                    FirstColumn.Width = new GridLength(WindowedWidth);
                    NavBorder.Margin = new Thickness(WindowedWidth + 50, 0, 0, 0);
                }
            }
        }

        private void ContentWindow_StateChanged(object? sender, EventArgs e)
        {
            if (WindowState == WindowState.Normal && FirstColumn != null)
            {
                if (WindowedWidthChanged == false)
                    WindowedWidth = 200;
                FirstColumn.Width = new GridLength(WindowedWidth);
                NavBorder.Margin = new Thickness(WindowedWidth + 50, 0, 0, 0);
            }
            else if (WindowState == WindowState.Maximized && FirstColumn != null)
            {
                if (FullScreenWidthChanged == false)
                    FullScreenWidth = 200;
                FirstColumn.Width = new GridLength(FullScreenWidth);
                NavBorder.Margin = new Thickness(FullScreenWidth + 50, 0, 0, 0);
            }
        }

        private void ViewModel_MousePressed(object? sender, bool e)
        {
            if (e == true)
            {
                ColorTargetAnimation("#C5C5C5", BorderContainer);
            }
            else
            {
                ColorTargetAnimation("#FFFFFF", BorderContainer);
            }
        }

        private void ViewModel_MouseEnterCollision(object? sender, bool e)
        {
            if (e == true)
            {
                ColorTargetAnimation("#A7A7A7", BorderContainer);
            }
            else
            {
                ColorTargetAnimation("#C5C5C5", BorderContainer);
            }
        }

        private void ColorTargetAnimation(string ColorHexCode, DependencyObject Target, bool CanReverse = false)
        {
            Storyboard storyboard = new Storyboard();
            ColorAnimation colorAnimation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString(ColorHexCode),
                Duration = TimeSpan.FromSeconds(0.2),
            };

            Storyboard.SetTarget(colorAnimation, Target);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
            storyboard.Children.Add(colorAnimation);

            if (CanReverse == true)
            {
                ColorAnimation colorToOriginal = new ColorAnimation
                {
                    To = (Color)ColorConverter.ConvertFromString("#FFFFFF"),
                    Duration = TimeSpan.FromSeconds(0.2),
                    BeginTime = TimeSpan.FromSeconds(0.5),
                };

                Storyboard.SetTarget(colorToOriginal, Target);
                Storyboard.SetTargetProperty(colorToOriginal, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
                storyboard.Children.Add(colorToOriginal);
            }

            storyboard.Begin();
        }

        private void StartDragModeAnimation(object? sender, EventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation();
            OpacityAnimation.To = 1;
            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));

            Storyboard.SetTarget(OpacityAnimation, DragBorder);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);


            DoubleAnimation OpacityTextBlockAnimation = new DoubleAnimation();
            OpacityTextBlockAnimation.BeginTime = TimeSpan.FromSeconds(0.2);
            OpacityTextBlockAnimation.To = 1;
            OpacityTextBlockAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));

            Storyboard.SetTarget(OpacityTextBlockAnimation, TextContainer);
            Storyboard.SetTargetProperty(OpacityTextBlockAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityTextBlockAnimation);

            storyboard.Begin();
        }

        private void ResetDragModeAnimation(object? sender, EventArgs e)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation();
            OpacityAnimation.To = 0;
            OpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));

            Storyboard.SetTarget(OpacityAnimation, DragBorder);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);

            DoubleAnimation OpacityTextBlockAnimation = new DoubleAnimation();
            OpacityTextBlockAnimation.BeginTime = TimeSpan.FromSeconds(0.0);
            OpacityTextBlockAnimation.To = 0;
            OpacityTextBlockAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.2));

            Storyboard.SetTarget(OpacityTextBlockAnimation, TextContainer);
            Storyboard.SetTargetProperty(OpacityTextBlockAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityTextBlockAnimation);
            storyboard.Begin();
        }

        private void TextContainer_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBlock textBlock)
                TextContainer = textBlock;
        }

        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Border border)
                DragBorder = border;
        }

        private void BorderContainer_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is Border border)
                BorderContainer = border;
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            grid.Focus();
        }

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                FirstColumn = ListGrid.ColumnDefinitions[0];
                FullScreenWidth = FirstColumn.ActualWidth + e.HorizontalChange;

                double NewWidthTarget = GridContent.ActualWidth - MinWidth;
                double StopWidthTarget = NewWidthTarget - 1.0;
                Debug.WriteLine(NewWidthTarget);

                if (FullScreenWidth < FirstColumn.MinWidth)
                    FullScreenWidth = FirstColumn.MinWidth;
                else if (FullScreenWidth > NewWidthTarget)
                    FullScreenWidth = StopWidthTarget;

                FirstColumn.Width = new GridLength(FullScreenWidth);
                NavBorder.Margin = new Thickness(FullScreenWidth + 50, 0, 0, 0);

                FullScreenWidthChanged = true;
            }
            else if (WindowState == WindowState.Normal)
            {
                FirstColumn = ListGrid.ColumnDefinitions[0];
                WindowedWidth = FirstColumn.ActualWidth + e.HorizontalChange;

                double NewWidthTarget = GridContent.ActualWidth - MinWidth;
                double StopWidthTarget = NewWidthTarget - 1.0;
                Debug.WriteLine(NewWidthTarget);

                if (WindowedWidth < FirstColumn.MinWidth)
                    WindowedWidth = FirstColumn.MinWidth;
                else if (WindowedWidth > NewWidthTarget)
                    WindowedWidth = StopWidthTarget;

                FirstColumn.Width = new GridLength(WindowedWidth);
                NavBorder.Margin = new Thickness(WindowedWidth + 50, 0, 0, 0);

                WindowedWidthChanged = true;
            }
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
