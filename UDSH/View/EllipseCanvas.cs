using System.Windows;
using System.Windows.Media;

namespace UDSH.View
{
    public class EllipseCanvas : FrameworkElement
    {
        public static readonly DependencyProperty EllipseDiameterProperty =
        DependencyProperty.Register(nameof(EllipseDiameter), typeof(double), typeof(EllipseCanvas),
            new FrameworkPropertyMetadata(5.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double EllipseDiameter
        {
            get => (double)GetValue(EllipseDiameterProperty);
            set => SetValue(EllipseDiameterProperty, value);
        }

        public static readonly DependencyProperty EllipseCountProperty =
            DependencyProperty.Register(nameof(EllipseCount), typeof(int), typeof(EllipseCanvas),
                new FrameworkPropertyMetadata(20, FrameworkPropertyMetadataOptions.AffectsRender));

        public int EllipseCount
        {
            get => (int)GetValue(EllipseCountProperty);
            set => SetValue(EllipseCountProperty, value);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            EllipseCount = (int)(10 * Math.Sqrt((ActualWidth * ActualHeight) / (1600.0 * 900.0)));
            double TotalWidthEllipses = EllipseCount * EllipseDiameter;
            double TotalHeightEllipses = EllipseCount * EllipseDiameter;
            double XSpace = (ActualWidth - TotalWidthEllipses) / (EllipseCount - 1);
            double YSpace = (ActualHeight - TotalHeightEllipses) / (EllipseCount - 1);

            double CurrentX = 0;
            double CurrentY = 0;

            for (int i = 0; i < EllipseCount; i++)
            {
                CurrentX = 0;
                for (int j = 0; j < EllipseCount; j++)
                {
                    Point center = new Point(CurrentX + EllipseDiameter / 2, CurrentY + EllipseDiameter / 2);
                    drawingContext.DrawEllipse(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B7B7B7")), null, center, EllipseDiameter / 2, EllipseDiameter / 2);
                    CurrentX += EllipseDiameter + XSpace;
                }
                CurrentY += EllipseDiameter + YSpace;
            }
        }
    }
}
