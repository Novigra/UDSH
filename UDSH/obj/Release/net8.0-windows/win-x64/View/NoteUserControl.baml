   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View      UDSH.View.NoteUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  :d2http://schemas.microsoft.com/expression/blend/2008  #localclr-namespace:UDSH.View  .��� -     �Name$  	OuterGrid��.+��� -    ��Name$ 
NoteBorder��   NoteBorderThickness#��[    MainNoteBorderSCB#��[ $
|�True��  MainNoteBackgroundSCB#��[  �� CornerRadius$ 10�� � FocusVisualStyle Z� ._�� }��� *Test+ � RowDefinitions �� $
��auto ��� $
��auto �.+�� $��0��$
��Left=�$	��Top=� K� ZIndex$ 0�� Z� .+���   NoteHeadlineTextBlock#��[ $��Title��$��30(� J� Opacity$	 0.8Q�$��
30,30,0,10q��� -    ��Name$ TitleTextBox��  NoteContentTextBox#��[ $��30(�$��
30,30,0,10q����� *Title+ �� UpdateSourceTrigger$ PropertyChanged=�.��� $��0��$��30��$��30��$��Right=�$��Center=�$ 2��$��0,0,30,0q� Z� .+��� -     NoteCloseButton#��[ {��� *CanStartEditing+�� $!��/Resource/CloseNoteIcon.png��$��10��$��10������ 	 �� 
TargetType"	 �.��� 
 �� Property
 �@J�Opacity �� Value$	 0.0Q� �� Triggers ��  �� Binding �� *CanStartEditing+ �� Value True N� EnterActions �� .���� .��W�  �� TargetProperty$ Opacity�	 W� To 1.0 g� Duration$ 0:0:0.2D� N� ExitActions �� .���� .��W� $ Opacity� 0.0$ 0:0:0.2D�� $��1��$
��Left=�$��Stretch=�$ 0�� Z� .+���   NoteContentTextBlock#��[ $��Write your note...��$��15(�$	 0.8Q�$��
30,0,20,30q��� -   $ ContentTextBox��#��[ $��15(� �� AcceptsReturn$
 True�� �� TextWrapping$
 Wrap=�$��
30,0,20,30q����� *	Content+$ PropertyChanged=�.��*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ TextChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *CheckHeight+   CommandParameter ��  �� ElementName$ ContentTextBox��.���� -   $ BorderCollision��$��2��$ 1��$ 10��$
|�True��  MainBlankSCB#��[ $��
0,0,0,-132q� J� IsHitTestVisible �� *CanStartEditing+   $ Loaded��.    �� *BorderCollisionLoaded+ �� $ BorderCollision��  $ MouseLeftButtonDown��.      PassEventArgsToCommand$
 True�� �� *$"BorderCollisionMouseLeftButtonDown+  $ MouseLeftButtonUp��.   $
 True�� �� *" BorderCollisionMouseLeftButtonUp+  $ 	MouseMove��.   $
 True�� �� *BorderCollisionMouseMovement+���� "	 ��.��� 
 �@J�Opacity$	 0.0Q� ��  �� *CanStartEditing+ True.a��� 
 �@J�Opacity$	 1.0Q�   $ Loaded��.    �� *BorderLoaded+ �� $ 
NoteBorder��  $ 
MouseEnter��.    �� *NoteBorderMouseEnter+  $ 
MouseLeave��.    �� *NoteBorderMouseLeave+���� "	 �� :�  :� RoutedEvent$ Loaded��.[��� .���� .��W� $ Opacity� W� From 0.0 1.0$ 0:0:0.3D�� -   $  R_BorderEdge��$��2��
��� � �$��Right=�$��Center=�$��20��$	��260��$��	0,0,-10,0q�   $ 
MouseEnter��.    �� *NoteOuterGridMouseEnter+ �� $ R_BorderEdge��  $ 
MouseLeave��.    �� *NoteOuterGridMouseLeave+  $ MouseLeftButtonDown��.   $
 True�� �� *$"R_NoteOuterGridMouseLeftButtonDown+  $ MouseLeftButtonUp��.   $
 True�� �� *" R_NoteOuterGridMouseLeftButtonUp+  $ 	MouseMove��.   $
 True�� �� *R_NoteOuterGridMouseMove+   $ Loaded��.    �� *OuterGridLoaded+ �� $ 	OuterGrid��