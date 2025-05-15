using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using UDSH.MVVM;
using UDSH.Services;
using UDSH.View;
using UDSH.Model;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace UDSH.ViewModel
{
    public enum ScriptElement
    {
        [Description("Scene Heading")]
        SceneHeading,
        Action,
        Character,
        Dialogue
    }

    public enum UIContainer
    {
        ConnectedMKBButton
    }
    public class MKCUserControlViewModel : ViewModelBase
    {
        private readonly IWorkspaceServices _workspaceServices;
        private readonly string _workspaceServicesID;

        private Point InitialMousePosition;
        private Point CurrentMousePosition;

        private bool IsNextToAMarkdown;
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

        private bool IsShortcutSource { get; set; } = false;

        private RichTextBox mKRichTextBox;
        public RichTextBox MKRichTextBox
        {
            get { return mKRichTextBox; }
            set { mKRichTextBox = value; OnPropertyChanged(); }
        }

        private Paragraph LastPickedParagraph;
        private int LastPickedParagraphListIndex;

        private Thickness SceneHeadingMargins;
        private Thickness ActionMargins;
        private Thickness CharacterMargins;
        private Thickness DialogueMargins;

        private Thickness DefaultMargin;

        private Grid GridTarget;
        private Grid ParentGridTarget;
        private Button NoteButton;

        public MKUserControl MKCurrentUserControl;
        private NoteUserControl TempNoteUserControl;

        private bool FirstLaunch;
        private FileSystem file;

        private ObservableCollection<ScriptElement> scriptElements;
        public ObservableCollection<ScriptElement> ScriptElements
        {
            get => scriptElements;
            set { scriptElements = value; OnPropertyChanged(); }
        }

        private ScriptElement selectedScriptElement;
        public ScriptElement SelectedScriptElement
        {
            get => selectedScriptElement;
            set { selectedScriptElement = value; OnPropertyChanged(); }
        }

        private Project CurrentProject { get; set; }
        private FileSystem AssociatedFile { get; set; }

        private ObservableCollection<FileSystem> mKBFiles = new ObservableCollection<FileSystem>();
        public ObservableCollection<FileSystem> MKBFiles
        {
            get => mKBFiles;
            set { mKBFiles = value; OnPropertyChanged(); }
        }

        private FileSystem selectedMKBFile;
        public FileSystem SelectedMKBFile
        {
            get => selectedMKBFile;
            set { selectedMKBFile = value; OnPropertyChanged(); }
        }

        private Canvas PaperCanvas { get; set; }
        private CharacterLinkButton CurrentCharacterLinkButton { get; set; }
        private MKBSelectionBox CurrentMKBSelectionBox { get; set; }
        private Paragraph CurrentDialogueParagraph { get; set; }

        private bool ToggleBlockDeletion;

        public RelayCommand<RichTextBox> RichTextLoaded => new RelayCommand<RichTextBox>(OnRichTextBoxLoaded);

        public RelayCommand<Button> BoldText => new RelayCommand<Button>(execute => ChangeBoldText());
        public RelayCommand<Button> ItalicText => new RelayCommand<Button>(execute => ChangeItalicText());
        public RelayCommand<Button> StrikethroughText => new RelayCommand<Button>(execute => ChangeStrikethroughText());
        public RelayCommand<Button> UnderlineText => new RelayCommand<Button>(execute => ChangeUnderlineText());

        public RelayCommand<KeyEventArgs> PressedEnter => new RelayCommand<KeyEventArgs>(execute => CreateANewParagraph(), canExecute => CanCreateANewParagraph());
        public RelayCommand<KeyEventArgs> DeleteAll => new RelayCommand<KeyEventArgs>(execute => DeleteAllText());

        public RelayCommand<Paragraph> CaptureParagraph => new RelayCommand<Paragraph>(GetParagraph);
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
        public RelayCommand<object> DeleteButton => new RelayCommand<object>(execute => DeleteFile());

        public RelayCommand<RichTextBox> ParagraphTextChanged => new RelayCommand<RichTextBox>(execute => CheckTextStatus());
        public RelayCommand<RichTextBox> ActiveElementSelectionChanged => new RelayCommand<RichTextBox>(execute => UpdateLastPickedParagraphActiveElement(), canExecute => !IsShortcutSource);
        public RelayCommand<object> RichLMBUp => new RelayCommand<object>(execute => HandleLeftMouseButtonUp());
        public RelayCommand<object> UserControlLoaded => new RelayCommand<object>(execute => UpdateDetailsOnUserControlLoaded());
        public RelayCommand<Canvas> CanvasLoaded => new RelayCommand<Canvas>(LoadPaperCanvas);

        public MKCUserControlViewModel(IWorkspaceServices workspaceServices)
        {
            _workspaceServices = workspaceServices;

            _workspaceServicesID = Guid.NewGuid().ToString();
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);

            IsNextToAMarkdown = false;
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
            ToggleBlockDeletion = false;

            SceneHeadingMargins = new Thickness(0, 30, 0, 0);
            ActionMargins = new Thickness(0, 12, 0, 0);
            CharacterMargins = new Thickness(211, 18, 0, 0);
            DialogueMargins = new Thickness(96, 6, 0, 0);

            ScriptElements = new ObservableCollection<ScriptElement>(Enum.GetValues(typeof(ScriptElement)).Cast<ScriptElement>());
        }

        #region Rich Text Box Initiation And Starting The First Paragraph
        private void OnRichTextBoxLoaded(RichTextBox richTextBox)
        {
            if (richTextBox != null)
            {
                MKRichTextBox = richTextBox;

                // Remove this after testing!!!!
                InitiateFirstParagraph();

                /*if (_workspaceServices.UserDataServices.CurrentSelectedFile != null && FirstLaunch == true)
                    LoadContent();*/
            }
        }

        private void InitiateFirstParagraph()
        {
            Paragraph FirstParagraph = new Paragraph();
            FirstParagraph.Margin = new Thickness(0);
            FirstParagraph.Tag = ScriptElement.SceneHeading;
            LastPickedParagraph = FirstParagraph;
            MKRichTextBox.Document.Blocks.Add(LastPickedParagraph);
            MKRichTextBox.CaretPosition = LastPickedParagraph.ContentStart;
            MKRichTextBox.Focus();

            SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
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
            ScriptElement LastScriptElement = (ScriptElement)LastPickedParagraph.Tag;

            while (CurrentBlock != null)
            {
                if (CurrentBlock == LastPickedParagraph)
                {
                    PerserveTextAndStyle(paragraph, LastScriptElement);

                    Blocks.InsertAfter(CurrentBlock, paragraph);
                    MKRichTextBox.CaretPosition = paragraph.ContentStart;

                    LastPickedParagraph.Focusable = true;
                    paragraph.Focusable = false;
                    LastPickedParagraph = paragraph;
                    LastPickedParagraph.Tag = UpdateScriptElement(LastScriptElement, LastPickedParagraph);

                    if (IsNextToAMarkdown == true)
                    {
                        IsNextToAMarkdown = false;
                    }

                    return;
                }

                CurrentBlock = CurrentBlock.NextBlock;
            }
        }

        private void PerserveTextAndStyle(Paragraph PreserveParagraph, ScriptElement scriptElement)
        {
            if (scriptElement == ScriptElement.Character)
                return;

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

                textRange.Text = string.Empty;
            }
        }

        private ScriptElement UpdateScriptElement(ScriptElement scriptElement, Paragraph ParagraphTarget)
        {
            switch (scriptElement)
            {
                case ScriptElement.SceneHeading:
                    SelectedScriptElement = ScriptElement.Action;
                    ParagraphTarget.Margin = ActionMargins;
                    return SelectedScriptElement;

                case ScriptElement.Action:
                    SelectedScriptElement = ScriptElement.Action;
                    ParagraphTarget.Margin = ActionMargins;
                    return SelectedScriptElement;

                case ScriptElement.Character:
                    SelectedScriptElement = ScriptElement.Dialogue;
                    ParagraphTarget.Margin = DialogueMargins;
                    return SelectedScriptElement;

                case ScriptElement.Dialogue:
                    SelectedScriptElement = ScriptElement.Action;
                    ParagraphTarget.Margin = ActionMargins;
                    return SelectedScriptElement;

                default:
                    SelectedScriptElement = ScriptElement.Action;
                    ParagraphTarget.Margin = ActionMargins;
                    return SelectedScriptElement;
            }
        }

        private bool CanCreateANewParagraph()
        {
            if (MKRichTextBox.Selection.Text == "")
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
                    SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    Para2.Focusable = false;

                    if (Para1 != Para2)
                        Para1.Focusable = true;
                }

                /*if (SelectedScriptElement == ScriptElement.Character)
                    GetCurrentCharacterLinkButton();
                else
                    CurrentCharacterLinkButton = null;*/
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
                SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                string ParaText = textRange.Text;

                UpdateToggleBlockDeletionState();

                /*if (SelectedScriptElement == ScriptElement.Character)
                    GetCurrentCharacterLinkButton();
                else
                    CurrentCharacterLinkButton = null;*/
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
            SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            string ParaText = textRange.Text;

            UpdateCharacterCaretPosition();
            UpdateToggleBlockDeletionState();
            UpdateAllPaperCanvasButtonsLocation();

            /*if (SelectedScriptElement == ScriptElement.Character)
                GetCurrentCharacterLinkButton();
            else
                CurrentCharacterLinkButton = null;*/

            Debug.WriteLine($"Current Paragraph: {ParaText}");
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
                    SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    LastPickedParagraph.Focusable = false;

                    TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                    Debug.WriteLine($"MK Paragraph: {textRange.Text}");

                    UpdateCharacterCaretPosition();
                }
            }
        }
        #endregion

        #region Markdown Functionalities
        public bool SpaceMarkdown()
        {
            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);

            if (textRange.Text.Equals("#S", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine($"Play #S Markdown...");
                textRange.Text = "";
                IsShortcutSource = true;
                SelectedScriptElement = ScriptElement.SceneHeading;
                UpdateLastPickedParagraphActiveElement();
                IsShortcutSource = false;

                return true;
            }
            else if (textRange.Text.Equals("#A", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine($"Play #A Markdown...");
                textRange.Text = "";
                IsShortcutSource = true;
                SelectedScriptElement = ScriptElement.Action;
                UpdateLastPickedParagraphActiveElement();
                IsShortcutSource = false;

                return true;
            }
            else if (textRange.Text.Equals("#C", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine($"Play #C Markdown...");
                textRange.Text = "";
                IsShortcutSource = true;
                SelectedScriptElement = ScriptElement.Character;
                UpdateLastPickedParagraphActiveElement();
                IsShortcutSource = false;

                return true;
            }
            else if (textRange.Text.Equals("#D", StringComparison.OrdinalIgnoreCase))
            {
                Debug.WriteLine($"Play #D Markdown...");
                textRange.Text = "";
                IsShortcutSource = true;
                SelectedScriptElement = ScriptElement.Dialogue;
                UpdateLastPickedParagraphActiveElement();
                IsShortcutSource = false;

                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Deletion Logic
        public async Task<bool> SetLastPickedParagraphAfterParagraphUpdate()
        {
            if (CanDeleteAllText == true && MKRichTextBox.Selection.Text != "")
            {
                /*MKRichTextBox.Document.Blocks.Clear();
                InitiateFirstParagraph();*/
                Paragraph paragraph = new Paragraph();
                paragraph = LastPickedParagraph.PreviousBlock as Paragraph;

                if (paragraph == null)
                    return false;

                if (SelectedScriptElement == ScriptElement.Character)
                    DeleteButtonLink();

                MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                LastPickedParagraph = paragraph;
                SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                if (SelectedScriptElement == ScriptElement.Character)
                {
                    Inline CharacterNameInline = LastPickedParagraph.Inlines.ElementAt(LastPickedParagraph.Inlines.Count - 2);
                    MKRichTextBox.CaretPosition = CharacterNameInline.ContentEnd;
                }
                else
                {
                    MKRichTextBox.CaretPosition = LastPickedParagraph.ContentEnd;
                }

                CanDeleteAllText = false;
                return true;
            }

            CanDeleteAllText = false;

            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);

            TextPointer CaretOffset = MKRichTextBox.CaretPosition.GetInsertionPosition(LogicalDirection.Forward);
            TextPointer LastPickedParagraphStartOffset = LastPickedParagraph.ContentStart.GetInsertionPosition(LogicalDirection.Forward);

            if (!string.IsNullOrWhiteSpace(textRange.Text) && CaretOffset.CompareTo(LastPickedParagraphStartOffset) == 0)
            {
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
            SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            Debug.WriteLine($"Current Paragraph: {textRange.Text}");

            if (SelectedScriptElement == ScriptElement.Character && string.IsNullOrWhiteSpace(textRange.Text) && MKRichTextBox.Document.Blocks.Count == 1)
            {
                if (ToggleBlockDeletion == true)
                {
                    MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                    LastPickedParagraph = new Paragraph();
                    LastPickedParagraph.Tag = ScriptElement.SceneHeading;
                    SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                    MKRichTextBox.Document.Blocks.Add(LastPickedParagraph);
                    MKRichTextBox.CaretPosition = LastPickedParagraph.ContentStart;
                }

                ToggleBlockDeletion = true;
            }
            else if (string.IsNullOrWhiteSpace(textRange.Text) && MKRichTextBox.Document.Blocks.Count > 1)
            {
                if (ToggleBlockDeletion == true)
                {
                    if (SelectedScriptElement == ScriptElement.Character)
                        DeleteButtonLink();

                    Block PreviousBlock = LastPickedParagraph.PreviousBlock;
                    if (PreviousBlock != null)
                    {
                        MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                        LastPickedParagraph = PreviousBlock as Paragraph;
                        SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                        UpdateCharacterCaretPosition();
                    }
                }

                ToggleBlockDeletion = true;
            }
            else
            {
                ToggleBlockDeletion = false;
            }    
        }

        private void DeleteAllText()
        {
            MKRichTextBox.Selection.Select(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            CanDeleteAllText = true;
            Debug.WriteLine("Delete All Text");
        }

        private void DeleteButtonLink()
        {
            GetCurrentCharacterLinkButton();

            if (CurrentCharacterLinkButton == null)
                return;

            PaperCanvas.Children.Remove(CurrentCharacterLinkButton);

            CurrentCharacterLinkButton.Click -= CharacterLinkButton_Click;
            CurrentCharacterLinkButton.MouseEnter -= CharacterLinkButton_MouseEnter;

            CurrentCharacterLinkButton = null;
        }
        #endregion

        #region Scroll Logic
        private void UpdateVisuals(ScrollViewer scrollViewer)
        {
            if (scrollViewer != null)
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

                UpdateAllPaperCanvasButtonsLocation();
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
        #endregion

        #region Note Logic [Need A Re-work]
        private void GetNoteButtonRef(Button Btn)
        {
            if (Btn != null)
            {
                NoteButton = Btn;
                Debug.WriteLine($"Note Button Ref: {NoteButton.Name}");
            }
        }
        private void NoteFunctionality(MouseButtonEventArgs e)
        {
            if (e != null && ParentGridTarget != null)
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
                if (CurrentMousePosition != InitialMousePosition && (CurrentMousePosition.Y - InitialMousePosition.Y) > 30)
                {
                    CreateNewNote(CurrentMousePosition);
                }
                Debug.WriteLine($"Current Mouse Position: X = {e.GetPosition(MKCurrentUserControl).X}, Y = {e.GetPosition(MKCurrentUserControl).Y}");
            }
        }

        private void CreateNewNote(Point Position)
        {
            if (CanCreateANewNote == true)
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
        #endregion

        #region File Modifications [IT USES MKM LOGIC. UPDATE IT]
        private async Task SaveContent()
        {
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
                file.OpenSaveMessage = false;
            }));

            Debug.WriteLine("Saved!");
        }

        private void DeleteFile()
        {
            FileSystem file = _workspaceServices.UserDataServices.CurrentSelectedFile;
            PopupWindow popupWindow = new PopupWindow(ProcessType.Delete, file.FileName + "." + file.FileType);
            bool? CanDelete = popupWindow.ShowDialog();

            if (CanDelete == true)
            {
                _workspaceServices.UserDataServices.FileQuickDeleteAction(file);
            }
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
        #endregion

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

        private void CreateTexture()
        {
            TextureCreationWindow textureCreationWindow = new TextureCreationWindow(MKRichTextBox, file);
            textureCreationWindow.ShowDialog();
        }

        private void InitiateGrid(Grid grid)
        {
            if (grid != null)
            {
                GridTarget = grid;
            }
        }

        private void GetParentGridRef(Grid grid)
        {
            if (grid != null)
            {
                ParentGridTarget = grid;
            }
        }

        private void CheckTextStatus()
        {
            if (SelectedScriptElement == ScriptElement.SceneHeading || SelectedScriptElement == ScriptElement.Character)
            {
                int Offset = MKRichTextBox.Document.ContentStart.GetOffsetToPosition(MKRichTextBox.CaretPosition);

                foreach (var inline in LastPickedParagraph.Inlines.ToList())
                {
                    if (inline is Run run)
                    {
                        run.Text = run.Text.ToUpper();
                    }
                }

                TextPointer CurrentCaretPosition = MKRichTextBox.Document.ContentStart.GetPositionAtOffset(Offset);
                MKRichTextBox.CaretPosition = CurrentCaretPosition;
            }

            UpdateAllPaperCanvasButtonsLocation();
        }

        private void UpdateAllPaperCanvasButtonsLocation()
        {
            foreach (var button in PaperCanvas.Children)
            {
                if (button is CharacterLinkButton characterLinkButton)
                    UpdateButtonLinkLocation(characterLinkButton);
            }
        }

        public async Task LRCharacterNavigation()
        {
            await Task.Delay(50);
            UpdateCharacterCaretPosition();
        }

        public void UpdateCharacterCaretPosition()
        {
            if (SelectedScriptElement != ScriptElement.Character)
                return;

            int CaretOffset = MKRichTextBox.Document.ContentStart.GetOffsetToPosition(MKRichTextBox.CaretPosition);

            Inline CharacterNameInline = LastPickedParagraph.Inlines.ElementAt(LastPickedParagraph.Inlines.Count - 2);
            int CharacterNameLastCharOffset = MKRichTextBox.Document.ContentStart.GetOffsetToPosition(CharacterNameInline.ContentEnd);

            if (CaretOffset > CharacterNameLastCharOffset)
                MKRichTextBox.CaretPosition = MKRichTextBox.Document.ContentStart.GetPositionAtOffset(CharacterNameLastCharOffset);
        }

        private void UpdateLastPickedParagraphActiveElement()
        {
            LastPickedParagraph.Tag = SelectedScriptElement;

            switch (SelectedScriptElement)
            {
                case ScriptElement.SceneHeading:
                    LastPickedParagraph.Margin = SceneHeadingMargins;
                    break;

                case ScriptElement.Action:
                    LastPickedParagraph.Margin = ActionMargins;
                    break;

                case ScriptElement.Character:
                    LastPickedParagraph.Margin = CharacterMargins;
                    CreateCharacterLinkButton();
                    break;

                case ScriptElement.Dialogue:
                    LastPickedParagraph.Margin = DialogueMargins;
                    break;
            }

            CheckTextStatus();
        }

        private void UpdateToggleBlockDeletionState()
        {
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            if (textRange.Text.Equals(""))
                ToggleBlockDeletion = true;
            else
                ToggleBlockDeletion = false;
        }

        private void CreateCharacterLinkButton()
        {
            if (LastPickedParagraph.Inlines.LastInline is InlineUIContainer)
                return;

            /*CharacterLinkButton characterLinkButton = new CharacterLinkButton
            {
                Style = (Style)Application.Current.FindResource("BNCharacterConnectionButton"),
                Margin = new Thickness(10,0,0,0),
                CharacterParagraph = LastPickedParagraph
            };

            characterLinkButton.Click += CharacterLinkButton_Click;
            characterLinkButton.MouseEnter += CharacterLinkButton_MouseEnter;*/

            Border border = new Border
            {
                Width = 1,
                Height = 1,
                IsHitTestVisible = false,
                Background = new SolidColorBrush(Colors.Transparent)
            };

            InlineUIContainer inlineUIContainer = new InlineUIContainer(border);
            inlineUIContainer.BaselineAlignment = BaselineAlignment.Center;

            LastPickedParagraph.Inlines.Add(inlineUIContainer);

            CharacterLinkButton characterLinkButton = new CharacterLinkButton
            {
                Margin = new Thickness(10, 0, 0, 0),
                CharacterParagraph = LastPickedParagraph,
                Placeholder = inlineUIContainer
            };

            characterLinkButton.Click += CharacterLinkButton_Click;
            characterLinkButton.MouseEnter += CharacterLinkButton_MouseEnter;

            PaperCanvas.Children.Add(characterLinkButton);
            UpdateButtonLinkLocation(characterLinkButton);

            CurrentCharacterLinkButton = characterLinkButton;
        }

        private void CharacterLinkButton_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void CharacterLinkButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterLinkButton characterLinkButton = (CharacterLinkButton)sender;
            Block block = characterLinkButton.CharacterParagraph.NextBlock;
            if (block != null && (ScriptElement)block.Tag == ScriptElement.Dialogue)
            {
                CurrentDialogueParagraph = block as Paragraph;

                Paragraph paragraph = new Paragraph();
                paragraph.TextAlignment = TextAlignment.Center;
                paragraph.Tag = UIContainer.ConnectedMKBButton;

                //// IMPORTANT: ANIMATION IS FROZEN. WE CAN'T USE INLINE UI CONTAINERS. WHHHHYYYYY!!!!
                //MKBSelectionBox mKBSelectionBox = new MKBSelectionBox(this);

                Border border = new Border
                {
                    Width = 1,
                    Height = 50,
                    IsHitTestVisible = false,
                    Background = new SolidColorBrush(Colors.Transparent)
                };

                InlineUIContainer inlineUIContainer = new InlineUIContainer(border);
                inlineUIContainer.BaselineAlignment = BaselineAlignment.Center;

                paragraph.Inlines.Add(inlineUIContainer);
                MKRichTextBox.Document.Blocks.InsertAfter(block, paragraph);

                MKBSelectionBox mKBSelectionBox = new MKBSelectionBox(this, inlineUIContainer, paragraph);
                mKBSelectionBox.MKBSelectionBoxRequestedRemoval += MKBSelectionBox_MKBSelectionBoxRequestedRemoval;
                PaperCanvas.Children.Add(mKBSelectionBox);
                CurrentMKBSelectionBox = mKBSelectionBox;
                UpdateButtonLinkLocation(mKBSelectionBox);
            }
            else
            {
                Paragraph paragraph = new Paragraph();
                paragraph.Tag = ScriptElement.Dialogue;
                paragraph.Margin = DialogueMargins;

                Run run = new Run("Dialogue");
                paragraph.Inlines.Add(run);

                MKRichTextBox.Document.Blocks.InsertAfter(characterLinkButton.CharacterParagraph, paragraph);
                MKRichTextBox.CaretPosition = paragraph.ContentEnd;

                CurrentDialogueParagraph = paragraph;

                Paragraph MKBParagraph = new Paragraph();
                MKBParagraph.TextAlignment = TextAlignment.Center;
                MKBParagraph.Tag = UIContainer.ConnectedMKBButton;

                Border border = new Border
                {
                    Width = 1,
                    Height = 50,
                    IsHitTestVisible = false,
                    Background = new SolidColorBrush(Colors.Transparent)
                };

                InlineUIContainer inlineUIContainer = new InlineUIContainer(border);
                inlineUIContainer.BaselineAlignment = BaselineAlignment.Center;

                MKBParagraph.Inlines.Add(inlineUIContainer);
                MKRichTextBox.Document.Blocks.InsertAfter(paragraph, MKBParagraph);

                MKBSelectionBox mKBSelectionBox = new MKBSelectionBox(this, inlineUIContainer, MKBParagraph);
                mKBSelectionBox.MKBSelectionBoxRequestedRemoval += MKBSelectionBox_MKBSelectionBoxRequestedRemoval;
                PaperCanvas.Children.Add(mKBSelectionBox);
                CurrentMKBSelectionBox = mKBSelectionBox;
                UpdateButtonLinkLocation(mKBSelectionBox);
            }
        }

        private void MKBSelectionBox_MKBSelectionBoxRequestedRemoval(object? sender, bool e)
        {
            if (e == true)
            {
                if(AssociatedFile.ConnectedMKMFiles == null)
                    AssociatedFile.ConnectedMKMFiles = new List<FileSystem>();

                AssociatedFile.ConnectedMKMFiles.Add(SelectedMKBFile);
                DialogueLinkButton dialogueLinkButton = CreateDialogueLinkButton();
                CurrentCharacterLinkButton.AssociatedDialogueButton = dialogueLinkButton;
                CurrentCharacterLinkButton.UpdateButtonVisuals(e);
            }
            else
            {
                MKRichTextBox.Document.Blocks.Remove(CurrentMKBSelectionBox.ParagraphHolder);
            }

            PaperCanvas.Children.Remove(CurrentMKBSelectionBox);
            CurrentMKBSelectionBox.MKBSelectionBoxRequestedRemoval -= MKBSelectionBox_MKBSelectionBoxRequestedRemoval;
            CurrentMKBSelectionBox = null;
        }

        private DialogueLinkButton CreateDialogueLinkButton()
        {
            DialogueLinkButton dialogueLinkButton = new DialogueLinkButton
            {
                CharacterParagraph = CurrentCharacterLinkButton.CharacterParagraph,
                DialogueParagraph = CurrentDialogueParagraph,
                ParagraphHolder = CurrentMKBSelectionBox.ParagraphHolder,
                Placeholder = CurrentMKBSelectionBox.Placeholder,
                AssociatedMKBFile = SelectedMKBFile
            };

            PaperCanvas.Children.Add(dialogueLinkButton);
            UpdateButtonLinkLocation(dialogueLinkButton);

            return dialogueLinkButton;
        }

        private void HandleLeftMouseButtonUp()
        {
            if (MKRichTextBox.Selection.IsEmpty == true)
                return;

            TextPointer Start = MKRichTextBox.Selection.Start;
            TextPointer End = MKRichTextBox.Selection.End;

            Paragraph ParagraphStart = Start.Paragraph;
            Paragraph ParagraphEnd = End.Paragraph;
            ScriptElement ParagraphEndScriptElement = (ScriptElement)ParagraphEnd.Tag;

            bool StartRecording = false;
            bool ChangedStart = false;

            foreach (var CurrentBlock in MKRichTextBox.Document.Blocks)
            {
                if (CurrentBlock == ParagraphEnd)
                    break;

                if (CurrentBlock == ParagraphStart)
                {
                    StartRecording = true;
                }

                if (StartRecording == true)
                {
                    if ((ScriptElement)CurrentBlock.Tag != ParagraphEndScriptElement)
                    {
                        ParagraphStart = CurrentBlock.NextBlock as Paragraph;
                        ChangedStart = true;
                    }
                }
            }

            MKRichTextBox.Selection.Select((ChangedStart == true) ? ParagraphStart.ContentStart : Start, End);
        }

        private void UpdateButtonLinkLocation(CharacterLinkButton characterLinkButton)
        {
            TextPointer PlaceholderPosition = characterLinkButton.Placeholder.ContentStart;
            Rect CharacterRect = PlaceholderPosition.GetCharacterRect(LogicalDirection.Forward);

            Point relativeToRichTextBox = MKRichTextBox.TransformToAncestor(GridTarget).Transform(new Point(CharacterRect.X, CharacterRect.Y));

            if (Double.IsInfinity(relativeToRichTextBox.X))
                return;

            Canvas.SetLeft(characterLinkButton, relativeToRichTextBox.X);
            Canvas.SetTop(characterLinkButton, relativeToRichTextBox.Y - 2);
        }

        private void UpdateButtonLinkLocation(DialogueLinkButton dialogueLinkButton)
        {
            TextPointer PlaceholderPosition = dialogueLinkButton.Placeholder.ContentStart;
            Rect CharacterRect = PlaceholderPosition.GetCharacterRect(LogicalDirection.Forward);

            Point relativeToRichTextBox = MKRichTextBox.TransformToAncestor(GridTarget).Transform(new Point(CharacterRect.X, CharacterRect.Y));

            if (Double.IsInfinity(relativeToRichTextBox.X))
                return;

            double Base = 180.0 / 290.61333333333334;
            double OffsetX = dialogueLinkButton.TotalWidth * Base;

            /*
             * TODO:
             * - Have static width instead of a dynamic one.
             * - Finish Dialogue Link Button design.
             * - Modify animations.
             * - Update Dialogue Link Buttons when:
             *      * Scrolling.
             *      * Text input.
             * - Change Caret if entered the Dialogue Link Button's Paragraph.
             * - Add transition to the associated mkb file.
             * - Add order index to know which paragraph came first.
             * - Add a list for BN to add the nodes.
             * - the Delete key removes the placeholders. override the button.
             */

            Canvas.SetLeft(dialogueLinkButton, relativeToRichTextBox.X - OffsetX - 160);
            Canvas.SetTop(dialogueLinkButton, relativeToRichTextBox.Y - 2);
        }

        private void UpdateButtonLinkLocation(MKBSelectionBox mKBSelectionBox)
        {
            TextPointer PlaceholderPosition = mKBSelectionBox.Placeholder.ContentStart;
            Rect CharacterRect = PlaceholderPosition.GetCharacterRect(LogicalDirection.Forward);

            Point relativeToRichTextBox = MKRichTextBox.TransformToAncestor(GridTarget).Transform(new Point(CharacterRect.X, CharacterRect.Y));

            if (Double.IsInfinity(relativeToRichTextBox.X))
                return;

            Canvas.SetLeft(mKBSelectionBox, relativeToRichTextBox.X - 330);
            Canvas.SetTop(mKBSelectionBox, relativeToRichTextBox.Y - 2);
        }

        private void GetCurrentCharacterLinkButton()
        {
            foreach(var button in PaperCanvas.Children)
            {
                if (button is CharacterLinkButton characterLinkButton)
                {
                    if (characterLinkButton.CharacterParagraph == LastPickedParagraph)
                        CurrentCharacterLinkButton = characterLinkButton;
                }
            }
        }

        private void UpdateDetailsOnUserControlLoaded()
        {
            _workspaceServices.SetCurrentActiveWorkspaceID(_workspaceServicesID);
            LoadMKBFiles();
        }

        private void LoadMKBFiles()
        {
            // this approach isn't bad, but if we want to increase performance,
            // we can add an event so when a new file gets created, it gets added if it is of a type mkb.
            MKBFiles.Clear();

            CurrentProject = _workspaceServices.UserDataServices.ActiveProject;
            foreach (var file in CurrentProject.Files)
            {
                //if (file.FileType.Equals("mkb"))
                    MKBFiles.Add(file);
            }

            AssociatedFile = _workspaceServices.UserDataServices.CurrentSelectedFile;
        }

        private void LoadPaperCanvas(Canvas canvas)
        {
            PaperCanvas = canvas;
        }
    }
}
