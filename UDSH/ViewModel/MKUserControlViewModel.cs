using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Xml;
using UDSH.Model;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;

namespace UDSH.ViewModel
{
    enum TextType
    {
        Normal,
        Header1,
        Header2,
        Header3,
        List
    }
    enum ListNavigation
    {
        Mouse,
        ArrowKeyUp,
        ArrowKeyDown
    }
    public class MKUserControlViewModel : ViewModelBase
    {
        #region Properties
        private readonly IWorkspaceServices _workspaceServices;
        private Point InitialMousePosition;
        private Point CurrentMousePosition;

        private List CurrentList;
        private ListItem CurrentListItem;

        private bool IsNextToAMarkdown;
        private bool IsListItem;
        private bool ReachedListBoundary;
        private bool CanDeleteAllText;

        private bool IsMouseLeftButtonPressed;
        private bool CanCreateANewNote;

        private bool firstStartAnimPlayed;
        public bool FirstStartAnimPlayed
        {
            get { return firstStartAnimPlayed; }
            set { firstStartAnimPlayed = value; OnPropertyChanged(); }
        }

        private bool canUpdateTransition;
        public bool CanUpdateTransition
        {
            get { return canUpdateTransition; }
            set { canUpdateTransition = value; OnPropertyChanged(); }
        }

        private bool isInsideScrollBar;
        public bool IsInsideScrollBar
        {
            get { return isInsideScrollBar; }
            set { isInsideScrollBar = value; OnPropertyChanged(); }
        }

        private bool isScrollBarMouseDown;
        public bool IsScrollBarMouseDown
        {
            get { return isScrollBarMouseDown; }
            set { isScrollBarMouseDown = value; OnPropertyChanged(); }
        }

        private bool isInsideScrollBarHitCollision;
        public bool IsInsideScrollBarHitCollision
        {
            get { return isInsideScrollBarHitCollision; }
            set { isInsideScrollBarHitCollision = value; OnPropertyChanged(); }
        }

        private RichTextBox mKRichTextBox;
        public RichTextBox MKRichTextBox
        {
            get { return mKRichTextBox; }
            set { mKRichTextBox = value; OnPropertyChanged(); }
        }

        private Paragraph LastPickedParagraph;
        private int LastPickedParagraphListIndex;

        private int NormalFontSize;
        private int HeaderOneFontSize;
        private int HeaderTwoFontSize;
        private int HeaderThreeFontSize;

        private Thickness DefaultMargin;

        private Grid GridTarget;
        private Grid ParentGridTarget;
        private Button NoteButton;

        public MKUserControl MKCurrentUserControl;
        private NoteUserControl TempNoteUserControl;

        private bool FirstLaunch;
        private FileSystem file;
        #endregion

        #region Commands
        public RelayCommand<RichTextBox> RichTextLoaded => new RelayCommand<RichTextBox>(OnRichTextBoxLoaded);

        public RelayCommand<Button> AlignLeft => new RelayCommand<Button>(execute => AlignParagraphTargetLeft());
        public RelayCommand<Button> AlignCenter => new RelayCommand<Button>(execute => AlignParagraphTargetCenter());
        public RelayCommand<Button> AlignRight => new RelayCommand<Button>(execute => AlignParagraphTargetRight());

        public RelayCommand<Button> BoldText => new RelayCommand<Button>(execute => ChangeBoldText());
        public RelayCommand<Button> ItalicText => new RelayCommand<Button>(execute => ChangeItalicText());
        public RelayCommand<Button> StrikethroughText => new RelayCommand<Button>(execute => ChangeStrikethroughText());
        public RelayCommand<Button> UnderlineText => new RelayCommand<Button>(execute => ChangeUnderlineText());

        public RelayCommand<KeyEventArgs> PressedEnter => new RelayCommand<KeyEventArgs>(execute => CreateANewParagraph(), canExecute => CanCreateANewParagraph());
        public RelayCommand<KeyEventArgs> DeleteAll => new RelayCommand<KeyEventArgs>(execute => DeleteAllText());

        public RelayCommand<Paragraph> CaptureParagraph => new RelayCommand<Paragraph> (GetParagraph);
        public RelayCommand<RichTextBox> ParagraphFocus => new RelayCommand<RichTextBox>(execute => UpdateLastPickedParagraph());

        public RelayCommand<ScrollViewer> ScrollChanged => new RelayCommand<ScrollViewer>(UpdateVisuals);
        public RelayCommand<Grid> GridLoaded => new RelayCommand<Grid>(InitiateGrid);

        public RelayCommand<Rectangle> SideScrollCollisionMouseEnter => new RelayCommand<Rectangle>(UpdatedScrollBarShape);
        public RelayCommand<Rectangle> SideScrollCollisionMouseLeave => new RelayCommand<Rectangle>(UpdatedScrollBarShape);

        public RelayCommand<ScrollBar> SideScrollMouseEnter => new RelayCommand<ScrollBar>(UpdatedScrollBarData);
        public RelayCommand<ScrollBar> SideScrollMouseLeave => new RelayCommand<ScrollBar>(UpdatedScrollBarData);

        public RelayCommand<Grid> ParentGridLoaded => new RelayCommand<Grid>(GetParentGridRef);
        public RelayCommand<Button> NoteButtonLoaded => new RelayCommand<Button>(GetNoteButtonRef);
        public RelayCommand<MouseButtonEventArgs> AddNote => new RelayCommand<MouseButtonEventArgs>(NoteFunctionality);
        public RelayCommand<MouseEventArgs> NoteButtonMouseMove => new RelayCommand<MouseEventArgs>(RecordNoteButtonMouseMovement);
        public RelayCommand<MouseButtonEventArgs> StopAddingNoteProcess => new RelayCommand<MouseButtonEventArgs>(StopRecordingNoteButtonMouseMovement);

        public RelayCommand<object> SaveButton => new RelayCommand<object>(execute => SaveContent());
        #endregion

        public MKUserControlViewModel(IWorkspaceServices workspaceServices) //MKUserControl control
        {
            _workspaceServices = workspaceServices;

            IsNextToAMarkdown = false;
            IsListItem = false;
            ReachedListBoundary = false;
            CanDeleteAllText = false;
            CanUpdateTransition = false;
            FirstStartAnimPlayed = false;
            IsInsideScrollBar = false;
            IsScrollBarMouseDown = false;
            IsInsideScrollBarHitCollision = false;
            IsMouseLeftButtonPressed = false;
            CanCreateANewNote = true;
            FirstLaunch = true;

            NormalFontSize = 20;
            HeaderOneFontSize = 40;
            HeaderTwoFontSize = 30;
            HeaderThreeFontSize = 25;

            //MKCurrentUserControl = control;
        }

        #region Rich Text Box Initiation And Starting The First Paragraph
        private void OnRichTextBoxLoaded(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                MKRichTextBox = richTextBox;

                if (_workspaceServices.UserDataServices.CurrentSelectedFile != null && FirstLaunch == true)
                    LoadContent();
            }
        }

        private void InitiateFirstParagraph()
        {
            Paragraph FirstParagraph = new Paragraph();
            FirstParagraph.Tag = TextType.Normal;
            LastPickedParagraph = FirstParagraph;

            MKRichTextBox.Document.Blocks.Add(LastPickedParagraph);
        }
        #endregion

        #region Text Alignment Functions
        private void AlignParagraphTargetLeft()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Left;
        }
        private void AlignParagraphTargetCenter()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Center;
        }

        private void AlignParagraphTargetRight()
        {
            if (LastPickedParagraph != null)
                LastPickedParagraph.TextAlignment = TextAlignment.Right;
        }
        #endregion

        #region Text Font Properties Functions
        private void ChangeBoldText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(TextElement.FontWeightProperty).Equals(FontWeights.Bold))
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
        }

        private void ChangeItalicText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(TextElement.FontStyleProperty).Equals(FontStyles.Italic))
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
        }

        private void ChangeStrikethroughText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Strikethrough))
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Strikethrough);
            }
        }

        private void ChangeUnderlineText()
        {
            if (MKRichTextBox != null)
            {
                if (MKRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty).Equals(TextDecorations.Underline))
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                else
                    MKRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
        }
        #endregion

        #region Create A New Paragraph And Additional Functionalities
        private void CreateANewParagraph()
        {
            Paragraph paragraph = new Paragraph();

            BlockCollection Blocks = MKRichTextBox.Document.Blocks;
            Block CurrentBlock = Blocks.FirstBlock;

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;

            if (IsListItem == true)
            {
                Paragraph NewListParagraph = new Paragraph();
                ListItem NewListItem = new ListItem(NewListParagraph);

                PerserveTextAndStyle(NewListParagraph);

                CurrentList.ListItems.InsertAfter(CurrentListItem, NewListItem);
                CurrentListItem = NewListItem;
                LastPickedParagraphListIndex++;

                //MKRichTextBox.Document.Blocks.Add(CurrentList);
                MKRichTextBox.CaretPosition = NewListParagraph.ContentStart;
                LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
                LastPickedParagraph.Tag = TextType.List;

                return;
            }

            while (CurrentBlock != null)
            {
                if (CurrentBlock == LastPickedParagraph)
                {
                    PerserveTextAndStyle(paragraph);

                    Blocks.InsertAfter(CurrentBlock, paragraph);
                    MKRichTextBox.CaretPosition = paragraph.ContentStart;

                    LastPickedParagraph.Focusable = true;
                    paragraph.Focusable = false;
                    LastPickedParagraph = paragraph;
                    LastPickedParagraph.Tag = TextType.Normal;

                    if (IsNextToAMarkdown == true)
                    {
                        Thickness HeaderThickness = new Thickness(0, 10, 0, 0);
                        LastPickedParagraph.Margin = HeaderThickness;

                        IsNextToAMarkdown = false;
                    }

                    return;
                }

                CurrentBlock = CurrentBlock.NextBlock;
            }
        }

        private void PerserveTextAndStyle(Paragraph PreserveParagraph)
        {
            if (MKRichTextBox.CaretPosition != LastPickedParagraph.ContentEnd)
            {
                TextRange textRange = new TextRange(MKRichTextBox.CaretPosition, LastPickedParagraph.ContentEnd);

                using (var memoryStream = new MemoryStream())
                {
                    textRange.Save(memoryStream, DataFormats.Xaml);
                    memoryStream.Position = 0;

                    TextRange NewPara = new TextRange(PreserveParagraph.ContentStart, PreserveParagraph.ContentEnd);
                    NewPara.Load(memoryStream, DataFormats.Xaml);
                }

                /*run = new Run(textRange.Text);
                paragraph.Inlines.Add(run);*/

                textRange.Text = string.Empty;
            }
        }

        private bool CanCreateANewParagraph()
        {
            if (MKRichTextBox.Selection.Text == "" || IsListItem == true)
                return true;

            ManageSelection();
            return false;
        }

        private void ManageSelection()
        {
            if (MKRichTextBox != null)
            {
                Debug.WriteLine($"What is after End :{MKRichTextBox.Selection.End.GetTextInRun(LogicalDirection.Forward)}[stop]");

                if (MKRichTextBox.Selection.End.GetTextInRun(LogicalDirection.Forward) == "")
                {
                    MKRichTextBox.Selection.Text = "";
                }
                else
                {
                    TextPointer CurrentPointer = MKRichTextBox.Selection.End;

                    Paragraph Para1 = MKRichTextBox.Selection.Start.Paragraph;

                    MKRichTextBox.Selection.Text = "\n";
                    MKRichTextBox.CaretPosition = CurrentPointer;

                    Paragraph Para2 = MKRichTextBox.Selection.End.Paragraph;
                    LastPickedParagraph = Para2;
                    Para2.Focusable = false;

                    if (Para1 != Para2)
                        Para1.Focusable = true;
                }
            }
        }
        #endregion

        #region Paragraph Selection And Navigation Logic
        private void GetParagraph(Paragraph paragraph)
        {
            if (paragraph != null)
            {
                if (LastPickedParagraph != null)
                    LastPickedParagraph.Focusable = true;

                paragraph.Focusable = false;
                LastPickedParagraph = paragraph;

                CheckParagraphStatus();

                TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                string ParaText = textRange.Text;

                //Debug.WriteLine($"Text : {ParaText}");
            }
        }

        private void CheckParagraphStatus()
        {
            var SelectedListItem = LastPickedParagraph.Parent as ListItem;
            if (SelectedListItem != null)
            {
                //Debug.WriteLine("I do have the list item");

                IsListItem = true;

                CurrentListItem = SelectedListItem;

                var SelectedList = SelectedListItem.Parent as List;

                if (SelectedList != null)
                {
                    CurrentList = SelectedList;
                    int index = 0;
                    foreach (var item in SelectedList.ListItems)
                    {
                        if (item == SelectedListItem)
                            break;
                        index++;
                    }
                    LastPickedParagraphListIndex = index;

                    TextRange textRange = new TextRange(SelectedListItem.ContentStart, SelectedListItem.ContentEnd);
                    string ParaText = textRange.Text;

                    //Debug.WriteLine($"Index = {index} And The Item Is : {ParaText}");
                }
            }
            else
            {
                IsListItem = false;
            }
        }

        public async void NavigateListItem(Key key)
        {
            await AssignLastPara(key);
        }

        private async Task AssignLastPara(Key key)
        {
            await Task.Delay(50);

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            string ParaText = textRange.Text;

            Debug.WriteLine($"Current Paragraph: {ParaText}");

            CheckParagraphStatus();
        }

        private void UpdateLastPickedParagraph()
        {
            if (MKRichTextBox != null)
            {
                TextPointer caretPosition = MKRichTextBox.CaretPosition;
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null && paragraph.IsKeyboardFocused == false)
                {
                    paragraph.Focus();

                    if (LastPickedParagraph != null)
                        LastPickedParagraph.Focusable = true;

                    LastPickedParagraph = paragraph;
                    LastPickedParagraph.Focusable = false;

                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"MK Paragraph: {textRange.Text}");
                }
            }
        }
        #endregion

        #region Markdown Functionalities
        public bool SpaceMarkdown()
        {
            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);

            if(textRange.Text.Equals("#"))
            {
                Debug.WriteLine($"Play # Markdown...");
                SpaceMarkdown_Header(textRange, HeaderOneFontSize, TextType.Header1);

                return true;
            }
            else if(textRange.Text.Equals("##"))
            {
                Debug.WriteLine($"Play ## Markdown...");
                SpaceMarkdown_Header(textRange, HeaderTwoFontSize, TextType.Header2);

                return true;
            }
            else if(textRange.Text.Equals("###"))
            {
                Debug.WriteLine($"Play ### Markdown...");
                SpaceMarkdown_Header(textRange, HeaderThreeFontSize, TextType.Header3);

                return true;
            }
            else if(textRange.Text.Equals("-"))
            {
                Debug.WriteLine($"Play - Markdown...");
                
                Paragraph paragraph = new Paragraph();

                CurrentListItem = new ListItem(paragraph);
                CurrentList = new List();
                CurrentList.ListItems.Add(CurrentListItem);

                BlockCollection Blocks = MKRichTextBox.Document.Blocks;
                Block CurrentBlock = Blocks.FirstBlock;

                while (CurrentBlock != null)
                {
                    if (CurrentBlock == LastPickedParagraph)
                    {
                        Blocks.InsertAfter(CurrentBlock, CurrentList);
                        MKRichTextBox.CaretPosition = CurrentList.ContentEnd;
                        MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);

                        LastPickedParagraph.Focusable = true;
                        paragraph.Focusable = false;

                        LastPickedParagraph = paragraph;
                        LastPickedParagraph.Tag = TextType.List;
                        //LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
                        textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
                        Debug.WriteLine($"Line of the last paragraph:{textRange.Text}");

                        IsListItem = true;

                        return true;
                    }

                    CurrentBlock = CurrentBlock.NextBlock;
                }


                /*MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                MKRichTextBox.Document.Blocks.Add(CurrentList);
                MKRichTextBox.CaretPosition = CurrentList.ContentEnd;
                
                IsListItem = true;*/

                return false;
            }
            else
            {
                return false;
            }
                
        }

        private void SpaceMarkdown_Header(TextRange textRange, int FontSize, TextType DataType)
        {
            textRange.Text = "";
            LastPickedParagraph.FontSize = FontSize;

            DefaultMargin = LastPickedParagraph.Margin;
            Thickness HeaderThickness = new Thickness(0, 0, 0, 0);
            LastPickedParagraph.Margin = HeaderThickness;

            LastPickedParagraph.Tag = DataType;

            IsNextToAMarkdown = true;
        }
        #endregion

        #region Deletion Logic
        public async Task<bool> SetLastPickedParagraphAfterParagraphUpdate()
        {
            if (CanDeleteAllText == true && MKRichTextBox.Selection.Text != "")
            {
                MKRichTextBox.Document.Blocks.Clear();
                InitiateFirstParagraph();
                CanDeleteAllText = false;
                return false;
            }

            CanDeleteAllText = false;

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);

            if ((LastPickedParagraph.Tag.Equals(TextType.Header1) || LastPickedParagraph.Tag.Equals(TextType.Header2) || LastPickedParagraph.Tag.Equals(TextType.Header3)) && textRange.IsEmpty)
            {
                ResetTextStyle();
                return true;
            }
            else
            {
                await LastPickedParagraphAfterParagraphUpdate();
                return false;
            }
            
        }

        private async Task LastPickedParagraphAfterParagraphUpdate()
        {
            await Task.Delay(50);

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            Debug.WriteLine($"Current Paragraph: {textRange.Text}");
            if (textRange.Text.Equals(""))
            {
                IsListItem = false;

                LastPickedParagraph.Inlines.Clear();
                LastPickedParagraph.FontSize = NormalFontSize;
                LastPickedParagraph.Tag = TextType.Normal;
            }
            else if (textRange.Text.StartsWith("•"))
            {
                IsListItem = true;
                CheckParagraphStatus();
            }
        }

        private void ResetTextStyle()
        {
            LastPickedParagraph.FontSize = NormalFontSize;
            LastPickedParagraph.Margin = DefaultMargin;
            LastPickedParagraph.Tag = TextType.Normal;

            IsNextToAMarkdown = false;
        }

        private void DeleteAllText()
        {
            MKRichTextBox.SelectAll();
            CanDeleteAllText = true;
            Debug.WriteLine("Delete All Text");
        }
        #endregion

        private void UpdateVisuals(ScrollViewer scrollViewer)
        {
            if(scrollViewer != null)
            {
                double VerticalOffset = scrollViewer.VerticalOffset;
                double ScreenViewportHeight = scrollViewer.ViewportHeight;
                double ScrollHeight = scrollViewer.ScrollableHeight;

                double Result = VerticalOffset + ScreenViewportHeight;
                if (VerticalOffset > 30.0)
                {
                    FirstStartAnimPlayed = true;
                }
                else
                {
                    FirstStartAnimPlayed = false;
                }

                //Debug.WriteLine($"VerticalOffset = {VerticalOffset} --- ScreenViewportHeight = {ScreenViewportHeight} --- ScrollableHeight = {ScrollHeight}");
            }
        }

        private void InitiateGrid(Grid grid)
        {
            if(grid != null)
            {
                GridTarget = grid;
            }
        }

        private void UpdatedScrollBarShape(Rectangle rectangle)
        {
            IsInsideScrollBarHitCollision = !IsInsideScrollBarHitCollision;
            Debug.WriteLine("Hit Collision Changed.");
        }

        private void UpdatedScrollBarData(ScrollBar scrollBar)
        {
            IsInsideScrollBar = !IsInsideScrollBar;
            Debug.WriteLine("Scroll Bar State Changed.");

            if (IsInsideScrollBar == false)
                Debug.WriteLine("Leaved Scroll Bar");
        }

        private void GetParentGridRef(Grid grid)
        {
            if (grid != null)
            {
                ParentGridTarget = grid;
            }
        }

        private void GetNoteButtonRef(Button Btn)
        {
            if(Btn != null)
            {
                NoteButton = Btn;
                Debug.WriteLine($"Note Button Ref: {NoteButton.Name}");
            }
        }
        private void NoteFunctionality(MouseButtonEventArgs e)
        {
            if(e != null && ParentGridTarget != null)
            {
                InitialMousePosition = e.GetPosition(MKCurrentUserControl);
                IsMouseLeftButtonPressed = true;
                ParentGridTarget.CaptureMouse();
            }
        }

        private void RecordNoteButtonMouseMovement(MouseEventArgs e)
        {
            if (IsMouseLeftButtonPressed == true && e != null)
            {
                CurrentMousePosition = e.GetPosition(MKCurrentUserControl);
                if(CurrentMousePosition != InitialMousePosition && (CurrentMousePosition.Y - InitialMousePosition.Y) > 30)
                {
                    CreateNewNote(CurrentMousePosition);
                }
                Debug.WriteLine($"Current Mouse Position: X = {e.GetPosition(MKCurrentUserControl).X}, Y = {e.GetPosition(MKCurrentUserControl).Y}");
            }
        }

        private void CreateNewNote(Point Position)
        {
            if(CanCreateANewNote == true)
            {
                TempNoteUserControl = new NoteUserControl(MKCurrentUserControl);
                TempNoteUserControl.RemoveCurrentNote += RemoveNote;
                CanCreateANewNote = false;

                ParentGridTarget.Children.Add(TempNoteUserControl);

                Grid.SetRow(TempNoteUserControl, 1);
                Panel.SetZIndex(TempNoteUserControl, 10);

                Debug.WriteLine("Create A New Note...");
            }

            Position.X -= TempNoteUserControl.ActualWidth / 2;
            Position.Y -= TempNoteUserControl.ActualHeight / 2;
            TempNoteUserControl.Margin = new Thickness(Position.X, Position.Y, 0, 0);

            OnUpdateCoordinates(this, Position);
        }

        private void RemoveNote(object? sender, EventArgs e)
        {
            ParentGridTarget.Children.Remove((UIElement)sender!);
        }

        private void StopRecordingNoteButtonMouseMovement(MouseButtonEventArgs e)
        {
            if (e != null && ParentGridTarget != null)
            {
                IsMouseLeftButtonPressed = false;
                CanCreateANewNote = true;

                ParentGridTarget.ReleaseMouseCapture();
            }
        }

        private async Task SaveContent()
        {
            /*
             * Steps:
             *  - Write content [x]
             *  - Update file size [x]
             *  
             * Side note:
             *  - When renaming the file don't forget to write it in the file. (Do we really need file's name in the file?)
             */

            FileSystem file = _workspaceServices.UserDataServices.CurrentSelectedFile;
            FileManager fileManager = new FileManager();
            BlockCollection Blocks = MKRichTextBox.Document.Blocks;

            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(file.FileDirectory, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
                {
                    fileManager.UpdateFileData(xmlWriter, file, Blocks);
                }

                FileStructure fileStructure = new FileStructure();
                fileStructure.UpdateFileSize(file);
                _workspaceServices.UserDataServices.SaveUserDataAsync();
            }));

            Debug.WriteLine("Saved!");
        }

        private async Task LoadContent()
        {
            file = _workspaceServices.UserDataServices.CurrentSelectedFile;
            FileManager fileManager = new FileManager();

            await Application.Current.Dispatcher.InvokeAsync((Action)(delegate
            {
                bool AddedData = fileManager.LoadFileDataContent(file, MKRichTextBox);
                if (AddedData == false)
                    InitiateFirstParagraph();
                FirstLaunch = false;

                file.CurrentRichTextBox = MKRichTextBox;
                file.InitialRichTextBox = new RichTextBox();
                _ = CopyRTBData(MKRichTextBox, file.InitialRichTextBox);
            }));
        }

        public void ChangeSaveStatus()
        {
            if (file != null)
                file.OpenSaveMessage = true;
        }

        private async Task CopyRTBData(RichTextBox richTextBox, RichTextBox targetRichTextBox)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                string DocumentData = string.Empty;
                using (var memoryStream = new MemoryStream())
                {
                    textRange.Save(memoryStream, DataFormats.Xaml);
                    DocumentData = Encoding.UTF8.GetString(memoryStream.ToArray());
                }

                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(DocumentData)))
                {
                    TextRange targetTextRange = new TextRange(targetRichTextBox.Document.ContentStart, targetRichTextBox.Document.ContentEnd);
                    targetTextRange.Load(memoryStream, DataFormats.Xaml);
                }
            });
        }
    }
}
