   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  5   	       UDSH.View.PopupWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   #localclr-namespace:UDSH.View  5   	     � Title$  PopupWindow��5
   	   $	��170��6   $	��600��6*   ���#434343$��5q�6L    � WindowStartupLocation$ CenterScreen=�6`    � 
ResizeMode$ NoResize=�6�   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5         ��Name$ Message��5         DefaultText#��[  6%   $��Center=�6J   $��Center=�6g   �� 5   
   -   5        $��1��5        PopupWindowConfirmButton#��[ 6   $
��Left=�6f   $��Center=�6�   $	��130��6�   $��40��6�   $��	100,0,0,0q�6�   �� 5   
   -   5        $��1��5        PopupWindowCancelButton#��[ 6   $��Right=�6d   $��Center=�6�   $	��130��6�   $��40��6�   $��	0,0,100,0q�6�   5      ��5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value �#1D1B206+   �� 5       � �� " MainContentWindowBackgroundSCB# [ 6+   �� 5      '  !System.Windows.Shell.WindowChrome �@ WindowChrome 5        5         UseAeroCaptionButtons$ False��6#   	   CaptionHeight$	 70Q�6A   5      �� 5       � �� 5       �� 5!      
 �� 
TargetType"
 �6&   .R�5"      ��  ��Name$ WindowBorder��6!   #��z��67   #��z��6c    �� CornerRadius$ 5��6�   ._�5#      �  �Name$ Layout��6#   #��z��63    5$   "   �� 5%   &   $
��auto �64   �� 5&   &   $��* �64   .+�5)   "   � $ WindowTitlePanel��6'   $��40��6A   
������ $��0,5,0,0q�6f   .+�5*   &   ��  �� Orientation$ 
Horizontal=�61   $
��Left=�6J   .+�5+   *   �� $!��/Resource/HorusEyeBlack.png��60   $��Center=�6U   $��15,0,0,0q�6p   $��30��6�   $��30��6�   �� 5,   *   -   5        #��[  5,   4   $��10,0,0,0q�6o   $��Center=�6�   $��Center=�6�   �� 5-   *   #��[  64   $��Process��6Y   $��5,0,0,0q�6h   $��Center=�6y   $��Center=�6�   5.   '   �� 50   &    ��Name$ WindowCommandButtons��61   $��2��6O   $ 
Horizontal=�6_   $��0,0,10,0q�6x   $��Right=�51   -   $��Stretch=�6I   
������ .+�52   *   ��  ��Name$ CloseButton��61   -   5        $��40��52   F   $��40��6Q     Content_CloseButton#��[ 6]      IsHitTestVisibleInChrome$
 True��6�   53   '   54   #   �� 56   "   $��1��63   -  'System.Windows.Input.KeyboardNavigation   	IsTabStop$ False��6@   .u�57   &   ��  ��Name$ MainContentPresenter��67   #��z��6U      TabNavigation$ Cycle=�6y   58   #   59      5:      5;      5=      5>      5@      