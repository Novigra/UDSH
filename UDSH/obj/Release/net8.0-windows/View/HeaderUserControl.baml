   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  ? ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View !   UDSH.View.HeaderUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9System&clr-namespace:System;assembly=mscorlib     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  #localclr-namespace:UDSH.View  $
|�True��  � 	Resources     StartupProjectNameAnim g� FillBehavior �� TargetProperty	 W� To g� Duration g� 	BeginTime  AddedProjectNameAnim W� From��%P  &	        &	 �     �� -   $
 Stop=�.��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.5D� 0:0:0.7W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:0.7�� -   $
 Stop=�.��W� $, &RenderTransform.(TranslateTransform.X)�	 -40.0 0.0$ 0:0:0.5D�W� $ Opacity� 0.0 1.0$ 0:0:0.5D�.���  � RowDefinitions �� �� .+�� $��0�� � ColumnDefinitions �� $��70 ��� �� $	��150 �.+��� $��0�����	#FFADADAD	 K� ZIndex$	 1���� $��0��$	 2��$��/Resource/Logo.png��$	��-10q��� -   $	 0��$��1�����#CDCDCD�� -   $��ProjectName��$��1��$
��Left=�$��Center=�$��5,0,0,0q�
 J� RenderTransformOrigin$
 0.5,0.5&����� *ProjectName+����  �� 
TargetType" ��  DefaultText �� BasedOn# [ .���  �� Property �@J�Opacity �� Value$	 0.0Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T�  T� X$	 -40Q� T� Y$ 0Q� �� Triggers :�  :� RoutedEvent$ UserControl.Loaded��.[���  ��Name$ Startup��#��[  ��  �� Binding �� *CanPlayProjectAnimation+ �� Value True N� EnterActions �� $ NewProjectAdded��#��[ � $��0��$��3��$��Center=�$��Center=� J� IsHitTestVisible$ False��.+���  �� Orientation$ 
Horizontal=�.+��� #��[ $
��UDSH��$��Center=�$��0,0,0,0q��� #��[ $��|��$��Center=�$��5,0,5,5q�$��SemiBold"��� -   $��UserName��#��[ $��Center=�$��0,0,0,0q����� *DisplayName+�� $��Center=�$��10,8,0,8q����� *Icon1+�� $��Center=�$��10,8,0,8q����� *Icon2+�� $��Center=�$��10,8,0,8q����� *Icon3+� $��2�� �� �� �� .+��� $��0��$	 0���� -   $��0��$	 1���� $��0��$	 2��$��/Resource/Minimize.png��$��12,10,12,10q��� $��1��$	 0���� -   $��1��$	 1���� $��1��$	 2��$��/Resource/Normal.png��$��15,13,15,13q��� $��2��$	 0���� -   $��2��$	 1��  CloseButton#��[ �� $��2��$	 2��$��/Resource/Close.png��$��15,13,15,13q�� $��1�� �� $��50 ��� $
��auto ��� $��* ��� $	��150 �.+��� $��4�����#EEEEEE� $��0��.+��� -	   $��PenTool��$	 0��$��5q����� *PenToolLeftMouseButtonDown+.���� $ 
Horizontal=�.+�� -
    �Name$ MyPop�� � 	StaysOpen$ False�� � AllowsTransparency$
 True��$��Relative=�$
��Fade=� � VerticalOffset$ 25������ *CanClosePopup+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Closed��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction      Command  �� *PenToolPopupClosed+.���� -   $��0q�  SideContentSCB#��[ $	��300��! �� CornerRadius$! 10��._�� .+��� -   " ��Name$" StackCollision��$
|�True��$	 0��$ Vertical=�$��Stretch=�$	��Top=�# � FocusVisualStyle# Z�    $ 
MouseEnter��.     �� *MouseEnterPenToolPopup+$   CommandParameter$ �� % �� ElementName$% StackCollision��.+��� $ 
Horizontal=�$��5,5,0,0q�.+��� $��12(�$��Medium"����� *ProjectTitleSection+��   SideContentTitleBarSCB#��[ $��2,0,0,0q�$��Center=�$��Stretch=�$	��240��$��3��$
 0.5,0.5&�z�V� .~��� �� �� T� $	 1.5Q�� $
|�True��$��Stretch=�$��Stretch=� �� $��30 ��� �� $
��auto �.+��� $��0��$	 1��$��/Resource/NewFile.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��New File��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��3������ *	NewFile+{��� *CanEnablePenToolButton+�� $��2�� 
 QADots#��[ $��0������ *
QNewFile+�� $��2��  QABorder#��[ �� $��2��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=� ! QuickActionDotsWidthAndHeight#��[ #��[  	 QuickActionDotsMargins#��[	 $ False��$
 0.5,0.5&�z��� & �� ScaleX$	& 0.0Q�' �� ScaleY$	' 0.0Q����� " ��.���  �@J�Opacity$ 1Q� ^� ( ^� 
Conditions( �� ) �� Binding) �� *
CurrentDot+* �� Value* 0�� ) �� *PlayDotAnim+* True �� + �� BeginStoryboardName$
+ Rev1���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 0�� ) �� *ReverseDotAnim+* True �� $
 Rev1��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��2�� 
 DisabledPenToolButton#��[
 � $��Stretch=�$��Stretch=� �� $��30 ��� $
��auto ��� �� $
��auto �.+��� $��0��$	 1��$��/Resource/SaveFile.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$
��Save��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��$��5,0,0,0q�$	 1��$��CharacterEllipsis=�$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False������ *SaveFileText+�� $��4������ *
SaveFile+{��� *CanEnablePenToolButton+�� $��3��#��[ $��1������ *	QSaveFile+�� $��3��#��[ �� $��3��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=�#��[ #��[ #��[	 $ False��$
 0.5,0.5&�z��� $	& 0.0Q�$	' 0.0Q����� " �� ^� ( �� ) �� *
CurrentDot+* 1�� ) �� *PlayDotAnim+* True �� $
+ Rev2���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 1�� ) �� *ReverseDotAnim+* True �� $
 Rev2��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��3��#��[
 � $��Stretch=�$��Stretch=� �� $��30 ��� �� $
��auto �.+��� $��0��$	 1��$ ��/Resource/SaveAllFiles.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Save All Open Files��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��3������ *SaveAllFiles+{��� *CanEnablePenToolButton+�� $��2��#��[ $��2������ *QSaveAllFiles+�� $��2��#��[ �� $��2��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=�#��[ #��[ #��[	 $ False��$
 0.5,0.5&�z��� $	& 0.0Q�$	' 0.0Q����� " �� ^� ( �� ) �� *
CurrentDot+* 2�� ) �� *PlayDotAnim+* True �� $
+ Rev3���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 2�� ) �� *ReverseDotAnim+* True �� $
 Rev3��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��2��#��[
 � $��Stretch=�$��Stretch=� �� $��30 ��� $
��auto ��� �� $
��auto �.+��� $��0��$	 1��$��/Resource/DeleteFile.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Delete��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��$��5,0,0,0q�$	 1��$��CharacterEllipsis=�$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False������ *DeleteFileText+�� $��4������ *
DeleteFile+{��� *CanEnablePenToolButton+�� $��3��#��[ $��3������ *QDeleteFile+�� $��3��#��[ �� $��3��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=�#��[ #��[ #��[	 $ False��$
 0.5,0.5&�z��� $	& 0.0Q�$	' 0.0Q����� " �� ^� ( �� ) �� *
CurrentDot+* 3�� ) �� *PlayDotAnim+* True �� $
+ Rev4���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 3�� ) �� *ReverseDotAnim+* True �� $
 Rev4��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��3��#��[
 � $��Stretch=�$��Stretch=� �� $��30 ��� �� $
��auto �.+��� $��0��$	 1��$ ��/Resource/Localization.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Localization��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��3������ *Localization+{��� *CanEnablePenToolButton+�� $��2��#��[ $��4������ *QLocalization+�� $��2��#��[ �� $��2��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=�#��[ #��[ #��[	 $ False��$
 0.5,0.5&�z��� $	& 0.0Q�$	' 0.0Q����� " �� ^� ( �� ) �� *
CurrentDot+* 4�� ) �� *PlayDotAnim+* True �� $
+ Rev5���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 4�� ) �� *ReverseDotAnim+* True �� $
 Rev5��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��2��#��[
 � $��Stretch=�$��Stretch=� �� $��30 ��� �� $
��auto �.+��� $��0��$	 1��$!��/Resource/FolderContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Content��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��3������ *ContentFolder+{��� *CanEnablePenToolButton+�� $��2��#��[ $��5������ *QContentFolder+�� $��2��#��[ �� $��2��$! 20��$��0q����#393E46$	 2��$��Right=�$��Center=�#��[ #��[ #��[	 $ False��$
 0.5,0.5&�z��� $	& 0.0Q�$	' 0.0Q����� " �� ^� ( �� ) �� *
CurrentDot+* 5�� ) �� *PlayDotAnim+* True �� $
+ Rev6���� .���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 0.0 1.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 0.0 1.0$ 0:0:0.1D�^� ( �� ) �� *
CurrentDot+* 5�� ) �� *ReverseDotAnim+* True �� $
 Rev6��.���� .��W� $- 'RenderTransform.(ScaleTransform.ScaleX)� 1.0 0.0$ 0:0:0.1D�W� $- 'RenderTransform.(ScaleTransform.ScaleY)� 1.0 0.0$ 0:0:0.1D��� $��2��#��[
 �� $ 
Horizontal=�$��5,0,0,0q�.+��� $
��UDSH��$��12(�$��Medium"��� #��[ $��2,0,0,0q�$��Center=�$��Stretch=�$	��250��$��3��$
 0.5,0.5&�z�V� .~��� �� �� T� $	 1.5Q�� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$$��/Resource/FolderNewContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��New Project��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2������ *
NewProject+� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$'��!/Resource/FolderSwitchContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Switch Project��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$'��!/Resource/FolderSaveContentAs.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Save Project As...��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$'��!/Resource/FolderDeleteContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Delete Project��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$'��!/Resource/FolderImportContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Import Project��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2��� $��Stretch=�$��Stretch=� �� $��30 ��� .+��� $��0��$	 1��$%��/Resource/FolderOpenContent.png��$��5q�$��20��$��20��$
��Left=�$��Center=��� $��1��$	 1��$��Open Directory��$��15(�$��DemiBold"�$
��Left=�$��Center=�$ False���� $��2���� $��/Resource/PenTool.png��$��7q�, �� BitmapScalingMode$, HighQuality=�$ False��$
 0.5,0.5&�z�V� .~��� �� �� - �� Angle$- 0Q�T� ���� " �� ��  �� *IsPenToolButtonClicked+ True �� .���� .��W� $' !RenderTransform.Children[2].Angle� 0.0 35.0$ 0:0:0.1D�. N� ExitActions. �� .���� .��W� $' !RenderTransform.Children[2].Angle� 0.0$ 0:0:0.1D���  �� *CanClosePopup+	 False �� .���� .��W� $' !RenderTransform.Children[2].Angle� 0.0$ 0:0:0.1D�. �� .���� .��W� $' !RenderTransform.Children[2].Angle� 35.0$ 0:0:0.1D�� -   $��QuickActions��$��1��$	��130��$
��Left=� �� $��3 ��� �� �� .+��� -   $��PQSeparator��$��0�����#000000$��0,2,0,2q�/ �� RadiusX$/ 3��0 �� RadiusY$0 3���� -   $��QuickAction1��$��1��$��5q����� *QuickActionButton1+{��� *IsQAButtonEnabled1+�� $��1��$��7q�z�T� $ 0Q�$ 0Q����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�Opacity$ 0Q���  �@J�IsHitTestVisible$ False�� ��  �� *QButtonImage1+ -1.a���  � ��$ /Resource/Placeholder.png����  �� *QButtonImage1+ 0.a���  � ��$ /Resource/NewFile.png����  �� *QButtonImage1+ 1.a���  � ��$ /Resource/SaveFile.png����  �� *QButtonImage1+ 2.a���  � ��$  /Resource/SaveAllFiles.png����  �� *QButtonImage1+ 3.a���  � ��$ /Resource/DeleteFile.png����  �� *QButtonImage1+ 4.a���  � ��$  /Resource/Localization.png����  �� *QButtonImage1+ 5.a���  � ��$! /Resource/FolderContent.png����  �� *CanPlayAssignAnim1+ True �� $+ A1���� $+ B1���� .���� .��W� $ Opacity� 0.0 1.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 5.0 0.0$ 0:0:0.2D���  �� *CanPlayReassignAnim1+ True �� $ A1��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�1 g� AutoReverse$
1 True��W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D�$
1 True����  �� *CanPlayRemoveAnim1+ True �� $ B1��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D��� -   $��QuickAction2��$��2��$��5q����� *QuickActionButton2+{��� *IsQAButtonEnabled2+�� $��2��$��7q�z�T� $ 0Q�$ 0Q����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� ��  �� *QButtonImage2+ -1.a���  � ��$ /Resource/Placeholder.png����  �� *QButtonImage2+ 0.a���  � ��$ /Resource/NewFile.png����  �� *QButtonImage2+ 1.a���  � ��$ /Resource/SaveFile.png����  �� *QButtonImage2+ 2.a���  � ��$  /Resource/SaveAllFiles.png����  �� *QButtonImage2+ 3.a���  � ��$ /Resource/DeleteFile.png����  �� *QButtonImage2+ 4.a���  � ��$  /Resource/Localization.png����  �� *QButtonImage2+ 5.a���  � ��$! /Resource/FolderContent.png����  �� *CanPlayAssignAnim2+ True �� $+ A2���� $+ B2���� .���� .��W� $ Opacity� 0.0 1.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 5.0 0.0$ 0:0:0.2D���  �� *CanPlayReassignAnim2+ True �� $ A2��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�$
1 True��W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D�$
1 True����  �� *CanPlayRemoveAnim2+ True �� $ B2��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D��� -   $��QuickAction3��$��3��$��5q����� *QuickActionButton3+{��� *IsQAButtonEnabled3+�� $��3��$��7q�z�T� $ 0Q�$ 0Q����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� ��  �� *QButtonImage3+ -1.a���  � ��$ /Resource/Placeholder.png����  �� *QButtonImage3+ 0.a���  � ��$ /Resource/NewFile.png����  �� *QButtonImage3+ 1.a���  � ��$ /Resource/SaveFile.png����  �� *QButtonImage3+ 2.a���  � ��$  /Resource/SaveAllFiles.png����  �� *QButtonImage3+ 3.a���  � ��$ /Resource/DeleteFile.png����  �� *QButtonImage3+ 4.a���  � ��$  /Resource/Localization.png����  �� *QButtonImage3+ 5.a���  � ��$! /Resource/FolderContent.png����  �� *CanPlayAssignAnim3+ True �� $+ A3���� $+ B3���� .���� .��W� $ Opacity� 0.0 1.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 5.0 0.0$ 0:0:0.2D���  �� *CanPlayReassignAnim3+ True �� $ A3��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�$
1 True��W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D�$
1 True����  �� *CanPlayRemoveAnim3+ True �� $ B3��.���� .��W� $ Opacity� 1.0 0.0$ 0:0:0.2D�W� $, &RenderTransform.(TranslateTransform.Y)� 0.0 5.0$ 0:0:0.2D�� -   $��OpenedFiles��$��2��$
��Left=� �� $��3 ��� $��* �   $ Loaded��.     �� *LoadedContentLayoutGrid+$ �� $% OpenedFiles��z�T� $
 -130Q�$ 0Q�'  !System.Windows.VisualStateManager2   VisualStateGroups2 %  System.Windows.VisualStateGroup  3   Name$3 QuickActionStates��4   States.4    System.Windows.VisualState  5   Name$5 State0��6   
Storyboard.6 �� .��W� $, &RenderTransform.(TranslateTransform.X)�
 -130.0$ 0:0:0.1D�  $5 State1��.6 �� .��W� $, &RenderTransform.(TranslateTransform.X)�	 -85.0$ 0:0:0.1D�  $5 State2��.6 �� .��W� $, &RenderTransform.(TranslateTransform.X)�	 -43.0$ 0:0:0.1D�  $5 State3��.6 �� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.1D�.+��� -   $��QOSeparator��$��0�����#000000$��0,2,0,2q�$/ 3��$0 3��$
 0.5,0.5&��� -   $��	PagesList��$��1��$��5,0,0,0q�$
��Left=�  OpenedPagesLayout#��[   OpenedPages#��[ ���� *	OpenFiles+7 �� SelectedItem7 �� *SelectedFile+   $ SelectionChanged��.     �� *OpenFilesSelectionChanged+�� $��1��  OpenFilesButtonRight#��[ $��Right=�$��50������ *ScrollRight+�� $��1��  OpenFilesButtonLeft#��[ $
��Left=�$��50������ *
ScrollLeft+� $��3�� �� �� �� .+��� -   $��ProfileSettings��$��1��$	 0��$��18,5,0,5q�A� $��1��$��30��$��30��$��17,0,0,0q�$	 1��$, HighQuality=�$ False������ 8 �� ImageSource8 �� *ProfilePicture+�� -   $��ProfilePicture��$��1��$"��/Resource/ProfilePicture.png��$��18,9,0,9q�$	 1��9 J� Opacity9 �� *BlankProfilePictureOpacity+�� -   $��AppSettings��$��2��$	 0��$��	10,5,10,5q��� $��2��$��/Resource/Settings.png��$��10q�$	 1���� -   $��OpenedFilesList��$��0��$	 0��$��	10,5,10,5q����� *OpenFilesList+{��� *CanOpenFilesList+.��� -   $ OpenFilesListPopup��: � HorizontalOffset$
: -200��$ 25��$ False��$
 True��$��Relative=�$
��Fade=����� *IsOpenFilesListPopupOpen+; �� UpdateSourceTrigger$; PropertyChanged=�.���� -   ���#EEEEEE$	��300��$	��400��$! 10��._��� < �� SelectionMode$< Single=�  OpenedPagesListLayout#��[   OpenedPagesListItem#��[ ���� *	OpenFiles+7 �� *SelectedFile+�� $��0��$'��!/Resource/OpenedFilesDropdown.png��$��10q�$	 1������ " ��.���  �@J�Opacity$ 0Q���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� ��  �� *CanOpenFilesList+ True �� .���� .��W� $ Opacity� 1.0$ 0:0:0.2D�. �� .���� .��W� $ Opacity� 0.0$ 0:0:0.2D�