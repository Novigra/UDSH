   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   QuickActionDotsMargins  u� Left
 u� Top u� Right u� Bottom ! QuickActionDotsWidthAndHeight  Button.Radius �� TopLeft �� 
BottomLeft �� TopRight �� BottomRight   CloseOpenedPageButton.Radius  ShortcutButton.Radius  NewFileCloseButton.Radius  NoteCloseButton.Radius  StartupButton.Radius  ConfirmButton.Radius �� 
TargetType 	 Placeholder	 �� Property 
 MainUnselectedButtonSCB
 �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property P� Value N� EnterActions �� TargetProperty ! MainHoveredControlButtonColor	 �� To g� Duration N� ExitActions  MainUnselectedButtonColor  MainPressedButtonSCB  CloseButton " MainHoveredCloseAppButtonColor  CloseOpenedPageButton # MainHoveredClosePageButtonColor  NoteCloseButton  MainNoteClosePageButtonSCB ' #MainHoveredNoteClosePageButtonColor   MainNoteClosePageButtonColor  NewFileCreationButton  ShortcutButtonSCB  ShortcutButtonColor " SelectionNewFileCreationButton  MainBlankSCB 
 QADots  NewFileCreationCloseButton  HeaderToggleButtons  StartupButton  
HoldButton   ImageConfirm ! OpenFilesButtonRight !" MainOpenPageRepeatButtonRight'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value �� Binding �� Value	 W� To # OpenFilesButtonLeft  $ MainOpenPageRepeatButtonLeft % Content_MinimizeButton & Content_NormalButton ' Content_MaxButton ( Content_CloseButton ) ContentBackButton J� RenderTransformOrigin T� X T� Y  ��Name! J� Opacity" �� Triggers# �� 
TargetName * ContentNextButton + RenameItemConfirmButton , DefaultText - RenameItemCancelButton . NameItemConfirmButton / DeleteConfirmButton 0 DeleteCancelButton 1 TransparentButton 2 ContextConnectButton 3 PopupWindowConfirmButton$ ��Name 4 PopupWindowCancelButton 5 ButtonInterp% :� RoutedEvent�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     1System&clr-namespace:System;assembly=mscorlib  %�<  &	        &	 %     &	 1     &	 V     &	 {     &	 �     &	 �     &	 �     &	 �     &		 �     '�� �     7 
 7  7  7  7  &	 V    7 
 7  7  7  &	 �    7 
 7  7  7  7  &	 W    7  7  7  7  7  &	 �    7  7  7  7  7  &	 �    7  7  &	 	    7 
 7  7  7   7  &	 �
    7 
 7  7  7  7  &	 #    7 
 7  7  7  7  &	 �    7 
 7  7  7  7  &	 S    7 
 7  7  &	      7  7  &	! �    7 " 7  &	# O    7 $ 7  &	% �    7 
 7  7  7  7  &	& �    7 
 7  7  7  7  &	' W    7 
 7  7  7  7  &	( !    7 
 7  7  7  7  &	) �    &	* #    &	+ @&    7 , &	- H)    7 , &	. �*    7 , &	/ �-    7 , &	0 �.    7 , &	1 ]/    &	2 �/    7 
 7  7  7  &	3 �1    7 , &	4 �4    7 , &	5 G8    7 
 7  7  u� $  0Q�$ 0Q�$ 8Q�$ 0Q�X�17Q��� $ 0Q�$ 0Q�$ 0Q�$ 0Q��� $ 0Q�$ 0Q�$ 5Q�$ 0Q���15����10����8����8����4���� " ���� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��
 �� " ��.R��� #��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=��� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � {�
 �� *IsPenToolButtonDisabled+�� 	 �@J�IsHitTestVisible
 �� *IsPenToolButtonDisabled+�� 	 � ��$
 Right=��� 	 � ��$
 Center=��� 	 � ��8
  �� 	 � ��8
  �� 	 � ��8
  �� 	 �@K�ZIndex$
 0���� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=��� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " d�.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " d�.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.3D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.3D�P�  � ��$
 True��. ��� 	 � ��8
  �� " d�.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " d�.R��� #��z��8  ._��� $��Center=�$��Center=� P�  �@d�	IsChecked$
 TrueY�. ��� 	 � ��8
  �� " ��.��� 	 � ��
 �#393E46�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8   ._��� $��Confirm�����#FFFFFF$��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 �@J�IsHitTestVisible
 �� *IsRightScrollButtonActive+�� 	 �@J�Opacity$
 0Q��� 	 � ��
 �� " ��.R��� #��z��8  ._�� .+��� $��/Resource/RightArrow.png��$��20��$��20��$��Center=�$��Center=�$��	0,0,-10,0q��� $��Center=�$��Center=�   CommonStates��.   
Normal��. �� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0:0:0.3��$ 	#00DBAD2F��  	MouseOver��. �� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0:0:0.3��$ 	#33DBAD2F��  Pressed��  Disabled�� ��  �� *IsRightScrollButtonActive+ True �� .���� .��W� $ Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $ Opacity� 0.0$ 0:0:0.2D��� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 �@J�IsHitTestVisible
 �� *IsLeftScrollButtonActive+�� 	 �@J�Opacity$
 0Q��� 	 � ��
 �� " ��.R��� #��z��8  ._�� .+��� $��/Resource/LeftArrow.png��$��20��$��20��$��Center=�$��Center=�$��	-10,0,0,0q��� $��Center=�$��Center=�   CommonStates��.   
Normal��. �� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0:0:0.3��$ 	#00DBAD2F��  	MouseOver��. �� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0:0:0.3��$ 	#33DBAD2F��  Pressed��  Disabled�� ��  �� *IsLeftScrollButtonActive+ True �� .���� .��W� $ Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $ Opacity� 0.0$ 0:0:0.2D��� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��/Resource/Minimize.png��$��15��$��15��$��Center=�$��Center=�$��5q� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��/Resource/Normal.png��$��Center=�$��Center=�$��15��$��15��$��5q� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��/Resource/Max.png��$��Center=�$��Center=�$��15��$��15��$��5q� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��/Resource/Close.png��$��Center=�$��15��$��15��$��Center=�$��5q� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  �� " ��.��� 	 � ��
 �#939393�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 4,0,0,4��._�� $��Center=�$��Center=�$ 0.5,0.5&�z�T� ArrowTranslate��$ 0Q�$ 0Q�.+��� $��/Resource/LeftArrow.png��$��10q��� $  ImageMouseOver��$#��/Resource/LeftArrowOrange.png��$! 0Q�$��10q�" P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D�W� $# ImageMouseOver��$ Opacity� 1$ 0:0:0.2D�W� $ X�$# ArrowTranslate�� -3$ 0:0:0.2D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #939393$ 0:0:0.2D�W� $# ImageMouseOver��$ Opacity� 0$ 0:0:0.2D�W� $ X�$# ArrowTranslate�� 0$ 0:0:0.2D��� " ��.��� 	 � ��
 �#939393�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 0,4,4,0��._�� $��Center=�$��Center=�$ 0.5,0.5&�z�T� ArrowTranslate��$ 0Q�$ 0Q�.+��� $��/Resource/RightArrow.png��$��10q��� $  ImageMouseOver��$$��/Resource/RightArrowOrange.png��$! 0Q�$��10q�" P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D�W� $# ImageMouseOver��$ Opacity� 1$ 0:0:0.2D�W� $ X�$# ArrowTranslate�� 3$ 0:0:0.2D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #939393$ 0:0:0.2D�W� $# ImageMouseOver��$ Opacity� 0$ 0:0:0.2D�W� $ X�$# ArrowTranslate�� 0$ 0:0:0.2D��� " ��.��� 	 � ��
 �#DBAD2F�� 	 � ��$
 0q��� 	 � {�
 �� *" IsRenameItemConfirmButtonEnabled+�� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� 8��  $��Confirm��$��Center=�$��Center=�   
States��.   
Normal��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D�  Disabled��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #ADADAD$ 0:0:0.2D� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #C79E2D$ 0:0:0.2D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D���  �� *RenameNewName+  .a��� 	 � {�$
 False���� " ��.��� 	 � ��
 �#F83030�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� 8��  $��Cancel��$��Center=�$��Center=� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DF2727$ 0:0:0.2D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #F83030$ 0:0:0.2D��� " ��.��� 	 � ��
 �#DBAD2F�� 	 � ��$
 0q��� 	 � {�
 �� * IsNameItemConfirmButtonEnabled+�� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� 8��  $��Confirm��$��Center=�$��Center=�   
States��.   
Normal��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D�  Disabled��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #ADADAD$ 0:0:0.2D� P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #C79E2D$ 0:0:0.2D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D���  �� *ItemNewName+  .a��� 	 � {�$
 False���� " ��.��� 	 � ��
 �#DBAD2F�� 	 � ��$
 0q��� 	 � {�$

 True���� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� 8��  $��Confirm��$��Center=�$��Center=��� " ��.��� 	 � ��
 �#F83030�� 	 � ��$
 0q��� 	 � {�$

 True���� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� 8��  $��Cancel��$��Center=�$��Center=��� " ��.��� 	 � ��

 ���� �� 	 � ��$
 0q��� 	 � {�$

 True���� 	 � ��
 �� " ��.R��� #��z���� " ��.��� 	 � ��8
   �� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 0��._���  P�  �@J�IsMouseOver$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�P�  � ��$
 True��. ��� 	 � ��8
  P�  � {�$ False��. ��� 	 � ��

 ���� �� " ��.��� 	 � ��
 �#FFFFFF�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� $$ ButtonTextContent��8��  $��Confirm��$��Center=�$��Center=�   
States��.   
Normal��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #FFFFFF$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #000000$ 0:0:0.2D�  	MouseOver��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D�  Pressed��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D��� " ��.��� 	 � ��
 �#FFFFFF�� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� #��z��$ 4��._��� $$ ButtonTextContent��8��  $��Cancel��$��Center=�$��Center=�   
States��.   
Normal��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #FFFFFF$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #000000$ 0:0:0.2D�  	MouseOver��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D�  Pressed��. �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� $# ButtonTextContent��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D��� " ��.��� 	 � ��8
   �� 	 � ��
 �� " ��.R��� #��z��8  ._��� $��Center=�$��Center=� :� $% Button.MouseEnter��.[��� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�
 Yellow$ 0:0:0.1D�:� $% Button.MouseLeave��.[��� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D�