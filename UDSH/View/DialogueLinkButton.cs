// Copyright (C) 2025 Mohammed Kenawy
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using UDSH.Model;

namespace UDSH.View
{
    public class DialogueLinkButton : Button
    {
        public Paragraph CharacterParagraph { get; set; }
        public Paragraph DialogueParagraph { get; set; }
        public Paragraph ParagraphHolder { get; set; }
        public InlineUIContainer Placeholder { get; set; }
        public FileSystem AssociatedMKBFile { get; set; }
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
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            FileTitle = GetTemplateChild("Title") as TextBlock;
            FileTitle.Text = AssociatedMKBFile.FileName;

            double AddedLength = 185;
            TotalWidth = GetTextBlockWidth(AssociatedMKBFile.FileName, "Segoe UI", 20) + AddedLength;
            if (TotalWidth < MaxWidth)
                Width = TotalWidth;
            else
                Width = MaxWidth;
        }

        private double GetTextBlockWidth(string BoundText, string FontFamily, double FontSize)
        {
            FormattedText formattedText = new FormattedText(BoundText, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface(new FontFamily(FontFamily), FontStyles.Normal, FontWeights.DemiBold, FontStretches.Normal),
                FontSize, Brushes.Black, new NumberSubstitution(), 1);

            return formattedText.Width;
        }
    }
}
