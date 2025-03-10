// Copyright (C) 2025 Mohammed Kenawy
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UDSH.MVVM
{
    public class ListViewCommand
    {
        public static readonly DependencyProperty ListViewItemMouseLeaveCommandProperty = DependencyProperty.RegisterAttached("ListViewItemMouseLeaveCommand", typeof(ICommand), typeof(ListViewCommand),
                new PropertyMetadata(null, OnListViewItemMouseLeaveCommandChanged));

        #region ListViewItem Mouse Leave
        public static void SetListViewItemMouseLeaveCommand(DependencyObject Dep, ICommand Command)
        {
            Dep.SetValue(ListViewItemMouseLeaveCommandProperty, Command);
        }

        public static ICommand GetListViewItemMouseLeaveCommand(DependencyObject Dep)
        {
            return (ICommand)Dep.GetValue(ListViewItemMouseLeaveCommandProperty);
        }

        private static void OnListViewItemMouseLeaveCommandChanged(DependencyObject Dep, DependencyPropertyChangedEventArgs Event)
        {
            if (Dep is ListViewItem listViewItem)
            {
                listViewItem.MouseLeave -= RecordListViewItemMouseLeave;

                if (Event.NewValue is ICommand)
                    listViewItem.MouseLeave += RecordListViewItemMouseLeave;
            }
        }

        private static void RecordListViewItemMouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is ListViewItem listViewItem)
            {
                ICommand Command = GetListViewItemMouseLeaveCommand(listViewItem);
                if (Command != null && Command.CanExecute(listViewItem))
                    Command.Execute(listViewItem);
            }
        }
        #endregion
    }
}
