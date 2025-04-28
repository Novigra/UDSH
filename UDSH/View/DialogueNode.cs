// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Diagnostics;
using System.Windows.Shapes;
using System.Windows.Data;
using UDSH.ViewModel;
using System.Reflection;
using UDSH.Model;
using static System.Formats.Asn1.AsnWriter;

namespace UDSH.View
{
    public enum ElementType
    {
        Choice,
        Character,
        Dialogue
    }

    public enum ConnectionType
    {
        Parent,
        Child
    }

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
        #region Properties
        #region Collision Border Properties
        private Border CollisionBorder { get; set; }
        #endregion

        #region Content StackPanel Property
        private StackPanel ContentStackPanel { get; set; }
        #endregion

        #region Container Border Properties
        private Border ContainerBorder { get; set; }
        private SolidColorBrush DialogueContainerBackgroundColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
        private SolidColorBrush ChoiceContainerBackgroundColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));
        private SolidColorBrush ContainerBorderBrushFill { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
        private SolidColorBrush ContainerBorderBrushStroke { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#393E46"));
        #endregion

        #region Node Collision Border Properties
        public Border ParentNodeCollisionBorder { get; set; }
        public Border ChildrenNodeCollisionBorder { get; set; }
        private SolidColorBrush NodeCollisionBorderUnconnectedDialogueBackground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66E0911A"));
        private SolidColorBrush NodeCollisionBorderUnconnectedPlayerChoiceBackground { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66C41CFF"));
        private VerticalAlignment NodeCollisionBorderVerticalAlignment { get; set; } = VerticalAlignment.Center;
        private HorizontalAlignment NodeCollisionBorderHorizontalAlignment { get; set; } = HorizontalAlignment.Center;
        private SolidColorBrush NodeCollisionBorderFill { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
        private SolidColorBrush NodeCollisionBorderDialogueStroke { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E0911A"));
        private SolidColorBrush NodeCollisionBorderPlayerChoiceStroke { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C41CFF"));
        #endregion

        #region Node Title Properties
        private Orientation NodeTitleCardOrientation { get; set; } = Orientation.Horizontal;
        private bool NodeTitleCardIsHitTestVisible { get; set; } = false;
        private BitmapImage NodeTitleIcon { get; set; } = new BitmapImage(new Uri("pack://application:,,,/Resource/Speaker.png"));
        private Style NodeTitleStyle { get; set; } = (Style)Application.Current.FindResource("DefaultText");
        private string NodeTitleDialogueTextBlockText { get; set; } = "Dialogue";
        private TextTrimming NodeTitleDialogueTextBlockTextTrimming { get; set; } = TextTrimming.CharacterEllipsis;
        private VerticalAlignment NodeTitleVerticalAlignment { get; set; } = VerticalAlignment.Center;
        #endregion

        #region Choice [Dialogue Option] Properties
        private TextBlock ChoiceMarkTextBlock { get; set; }
        private RichTextBox ChoiceRTB { get; set; }
        private Paragraph ChoiceParagraph { get; set; }
        private Orientation NodeChoiceOrientation { get; set; } = Orientation.Horizontal;
        private Style NodeChoiceRTBStyle { get; set; } = (Style)Application.Current.FindResource("MKBText");
        private VerticalAlignment NodeChoiceRTBVerticalAlignment { get; set; } = VerticalAlignment.Top;
        private string NodeChoiceMarkText { get; set; } = "Dialogue Option...";
        #endregion

        #region Character Properties
        private TextBlock CharacterMarkTextBlock { get; set; }
        private RichTextBox CharacterRTB { get; set; }
        private Paragraph CharacterParagraph { get; set; }
        private Orientation NodeCharacterOrientation { get; set; } = Orientation.Horizontal;
        private BitmapImage NodeCharacterIcon { get; set; } = new BitmapImage(new Uri("pack://application:,,,/Resource/ProfilePicture.png"));
        private VerticalAlignment NodeCharacterIconVerticalAlignment { get; set; } = VerticalAlignment.Top;
        private Style NodeCharacterRTBStyle { get; set; } = (Style)Application.Current.FindResource("MKBText");
        private VerticalAlignment NodeCharacterRTBVerticalAlignment { get; set; } = VerticalAlignment.Top;
        private Style NodeMarkStyle { get; set; } = (Style)Application.Current.FindResource("DefaultText");
        private string NodeCharacterMarkText { get; set; } = "Character Name...";
        private SolidColorBrush NodeMarkColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        private FontStyle NodeMarkFontStyle { get; set; } = FontStyles.Italic;
        private VerticalAlignment NodeMarkVerticalAlignment { get; set; } = VerticalAlignment.Center;
        private bool NodeMarkIsHitTestVisible { get; set; } = false;
        #endregion

        #region Dialogue Properties
        private TextBlock DialogueMarkTextBlock { get; set; }
        private RichTextBox DialogueRTB { get; set; }
        private Paragraph DialogueParagraph { get; set; }
        private Orientation NodeDialogueOrientation { get; set; } = Orientation.Horizontal;
        private BitmapImage NodeDialogueIcon { get; set; } = new BitmapImage(new Uri("pack://application:,,,/Resource/Log.png"));
        private VerticalAlignment NodeDialogueIconVerticalAlignment { get; set; } = VerticalAlignment.Top;
        private Style NodeDialogueRTBStyle { get; set; } = (Style)Application.Current.FindResource("MKBText");
        private VerticalAlignment NodeDialogueRTBVerticalAlignment { get; set; } = VerticalAlignment.Top;
        private string NodeDialogueMarkText { get; set; } = "Dialogue...";
        #endregion

        #region Node Order Properties
        private TextBlock NodeOrderTextBlock { get; set; }
        private HorizontalAlignment NodeOrderTextBlockHorizontalAlignment { get; set; } = HorizontalAlignment.Right;
        private bool NodeOrderTextBlockIsHitTestVisible { get; set; } = false;
        public int NodeOrder { get; set; } = 0;
        #endregion
        #endregion

        public BNType NodeType { get; set; }
        public NodePosition Position;

        private Point InitialMousePosition { get; set; }
        private bool LeftMousePressed { get; set; } = false;
        private bool CanDrawPathLine { get; set; } = false;
        private bool IsHeightAnimationPlaying { get; set; } = false;
        public bool IsRootNode { get; set; }
        public bool IsSubRootNode { get; set; }

        private double LastBorderHeight { get; set; }

        private MKBUserControlViewModel ViewModel { get; set; }

        public List<Path> ParentsPath { get; set; } = new List<Path>();
        public List<Path> ChildrenPath { get; set; } = new List<Path>();

        public bool IsSelectedInMultiSelectionProcess { get; set; } = false;

        public event EventHandler<DroppedNodeEventArgs> NodePositionChanged;
        public event EventHandler NodeStartedConnectionProcess;
        public event EventHandler ChildNodeRequestedConnection;
        public event EventHandler<bool> NotifyPathConnectionColorUpdate;
        public event EventHandler<BranchNodeRemovalEventArgs> NodeRequestedConnectionRemoval;
        public event EventHandler NodeSelectionChanged;
        public event EventHandler NodeRequestedOrderUpdate;
        public event EventHandler NodeRequestedSelectedNodesToMove;
        public event EventHandler NodeRequestedSelectedNodesToStop;
        public event EventHandler NodeRequestedSelectedNodesRemoval;

        public DialogueNode(BNType NodeType, NodePosition nodePosition, MKBUserControlViewModel viewModel, bool isRootNode = false, bool isSubRootNode = false)
        {
            this.NodeType = NodeType;
            Position = nodePosition;
            DataContext = viewModel;
            ViewModel = viewModel;
            IsRootNode = isRootNode;
            IsSubRootNode = isSubRootNode;

            if (IsRootNode == true)
            {
                ConstructRoot();
            }
            else
            {
                Construct();
            }

            Canvas.SetLeft(this, Position.X);
            Canvas.SetTop(this, Position.Y);
        }

        public DialogueNode()
        {
            Construct();
        }

        #region Node Construction
        private void ConstructRoot()
        {
            ChildrenNodeCollisionBorder = CreateNodeAttachmentCollision(false);

            CollisionBorder = new Border
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Width = ViewModel.CollisionBorderWidth,
                Padding = ViewModel.RootCollisionBorderPadding
            };

            CollisionBorder.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(ViewModel.CollisionBorderWidth)));
            CollisionBorder.SetBinding(Border.PaddingProperty, new Binding(nameof(ViewModel.RootCollisionBorderPadding)));

            ContainerBorder = new Border
            {
                Background = DialogueContainerBackgroundColor,
                Height = ViewModel.RootContainerBorderHeight,
                CornerRadius = ViewModel.ContainerBorderCornerRadius,
                BorderThickness = ViewModel.ContainerBorderThickness
            };

            ContainerBorder.SetBinding(Border.HeightProperty, new Binding(nameof(ViewModel.RootContainerBorderHeight)));
            ContainerBorder.SetBinding(Border.CornerRadiusProperty, new Binding(nameof(ViewModel.ContainerBorderCornerRadius)));
            ContainerBorder.SetBinding(Border.BorderThicknessProperty, new Binding(nameof(ViewModel.ContainerBorderThickness)));


            Image ContainerImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Resource/HorusEyeBlack.png")),
                Width = ViewModel.RootContainerImageWidth,
                Height = ViewModel.RootContainerImageHeight,
                IsHitTestVisible = false
            };

            ContainerImage.SetBinding(Image.WidthProperty, new Binding(nameof(ViewModel.RootContainerImageWidth)));
            ContainerImage.SetBinding(Image.HeightProperty, new Binding(nameof(ViewModel.RootContainerImageHeight)));

            StackPanel NodeStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };


            // Build
            Content = CollisionBorder;
            FocusVisualStyle = null;
            CollisionBorder.Child = NodeStackPanel;
            ContainerBorder.Child = ContainerImage;

            NodeStackPanel.Children.Add(ContainerBorder);
            NodeStackPanel.Children.Add(ChildrenNodeCollisionBorder);

            CollisionBorder.MouseEnter += CollisionBorder_MouseEnter;
            CollisionBorder.MouseLeave += CollisionBorder_MouseLeave;

            ContainerBorder.MouseLeftButtonDown += DialogueNode_MouseLeftButtonDown;
            ContainerBorder.MouseLeftButtonUp += DialogueNode_MouseLeftButtonUp;
            ContainerBorder.MouseMove += DialogueNode_MouseMove;

            ChildrenNodeCollisionBorder.Child.MouseLeftButtonDown += ChildrenNodeCollisionBorder_MouseLeftButtonDown;
            ChildrenNodeCollisionBorder.Child.MouseLeftButtonUp += ChildrenNodeCollisionBorder_MouseLeftButtonUp;
            ChildrenNodeCollisionBorder.Child.MouseMove += ChildrenNodeCollisionBorder_MouseMove;
        }

        private void Construct()
        {
            ParentNodeCollisionBorder = CreateNodeAttachmentCollision(true);
            ChildrenNodeCollisionBorder = CreateNodeAttachmentCollision(false);

            CollisionBorder = new Border
            {
                Background = new SolidColorBrush(Colors.Transparent),
                Width = ViewModel.CollisionBorderWidth,
                Padding = ViewModel.CollisionBorderPadding
            };

            CollisionBorder.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(ViewModel.CollisionBorderWidth)));
            CollisionBorder.SetBinding(Border.PaddingProperty, new Binding(nameof(ViewModel.CollisionBorderPadding)));


            ContainerBorder = CreateContainerBorder();

            StackPanel NodeStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            ContentStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Margin = ViewModel.ContentStackPanelMargin
            };

            ContentStackPanel.SetBinding(FrameworkElement.MarginProperty, new Binding(nameof(ViewModel.ContentStackPanelMargin)));


            StackPanel TitleStackPanel = CreateTitleCard();
            StackPanel CharacterStackPanel = CreateCharacterCard();
            StackPanel DialogueStackPanel = CreateDialogueCard();

            // Build
            Content = CollisionBorder;
            FocusVisualStyle = null;
            CollisionBorder.Child = NodeStackPanel;
            ContainerBorder.Child = ContentStackPanel;

            NodeStackPanel.Children.Add(ParentNodeCollisionBorder);
            NodeStackPanel.Children.Add(ContainerBorder);
            NodeStackPanel.Children.Add(ChildrenNodeCollisionBorder);

            ContentStackPanel.Children.Add(TitleStackPanel);
            ContentStackPanel.Children.Add(CharacterStackPanel);
            ContentStackPanel.Children.Add(DialogueStackPanel);
            if (NodeType == BNType.Choice)
                ContentStackPanel.Children.Add(CreateNodeOrderTextBlock());

            if (NodeType == BNType.Choice)
                LastBorderHeight = ChoiceRTB.ActualHeight + CharacterRTB.ActualHeight + DialogueRTB.ActualHeight;
            else
                LastBorderHeight = CharacterRTB.ActualHeight + DialogueRTB.ActualHeight;

            CollisionBorder.MouseEnter += CollisionBorder_MouseEnter;
            CollisionBorder.MouseLeave += CollisionBorder_MouseLeave;

            ContainerBorder.MouseLeftButtonDown += DialogueNode_MouseLeftButtonDown;
            ContainerBorder.MouseLeftButtonUp += DialogueNode_MouseLeftButtonUp;
            ContainerBorder.MouseMove += DialogueNode_MouseMove;

            ParentNodeCollisionBorder.Child.MouseLeftButtonDown += ParentNodeCollisionBorder_MouseLeftButtonDown;

            ChildrenNodeCollisionBorder.Child.MouseLeftButtonDown += ChildrenNodeCollisionBorder_MouseLeftButtonDown;
            ChildrenNodeCollisionBorder.Child.MouseLeftButtonUp += ChildrenNodeCollisionBorder_MouseLeftButtonUp;
            ChildrenNodeCollisionBorder.Child.MouseMove += ChildrenNodeCollisionBorder_MouseMove;
        }

        private Border CreateNodeAttachmentCollision(bool IsParent)
        {
            Border border = new Border
            {
                Background = (NodeType == BNType.Choice) ? NodeCollisionBorderUnconnectedPlayerChoiceBackground : NodeCollisionBorderUnconnectedDialogueBackground,
                VerticalAlignment = NodeCollisionBorderVerticalAlignment,
                HorizontalAlignment = NodeCollisionBorderHorizontalAlignment,
                Width = ViewModel.NodeCollisionBorderWidth,
                Height = ViewModel.NodeCollisionBorderHeight,
                CornerRadius = ViewModel.NodeCollisionBorderCornerRadius,
                Margin = (IsParent == true) ? ViewModel.NodeCollisionBorderParentMargin : ViewModel.NodeCollisionBorderChildMargin,
                BorderThickness = ViewModel.NodeCollisionBorderThickness,
                Opacity = 0,
                IsHitTestVisible = false
            };

            border.SetBinding(Border.WidthProperty, new Binding(nameof(ViewModel.NodeCollisionBorderWidth)));
            border.SetBinding(Border.HeightProperty, new Binding(nameof(ViewModel.NodeCollisionBorderHeight)));
            border.SetBinding(Border.CornerRadiusProperty, new Binding(nameof(ViewModel.NodeCollisionBorderCornerRadius)));
            if (IsParent == true)
                border.SetBinding(Border.MarginProperty, new Binding(nameof(ViewModel.NodeCollisionBorderParentMargin)));
            else
                border.SetBinding(Border.MarginProperty, new Binding(nameof(ViewModel.NodeCollisionBorderChildMargin)));

            border.SetBinding(Border.BorderThicknessProperty, new Binding(nameof(ViewModel.NodeCollisionBorderThickness)));

            Rectangle rectangle = new Rectangle
            {
                StrokeDashArray = ViewModel.NodeCollisionBorderStrokeDashArray,
                Fill = NodeCollisionBorderFill,
                Stroke = (NodeType == BNType.Choice) ? NodeCollisionBorderPlayerChoiceStroke : NodeCollisionBorderDialogueStroke,
                StrokeThickness = ViewModel.NodeCollisionBorderStrokeThickness,
                RadiusX = ViewModel.NodeCollisionBorderRadiusX,
                RadiusY = ViewModel.NodeCollisionBorderRadiusY
            };

            rectangle.SetBinding(Rectangle.StrokeDashArrayProperty, new Binding(nameof(ViewModel.NodeCollisionBorderStrokeDashArray)));
            rectangle.SetBinding(Rectangle.StrokeThicknessProperty, new Binding(nameof(ViewModel.NodeCollisionBorderStrokeThickness)));
            rectangle.SetBinding(Rectangle.RadiusXProperty, new Binding(nameof(ViewModel.NodeCollisionBorderRadiusX)));
            rectangle.SetBinding(Rectangle.RadiusYProperty, new Binding(nameof(ViewModel.NodeCollisionBorderRadiusY)));

            rectangle.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(Border.ActualWidth))
            {
                Source = border
            });

            rectangle.SetBinding(FrameworkElement.HeightProperty, new Binding(nameof(Border.ActualHeight))
            {
                Source = border
            });

            VisualBrush visualBrush = new VisualBrush(rectangle);
            border.BorderBrush = visualBrush;

            Grid grid = new Grid();
            grid.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Transparent"));
            border.Child = grid;

            return border;
        }

        private VisualBrush CreateNodeAttachmentBorderBrush(Border NodeAttachmentBorder)
        {
            Rectangle rectangle = new Rectangle
            {
                StrokeDashArray = ViewModel.NodeCollisionBorderStrokeDashArray,
                Fill = NodeCollisionBorderFill,
                Stroke = (NodeType == BNType.Choice) ? NodeCollisionBorderPlayerChoiceStroke : NodeCollisionBorderDialogueStroke,
                StrokeThickness = ViewModel.NodeCollisionBorderStrokeThickness,
                RadiusX = ViewModel.NodeCollisionBorderRadiusX,
                RadiusY = ViewModel.NodeCollisionBorderRadiusY
            };

            rectangle.SetBinding(Rectangle.StrokeDashArrayProperty, new Binding(nameof(ViewModel.NodeCollisionBorderStrokeDashArray)));
            rectangle.SetBinding(Rectangle.StrokeThicknessProperty, new Binding(nameof(ViewModel.NodeCollisionBorderStrokeThickness)));
            rectangle.SetBinding(Rectangle.RadiusXProperty, new Binding(nameof(ViewModel.NodeCollisionBorderRadiusX)));
            rectangle.SetBinding(Rectangle.RadiusYProperty, new Binding(nameof(ViewModel.NodeCollisionBorderRadiusY)));

            rectangle.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(Border.ActualWidth))
            {
                Source = NodeAttachmentBorder
            });

            rectangle.SetBinding(FrameworkElement.HeightProperty, new Binding(nameof(Border.ActualHeight))
            {
                Source = NodeAttachmentBorder
            });

            return new VisualBrush(rectangle);
        }

        private Border CreateContainerBorder()
        {
            Border border = new Border
            {
                Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor,
                CornerRadius = ViewModel.ContainerBorderCornerRadius,
                BorderThickness = ViewModel.ContainerBorderThickness
            };

            border.SetBinding(Border.CornerRadiusProperty, new Binding(nameof(ViewModel.ContainerBorderCornerRadius)));
            border.SetBinding(Border.BorderThicknessProperty, new Binding(nameof(ViewModel.ContainerBorderThickness)));

            return border;
        }

        private void UpdateContainerBorderBrush()
        {
            Rectangle rectangle = new Rectangle
            {
                StrokeDashArray = ViewModel.ContainerBorderBrushStrokeDashArray,
                Fill = ContainerBorderBrushFill,
                Stroke = ContainerBorderBrushStroke,
                StrokeThickness = ViewModel.ContainerBorderBrushStrokeThickness,
                RadiusX = ViewModel.ContainerBorderBrushRadiusX,
                RadiusY = ViewModel.ContainerBorderBrushRadiusY,
            };

            rectangle.SetBinding(Rectangle.StrokeDashArrayProperty, new Binding(nameof(ViewModel.ContainerBorderBrushStrokeDashArray)));
            rectangle.SetBinding(Rectangle.StrokeThicknessProperty, new Binding(nameof(ViewModel.ContainerBorderBrushStrokeThickness)));
            rectangle.SetBinding(Rectangle.RadiusXProperty, new Binding(nameof(ViewModel.ContainerBorderBrushRadiusX)));
            rectangle.SetBinding(Rectangle.RadiusYProperty, new Binding(nameof(ViewModel.ContainerBorderBrushRadiusY)));

            rectangle.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(Border.ActualWidth))
            {
                Source = ContainerBorder
            });

            rectangle.SetBinding(FrameworkElement.HeightProperty, new Binding(nameof(Border.ActualHeight))
            {
                Source = ContainerBorder
            });

            VisualBrush visualBrush = new VisualBrush(rectangle);
            ContainerBorder.BorderBrush = visualBrush;
        }

        private StackPanel CreateTitleCard()
        {
            StackPanel TitleCard = new StackPanel
            {
                Orientation = NodeTitleCardOrientation,
                IsHitTestVisible = (NodeType == BNType.Choice) ? true : false
            };

            Image TitleIcon = new Image
            {
                Source = NodeTitleIcon,
                Width = ViewModel.NodeTitleIconWidth,
                Height = ViewModel.NodeTitleIconHeight
            };

            TitleIcon.SetBinding(Image.WidthProperty, new Binding(nameof(ViewModel.NodeTitleIconWidth)));
            TitleIcon.SetBinding(Image.HeightProperty, new Binding(nameof(ViewModel.NodeTitleIconHeight)));

            TitleCard.Children.Add(TitleIcon);

            if (NodeType == BNType.Dialogue)
            {
                TextBlock DialogueTitle = new TextBlock
                {
                    Style = NodeTitleStyle,
                    Width = ViewModel.NodeTitleDialogueTextBlockWidth,
                    Text = NodeTitleDialogueTextBlockText,
                    TextTrimming = NodeTitleDialogueTextBlockTextTrimming,
                    FontSize = ViewModel.NodeTitleFontSize,
                    VerticalAlignment = NodeTitleVerticalAlignment,
                    Margin = ViewModel.NodeTitleMargin
                };

                DialogueTitle.SetBinding(TextBlock.WidthProperty, new Binding(nameof(ViewModel.NodeTitleDialogueTextBlockWidth)));
                DialogueTitle.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(ViewModel.NodeTitleFontSize)));
                DialogueTitle.SetBinding(TextBlock.MarginProperty, new Binding(nameof(ViewModel.NodeTitleMargin)));

                TitleCard.Children.Add(DialogueTitle);
            }
            else
            {
                Grid TextRegionGrid = new Grid { VerticalAlignment = VerticalAlignment.Top };

                ChoiceRTB = new RichTextBox
                {
                    Style = NodeChoiceRTBStyle,
                    VerticalAlignment = NodeChoiceRTBVerticalAlignment,
                    Width = ViewModel.NodeChoiceRTBWidth,
                    FontSize = ViewModel.RTBFontSize
                };

                ChoiceRTB.SetBinding(RichTextBox.FontSizeProperty, new Binding(nameof(ViewModel.RTBFontSize)));
                ChoiceRTB.SetBinding(RichTextBox.WidthProperty, new Binding(nameof(ViewModel.NodeChoiceRTBWidth)));

                // Temp Test
                ChoiceRTB.SpellCheck.IsEnabled = true;
                ChoiceRTB.Document.Blocks.Clear();
                ChoiceParagraph = new Paragraph();
                ChoiceParagraph.Inlines.Add(new Run { Text = "", FontWeight = FontWeights.Normal });
                ChoiceRTB.Document.Blocks.Add(ChoiceParagraph);
                ChoiceRTB.TextChanged += ChoiceRTB_TextChanged;
                ChoiceRTB.PreviewKeyDown += ChoiceRTB_KeyDown;
                ChoiceRTB.PreviewMouseLeftButtonDown += RTB_PreviewMouseLeftButtonDown;
                TextRegionGrid.Children.Add(ChoiceRTB);

                ChoiceMarkTextBlock = new TextBlock
                {
                    Style = NodeMarkStyle,
                    Text = NodeChoiceMarkText,
                    Foreground = NodeMarkColor,
                    FontStyle = NodeMarkFontStyle,
                    FontSize = ViewModel.NodeMarkFontSize,
                    VerticalAlignment = NodeMarkVerticalAlignment,
                    Width = ViewModel.NodeRTBWidth,
                    IsHitTestVisible = NodeMarkIsHitTestVisible,
                    Margin = ViewModel.NodeMarkMargin
                };

                ChoiceMarkTextBlock.SetBinding(TextBlock.WidthProperty, new Binding(nameof(ViewModel.NodeRTBWidth)));
                ChoiceMarkTextBlock.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(ViewModel.NodeMarkFontSize)));
                ChoiceMarkTextBlock.SetBinding(TextBlock.MarginProperty, new Binding(nameof(ViewModel.NodeMarkMargin)));

                TextRegionGrid.Children.Add(ChoiceMarkTextBlock);

                TitleCard.Children.Add(TextRegionGrid);
            }

            return TitleCard;
        }

        private StackPanel CreateCharacterCard()
        {
            StackPanel CharacterCard = new StackPanel
            {
                Orientation = NodeCharacterOrientation,
                Margin = ViewModel.NodeCharacterMargin
            };

            CharacterCard.SetBinding(StackPanel.MarginProperty, new Binding(nameof(ViewModel.NodeCharacterMargin)));

            Image CharacterIcon = new Image
            {
                Source = NodeCharacterIcon,
                Width = ViewModel.NodeCharacterIconWidth,
                Height = ViewModel.NodeCharacterIconHeight,
                VerticalAlignment = NodeCharacterIconVerticalAlignment
            };

            CharacterIcon.SetBinding(Image.WidthProperty, new Binding(nameof(ViewModel.NodeCharacterIconWidth)));
            CharacterIcon.SetBinding(Image.HeightProperty, new Binding(nameof(ViewModel.NodeCharacterIconHeight)));

            CharacterCard.Children.Add(CharacterIcon);

            Grid TextRegionGrid = new Grid { VerticalAlignment = VerticalAlignment.Top };

            CharacterRTB = new RichTextBox
            {
                Style = NodeCharacterRTBStyle,
                VerticalAlignment = NodeCharacterRTBVerticalAlignment,
                Width = ViewModel.NodeRTBWidth,
                FontSize = ViewModel.RTBFontSize
            };

            CharacterRTB.SetBinding(RichTextBox.FontSizeProperty, new Binding(nameof(ViewModel.RTBFontSize)));
            CharacterRTB.SetBinding(RichTextBox.WidthProperty, new Binding(nameof(ViewModel.NodeRTBWidth)));

            // Temp Test
            CharacterRTB.SpellCheck.IsEnabled = true;
            CharacterRTB.Document.Blocks.Clear();
            CharacterParagraph = new Paragraph();
            CharacterParagraph.Inlines.Add(new Run { Text = "", FontWeight = FontWeights.Normal });
            CharacterRTB.Document.Blocks.Add(CharacterParagraph);
            CharacterRTB.TextChanged += CharacterRTB_TextChanged;
            CharacterRTB.PreviewKeyDown += CharacterRTB_KeyDown;
            CharacterRTB.PreviewMouseLeftButtonDown += RTB_PreviewMouseLeftButtonDown;
            TextRegionGrid.Children.Add(CharacterRTB);

            CharacterMarkTextBlock = new TextBlock
            {
                Style = NodeMarkStyle,
                Text = NodeCharacterMarkText,
                Foreground = NodeMarkColor,
                FontStyle = NodeMarkFontStyle,
                FontSize = ViewModel.NodeMarkFontSize,
                VerticalAlignment = NodeMarkVerticalAlignment,
                Width = ViewModel.NodeRTBWidth,
                IsHitTestVisible = NodeMarkIsHitTestVisible,
                Margin = ViewModel.NodeMarkMargin
            };

            CharacterMarkTextBlock.SetBinding(TextBlock.WidthProperty, new Binding(nameof(ViewModel.NodeRTBWidth)));
            CharacterMarkTextBlock.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(ViewModel.NodeMarkFontSize)));
            CharacterMarkTextBlock.SetBinding(TextBlock.MarginProperty, new Binding(nameof(ViewModel.NodeMarkMargin)));

            TextRegionGrid.Children.Add(CharacterMarkTextBlock);

            CharacterCard.Children.Add(TextRegionGrid);

            return CharacterCard;
        }

        private StackPanel CreateDialogueCard()
        {
            StackPanel DialogueCard = new StackPanel
            {
                Orientation = NodeDialogueOrientation,
                Margin = ViewModel.NodeDialogueMargin
            };

            DialogueCard.SetBinding(StackPanel.MarginProperty, new Binding(nameof(ViewModel.NodeDialogueMargin)));

            Image DialogueIcon = new Image
            {
                Source = NodeDialogueIcon,
                Width = ViewModel.NodeDialogueIconWidth,
                Height = ViewModel.NodeDialogueIconHeight,
                VerticalAlignment = NodeDialogueIconVerticalAlignment
            };

            DialogueIcon.SetBinding(Image.WidthProperty, new Binding(nameof(ViewModel.NodeDialogueIconWidth)));
            DialogueIcon.SetBinding(Image.HeightProperty, new Binding(nameof(ViewModel.NodeDialogueIconHeight)));

            DialogueCard.Children.Add(DialogueIcon);

            DialogueRTB = new RichTextBox
            {
                Style = NodeDialogueRTBStyle,
                VerticalAlignment = NodeDialogueRTBVerticalAlignment,
                Width = ViewModel.NodeDialogueRTBWidth,
                FontSize = ViewModel.RTBFontSize
            };

            DialogueRTB.SetBinding(RichTextBox.FontSizeProperty, new Binding(nameof(ViewModel.RTBFontSize)));
            DialogueRTB.SetBinding(RichTextBox.WidthProperty, new Binding(nameof(ViewModel.NodeDialogueRTBWidth)));

            Grid TextRegionGrid = new Grid { VerticalAlignment = VerticalAlignment.Top };

            // Temp Test
            DialogueRTB.SpellCheck.IsEnabled = true;
            DialogueRTB.Document.Blocks.Clear();
            DialogueParagraph = new Paragraph();
            DialogueParagraph.Inlines.Add(new Run { Text = "", FontWeight = FontWeights.Normal });
            DialogueRTB.Document.Blocks.Add(DialogueParagraph);
            DialogueRTB.TextChanged += DialogueRTB_TextChanged;
            DialogueRTB.PreviewKeyDown += DialogueRTB_PreviewKeyDown;
            DialogueRTB.PreviewMouseLeftButtonDown += RTB_PreviewMouseLeftButtonDown;
            TextRegionGrid.Children.Add(DialogueRTB);

            DialogueMarkTextBlock = new TextBlock
            {
                Style = NodeMarkStyle,
                Text = NodeDialogueMarkText,
                Foreground = NodeMarkColor,
                FontStyle = NodeMarkFontStyle,
                FontSize = ViewModel.NodeMarkFontSize,
                VerticalAlignment = NodeMarkVerticalAlignment,
                Width = ViewModel.NodeRTBWidth,
                IsHitTestVisible = NodeMarkIsHitTestVisible,
                Margin = ViewModel.NodeMarkMargin
            };

            DialogueMarkTextBlock.SetBinding(TextBlock.WidthProperty, new Binding(nameof(ViewModel.NodeRTBWidth)));
            DialogueMarkTextBlock.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(ViewModel.NodeMarkFontSize)));
            DialogueMarkTextBlock.SetBinding(TextBlock.MarginProperty, new Binding(nameof(ViewModel.NodeMarkMargin)));

            TextRegionGrid.Children.Add(DialogueMarkTextBlock);

            DialogueCard.Children.Add(TextRegionGrid);

            return DialogueCard;
        }

        private TextBlock CreateNodeOrderTextBlock()
        {
            NodeOrderTextBlock = new TextBlock
            {
                Style = NodeMarkStyle,
                Text = NodeOrder.ToString() + ".",
                HorizontalAlignment = NodeOrderTextBlockHorizontalAlignment,
                FontSize = ViewModel.NodeOrderTextBlockFontSize,
                IsHitTestVisible = NodeOrderTextBlockIsHitTestVisible,
                Margin = ViewModel.NodeOrderTextBlockMargin
            };

            NodeOrderTextBlock.SetBinding(TextBlock.FontSizeProperty, new Binding(nameof(ViewModel.NodeOrderTextBlockFontSize)));
            NodeOrderTextBlock.SetBinding(TextBlock.MarginProperty, new Binding(nameof(ViewModel.NodeOrderTextBlockMargin)));

            return NodeOrderTextBlock;
        }
        #endregion

        private void CollisionBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            ViewModel.CanReceiveCanvasMouseInput = false;

            if (ViewModel.IsConnecting == false && ChildrenPath.Count == 0)
            {
                OpacityAnimation(1, ChildrenNodeCollisionBorder);
                ChildrenNodeCollisionBorder.IsHitTestVisible = true;
            }

            NotifyPathConnectionColorUpdate?.Invoke(this, true);
        }

        private void CollisionBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ViewModel.CanReceiveCanvasMouseInput = true;

            if (ViewModel.IsConnecting == false && ChildrenPath.Count == 0)
            {
                OpacityAnimation(0, ChildrenNodeCollisionBorder);
                ChildrenNodeCollisionBorder.IsHitTestVisible = false;
            }

            NotifyPathConnectionColorUpdate?.Invoke(this, false);
        }

        public void OpacityAnimation(double Target, DependencyObject dependencyObject)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation ControlOpacityAnimation = new DoubleAnimation();
            ControlOpacityAnimation.BeginTime = TimeSpan.FromSeconds(0);
            ControlOpacityAnimation.To = Target;
            ControlOpacityAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            Storyboard.SetTarget(ControlOpacityAnimation, dependencyObject);
            Storyboard.SetTargetProperty(ControlOpacityAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(ControlOpacityAnimation);

            storyboard.Begin();
        }

        private void DialogueNode_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitialMousePosition = Mouse.GetPosition(null);
            Mouse.Capture((UIElement)sender);
            LeftMousePressed = true;

            if (IsSelectedInMultiSelectionProcess == false)
            {
                UpdateNodeSelectionStatus();
                NodeRequestedSelectedNodesRemoval.Invoke(this, EventArgs.Empty);
            }
        }

        private void DialogueNode_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LeftMousePressed = false;
            Mouse.Capture(null);

            if (IsSelectedInMultiSelectionProcess == true)
                NodeRequestedSelectedNodesToStop.Invoke(this, EventArgs.Empty);

            NodePositionChanged.Invoke(this, new DroppedNodeEventArgs(this, Position));
        }

        private void DialogueNode_MouseMove(object sender, MouseEventArgs e)
        {
            if(LeftMousePressed == true && IsSelectedInMultiSelectionProcess == false)
            {
                Point CurrentMousePosition = Mouse.GetPosition(null);
                double DeltaX = InitialMousePosition.X - CurrentMousePosition.X;
                double DeltaY = InitialMousePosition.Y - CurrentMousePosition.Y;

                double NewLocationX = Canvas.GetLeft(this) - DeltaX;
                double NewLocationY = Canvas.GetTop(this) - DeltaY;

                if (NewLocationX > 0 && NewLocationX < ViewModel.MainCanvas.Width)
                {
                    Canvas.SetLeft(this, NewLocationX);
                    Position.X = NewLocationX;
                }

                if (NewLocationY > 0 && NewLocationY < ViewModel.MainCanvas.Height)
                {
                    Canvas.SetTop(this, NewLocationY);
                    Position.Y = NewLocationY;
                }

                InitialMousePosition = CurrentMousePosition;

                UpdatePathsLocation();

                if (NodeType == BNType.Choice)
                {
                    NodeRequestedOrderUpdate.Invoke(this, EventArgs.Empty);
                }
            }
            else if (LeftMousePressed == true && IsSelectedInMultiSelectionProcess == true)
            {
                if (IsSelectedInMultiSelectionProcess == true)
                    NodeRequestedSelectedNodesToMove.Invoke(this, EventArgs.Empty);
            }
        }

        private void ParentNodeCollisionBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ParentsPath.Count > 0 && ViewModel.CanRemoveConnectedNodesPaths == true && ViewModel.IsConnecting == false)
            {
                NodeRequestedConnectionRemoval.Invoke(this, new BranchNodeRemovalEventArgs(ParentsPath, ConnectionType.Parent));
                ParentsPath.Clear();
            }

            if (ViewModel.IsConnecting == true)
                ChildNodeRequestedConnection.Invoke(this, new EventArgs());
        }

        private void ChildrenNodeCollisionBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            InitialMousePosition = Mouse.GetPosition(null);

            if (ChildrenPath.Count > 0 && ViewModel.CanRemoveConnectedNodesPaths == true && ViewModel.IsConnecting == false)
            {
                NodeRequestedConnectionRemoval.Invoke(this, new BranchNodeRemovalEventArgs(ChildrenPath, ConnectionType.Child));
                ChildrenPath.Clear();
            }
        }

        private void ChildrenNodeCollisionBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point CurrentMousePosition = Mouse.GetPosition(null);
            if (InitialMousePosition == CurrentMousePosition && ViewModel.IsConnecting == false && ViewModel.CanRemoveConnectedNodesPaths == false)
            {
                NodeStartedConnectionProcess.Invoke(this, new EventArgs());
            }
        }

        private void ChildrenNodeCollisionBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (CanDrawPathLine == true)
            {
                /*
                 * (Keep in mind some of these things are done differently)
                 * TODO: We can't use this mouse move for the border, instead we will follow these steps:
                 *      
                 *      - When pressing on the collision and releasing the button, if "CanDrawPathLine" is true we capture
                 *        this node so we know which parent we are currently holding.
                 *      
                 *      - After capturing the parent, we invoke an event for our BN class to record the current node we are holding.
                 *      - We then press on the parent collision of another node and if the current node that is waiting isn't nullptr,
                 *        we create our path and connect both nodes.
                 *        
                 *        [Done]
                 *      =================================================================================================================
                 *      Things to keep in mind:
                 *      
                 *      - When capturing a parent collision, we need to handle these situations:
                 *          * If the user released on the same node. [DONE]
                 *          * If the user released outside node collision. [DONE]
                 *          * If the user tried to connect the children collision to the parent collision of the same node. [Done]
                 *          * If the action is dragging, not clicking.
                 *      
                 *      [In-Progress]
                 *      =================================================================================================================
                 *      It would be nice if:
                 *      
                 *      - Ctrl + press on the connected collision to remove connections. [Done]
                 *      - For now, we won't add arrow shape for head. If everything went smoothly then give the arrow head a try. [No Need for arrow head, the design changed!]
                 *      - Delete node. [DONE] [ONLY THROUGH SHORTCUT. ADD RIGHT-CLICK IMPLEMENTATION]
                 *      - Multi selection and move multiple nodes simultaneously. [DONE] [Don't forget to add the ability to delete multi-selected nodes!] [Also DONE]
                 *      - Scaling [NIGHTMARE BUT MUST BE DONE] [DONE BROOOOOO]. BUT, We scale each item instead of the canvas as it is less destructive. [DONE]
                 *      - Branch Node can't have both Dialogue and player choice in the children list. Only one of them can exist! [DONE]
                 *      - Undo/Redo
                 *      
                 */
            }
        }

        #region Choice, Character and Dialogue RTB Logic
        private void ChoiceRTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CharacterRTB.CaretPosition = CharacterParagraph.ContentEnd;
                CharacterRTB.Focus();
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                _ = ManageCharacterParagraph(ChoiceRTB, ChoiceParagraph, ElementType.Choice);
            }
        }

        private async void ChoiceRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ManageMark(ChoiceParagraph, ChoiceMarkTextBlock);

            await Task.Delay(100);
            UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - 55 * ViewModel.Scale));
        }

        private void CharacterRTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogueRTB.CaretPosition = DialogueParagraph.ContentEnd;
                DialogueRTB.Focus();
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                _ = ManageCharacterParagraph(CharacterRTB, CharacterParagraph, ElementType.Character);
            }
        }

        private async void CharacterRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCharacterRTBCase();
            ManageMark(CharacterParagraph, CharacterMarkTextBlock);

            await Task.Delay(100);
            UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - 55 * ViewModel.Scale));
        }

        private void UpdateCharacterRTBCase()
        {
            int Offset = CharacterRTB.Document.ContentStart.GetOffsetToPosition(CharacterRTB.CaretPosition);

            foreach (var inline in CharacterParagraph.Inlines.ToList())
            {
                if (inline is Run run)
                {
                    run.Text = run.Text.ToUpper();
                }
            }

            TextPointer CurrentCaretPosition = CharacterRTB.Document.ContentStart.GetPositionAtOffset(Offset);
            CharacterRTB.CaretPosition = CurrentCaretPosition;
        }

        private async void DialogueRTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            ManageMark(DialogueParagraph, DialogueMarkTextBlock);

            await Task.Delay(100);
            UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - 55 * ViewModel.Scale));
        }

        private void DialogueRTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }

            if (e.Key == Key.Back)
            {
                _ = ManageCharacterParagraph(DialogueRTB, DialogueParagraph, ElementType.Dialogue);
            }
        }

        private void ManageMark(Paragraph paragraph, TextBlock textBlock)
        {
            TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
            if (textRange.IsEmpty)
                textBlock.Visibility = Visibility.Visible;
            else
                textBlock.Visibility = Visibility.Collapsed;
        }

        private async Task ManageCharacterParagraph(RichTextBox richTextBox, Paragraph paragraph, ElementType elementType)
        {
            await Task.Delay(100);

            if (richTextBox.Document.Blocks.Count == 0)
            {
                paragraph = new Paragraph();
                paragraph.Inlines.Add(new Run { Text = "", FontWeight = FontWeights.Normal });
                richTextBox.Document.Blocks.Add(paragraph);

                switch (elementType)
                {
                    case ElementType.Choice:
                        ChoiceParagraph = paragraph;
                        richTextBox.TextChanged += ChoiceRTB_TextChanged;
                        richTextBox.PreviewKeyDown += ChoiceRTB_KeyDown;
                        break;
                    case ElementType.Character:
                        CharacterParagraph = paragraph;
                        richTextBox.TextChanged += CharacterRTB_TextChanged;
                        richTextBox.PreviewKeyDown += CharacterRTB_KeyDown;
                        break;
                    case ElementType.Dialogue:
                        DialogueParagraph = paragraph;
                        richTextBox.TextChanged += DialogueRTB_TextChanged;
                        richTextBox.PreviewKeyDown += DialogueRTB_PreviewKeyDown;
                        break;
                }
            }
        }

        // I don't use this anymore and collision border updates automatically. Keep it for future use.
        private void HeightAnimation(double HeightTarget)
        {
            if (IsHeightAnimationPlaying == true)
                return;

            Storyboard storyboard = new Storyboard();

            DoubleAnimation ControlHeightAnimation = new DoubleAnimation();
            ControlHeightAnimation.BeginTime = TimeSpan.FromSeconds(0);
            ControlHeightAnimation.To = HeightTarget;
            ControlHeightAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.1));

            Storyboard.SetTarget(ControlHeightAnimation, CollisionBorder);
            Storyboard.SetTargetProperty(ControlHeightAnimation, new PropertyPath("Height"));
            storyboard.Children.Add(ControlHeightAnimation);

            storyboard.Completed += (s, e) =>
            {
                IsHeightAnimationPlaying = false;
                UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - 55));
            };

            storyboard.Begin();
            IsHeightAnimationPlaying = true;
        }

        private void UpdateChildrenPathLocation(Point NewPathEdgeLocation)
        {
            foreach (Path path in ChildrenPath)
            {
                if (path.Data is GeometryGroup geometryGroup)
                {
                    foreach (Geometry geometry in geometryGroup.Children)
                    {
                        if (geometry is LineGeometry line)
                        {
                            line.StartPoint = NewPathEdgeLocation;
                            break;
                        }
                    }
                }

                if (path.Stroke is LinearGradientBrush gradient)
                {
                    gradient.StartPoint = NewPathEdgeLocation;
                }
            }
        }

        private void UpdateParentsPathLocation(Point NewPathEdgeLocation)
        {
            foreach (Path path in ParentsPath)
            {
                if (path.Data is GeometryGroup geometryGroup)
                {
                    foreach (Geometry geometry in geometryGroup.Children)
                    {
                        if (geometry is LineGeometry line)
                        {
                            line.EndPoint = NewPathEdgeLocation;
                            break;
                        }
                    }
                }

                if (path.Stroke is LinearGradientBrush gradient)
                {
                    gradient.EndPoint = NewPathEdgeLocation;
                }
            }
        }

        private void RTB_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsSelectedInMultiSelectionProcess == false)
                UpdateNodeSelectionStatus();
        }
        #endregion

        private void UpdateNodeSelectionStatus()
        {
            if (ViewModel.SelectedDialogueNode != null && ViewModel.SelectedDialogueNode != this)
            {
                ViewModel.SelectedDialogueNode.ContainerBorder.BorderBrush = null;
            }

            Focus();
            ViewModel.SelectedDialogueNode = this;
            UpdateContainerBorderBrush();
            NodeSelectionChanged.Invoke(this, EventArgs.Empty);
        }

        public void UpdateNodeLocation(double X, double Y)
        {
            Canvas.SetLeft(this, X);
            Canvas.SetTop(this, Y);

            Position.X = Canvas.GetLeft(this);
            Position.Y = Canvas.GetTop(this);

            double YOffset = 55;
            if (IsRootNode == true)
                YOffset = 15;

            UpdateParentsPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + 55 * ViewModel.Scale));
            UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - YOffset * ViewModel.Scale));
        }

        public void UpdatePathsLocation()
        {
            double YOffset = 55;
            if (IsRootNode == true)
                YOffset = 15;

            UpdateParentsPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + 55 * ViewModel.Scale));
            UpdateChildrenPathLocation(new Point(Position.X + (this.ActualWidth / 2), Position.Y + this.ActualHeight - YOffset * ViewModel.Scale));
        }

        public void UpdateNodeConnectionBackgroundColor(ConnectionType connectionType)
        {
            if (connectionType == ConnectionType.Parent)
            {
                if (ParentsPath.Count > 0)
                {
                    ParentNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor;
                    ParentNodeCollisionBorder.BorderBrush = null;
                }
                else
                {
                    ParentNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? NodeCollisionBorderUnconnectedPlayerChoiceBackground : NodeCollisionBorderUnconnectedDialogueBackground;
                    ParentNodeCollisionBorder.BorderBrush = CreateNodeAttachmentBorderBrush(ParentNodeCollisionBorder);
                }
            }
            else
            {
                if (ChildrenPath.Count > 0)
                {
                    ChildrenNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor;
                    ChildrenNodeCollisionBorder.BorderBrush = null;
                }
                else
                {
                    ChildrenNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? NodeCollisionBorderUnconnectedPlayerChoiceBackground : NodeCollisionBorderUnconnectedDialogueBackground;
                    ChildrenNodeCollisionBorder.BorderBrush = CreateNodeAttachmentBorderBrush(ChildrenNodeCollisionBorder);
                }
            }
        }

        public void UpdateNodeConnectionBackgroundColor(ConnectionType connectionType, bool IsFullyColored)
        {
            if (connectionType == ConnectionType.Parent)
            {
                if (IsFullyColored == true)
                {
                    ParentNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor;
                    ParentNodeCollisionBorder.BorderBrush = null;
                }
                else
                {
                    ParentNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? NodeCollisionBorderUnconnectedPlayerChoiceBackground : NodeCollisionBorderUnconnectedDialogueBackground;
                    ParentNodeCollisionBorder.BorderBrush = CreateNodeAttachmentBorderBrush(ParentNodeCollisionBorder);
                }
            }
            else
            {
                if (IsFullyColored == true)
                {
                    ChildrenNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? ChoiceContainerBackgroundColor : DialogueContainerBackgroundColor;
                    ChildrenNodeCollisionBorder.BorderBrush = null;
                }
                else
                {
                    ChildrenNodeCollisionBorder.Background = (NodeType == BNType.Choice) ? NodeCollisionBorderUnconnectedPlayerChoiceBackground : NodeCollisionBorderUnconnectedDialogueBackground;
                    ChildrenNodeCollisionBorder.BorderBrush = CreateNodeAttachmentBorderBrush(ChildrenNodeCollisionBorder);
                }
            }
        }

        public void UpdateNodeOrder(int Index)
        {
            NodeOrder = Index;
            NodeOrderTextBlock.Text = NodeOrder.ToString() + ".";
        }

        public void UpdateNodeMultiSelectionStatus(bool ShowSelectionVisual)
        {
            if (ShowSelectionVisual == true)
            {
                if (ViewModel.SelectedDialogueNode != null)
                {
                    ViewModel.SelectedDialogueNode.ContainerBorder.BorderBrush = null;
                    ViewModel.SelectedDialogueNode = null;
                    ViewModel.SelectedBranchNode = null;
                }

                UpdateContainerBorderBrush();
            }
            else
            {
                IsSelectedInMultiSelectionProcess = false;
                ContainerBorder.BorderBrush = null;
            }
        }

        public void ResetSelectedNodeVisual()
        {
            ContainerBorder.BorderBrush = null;
        }
    }
}
