using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;

namespace UDSH.View
{
    public enum BNType
    {
        Dialogue,
        Choice,
        AttachedDialogue
    }

    public class DialogueNode : ContentControl
    {
        private BNType NodeType;
        private SolidColorBrush DialogueContainerBackgroundColor;
        private SolidColorBrush ChoiceContainerBackgroundColor;

        private Point InitialMousePosition;
        private bool LeftMousePressed = false;

        public DialogueNode(BNType NodeType)
        {
            this.NodeType = NodeType;
            DialogueContainerBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
            ChoiceContainerBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));

            Construct();
        }

        private void DialogueNode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitialMousePosition = Mouse.GetPosition(null);
            Mouse.Capture((UIElement)sender);
            LeftMousePressed = true;
        }

        private void DialogueNode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LeftMousePressed = false;
            Mouse.Capture(null);
        }

        private void DialogueNode_MouseMove(object sender, MouseEventArgs e)
        {
            if(LeftMousePressed == true)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);
                double DeltaX = InitialMousePosition.X - CurrentMousePosition.X;
                double DeltaY = InitialMousePosition.Y - CurrentMousePosition.Y;

                Canvas.SetLeft(this, Canvas.GetLeft(this) - DeltaX);
                Canvas.SetTop(this, Canvas.GetTop(this) - DeltaY);

                InitialMousePosition = CurrentMousePosition;
            }
        }

        private void Construct()
        {
            Border CollisionBorder = new Border
            {
                Background = new SolidColorBrush(Colors.Red),
                Width = 500,
                Height = 220,
                Padding = new Thickness(40)
            };

            Border ContainerBorder = new Border
            {
                Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor,
                CornerRadius = new CornerRadius(5)
            };

            StackPanel ContentStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(10)
            };

            StackPanel TitleStackPanel = CreateTitleCard();
            StackPanel CharacterStackPanel = CreateCharacterCard();
            StackPanel DialogueStackPanel = CreateDialogueCard();

            // Build
            Content = CollisionBorder;
            CollisionBorder.Child = ContainerBorder;
            ContainerBorder.Child = ContentStackPanel;

            ContentStackPanel.Children.Add(TitleStackPanel);
            ContentStackPanel.Children.Add(CharacterStackPanel);
            ContentStackPanel.Children.Add(DialogueStackPanel);


            ContainerBorder.MouseLeftButtonDown += DialogueNode_MouseLeftButtonDown;
            ContainerBorder.MouseLeftButtonUp += DialogueNode_MouseLeftButtonUp;
            ContainerBorder.MouseMove += DialogueNode_MouseMove;
        }

        private StackPanel CreateTitleCard()
        {
            StackPanel TitleCard = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                IsHitTestVisible = false
            };

            Image TitleIcon = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Speaker.png")),
                Width = 30,
                Height = 30
            };
            TitleCard.Children.Add(TitleIcon);

            if (NodeType == BNType.Dialogue)
            {
                TextBlock DialogueTitle = new TextBlock
                {
                    Style = (Style)Application.Current.FindResource("DefaultText"),
                    Text = "Dialogue",
                    FontSize = 18,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, 0, 0, 0)
                };
                TitleCard.Children.Add(DialogueTitle);
            }

            return TitleCard;
        }

        private StackPanel CreateCharacterCard()
        {
            StackPanel CharacterCard = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Image CharacterIcon = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/ProfilePicture.png")),
                Width = 20,
                Height = 20,
                VerticalAlignment = VerticalAlignment.Top
            };
            CharacterCard.Children.Add(CharacterIcon);

            RichTextBox CharacterRTB = new RichTextBox
            {
                Style = (Style)Application.Current.FindResource("MKBText"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 368
            };

            // Temp Test
            CharacterRTB.Document.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("Hamo Kenawy"));
            CharacterRTB.Document.Blocks.Add(paragraph);

            CharacterCard.Children.Add(CharacterRTB);

            return CharacterCard;
        }

        private StackPanel CreateDialogueCard()
        {
            StackPanel DialogueCard = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Image DialogueIcon = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Log.png")),
                Width = 20,
                Height = 20,
                VerticalAlignment = VerticalAlignment.Top
            };
            DialogueCard.Children.Add(DialogueIcon);

            RichTextBox DialogueRTB = new RichTextBox
            {
                Style = (Style)Application.Current.FindResource("MKBText"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 368,
            };

            // Temp Test
            DialogueRTB.Document.Blocks.Clear();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("AAAAAAAAA pretty much!!!"));
            DialogueRTB.Document.Blocks.Add(paragraph);

            DialogueCard.Children.Add(DialogueRTB);

            return DialogueCard;
        }
    }
}
