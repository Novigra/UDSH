using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UDSH.MVVM;
using UDSH.View;

namespace UDSH.ViewModel
{
    class NoteUserControlViewModel : ViewModelBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set { content = value; OnPropertyChanged(); }
        }

        private double borderTargetHeight;
        public double BorderTargetHeight
        {
            get { return borderTargetHeight; }
            set { borderTargetHeight = value; OnPropertyChanged(); }
        }

        private bool isInsideNoteBorder;
        public bool IsInsideNoteBorder
        {
            get { return isInsideNoteBorder; }
            set { isInsideNoteBorder = value; OnPropertyChanged(); }
        }

        private bool canStartEditing;
        public bool CanStartEditing
        {
            get { return canStartEditing; }
            set { canStartEditing = value; OnPropertyChanged(); }
        }

        private bool canChangeBordersWidth;
        public bool CanChangeBordersWidth
        {
            get { return canChangeBordersWidth; }
            set { canChangeBordersWidth = value; OnPropertyChanged(); }
        }

        private bool isEditingRightBorder;
        public bool IsEditingRightBorder
        {
            get { return isEditingRightBorder; }
            set { isEditingRightBorder = value; OnPropertyChanged(); }
        }

        private bool IsMousePressed;

        private Grid OuterGridTarget;
        private Grid CurrentBorderEdge;

        private Border BorderTarget;
        private Border BorderCollisionTarget;

        UserControl ParentControl;
        NoteUserControl CurrentUserControl;

        private Point InitialMousePosition;
        private Point CurrentPosition;

        double InitialRightOffset;

        public RelayCommand<TextBox> CheckHeight => new RelayCommand<TextBox>(CheckHeightStatus);
        public RelayCommand<Grid> OuterGridLoaded => new RelayCommand<Grid>(GetOuterGridRef);
        public RelayCommand<Border> BorderLoaded => new RelayCommand<Border>(GetBorderRef);
        public RelayCommand<Border> BorderCollisionLoaded => new RelayCommand<Border>(GetBorderCollisionRef);

        public RelayCommand<Border> NoteBorderMouseEnter => new RelayCommand<Border>(execute => MouseEnteredBorder());
        public RelayCommand<Border> NoteBorderMouseLeave => new RelayCommand<Border>(execute => MouseLeavedBorder());

        public RelayCommand<Grid> NoteOuterGridMouseEnter => new RelayCommand<Grid>(MouseEnteredOuterGrid);
        public RelayCommand<Grid> NoteOuterGridMouseLeave => new RelayCommand<Grid>(execute => MouseLeavedOuterGrid());

        public RelayCommand<MouseButtonEventArgs> BorderCollisionMouseLeftButtonDown => new RelayCommand<MouseButtonEventArgs>(StartRecordingMouseMovement);
        public RelayCommand<MouseEventArgs> BorderCollisionMouseMovement => new RelayCommand<MouseEventArgs>(UpdateNotePosition);
        public RelayCommand<MouseButtonEventArgs> BorderCollisionMouseLeftButtonUp => new RelayCommand<MouseButtonEventArgs>(StopRecordingMouseMovement);

        public RelayCommand<MouseButtonEventArgs> R_NoteOuterGridMouseLeftButtonDown => new RelayCommand<MouseButtonEventArgs>(StartEditingRightBorder);
        public RelayCommand<MouseButtonEventArgs> R_NoteOuterGridMouseLeftButtonUp => new RelayCommand<MouseButtonEventArgs>(StopEditingRightBorder);
        //public RelayCommand<MouseButtonEventArgs> R_NoteOuterGridMouseMove => new RelayCommand<MouseButtonEventArgs>(UpdateRightBorder);

        public NoteUserControlViewModel(NoteUserControl control, UserControl ParentControl)
        {
            Title = string.Empty;
            Content = string.Empty;

            IsInsideNoteBorder = true;
            CanStartEditing = false;
            IsMousePressed = false;

            IsEditingRightBorder = false;

            CurrentUserControl = control;
            this.ParentControl = ParentControl;
            InitiateScrollingEvent();
            UpdateCoordinates += NoteUserControlViewModel_UpdateCoordinates;
        }

        private void NoteUserControlViewModel_UpdateCoordinates(object? sender, Point e)
        {
            CurrentPosition = e;
        }

        private void InitiateScrollingEvent()
        {
            if(ParentControl is MKUserControl control)
            {
                control.MouseScroll += UpdateNotePosition_MouseScroll;
            }
        }

        private void UpdateNotePosition_MouseScroll(object? sender, int Delta)
        {
            CurrentPosition.Y += Delta;
            CurrentUserControl.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
            Debug.WriteLine($"Scrolling: X = {CurrentPosition.X}, Y = {CurrentPosition.Y}");
        }

        private void CheckHeightStatus(TextBox textBox)
        {
            if (textBox != null && BorderTarget != null)
            {
                if(BorderTargetHeight != (textBox.ActualHeight + 100))
                {
                    BorderTargetHeight = textBox.ActualHeight + 100;
                    BorderCollisionTarget.Height = BorderTargetHeight;
                    BorderCollisionTarget.Margin = new Thickness(0);
                    PlayAnimation();
                }
            }
        }

        private void PlayAnimation()
        {
            var Storyboard = new Storyboard();
            var UpdatingAnimation = new DoubleAnimation
            {
                To = BorderTargetHeight,
                Duration = TimeSpan.FromSeconds(0.3),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(UpdatingAnimation, BorderTarget);
            Storyboard.SetTarget(UpdatingAnimation, OuterGridTarget);
            Storyboard.SetTargetProperty(UpdatingAnimation, new PropertyPath("Height"));
            Storyboard.Children.Add(UpdatingAnimation);
            Storyboard.Begin();
        }

        private void GetOuterGridRef(Grid grid)
        {
            if (grid != null)
            {
                OuterGridTarget = grid;

                OuterGridTarget.Width = 400;
                OuterGridTarget.Height = 260;
            }
        }

        private void GetBorderRef(Border border)
        {
            if (border != null)
            {
                BorderTarget = border;

                BorderTarget.Width = 400;
                BorderTarget.Height = 260;
                BorderTargetHeight = border.Height;
            }
        }

        private void GetBorderCollisionRef(Border border)
        {
            if (border != null)
            {
                BorderCollisionTarget = border;
            }
        }

        private void MouseEnteredBorder()
        {
            IsInsideNoteBorder = true;
            BorderTarget.Focus();

            Debug.WriteLine($"Is Mouse Inside The Border: {IsInsideNoteBorder}");
        }

        private void MouseLeavedBorder()
        {
            IsInsideNoteBorder = false;

            Debug.WriteLine($"Is Mouse Inside The Border: {IsInsideNoteBorder}");
        }

        private void MouseEnteredOuterGrid(Grid grid)
        {
            if(grid != null)
            {
                CurrentBorderEdge = grid;
                CanChangeBordersWidth = true;
                Debug.WriteLine($"CanChangeBordersWidth: {CanChangeBordersWidth}");
            }
        }

        private void MouseLeavedOuterGrid()
        {
            CanChangeBordersWidth = false;
            Debug.WriteLine($"CanChangeBordersWidth: {CanChangeBordersWidth}");
        }

        private void StartRecordingMouseMovement(MouseButtonEventArgs e)
        {
            if (e != null)
            {
                IsMousePressed = true;
                BorderCollisionTarget.CaptureMouse();
                Debug.WriteLine($"Pressing Left Mouse");
            }
        }

        private void UpdateNotePosition(MouseEventArgs e)
        {
            if (e != null && IsMousePressed == true)
            {
                CurrentPosition = e.GetPosition(ParentControl);

                CurrentPosition.X -= CurrentUserControl.ActualWidth / 2;
                CurrentPosition.Y -= CurrentUserControl.ActualHeight / 2;

                CurrentUserControl.Margin = new Thickness(CurrentPosition.X, CurrentPosition.Y, 0, 0);
                Debug.WriteLine($"Moving: X = {CurrentPosition.X}, Y = {CurrentPosition.Y}");
            }
        }

        private void StopRecordingMouseMovement(MouseButtonEventArgs e)
        {
            if (e != null)
            {
                IsMousePressed = false;
                BorderCollisionTarget.ReleaseMouseCapture();
                Debug.WriteLine($"Released Left Mouse");
            }
        }

        private void StartEditingRightBorder(MouseButtonEventArgs e)
        {
            if(e != null && CanChangeBordersWidth == true)
            {
                InitialMousePosition = e.GetPosition(OuterGridTarget);
                InitialRightOffset = CurrentUserControl.Margin.Right;
                CurrentBorderEdge.CaptureMouse();
                IsEditingRightBorder = true;

                Debug.WriteLine($"Starting...");
            }
        }

        private void StopEditingRightBorder(MouseButtonEventArgs e)
        {
            if (e != null)
            {
                CurrentBorderEdge.ReleaseMouseCapture();
                IsEditingRightBorder = false;

                Debug.WriteLine($"Stopped...");
            }
        }
    }
}
