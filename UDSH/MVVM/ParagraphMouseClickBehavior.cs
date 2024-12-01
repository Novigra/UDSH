using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace UDSH.MVVM
{
    class ParagraphMouseClickBehavior
    {
        #region Properties
        public static readonly DependencyProperty MouseLeftButtonDownCommandProperty = DependencyProperty.RegisterAttached("MouseLeftButtonDownCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
            new PropertyMetadata(null, OnCommandChanged));

        public static readonly DependencyProperty MouseEnterDragProperty = DependencyProperty.RegisterAttached("DragEnterCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
            new PropertyMetadata(null, OnMouseEnterCommandChanged));

        public static readonly DependencyProperty GotKeyboardFocusCommandProperty = DependencyProperty.RegisterAttached("GotKeyboardFocusCommand", typeof(ICommand), typeof(ParagraphMouseClickBehavior),
                new PropertyMetadata(null, OnGotKeyboardFocusCommandChanged));
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

        private static void OnMouseEnterCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
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
    }
}
