using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UDSH.MVVM
{
    class ParagraphMouseClickBehavior
    {
        #region Properties
        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty = DependencyProperty.RegisterAttached("MouseLeftButtonDownCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
            new PropertyMetadata(null, OnCommandChanged));

        public static readonly DependencyProperty MouseEnterDragProperty = DependencyProperty.RegisterAttached("DragEnterCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
            new PropertyMetadata(null, OnMouseEnterDragCommandChanged));

        public static readonly DependencyProperty GotKeyboardFocusCommandProperty = DependencyProperty.RegisterAttached("GotKeyboardFocusCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnGotKeyboardFocusCommandChanged));

        public static readonly DependencyProperty MouseEnterCommandProperty = DependencyProperty.RegisterAttached("MouseEnterCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnMouseEnterCommandChanged));
        public static readonly DependencyProperty MouseLeaveCommandProperty = DependencyProperty.RegisterAttached("MouseLeaveCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnMouseLeaveCommandChanged));

        public static readonly DependencyProperty MouseEnterScrollBarCommandProperty = DependencyProperty.RegisterAttached("MouseEnterScrollBarCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnMouseEnterScrollBarCommandChanged));
        public static readonly DependencyProperty MouseLeaveScrollBarCommandProperty = DependencyProperty.RegisterAttached("MouseLeaveScrollBarCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnMouseLeaveScrollBarCommandChanged));

        public static readonly DependencyProperty ScrollViewerLoadedCommandProperty = DependencyProperty.RegisterAttached("ScrollViewerLoadedCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnScrollViewerLoadedCommandChanged));
        #endregion

        #region L_Mouse Down
        public static void SetMouseLeftButtonDownCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseLeftButtonDownCommandProperty, Command);
        }

        public static ICommand GetMouseLeftButtonDownCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseLeftButtonDownCommandProperty);
        }

        private static void OnCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if(Dep is Paragraph paragraph)
            {
                paragraph.MouseLeftButtonDown -= RecordMouseLeftButtonDown;

                if (Event.NewValue is ICommand)
                    paragraph.MouseLeftButtonDown += RecordMouseLeftButtonDown;
            }
        }

        private static void RecordMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Paragraph paragraph)
            {
                ICommand Command = GetMouseLeftButtonDownCommand(paragraph);
                if(Command != null && Command.CanExecute(paragraph))
                    Command.Execute(paragraph);
            }

        }
        #endregion

        #region L_Drag Down
        public static void SetMouseEnterDragCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseEnterDragProperty, Command);
        }

        public static ICommand GetMouseEnterDragCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseEnterDragProperty);
        }

        private static void OnMouseEnterDragCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is Paragraph paragraph)
            {
                paragraph.DragEnter -= RecordMouseDragEnter;

                if (Event.NewValue is ICommand)
                    paragraph.DragEnter += RecordMouseDragEnter;
            }
        }

        private static void RecordMouseDragEnter(object sender, DragEventArgs e)
        {
            if (sender is Paragraph paragraph)
            {
                var Command = GetMouseEnterDragCommand(paragraph);
                if (Command != null && Command.CanExecute(paragraph))
                    Command.Execute(paragraph);
            }
        }
        #endregion

        #region Got Keyboard Focus
        public static void SetGotKeyboardFocusCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(GotKeyboardFocusCommandProperty, Command);
        }

        public static ICommand GetGotKeyboardFocusCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(GotKeyboardFocusCommandProperty);
        }

        private static void OnGotKeyboardFocusCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is Paragraph paragraph)
            {
                paragraph.GotKeyboardFocus -= OnParagraphGotKeyboardFocus;

                if (Event.NewValue is ICommand)
                {
                    paragraph.GotKeyboardFocus += OnParagraphGotKeyboardFocus;
                }
            }
        }

        private static void OnParagraphGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
           /* if (sender is Paragraph paragraph)
            {
                TextRange textRange = new TextRange(paragraph.ContentStart, paragraph.ContentEnd);
                string paragraphText = textRange.Text;

                Debug.WriteLine($"GotKeyboardFocus on Paragraph: {paragraphText}");

                ICommand command = GetGotKeyboardFocusCommand(paragraph);
                if (command != null && command.CanExecute(paragraph))
                {
                    command.Execute(paragraph);
                }
            }*/
        }
        #endregion

        #region Mouse Enter Scroll Bar Collision Hit
        public static void SetMouseEnterCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseEnterCommandProperty, Command);
        }

        public static ICommand GetMouseEnterCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseEnterCommandProperty);
        }

        private static void OnMouseEnterCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is Rectangle rectangle)
            {
                rectangle.MouseEnter -= RecordMouseEnter;

                if (Event.NewValue is ICommand)
                    rectangle.MouseEnter += RecordMouseEnter;
            }
        }

        private static void RecordMouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Rectangle rectangle)
            {
                Debug.WriteLine($"Rectangle Name: {rectangle.Name}");
                ICommand Command = GetMouseEnterCommand(rectangle);
                if (Command != null && Command.CanExecute(rectangle))
                    Command.Execute(rectangle);
            }

        }
        #endregion

        #region Mouse Leave Scroll Bar Collision Hit
        public static void SetMouseLeaveCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseLeaveCommandProperty, Command);
        }

        public static ICommand GetMouseLeaveCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseLeaveCommandProperty);
        }

        private static void OnMouseLeaveCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is Rectangle rectangle)
            {
                rectangle.MouseLeave -= RecordMouseLeave;

                if (Event.NewValue is ICommand)
                    rectangle.MouseLeave += RecordMouseLeave;
            }
        }

        private static void RecordMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Rectangle rectangle)
            {
                Debug.WriteLine($"Rectangle Name: {rectangle.Name}");
                ICommand Command = GetMouseLeaveCommand(rectangle);
                if (Command != null && Command.CanExecute(rectangle))
                    Command.Execute(rectangle);
            }

        }
        #endregion

        #region Mouse Enter Scroll Bar
        public static void SetMouseEnterScrollBarCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseEnterScrollBarCommandProperty, Command);
        }

        public static ICommand GetMouseEnterScrollBarCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseEnterScrollBarCommandProperty);
        }

        private static void OnMouseEnterScrollBarCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is ScrollBar scrollBar)
            {
                scrollBar.MouseEnter -= RecordMouseEnterScrollBar;

                if (Event.NewValue is ICommand)
                    scrollBar.MouseEnter += RecordMouseEnterScrollBar;
            }
        }

        private static void RecordMouseEnterScrollBar(object sender, MouseEventArgs e)
        {
            if (sender is ScrollBar scrollBar)
            {
                Debug.WriteLine($"ScrollBar Name: {scrollBar.Name}");
                ICommand Command = GetMouseEnterScrollBarCommand(scrollBar);
                if (Command != null && Command.CanExecute(scrollBar))
                    Command.Execute(scrollBar);
            }

        }
        #endregion

        #region Mouse Leave Scroll Bar
        public static void SetMouseLeaveScrollBarCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(MouseLeaveScrollBarCommandProperty, Command);
        }

        public static ICommand GetMouseLeaveScrollBarCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(MouseLeaveScrollBarCommandProperty);
        }

        private static void OnMouseLeaveScrollBarCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is ScrollBar scrollBar)
            {
                scrollBar.MouseLeave -= RecordMouseLeaveScrollBar;

                if (Event.NewValue is ICommand)
                    scrollBar.MouseLeave += RecordMouseLeaveScrollBar;
            }
        }

        private static void RecordMouseLeaveScrollBar(object sender, MouseEventArgs e)
        {
            if (sender is ScrollBar scrollBar)
            {
                Debug.WriteLine($"ScrollBar Name: {scrollBar.Name}");
                ICommand Command = GetMouseLeaveScrollBarCommand(scrollBar);
                if (Command != null && Command.CanExecute(scrollBar))
                    Command.Execute(scrollBar);
            }

        }
        #endregion


        #region Scroll Viewer Loaded
        public static void SetScrollViewerLoadedCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(ScrollViewerLoadedCommandProperty, Command);
        }

        public static ICommand GetScrollViewerLoadedCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(ScrollViewerLoadedCommandProperty);
        }

        private static void OnScrollViewerLoadedCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is CustomScrollViewer scrollViewer)
            {
                scrollViewer.Loaded -= RecordScrollViewerLoad;

                if (Event.NewValue is ICommand)
                    scrollViewer.Loaded += RecordScrollViewerLoad;
            }
        }

        private static void RecordScrollViewerLoad(object sender, RoutedEventArgs e)
        {
            if (sender is CustomScrollViewer scrollViewer)
            {
                Debug.WriteLine($"ScrollBar Name: {scrollViewer.Name}");
                ICommand Command = GetScrollViewerLoadedCommand(scrollViewer);
                if (Command != null && Command.CanExecute(scrollViewer))
                    Command.Execute(scrollViewer);
            }

        }
        #endregion
    }
}
