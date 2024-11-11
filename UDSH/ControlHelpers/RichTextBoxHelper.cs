using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UDSH.ControlHelpers
{
    // Rich Text Box doesn't allow straightforward bindings.
    internal class RichTextBoxHelper
    {
        public static readonly DependencyProperty BoundTextProperty = DependencyProperty.RegisterAttached("BoundText", typeof(string), typeof(RichTextBoxHelper), 
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundTextChanged));

        public static string GetBoundText(DependencyObject obj) => (string)obj.GetValue(BoundTextProperty);
        public static void SetBoundText(DependencyObject obj, string value) => obj.SetValue(BoundTextProperty, value);

        private static void OnBoundTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is RichTextBox richTextBox)
            {
                richTextBox.Document.Blocks.Clear();
                if (e.NewValue != null)
                    richTextBox.Document.Blocks.Add(new Paragraph(new Run((string)e.NewValue)));

                richTextBox.TextChanged += (sender, args) =>
                {
                    var newText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
                    SetBoundText(richTextBox, newText);
                };
            }
        }
    }
}
