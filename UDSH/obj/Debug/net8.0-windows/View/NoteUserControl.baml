   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  5          UDSH.View.NoteUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      #localclr-namespace:UDSH.View  5      .��5      � -   5          �Name$  	OuterGrid��5      .+�5   
   �� -   5         ��Name$ 
NoteBorder��5         NoteBorderThickness#��[  6?     MainNoteBorderSCB#��[ 6v   $
|�True��5        MainNoteBackgroundSCB#��[ 60    �� CornerRadius$ 10��6d    � FocusVisualStyle 6v   Z� ._�5      � }�6   �� *Test+ � RowDefinitions 5      �� 5      $
��auto �6$   �� 5      $
��auto �6$   .+�5      � $��0��6   $
��Left=�6$   $	��Top=�6?    K� ZIndex$ 0��6W    6h   Z� .+�5      ��   NoteHeadlineTextBlock#��[ 6    $��Title��6O   $��30(�6\    J� Opacity$	 0.8Q�6j   $��
30,30,0,10q�6x   �� 5      -   5         ��Name$ TitleTextBox��5        NoteContentTextBox#��[ 64   $��30(�6�   $��
30,30,0,10q�6�   ��6`   �� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5      5      � 5      $��0��6   $��30��6$   $��30��6/   $��Right=�6;   $��Center=�6W   $ 2��6r   $��0,0,30,0q�6�    6�   Z� .+�5      �� -   5          NoteCloseButton#��[ 5      {�6b   �� *CanStartEditing+�� 5      $!��/Resource/CloseNoteIcon.png��6   $��10��6A   $��10��6L   ��5      �� 5       	 �� 
TargetType"	 �6    .�5!      �� 
 �� Property
 �@J�Opacity �� Value$	 0.0Q�68    �� Triggers 5#      �� 5$   "    �� Binding 6.   �� *CanStartEditing+ �� Value True6R    N� EnterActions 5%   &   �� 5&   *   .��5'   .   �� .��5(   2   W�  �� TargetProperty$ Opacity�6B   	 W� To 1.05)   9    g� Duration$ 0:0:0.2D�5*   9   5+   /   5,   +    N� ExitActions 5.   &   �� 5/   *   .��50   .   �� .��51   2   W� $ Opacity�6B    0.052   :   $ 0:0:0.2D�53   :   54   /   55   +   57   #   59      5;      � 5=      $��1��6   $
��Left=�6$   $��Stretch=�6?   $ 0��6[    6l   Z� .+�5>      ��   NoteContentTextBlock#��[ 6    $��Write your note...��6N   $��15(�6h   $	 0.8Q�6v   $��
30,0,20,30q�6�   �� 5?      -   5        $ ContentTextBox��5?      #��[ 66   $��15(�6�    �� AcceptsReturn$
 True��6�    �� TextWrapping$
 Wrap=�5@      $��
30,0,20,30q�6*   ��5?   b   �� *	Content+$ PropertyChanged=�.��5@   >   *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5A      +  %Microsoft.Xaml.Behaviors.EventTrigger  5B         	EventName$ TextChanged��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5C   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *CheckHeight+   CommandParameter 6X   ��  �� ElementName$ ContentTextBox��5D      .��5E   2   5F      5G      �� 5I      -   5        $ BorderCollision��5I      $��2��62   $ 1��6C   $ 10��6T   $
|�True��6�     MainBlankSCB#��[ 5J      $��
0,0,0,-132q�6<    J� IsHitTestVisible 5I   f   �� *CanStartEditing+ 5K        5L      $ Loaded��6)   . 5M         64   �� *BorderCollisionLoaded+ 6^   �� $ BorderCollision��5N        5P      $ MouseLeftButtonDown��6)   . 5Q           PassEventArgsToCommand$
 True��6k    64   �� *$"BorderCollisionMouseLeftButtonDown+6k   5R        5T      $ MouseLeftButtonUp��6)   . 5U        $
 True��6i    64   �� *" BorderCollisionMouseLeftButtonUp+6i   5V        5X      $ 	MouseMove��6)   . 5Y        $
 True��6e    64   �� *BorderCollisionMouseMovement+6e   5Z      ��5]      �� 5^      "	 ��6    .�5_      �� 
 �@J�Opacity$	 0.0Q�68    5`      �� 5a   "    6.   �� *CanStartEditing+ True6R   .a�5b   &   �� 
 �@J�Opacity$	 1.0Q�6@   5c   #   5e      5g      5�       5�        5�      $ Loaded��6!   . 5�         6,   �� *BorderLoaded+ 6M   �� $ 
NoteBorder��5�        5�      $ 
MouseEnter��6!   . 5�         6,   �� *NoteBorderMouseEnter+5�        5�      $ 
MouseLeave��6!   . 5�         6,   �� *NoteBorderMouseLeave+5�      ��5�      �� 5�      "	 ��6    5�      :� 5�       :� RoutedEvent$ Loaded��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ Opacity�66    W� From 0.05�   .    1.05�   .   $ 0:0:0.3D�5�   .   5�   #   5�      5�      5�      5�      � 5�   
   -   5        $  R_BorderEdge��5�      $��2��6%   
��� � �$��Right=�6I   $��Center=�6e   $��20��6�   $	��260��6�   $��	0,0,-10,0q�6�    5�        5�      $ 
MouseEnter��6!   . 5�         6,   �� *NoteOuterGridMouseEnter+ 6X   �� $ R_BorderEdge��5�        5�      $ 
MouseLeave��6!   . 5�         6,   �� *NoteOuterGridMouseLeave+5�        5�      $ MouseLeftButtonDown��6!   . 5�        $
 True��6c    6,   �� *$"R_NoteOuterGridMouseLeftButtonDown+6c   5�        5�      $ MouseLeftButtonUp��6!   . 5�        $
 True��6a    6,   �� *" R_NoteOuterGridMouseLeftButtonUp+6a   5�        5�      $ 	MouseMove��6!   . 5�        $
 True��6Y    6,   �� *R_NoteOuterGridMouseMove+6Y   5�      5�       5�   
     5�      $ Loaded��6   . 5�         6(   �� *OuterGridLoaded+ 6L   �� $ 	OuterGrid��5�      5�      5�      