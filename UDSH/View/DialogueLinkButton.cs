// Copyright (C) 2025 Mohammed Kenawy
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using UDSH.Model;

namespace UDSH.View
{
    public class DialogueLinkButton : Button
    {
        public event EventHandler ButtonCreated;
        public Paragraph CharacterParagraph { get; set; }
        public Paragraph DialogueParagraph { get; set; }
        public Paragraph ParagraphHolder { get; set; }
        public InlineUIContainer Placeholder { get; set; }
        public FileSystem AssociatedMKBFile { get; set; }
        private Grid FileGrid { get; set; }
        private Grid GoToGrid { get; set; }
        private TextBlock FileTitle { get; set; }
        public double TotalWidth { get; set; } = 0;
        private double MaxWidth { get; set; } = 500;

        public DialogueLinkButton()
        {
            Construct();
        }

        private void Construct()
        {
            Style = (Style)Application.Current.FindResource("BNDialogueConnectionButton");

            MouseEnter += DialogueLinkButton_MouseEnter;
            MouseLeave += DialogueLinkButton_MouseLeave;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

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
    }
}
