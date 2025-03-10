// Copyright (C) 2025 Mohammed Kenawy
using System.Windows;

namespace UDSH.View
{
    /// <summary>
    /// Interaction logic for DeletionProcessWindow.xaml
    /// </summary>
    public partial class DeletionProcessWindow : Window
    {
        public DeletionProcessWindow(Window ParentWindow, string ItemName)
        {
            InitializeComponent();

            Owner = ParentWindow;
            ItemTextBlock.Text = ItemName;
        }
    }
}
