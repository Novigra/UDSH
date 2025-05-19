// Copyright (C) 2025 Mohammed Kenawy

using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.Model;
using UDSH.ViewModel;

namespace UDSH.View
{
    public class DialogueLinkButton : Button
    {
        public event EventHandler ButtonCreated;
        public event EventHandler<FileSystem> ButtonRequstedToOpenMKBFile;
        public Paragraph CharacterParagraph { get; set; }
        public Paragraph DialogueParagraph { get; set; }
        public Paragraph ParagraphHolder { get; set; }
        public InlineUIContainer Placeholder { get; set; }
        public FileSystem AssociatedMKBFile { get; set; }
        private Border ButtonBackgroundBorder { get; set; }
        private Grid FileGrid { get; set; }
        private Grid GoToGrid { get; set; }
        private TextBlock FileTitle { get; set; }
        public double TotalWidth { get; set; } = 0;
        private double MaxWidth { get; set; } = 500;
        private MKCUserControlViewModel ViewModel { get; set; }

        public DialogueLinkButton(MKCUserControlViewModel viewModel, Paragraph characterParagraph, Paragraph dialogueParagraph, Paragraph paragraphHolder, 
            InlineUIContainer placeholder, FileSystem associatedMKBFile)
        {
            ViewModel = viewModel;
            CharacterParagraph = characterParagraph;
            DialogueParagraph = dialogueParagraph;
            ParagraphHolder = paragraphHolder;
            Placeholder = placeholder;
            AssociatedMKBFile = associatedMKBFile;

            Construct();
        }

        private void Construct()
        {
            Style = (Style)Application.Current.FindResource("BNDialogueConnectionButton");

            Click += DialogueLinkButton_Click;
            MouseEnter += DialogueLinkButton_MouseEnter;
            MouseLeave += DialogueLinkButton_MouseLeave;
        }

        private void DialogueLinkButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonRequstedToOpenMKBFile.Invoke(this, AssociatedMKBFile);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ButtonBackgroundBorder = GetTemplateChild("ButtonBorder") as Border;
            FileGrid = GetTemplateChild("FileGrid") as Grid;
            GoToGrid = GetTemplateChild("GoToGrid") as Grid;
            FileTitle = GetTemplateChild("Title") as TextBlock;
            FileTitle.Text = AssociatedMKBFile.FileName;

            double AddedLength = 185;
            TotalWidth = GetTextBlockWidth(AssociatedMKBFile.FileName, "Segoe UI", 20) + AddedLength;
            if (TotalWidth < MaxWidth)
                Width = TotalWidth;
            else
                Width = MaxWidth;

            ButtonCreated.Invoke(this, EventArgs.Empty);
        }

        private void DialogueLinkButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonDetailsSwitchAnimation(FileGrid, 0);
            ButtonDetailsSwitchAnimation(GoToGrid, 1);
        }

        private void DialogueLinkButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonDetailsSwitchAnimation(FileGrid, 1);
            ButtonDetailsSwitchAnimation(GoToGrid, 0);
        }

        private double GetTextBlockWidth(string BoundText, string FontFamily, double FontSize)
        {
            FormattedText formattedText = new FormattedText(BoundText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface(new FontFamily(FontFamily), FontStyles.Normal, FontWeights.DemiBold, FontStretches.Normal),
                FontSize, Brushes.Black, new NumberSubstitution(), 1);

            return formattedText.Width;
        }

        private void ButtonDetailsSwitchAnimation(DependencyObject dependencyObject, double Target)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation OpacityAnimation = new DoubleAnimation
            {
                To = Target,
                Duration = TimeSpan.FromSeconds(0.3),
            };

            Storyboard.SetTarget(OpacityAnimation, dependencyObject);
            Storyboard.SetTargetProperty(OpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(OpacityAnimation);

            storyboard.Begin();
        }

        public void ButtonFocusAnimation(bool CanReverse)
        {
            double BeginTime = 0.7;

            Storyboard storyboard = new Storyboard();
            ColorAnimation colorAnimation = new ColorAnimation
            {
                To = (Color)ColorConverter.ConvertFromString("#E0911A"),
                Duration = TimeSpan.FromSeconds(0.3),
            };

            Storyboard.SetTarget(colorAnimation, ButtonBackgroundBorder);
            Storyboard.SetTargetProperty(colorAnimation, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
            storyboard.Children.Add(colorAnimation);

            DoubleAnimation FileOpacityAnimation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.3),
            };

            Storyboard.SetTarget(FileOpacityAnimation, FileGrid);
            Storyboard.SetTargetProperty(FileOpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(FileOpacityAnimation);

            DoubleAnimation GoToOpacityAnimation = new DoubleAnimation
            {
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
            };

            Storyboard.SetTarget(GoToOpacityAnimation, GoToGrid);
            Storyboard.SetTargetProperty(GoToOpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(GoToOpacityAnimation);

            if (CanReverse == true)
            {
                ColorAnimation colorToOriginal = new ColorAnimation
                {
                    To = (Color)ColorConverter.ConvertFromString("#C41CFF"),
                    Duration = TimeSpan.FromSeconds(0.3),
                    BeginTime = TimeSpan.FromSeconds(BeginTime)
                };

                Storyboard.SetTarget(colorToOriginal, ButtonBackgroundBorder);
                Storyboard.SetTargetProperty(colorToOriginal, new PropertyPath("(Control.Background).(SolidColorBrush.Color)"));
                storyboard.Children.Add(colorToOriginal);

                DoubleAnimation RevFileOpacityAnimation = new DoubleAnimation
                {
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.3),
                    BeginTime = TimeSpan.FromSeconds(BeginTime)
                };

                Storyboard.SetTarget(RevFileOpacityAnimation, FileGrid);
                Storyboard.SetTargetProperty(RevFileOpacityAnimation, new PropertyPath("Opacity"));
                storyboard.Children.Add(RevFileOpacityAnimation);

                DoubleAnimation RevGoToOpacityAnimation = new DoubleAnimation
                {
                    To = 0,
                    Duration = TimeSpan.FromSeconds(0.3),
                    BeginTime = TimeSpan.FromSeconds(BeginTime)
                };

                Storyboard.SetTarget(RevGoToOpacityAnimation, GoToGrid);
                Storyboard.SetTargetProperty(RevGoToOpacityAnimation, new PropertyPath("Opacity"));
                storyboard.Children.Add(RevGoToOpacityAnimation);
            }

            storyboard.Begin();
        }
    }
}
