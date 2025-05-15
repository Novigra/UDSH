using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.Converters;
using UDSH.ViewModel;

namespace UDSH.View
{
    public class MKBSelectionBox : ContentControl
    {
        public event EventHandler<bool> MKBSelectionBoxRequestedRemoval;
        private MKCUserControlViewModel ViewModel { get; set; }
        public Paragraph ParagraphHolder { get; set; }
        public InlineUIContainer Placeholder { get; set; }
        private Border ContentBorder { get; set; }

        public MKBSelectionBox(MKCUserControlViewModel viewModel, InlineUIContainer inlineUIContainer, Paragraph paragraphHolder)
        {
            ViewModel = viewModel;
            Placeholder = inlineUIContainer;
            ParagraphHolder = paragraphHolder;

            Construct();
        }

        private void Construct()
        {
            ContentBorder = CreateBackgroundBorder();
            StackPanel stackPanel = CreateContentStackPanelStructure();

            // Build
            Content = ContentBorder;
            ContentBorder.Child = stackPanel;

            HeightAnimation(400, 0, 0.5, ContentBorder);
        }

        private Border CreateBackgroundBorder()
        {
            Border border = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393E46")),
                CornerRadius = new CornerRadius(10),
                Width = 616,
                Height = 0 // 400
            };

            return border;
        }

        private StackPanel CreateContentStackPanelStructure()
        {
            StackPanel structure = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(5)
            };

            // Title + Cancel
            Grid TitleGrid = new Grid();
            TextBlock textBlock = new TextBlock
            {
                Style = (Style)Application.Current.FindResource("DefaultText"),
                Foreground = new SolidColorBrush(Colors.White),
                Text = "Pick A MKB File",
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0,10,0,0)
            };

            Button button = new Button
            {
                Style = (Style)Application.Current.FindResource("ResizeClose"),
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 50,
                Height = 30,
                Margin = new Thickness(0,10,10,0)
            };

            button.Click += CloseButton_Click;

            TitleGrid.Children.Add(textBlock);
            TitleGrid.Children.Add(button);
            structure.Children.Add(TitleGrid);

            // ListView
            ListView listView = new ListView
            {
                Style = (Style)Application.Current.FindResource("MKCConnectionListView"),
                ItemContainerStyle = (Style)Application.Current.FindResource("MKCConnectionListViewItem"),
                ItemsSource = ViewModel.MKBFiles,
                SelectedItem = ViewModel.SelectedMKBFile,
                Margin = new Thickness (10,10,10,0)
            };

            HeightConverter heightConverter = new HeightConverter();
            Binding HeightBinding = new Binding
            {
                Source = ContentBorder,
                Path = new PropertyPath("ActualHeight"),
                Converter = heightConverter,
                ConverterParameter = 70
            };

            listView.SetBinding(ListView.ItemsSourceProperty, new Binding(nameof(ViewModel.MKBFiles)));
            listView.SetBinding(ListView.SelectedItemProperty, new Binding(nameof(ViewModel.SelectedMKBFile)));
            listView.SetBinding(ListView.HeightProperty, HeightBinding);

            listView.MouseDoubleClick += ListView_MouseDoubleClick;

            structure.Children.Add(listView);

            return structure;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Request to delete the line
            MKBSelectionBoxRequestedRemoval.Invoke(this, false);
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MKBSelectionBoxRequestedRemoval.Invoke(this, true);
        }

        private void HeightAnimation(double HeightTarget, double BeginTime, double Duration, DependencyObject dependencyObject)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation ControlHeightAnimation = new DoubleAnimation();
            ControlHeightAnimation.BeginTime = TimeSpan.FromSeconds(0);
            ControlHeightAnimation.To = HeightTarget;
            ControlHeightAnimation.Duration = new Duration(TimeSpan.FromSeconds(Duration));
            ControlHeightAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

            Storyboard.SetTarget(ControlHeightAnimation, dependencyObject);
            Storyboard.SetTargetProperty(ControlHeightAnimation, new PropertyPath("Height"));
            storyboard.Children.Add(ControlHeightAnimation);

            storyboard.Begin();
        }
    }
}
