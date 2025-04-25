using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using UDSH.ViewModel;

namespace UDSH.View
{
    public class SelectionBorderBox : ContentControl
    {
        private MKBUserControlViewModel ViewModel { get; set; }
        private Border SelectionBorder { get; set; }
        private SolidColorBrush BorderBackgroundColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9980E3EA"));
        private SolidColorBrush BorderStrokeColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3F9BA1"));

        public SelectionBorderBox(MKBUserControlViewModel viewModel)
        {
            ViewModel = viewModel;

            Construct();

            Content = SelectionBorder;
        }

        private void Construct()
        {
            SelectionBorder = new Border
            {
                Background = BorderBackgroundColor,
                Width = ViewModel.SelectionBorderWidth,
                Height = ViewModel.SelectionBorderHeight,
                CornerRadius = new CornerRadius(10),
                BorderThickness = new Thickness(10)
            };

            SelectionBorder.SetBinding(Border.WidthProperty, new Binding(nameof(ViewModel.SelectionBorderWidth)));
            SelectionBorder.SetBinding(Border.HeightProperty, new Binding(nameof(ViewModel.SelectionBorderHeight)));

            Rectangle rectangle = new Rectangle
            {
                StrokeDashArray = new DoubleCollection { 4, 2 },
                Fill = new SolidColorBrush(Colors.Transparent),
                Stroke = BorderStrokeColor,
                StrokeThickness = 10,
                RadiusX = 10,
                RadiusY = 10
            };

            rectangle.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(Border.ActualWidth))
            {
                Source = SelectionBorder
            });

            rectangle.SetBinding(FrameworkElement.HeightProperty, new Binding(nameof(Border.ActualHeight))
            {
                Source = SelectionBorder
            });

            VisualBrush visualBrush = new VisualBrush(rectangle);
            SelectionBorder.BorderBrush = visualBrush;
        }
    }
}
