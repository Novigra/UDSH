using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Diagnostics;

namespace UDSH.View
{
    public enum BNType
    {
        Dialogue,
        Choice,
        AttachedDialogue
    }

    public struct NodePosition
    {
        public double X;
        public double Y;
    }

    public class DroppedNodeEventArgs : EventArgs
    {
        public DialogueNode Node { get; set; }
        public NodePosition Position { get; set; }

        public DroppedNodeEventArgs(DialogueNode node, NodePosition position)
        {
            Node = node;
            Position = position;
        }
    }

    public class DialogueNode : ContentControl
    {
        private BNType NodeType;
        private NodePosition Position;
        private SolidColorBrush DialogueContainerBackgroundColor;
        private SolidColorBrush ChoiceContainerBackgroundColor;

        private Border CollisionBorder;
        private Border ContainerBorder;
        private StackPanel ContentStackPanel;

        private RichTextBox CharacterRTB;
        private RichTextBox DialogueRTB;

        private Paragraph CharacterParagraph;
        private Paragraph DialogueParagraph;

        private Point InitialMousePosition;
        private bool LeftMousePressed = false;
        private bool IsHeightAnimationPlaying = false;

        public event EventHandler<DroppedNodeEventArgs> NodePositionChanged;

        public DialogueNode(BNType NodeType, NodePosition nodePosition)
        {
            this.NodeType = NodeType;
            Position = nodePosition;
            DialogueContainerBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
            ChoiceContainerBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));

            Construct();

            Canvas.SetLeft(this, Position.X);
            Canvas.SetTop(this, Position.Y);
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

            NodePositionChanged.Invoke(this, new DroppedNodeEventArgs(this, Position));
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

                Position.X = Canvas.GetLeft(this);
                Position.Y = Canvas.GetTop(this);

                InitialMousePosition = CurrentMousePosition;
            }
        }

        private void Construct()
        {
            CollisionBorder = new Border
            {
                Background = new SolidColorBrush(Colors.Red),
                Width = 500,
                Height = 230,
                Padding = new Thickness(40)
            };

            ContainerBorder = new Border
            {
                Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor,
                CornerRadius = new CornerRadius(5)
            };

            ContentStackPanel = new StackPanel
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
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/SceneHeadingIcon.png")),
                Width = 40,
                Height = 40
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
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Person.png")),
                Width = 30,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top
            };
            CharacterCard.Children.Add(CharacterIcon);

            CharacterRTB = new RichTextBox
            {
                Style = (Style)Application.Current.FindResource("MKBText"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 368
            };

            // Temp Test
            CharacterRTB.Document.Blocks.Clear();
            CharacterParagraph = new Paragraph();
            CharacterParagraph.Inlines.Add(new Run { Text = "", FontWeight = FontWeights.Normal });
            CharacterRTB.Document.Blocks.Add(CharacterParagraph);
            CharacterRTB.TextChanged += CharacterRTB_TextChanged;
            CharacterRTB.PreviewKeyDown += CharacterRTB_KeyDown;

            CharacterCard.Children.Add(CharacterRTB);

            return CharacterCard;
        }

        private void CharacterRTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogueRTB.CaretPosition = DialogueParagraph.ContentEnd;
                DialogueRTB.Focus();
                e.Handled = true;
            }
        }

        private async void CharacterRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Task.Delay(100);

            double TotalHeight = CharacterRTB.ActualHeight + DialogueRTB.ActualHeight;
            if (TotalHeight > 60)
            {
                double NewHeight = TotalHeight + 180;
                HeightAnimation(NewHeight);
            }
        }

        private void HeightAnimation(double HeightTarget)
        {
            if (IsHeightAnimationPlaying == true)
                return;

            Storyboard storyboard = new Storyboard();

            DoubleAnimation ControlHeightAnimation = new DoubleAnimation();
            ControlHeightAnimation.BeginTime = TimeSpan.FromSeconds(0);
            ControlHeightAnimation.To = HeightTarget;
            ControlHeightAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.5));

            Storyboard.SetTarget(ControlHeightAnimation, CollisionBorder);
            Storyboard.SetTargetProperty(ControlHeightAnimation, new PropertyPath("Height"));
            storyboard.Children.Add(ControlHeightAnimation);

            storyboard.Completed += (s, e) => { IsHeightAnimationPlaying = false; };

            storyboard.Begin();
            IsHeightAnimationPlaying = true;
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
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/Speaker.png")),
                Width = 30,
                Height = 30,
                VerticalAlignment = VerticalAlignment.Top
            };
            DialogueCard.Children.Add(DialogueIcon);

            DialogueRTB = new RichTextBox
            {
                Style = (Style)Application.Current.FindResource("MKBText"),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 368,
            };

            // Temp Test
            DialogueRTB.Document.Blocks.Clear();
            DialogueParagraph = new Paragraph();
            DialogueParagraph.Inlines.Add(new Run { Text = "AAAAAAAAA pretty much!!!", FontWeight = FontWeights.Normal });
            DialogueRTB.Document.Blocks.Add(DialogueParagraph);
            DialogueRTB.TextChanged += CharacterRTB_TextChanged;
            DialogueRTB.PreviewKeyDown += DialogueRTB_PreviewKeyDown;

            DialogueCard.Children.Add(DialogueRTB);

            return DialogueCard;
        }

        private void DialogueRTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        public void UpdateNodeLocation(double X, double Y)
        {
            Canvas.SetLeft(this, X);
            Canvas.SetTop(this, Y);

            Position.X = Canvas.GetLeft(this);
            Position.Y = Canvas.GetTop(this);
        }
    }
}
