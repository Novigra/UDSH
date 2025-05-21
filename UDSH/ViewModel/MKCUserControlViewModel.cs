// Copyright (C) 2025 Mohammed Kenawy
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
using System.Windows.Media.Animation;
using System.Globalization;
using static System.Reflection.Metadata.BlobBuilder;

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
        ConnectedMKBButton,
        ParagraphCoverBorder
    }

    public class MKCUserControlViewModel : ViewModelBase
    {
        public event EventHandler ResetCharacterLinkButtonStatus;
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
        private ParagraphCoverBorder CurrentParagraphCoverBorder { get; set; }
        private bool SelectParagraphSource { get; set; } = false;
        private TextPointer LastCaretPosition { get; set; }
        public bool CanUnlinkMKBButton { get; set; } = false;

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

            CurrentProject = _workspaceServices.UserDataServices.ActiveProject;
            AssociatedFile = _workspaceServices.UserDataServices.CurrentSelectedFile;

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

            SceneHeadingMargins = new Thickness(0, 30, 0, 0);
            ActionMargins = new Thickness(0, 12, 0, 0);
            CharacterMargins = new Thickness(211, 18, 0, 0);
            DialogueMargins = new Thickness(96, 6, 0, 0);

            ScriptElements = new ObservableCollection<ScriptElement>(Enum.GetValues(typeof(ScriptElement)).Cast<ScriptElement>());

            workspaceServices.ControlButtonPressed += WorkspaceServices_ControlButtonPressed;
            workspaceServices.ControlButtonReleased += WorkspaceServices_ControlButtonReleased;
            workspaceServices.MKCFileConnectionUpdated += WorkspaceServices_MKCFileConnectionUpdated;
            workspaceServices.MKBRequestedConnectionRemoval += WorkspaceServices_MKBRequestedConnectionRemoval;
        }

        private void WorkspaceServices_MKCFileConnectionUpdated(object? sender, MKBFileConnectionUpdateEventArgs e)
        {
            if (e.MKCFile.FileID == AssociatedFile.FileID)
            {
                if (!AssociatedFile.ConnectedMKMFilesID.Contains(e.MKBFile.FileID))
                {
                    AssociatedFile.ConnectedMKMFilesID.Add(e.MKBFile.FileID);
                }
            }
        }

        private void WorkspaceServices_MKBRequestedConnectionRemoval(object? sender, MKBFileConnectionUpdateEventArgs e)
        {
            if (e.MKCFile.FileID == AssociatedFile.FileID)
            {
                foreach (var button in PaperCanvas.Children.OfType<DialogueLinkButton>().ToList())
                {
                    if (e.MKBFile.FileID == button.AssociatedMKBFile.FileID)
                    {
                        UnlinkMKBConnection(button, true);
                    }
                }
            }
        }

        private void WorkspaceServices_ControlButtonPressed(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                if (e.KeyEvent.Key == Key.LeftCtrl)
                {
                    CanUnlinkMKBButton = true;
                    ResetCharacterLinkButtonStatus?.Invoke(this, new EventArgs());
                }
            }
        }

        private void WorkspaceServices_ControlButtonReleased(object? sender, Model.InputEventArgs e)
        {
            if (e.CurrentActiveWorkspaceID == _workspaceServicesID)
            {
                if (e.KeyEvent.Key == Key.LeftCtrl)
                {
                    CanUnlinkMKBButton = false;
                    ResetCharacterLinkButtonStatus?.Invoke(this, new EventArgs());
                }
            }
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

                    if (LastPickedParagraph.NextBlock != null)
                    {
                        if (LastPickedParagraph.NextBlock.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                        {
                            Blocks.InsertAfter(LastPickedParagraph.NextBlock, paragraph);
                            MKRichTextBox.CaretPosition = paragraph.ContentStart;
                        }
                        else
                        {
                            Blocks.InsertAfter(CurrentBlock, paragraph);
                            MKRichTextBox.CaretPosition = paragraph.ContentStart;
                        }
                    }
                    else
                    {
                        Blocks.InsertAfter(CurrentBlock, paragraph);
                        MKRichTextBox.CaretPosition = paragraph.ContentStart;
                    }

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
            if (scriptElement != ScriptElement.Action)
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
            }
        }
        #endregion

        #region Paragraph Selection And Navigation Logic
        private async void GetParagraph(Paragraph paragraph)
        {
            if (paragraph != null)
            {
                if (paragraph.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                {
                    await UpdateCaretPositionToLastPosition();
                    return;
                }

                if (LastPickedParagraph != null)
                    LastPickedParagraph.Focusable = true;

                LastPickedParagraph = paragraph;
                SelectParagraphSource = true;
                SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                LastPickedParagraph.Focusable = false;
                _ = UpdateLastCaretPositionAsync();

                UpdateCharacterCaretPosition();
            }
        }

        public void UpdateLastCaretPosition()
        {
            LastCaretPosition = MKRichTextBox.CaretPosition;
        }

        private async Task UpdateLastCaretPositionAsync()
        {
            await Task.Delay(50);
            LastCaretPosition = MKRichTextBox.CaretPosition;
        }

        private async Task UpdateCaretPositionToLastPosition()
        {
            await Task.Delay(50);
            MKRichTextBox.CaretPosition = LastCaretPosition;
        }

        public async void NavigateParagraphs(Key key)
        {
            await AssignLastPara(key);
        }

        private async Task AssignLastPara(Key key)
        {
            await Task.Delay(50);

            LastPickedParagraph.Focusable = true;
            if (key == Key.Up || key == Key.Left)
            {
                if (LastPickedParagraph.PreviousBlock != null)
                {
                    if (MKRichTextBox.CaretPosition.Paragraph.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                    {
                        LastPickedParagraph = LastPickedParagraph.PreviousBlock.PreviousBlock as Paragraph;
                        LastPickedParagraph.Focusable = false;
                        SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                        MKRichTextBox.CaretPosition = LastPickedParagraph.ContentEnd;
                        UpdateLastCaretPosition();
                    }
                    else
                    {
                        LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
                        LastPickedParagraph.Focusable = false;
                        SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    }
                }
            }
            else if (key == Key.Down || key == Key.Right)
            {
                if (LastPickedParagraph.NextBlock != null)
                {
                    if (MKRichTextBox.CaretPosition.Paragraph.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                    {
                        if (LastPickedParagraph.NextBlock.NextBlock == null)
                        {
                            MKRichTextBox.CaretPosition = LastPickedParagraph.ContentEnd;
                            UpdateLastCaretPosition();
                            UpdateAllPaperCanvasButtonsLocation();

                            return;
                        }

                        LastPickedParagraph = LastPickedParagraph.NextBlock.NextBlock as Paragraph;
                        LastPickedParagraph.Focusable = false;
                        SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                        MKRichTextBox.CaretPosition = LastPickedParagraph.ContentStart;
                        UpdateLastCaretPosition();
                    }
                    else
                    {
                        LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
                        LastPickedParagraph.Focusable = false;
                        SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    }
                }
            }

            UpdateCharacterCaretPosition();
            UpdateLastCaretPosition();
            UpdateAllPaperCanvasButtonsLocation();
        }

        // Delete this function later!!!
        private void UpdateLastPickedParagraph()
        {
            /*if (MKRichTextBox != null)
            {
                TextPointer caretPosition = MKRichTextBox.CaretPosition;
                Paragraph paragraph = caretPosition.Paragraph;

                if (paragraph != null && paragraph.IsKeyboardFocused == false)
                {
                    paragraph.Focus();

                    if (paragraph.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                    {
                        return;
                    }

                    if (LastPickedParagraph != null)
                        LastPickedParagraph.Focusable = true;

                    LastPickedParagraph = paragraph;
                    SelectParagraphSource = true;
                    SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    LastPickedParagraph.Focusable = false;

                    UpdateCharacterCaretPosition();
                }
            }*/
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
        public bool SetLastPickedParagraphAfterParagraphUpdate()
        {
            if (CanDeleteAllText == true && MKRichTextBox.Selection.Text != "")
            {
                MKRichTextBox.Selection.Text = "";
                UpdateLastCaretPosition();

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
                if (SelectedScriptElement == ScriptElement.Dialogue && LastPickedParagraph.NextBlock != null)
                {
                    if (string.IsNullOrWhiteSpace(textRange.Text) && LastPickedParagraph.NextBlock.Tag is UIContainer uIContainer
                        && uIContainer == UIContainer.ConnectedMKBButton)
                    {
                        return true;
                    }
                }
                else if (SelectedScriptElement == ScriptElement.Character && LastPickedParagraph.NextBlock != null)
                {
                    if (string.IsNullOrWhiteSpace(textRange.Text) && LastPickedParagraph.NextBlock.NextBlock.Tag is UIContainer uIContainer
                        && uIContainer == UIContainer.ConnectedMKBButton)
                    {
                        return true;
                    }
                }

                return LastPickedParagraphAfterParagraphUpdate();
            }
        }

        private bool LastPickedParagraphAfterParagraphUpdate()
        {
            LastPickedParagraph = MKRichTextBox.CaretPosition.Paragraph;
            SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
            TextRange textRange = new TextRange(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            Debug.WriteLine($"Current Paragraph: {textRange.Text}");

            if (SelectedScriptElement == ScriptElement.Character && string.IsNullOrWhiteSpace(textRange.Text) && MKRichTextBox.Document.Blocks.Count == 1)
            {
                MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);
                LastPickedParagraph = new Paragraph();
                LastPickedParagraph.Tag = ScriptElement.SceneHeading;
                SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;

                MKRichTextBox.Document.Blocks.Add(LastPickedParagraph);
                MKRichTextBox.CaretPosition = LastPickedParagraph.ContentStart;
                UpdateLastCaretPosition();

                return true;
            }
            else if (string.IsNullOrWhiteSpace(textRange.Text) && MKRichTextBox.Document.Blocks.Count > 1)
            {
                if (SelectedScriptElement == ScriptElement.Character)
                    DeleteButtonLink();

                Block PreviousBlock = LastPickedParagraph.PreviousBlock;
                if (PreviousBlock != null)
                {
                    MKRichTextBox.Document.Blocks.Remove(LastPickedParagraph);

                    if (PreviousBlock.Tag is UIContainer uIContainer && uIContainer == UIContainer.ConnectedMKBButton)
                        LastPickedParagraph = PreviousBlock.PreviousBlock as Paragraph;
                    else
                        LastPickedParagraph = PreviousBlock as Paragraph;

                    MKRichTextBox.CaretPosition = LastPickedParagraph.ContentEnd;
                    SelectedScriptElement = (ScriptElement)LastPickedParagraph.Tag;
                    UpdateCharacterCaretPosition();
                    UpdateLastCaretPosition();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void DeleteAllText()
        {
            if (SelectedScriptElement == ScriptElement.Character)
            {
                Inline CharacterNameInline = LastPickedParagraph.Inlines.ElementAt(LastPickedParagraph.Inlines.Count - 2);
                MKRichTextBox.Selection.Select(LastPickedParagraph.ContentStart, CharacterNameInline.ContentEnd);
            }
            else
            {
                MKRichTextBox.Selection.Select(LastPickedParagraph.ContentStart, LastPickedParagraph.ContentEnd);
            }

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

                if (CurrentParagraphCoverBorder != null)
                    UpdateParagraphCoverBorderLocation(CurrentParagraphCoverBorder);

                if (CurrentMKBSelectionBox != null)
                    UpdateMKBSelectionBoxLocation(CurrentMKBSelectionBox);
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

                else if (button is DialogueLinkButton dialogueLinkButton)
                    UpdateButtonLinkLocation(dialogueLinkButton);
            }
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
            if (SelectParagraphSource == true)
            {
                SelectParagraphSource = false;
                return;
            }

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

        private void CreateCharacterLinkButton()
        {
            if (LastPickedParagraph.Inlines.LastInline is InlineUIContainer)
                return;

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

            CharacterLinkButton characterLinkButton = new CharacterLinkButton(this, LastPickedParagraph, inlineUIContainer);
            characterLinkButton.Margin = new Thickness(10, 0, 0, 0);

            characterLinkButton.Click += CharacterLinkButton_Click;

            PaperCanvas.Children.Add(characterLinkButton);
            UpdateButtonLinkLocation(characterLinkButton);

            CurrentCharacterLinkButton = characterLinkButton;
        }

        private void CharacterLinkButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterLinkButton characterLinkButton = (CharacterLinkButton)sender;
            CurrentCharacterLinkButton = characterLinkButton;
            
            if (CurrentCharacterLinkButton.AssociatedDialogueButton == null)
            {
                StartMKBConnectionProcess(CurrentCharacterLinkButton);
            }
            else if (CanUnlinkMKBButton == true)
            {
                UnlinkMKBConnection(CurrentCharacterLinkButton.AssociatedDialogueButton);
            }
            else
            {
                CurrentCharacterLinkButton.AssociatedDialogueButton.ButtonFocusAnimation(true);
            }
        }

        private void UnlinkMKBConnection(DialogueLinkButton dialogueLinkButton, bool IsMKBSource = false)
        {
            // Don't forget to delete the node in MKB file and update the user data json file
            AssociatedFile.ConnectedMKMFilesID.Remove(dialogueLinkButton.AssociatedMKBFile.FileID);

            MKRichTextBox.Document.Blocks.Remove(dialogueLinkButton.ParagraphHolder);
            PaperCanvas.Children.Remove(dialogueLinkButton);

            if (IsMKBSource == false)
                _workspaceServices.OnMKRequestedConnectionRemoval(dialogueLinkButton.AssociatedMKBFile.FileID);

            dialogueLinkButton.ButtonCreated -= DialogueLinkButton_ButtonCreated;
            dialogueLinkButton.ButtonRequstedToOpenMKBFile -= DialogueLinkButton_ButtonRequstedToOpenMKBFile;
            CurrentCharacterLinkButton.AssociatedDialogueButton = null;
            CurrentCharacterLinkButton.UpdateButtonVisuals(false);
        }

        private void StartMKBConnectionProcess(CharacterLinkButton characterLinkButton)
        {
            Block block = characterLinkButton.CharacterParagraph.NextBlock;
            if (block != null && (ScriptElement)block.Tag == ScriptElement.Dialogue)
            {
                CurrentDialogueParagraph = block as Paragraph;
                PrepareRTBForMKBSelectionProcess();
            }
            else
            {
                MKRichTextBox.IsHitTestVisible = false;

                Paragraph paragraph = new Paragraph();
                paragraph.Tag = ScriptElement.Dialogue;
                paragraph.Margin = DialogueMargins;

                Run run = new Run("Dialogue");
                paragraph.Inlines.Add(run);

                MKRichTextBox.Document.Blocks.InsertAfter(characterLinkButton.CharacterParagraph, paragraph);
                MKRichTextBox.CaretPosition = paragraph.ContentEnd;

                CurrentDialogueParagraph = paragraph;

                if (CurrentDialogueParagraph.NextBlock != null)
                    AddBorderOnTopParagraph(CurrentDialogueParagraph.NextBlock as Paragraph, false);

                Paragraph MKBParagraph = new Paragraph();
                MKBParagraph.TextAlignment = TextAlignment.Center;
                MKBParagraph.Tag = UIContainer.ConnectedMKBButton;
                MKBParagraph.Focusable = false;

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
                UpdateMKBSelectionBoxLocation(mKBSelectionBox);
            }
        }

        private void MKBSelectionProcess()
        {
            Paragraph paragraph = new Paragraph();
            paragraph.TextAlignment = TextAlignment.Center;
            paragraph.Tag = UIContainer.ConnectedMKBButton;
            paragraph.Focusable = false;

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
            MKRichTextBox.Document.Blocks.InsertAfter(CurrentDialogueParagraph, paragraph);

            MKBSelectionBox mKBSelectionBox = new MKBSelectionBox(this, inlineUIContainer, paragraph);
            mKBSelectionBox.MKBSelectionBoxRequestedRemoval += MKBSelectionBox_MKBSelectionBoxRequestedRemoval;
            PaperCanvas.Children.Add(mKBSelectionBox);
            CurrentMKBSelectionBox = mKBSelectionBox;
            UpdateMKBSelectionBoxLocation(mKBSelectionBox);
        }

        private void PrepareRTBForMKBSelectionProcess()
        {
            MKRichTextBox.IsHitTestVisible = false;

            if (CurrentDialogueParagraph.NextBlock != null)
                AddBorderOnTopParagraph(CurrentDialogueParagraph.NextBlock as Paragraph, true);
            else
                MKBSelectionProcess();
        }

        private void AddBorderOnTopParagraph(Paragraph paragraph, bool DialogueSource)
        {
            CurrentParagraphCoverBorder = new ParagraphCoverBorder
            {
                Style = (Style)Application.Current.FindResource("ParagraphCoverBorder"),
                AssociatedParagraph = paragraph,
                Width = 700,
                Height = 0,
                IsHitTestVisible = false,
                Tag = UIContainer.ParagraphCoverBorder
            };

            PaperCanvas.Children.Add(CurrentParagraphCoverBorder);

            UpdateParagraphCoverBorderLocation(CurrentParagraphCoverBorder);
            ParagraphBorderCoverAnimation(CurrentParagraphCoverBorder, 600, 0, true, DialogueSource);
        }

        private void ParagraphBorderCoverAnimation(Border border, double Target, double BeginTime, bool IsStart, bool CanStartProcess)
        {
            Storyboard storyboard = new Storyboard();

            DoubleAnimation BorderAnimation = new DoubleAnimation();
            BorderAnimation.BeginTime = TimeSpan.FromSeconds(BeginTime);
            BorderAnimation.To = Target;
            BorderAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.6));
            BorderAnimation.EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut };

            Storyboard.SetTarget(BorderAnimation, border);
            Storyboard.SetTargetProperty(BorderAnimation, new PropertyPath("Height"));
            storyboard.Children.Add(BorderAnimation);

            storyboard.Completed += (s, e) =>
            {
                if (IsStart == true && CanStartProcess == true)
                {
                    MKBSelectionProcess();
                }
                else if (IsStart == false)
                {
                    PaperCanvas.Children.Remove(CurrentParagraphCoverBorder);
                    CurrentParagraphCoverBorder = null;
                }
            };

            storyboard.Begin();
        }

        private void MKBSelectionBox_MKBSelectionBoxRequestedRemoval(object? sender, bool e)
        {
            if (e == true)
            {
                if(AssociatedFile.ConnectedMKMFilesID == null)
                    AssociatedFile.ConnectedMKMFilesID = new List<string>();

                AssociatedFile.ConnectedMKMFilesID.Add(SelectedMKBFile.FileID);
                DialogueLinkButton dialogueLinkButton = CreateDialogueLinkButton();
                CurrentCharacterLinkButton.AssociatedDialogueButton = dialogueLinkButton;
                CurrentCharacterLinkButton.UpdateButtonVisuals(e);

                _workspaceServices.OnMKBFileConnectionUpdated(SelectedMKBFile, AssociatedFile);
            }
            else
            {
                MKRichTextBox.Document.Blocks.Remove(CurrentMKBSelectionBox.ParagraphHolder);
            }

            if (CurrentParagraphCoverBorder != null)
                ParagraphBorderCoverAnimation(CurrentParagraphCoverBorder, 0, 0, false, false);

            PaperCanvas.Children.Remove(CurrentMKBSelectionBox);
            CurrentMKBSelectionBox.MKBSelectionBoxRequestedRemoval -= MKBSelectionBox_MKBSelectionBoxRequestedRemoval;
            CurrentMKBSelectionBox = null;

            MKRichTextBox.IsHitTestVisible = true;
            MKRichTextBox.CaretPosition = CurrentDialogueParagraph.ContentEnd;
            UpdateLastCaretPosition();
        }

        private DialogueLinkButton CreateDialogueLinkButton()
        {
            DialogueLinkButton dialogueLinkButton = new DialogueLinkButton(this, CurrentCharacterLinkButton.CharacterParagraph, CurrentDialogueParagraph,
                CurrentMKBSelectionBox.ParagraphHolder, CurrentMKBSelectionBox.Placeholder, SelectedMKBFile);

            PaperCanvas.Children.Add(dialogueLinkButton);

            dialogueLinkButton.ButtonCreated += DialogueLinkButton_ButtonCreated;
            dialogueLinkButton.ButtonRequstedToOpenMKBFile += DialogueLinkButton_ButtonRequstedToOpenMKBFile;

            return dialogueLinkButton;
        }

        private void DialogueLinkButton_ButtonCreated(object? sender, EventArgs e)
        {
            if (sender is DialogueLinkButton dialogueLinkButton)
                UpdateButtonLinkLocation(dialogueLinkButton);
        }

        private void DialogueLinkButton_ButtonRequstedToOpenMKBFile(object? sender, FileSystem e)
        {
            if (e != null)
                _workspaceServices.UserDataServices.AddFileToHeader(e);
        }

        private void HandleLeftMouseButtonUp()
        {
            if (MKRichTextBox.Selection.IsEmpty == true)
                return;

            TextPointer Start = MKRichTextBox.Selection.Start;
            TextPointer End = MKRichTextBox.Selection.End;

            Paragraph ParagraphStart = Start.Paragraph;
            Paragraph ParagraphEnd = End.Paragraph;

            if (ParagraphStart.Tag is UIContainer)
            {
                ParagraphStart = ParagraphStart.NextBlock as Paragraph;
                Start = ParagraphStart.ContentStart;
            }

            if (ParagraphEnd.Tag is UIContainer)
            {
                ParagraphEnd = ParagraphEnd.PreviousBlock as Paragraph;
                End = ParagraphEnd.ContentEnd;
            }

            ScriptElement ParagraphEndScriptElement = (ScriptElement)ParagraphEnd.Tag;

            if (ParagraphEndScriptElement == ScriptElement.Character)
            {
                Inline CharacterNameInline = ParagraphEnd.Inlines.ElementAt(ParagraphEnd.Inlines.Count - 2);
                End = CharacterNameInline.ContentEnd;
            }

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

            // getting the mid-point of the placeholder's border is unnecessary as the width is 1, so the margin is negligible,
            // but to avoid future errors I will keep it :)
            double PlaceholderCenter = relativeToRichTextBox.X + (CharacterRect.Width / 2);
            double ButtonCenter = dialogueLinkButton.Width / 2;
            double Offset = 30;
            double ButtonPositionX = PlaceholderCenter - ButtonCenter - Offset;

            Canvas.SetLeft(dialogueLinkButton, ButtonPositionX);
            Canvas.SetTop(dialogueLinkButton, relativeToRichTextBox.Y);
        }

        private void UpdateMKBSelectionBoxLocation(MKBSelectionBox mKBSelectionBox)
        {
            TextPointer PlaceholderPosition = mKBSelectionBox.Placeholder.ContentStart;
            Rect CharacterRect = PlaceholderPosition.GetCharacterRect(LogicalDirection.Forward);

            Point relativeToRichTextBox = MKRichTextBox.TransformToAncestor(GridTarget).Transform(new Point(CharacterRect.X, CharacterRect.Y));

            if (Double.IsInfinity(relativeToRichTextBox.X))
                return;

            Canvas.SetLeft(mKBSelectionBox, relativeToRichTextBox.X - 330);
            Canvas.SetTop(mKBSelectionBox, relativeToRichTextBox.Y - 2);
        }

        private void UpdateParagraphCoverBorderLocation(ParagraphCoverBorder paragraphCoverBorder)
        {
            TextPointer PlaceholderPosition = paragraphCoverBorder.AssociatedParagraph.ContentStart;
            Rect CharacterRect = PlaceholderPosition.GetCharacterRect(LogicalDirection.Forward);

            Point relativeToRichTextBox = MKRichTextBox.TransformToAncestor(GridTarget).Transform(new Point(CharacterRect.X, CharacterRect.Y));

            if (Double.IsInfinity(relativeToRichTextBox.X))
                return;

            Canvas.SetLeft(paragraphCoverBorder, relativeToRichTextBox.X - 100);
            Canvas.SetTop(paragraphCoverBorder, relativeToRichTextBox.Y);
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

            foreach (var file in CurrentProject.Files)
            {
                if (file.FileType.Equals("mkb"))
                {
                    if (!string.IsNullOrEmpty(file.ConnectedMKCFileID) && file.ConnectedMKCFileID != AssociatedFile.FileID)
                        continue;

                    MKBFiles.Add(file);
                }
            }
        }

        private void LoadPaperCanvas(Canvas canvas)
        {
            PaperCanvas = canvas;
        }
    }
}

/*
             * TODO:
             * - Have static width instead of a dynamic one. [DONE] - Dynamic actually looks better.
             * - Finish Dialogue Link Button design. [DONE]
             * - Modify animations. [DONE]
             * - Update Dialogue Link Buttons when:
             *      * Scrolling. [DONE]
             *      * Text input. [DONE]
             * - Change Caret if entered the Dialogue Link Button's Paragraph. [DONE]
             * - Add transition to the associated mkb file. [DONE]
             * # Add order index to know which paragraph came first.
             * - Add a list for BN to add the nodes.
             * - the Delete key removes the placeholders. override the button. [DONE]
             * - [Important] moving up and down using arrow keys, you must change the focus state [DONE]
             * - [Important] Disable RTB, and Connection buttons when connecting [DONE]
             * 
             * - Update User Data when adding or removing MKB Connection
             * - In MKB ViewModel Check if the file is already connected to MKC
             * - MKB Selection Box should only show:
             *      * MKB Files
             *      * MKB Files that aren't Connected to a different MKC File
             * - MKB File should have a list of Connected Dialogues.
             * # There should be indexing so we can decide who's first and whether or not to spawn splitter.
             * - We may need to revisit Dialogue Node Structure.
             */