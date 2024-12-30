using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UDSH.MVVM
{
    public class CustomScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty OffsetAnimationProperty =
        DependencyProperty.Register("AnimatedOffset", typeof(double), typeof(CustomScrollViewer), new PropertyMetadata(0.0, OnAnimatedOffsetChanged));

        public event EventHandler ReachedMaxValue;

        public double AnimatedOffset
        {
            get { return (double)GetValue(OffsetAnimationProperty); }
            set { SetValue(OffsetAnimationProperty, value); }
        }

        private static void OnAnimatedOffsetChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            var scrollViewer = Dep as CustomScrollViewer;
            if (scrollViewer != null)
                scrollViewer.ScrollToHorizontalOffset((double)Event.NewValue);
        }

        public void SmoothScrollToHorizontalOffset(ScrollViewer scrollViewer, double TargetOffset, double duration = 0.5)
        {
            var animation = new DoubleAnimation
            {
                From = scrollViewer.HorizontalOffset,
                To = TargetOffset,
                Duration = TimeSpan.FromSeconds(duration),
                EasingFunction = new QuadraticEase()
            };

            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);

            Storyboard.SetTarget(animation, scrollViewer);
            Storyboard.SetTargetProperty(animation, new PropertyPath(CustomScrollViewer.OffsetAnimationProperty));

            storyboard.Completed += (sender, args) =>
            {
                /*if (scrollViewer.HorizontalOffset < 1 || scrollViewer.HorizontalOffset == scrollViewer.ScrollableWidth)
                    ReachedMaxValue.Invoke(this, EventArgs.Empty);*/
                /*else
                    Debug.WriteLine($"Didn't reach: {scrollViewer.HorizontalOffset}");*/
            };

            storyboard.Begin();
        }
    }
}
