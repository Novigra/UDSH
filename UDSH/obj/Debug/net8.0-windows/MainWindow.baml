   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UDSHUDSH  5   	   %clr-namespace:UDSH.View	UDSH.View  5   	       UDSH.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:UDSH     5   	   0userControlsclr-namespace:UDSH.View     5   	     � Title$  
MainWindow��5
   	   $	��900��6   $
��1600��6)    � WindowStartupLocation$ CenterScreen=�66   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$	 0.1q�6      CornerRadius$	 0.1��61      CaptionHeight$	 0.1Q�6D   .��5      � -   5         �Name$ MainWindowGrid��5       � RowDefinitions 5   
   �� 5      $��80 �6   �� 5      �� 5      $��35 �6   .+�5   
   � -   5        $ HeaderUserControl��5      $��0��6*   �� 5   
   -   5         ��Name$ TestContent��5      $��1��6.   	 K� ZIndex$	 0��6;   � 5   
   -   5        $ FooterUserControl��5      $��2��6*   $	 1��67   � 5   
   -   5        $ SideGrid��5      $��1��6!   $
��Left=�6.   $��0,40,0,0q�6I   5      5      5       