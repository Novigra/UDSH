   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  ? ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View %clr-namespace:UDSH.MVVM	UDSH.MVVM %   UDSH.View.NewFileCreationWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  3System&clr-namespace:System;assembly=mscorlib  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  #localclr-namespace:UDSH.View  "MVVMclr-namespace:UDSH.MVVM    � Title$  NewFileCreationWindow��$	��550��$
��1000�� � WindowStyle$
 None=�
������  � AllowsTransparency$
 True�� � WindowStartupLocation$ CenterOwner=� � ShowInTaskbar$ False�� J� Clip ��  �� Rect$ 0,0,1000,550� �� RadiusX$ 10Q� �� RadiusY$ 10Q�	 � 	Resources	    CSFDataType  
BNDataType  MiscDataType  ChooseNameAnim
 �� TargetProperty	 W� To g� Duration��%�   &	        &	      &	      &	 '     ��mkc����mkb����mkm���� .��W� $
 Opacity� 0.0$ 0:0:0.5D�W� -   $,
 &RenderTransform.(TranslateTransform.X)�
 -150.0$ 0:0:0.5D�.��� -    �Name$ WindowLayout�� � RowDefinitions �� $
��auto ��� $
��auto ��� $	��.8* ��� $	��.2* �.+�� $��4�����	#00151515 K� ZIndex$ 1������  �� 
TargetType" �.���  �� Property �@J�IsHitTestVisible �� Value$ False�� �� Triggers ��  �� Binding �� *CanShowDirectory+ �� Value True.a���  �@J�IsHitTestVisible$
 True�� N� EnterActions �� .���� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)�	 �� To 	#4C151515$ 0:0:0.1D� N� ExitActions �� .���� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)� 	#00151515$ 0:0:0.1D��� $��4��  NewFileProcessBackgroundSCB#��[ � $��0��.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��	10,10,0,0q�.+��� $��/Resource/NewFile.png��$��30��$��30���� $��New File Creation Process��$��17(�$��Center=�  DefaultText#��[ $��5,0,0,0q�� $��Right=�$	��Top=�$��	0,10,30,0q�.+��� $��40��$��40��  NewFileCreationCloseButton#��[ ���� *CloseWindow+�� $��/Resource/Close.png��$��10��$��10��� $��1��$��
0,-10,0,10q�.+�� $��Center=�$��Center=�.+��� $ 
Horizontal=�.+��� $��Choose��$��40(�#��[ �� $��.��$��40(�$��6,0,0,0q����� " �� �� BasedOn# [  :�  :� RoutedEvent$ Loaded��.[��� .���� .��U�  g� 	BeginTime 0:0:0.25$,
 &RenderTransform.(TranslateTransform.Y)�$ 0:0:1D� g� AutoReverse$
 True�� g� RepeatBehavior$ Forever��.\���  O� KeyTime$ 0:0:0.27��  O� Value  0�� $ 0:0:1��  -10�� $��.��$��40(����� " ��# [  :� $ Loaded��.[��� .���� .��U� $,
 &RenderTransform.(TranslateTransform.Y)� 0:0:0.6$ 0:0:1D�$
 True��$ Forever��.\��� $ 0:0:0.27��  0�� $ 0:0:1��  -10�� $��.��$��40(����� " ��# [  :� $ Loaded��.[��� .���� .��U� $,
 &RenderTransform.(TranslateTransform.Y)� 0:0:0.9$ 0:0:1D�$
 True��$ Forever��.\��� $ 0:0:0.27��  0�� $ 0:0:1��  -10���� " �.���  �@J�Opacity$ 1Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� ! T� X$! 0Q�" T� Y$" 0Q� ��  �� *IsItemSelected+ True �� #��[ � $ 1��$��1��$��
0,-13,0,13q�.+��� $ 
Horizontal=�$��Center=�$��Center=�.+��� $
��Name��$��40(�#��[ �� $��Stretch=�  SearchBarSCB#��[ $��2q�  SearchBarBorderSCB#��[ # �� CornerRadius$# 4��$��15,8,0,0q�$��40��$ � FocusVisualStyle$ Z� ._��� $ 
Horizontal=�$��Stretch=�$��Center=�$ Z� .+�� $��4,0,4,0q�$��Center=�.+��� -   % ��Name$% HighlightText�� 	 SearchTextHeaderSCB#��[	 $ 0��$��20(�$��SemiBold"�$|�False������ " ��.���  � y�$ 	Collapsed=� ��  �� *NewFileName+  .a���  � y�$ Visible=�*  $Microsoft.Xaml.Behaviors.Interaction&   Triggers& +  %Microsoft.Xaml.Behaviors.EventTrigger  '   	EventName$' Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase(   Actions.( 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  )   Command) �� *
LoadedText+*   CommandParameter* �� + �� ElementName$+ HighlightText���� -   , ��Name$, UserSearchBox�� 
 MainBlankSCB#��[
 $��20(�$
��Bold"�$	��400��$ 1��$��0q����� *NewFileName+- �� UpdateSourceTrigger$- PropertyChanged=�$ Z� .��&   $' TextChanged��.(   ) �� *FileNameChanged+.���� $
��Left=�$��	355,0,0,0q�. J� Opacity$. 0Q�$��30�����#FF4242$# 5��/ J� RenderTransformOrigin$/ 0.5,0.5&����� $+ FileWarningMessage��0 �� Path$0 ActualWidth�z�T� $" 30Q�._��� -   $% FileWarningMessage��#��[ $��15(����#FFFFFF$��5,5,0,0q����� *FileWarningMessage+���� " �� ��  �� *CanShowFileWarningMessage+ True �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.3D�W� $,
 &RenderTransform.(TranslateTransform.Y)� 43$ 0:0:0.3D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.3D�W� $,
 &RenderTransform.(TranslateTransform.Y)� 30$ 0:0:0.3D����� " �.���  �@J�Opacity$ 0Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $	! 350Q�$" 0Q� ��  �� *IsItemSelected+ True �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.8D� 0:0:0.5W� $,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.8D� 0:0:0.5� $��2��$��0,10,0,0q�1 � ColumnDefinitions1 �� �� �� .+��� $��0��$��	25,0,25,0q����#C5C5C5$# 15��._�� $��0,10,0,0q� �� $
��auto ��� $	��.6* ��� $	��.2* ��� $	��.2* �.+��� $��4�� " SelectionNewFileCreationButton#��[ $��	0,-10,0,0q�$ 1��#��[  ���� *SelectDataType+�� $��0��$��Classic Script Format��#��[ $��Center=��� $��1��$��/Resource/MKC_Large.png��$��
10,5,10,10q��� $��2��$1��+A Linear Story Formatted Like A Screenplay.��#��[ $��	20,0,20,0q�2 �� TextAlignment$2 Center=�$
��Wrap=�$��Center=��� $��3��$ 2��$# 15��$��0,20,0,0q����#A4A4A4._�� 1 �� �� .+��� $��0��$��Shortcut��#��[ $��Center=�$
��Left=�$��10,0,0,0q��� $��1��  NewFileCreationButton#��[ �� $��1��$��Ctrl + Q��#��[ $��Center=�$��Center=�3 J� IsHitTestVisible$3 False���� $��4��$��5q� ! NewFileProcessSelectedTypeSCB#��[ $# 15��$��-10,-20,-10,-10q����� " ��.���  �@J�Opacity$	 0.0Q� �� # [   �� *CurrentDatatype+ �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.2D����� " ��.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $! 0Q�$" 0Q� �� # [   �� *CurrentDatatype+ �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)�	 -10.0$ 0:0:0.3D� �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)� 0.0$ 0:0:0.3D��� $��1��$��	25,0,25,0q����#C5C5C5$# 15��._�� $��0,10,0,0q� �� $
��auto ��� $	��.6* ��� $	��.2* ��� $	��.2* �.+��� $��4��#��[ $��	0,-10,0,0q�$ 1��#��[ ���� *SelectDataType+�� $��0��$��Branching Nodes��#��[ $��Center=��� $��1��$��/Resource/BN_Large.png��$��
10,5,10,10q��� $��2��$,��&A Branching Story With Player Choices.��#��[ $��	20,0,20,0q�$2 Center=�$
��Wrap=�$��Center=��� $��3��$ 2��$# 15��$��0,20,0,0q����#A4A4A4._�� 1 �� �� .+��� $��0��$��Shortcut��#��[ $��Center=�$
��Left=�$��10,0,0,0q��� $��1��#��[ �� $��1��$��Ctrl + W��#��[ $��Center=�$��Center=�$3 False���� $��4��$��5q�#��[ $# 15��$��-10,-20,-10,-10q����� " ��.���  �@J�Opacity$	 0.0Q� �� # [  �� *CurrentDatatype+ �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.2D����� " ��.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $! 0Q�$" 0Q� �� # [  �� *CurrentDatatype+ �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)�	 -10.0$ 0:0:0.3D� �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)� 0.0$ 0:0:0.3D��� $��2��$��	25,0,25,0q����#C5C5C5$# 15��._�� $��0,10,0,0q� �� $
��auto ��� $	��.6* ��� $	��.2* ��� $	��.2* �.+��� $��4��#��[ $��	0,-10,0,0q�$ 1��#��[ ���� *SelectDataType+�� $��0��$��Miscellaneous��#��[ $��Center=��� $��1��$��/Resource/MKM_Large.png��$��
10,5,10,10q��� $��2��$.��(A plaintext that can be used on objects.��#��[ $��	20,0,20,0q�$2 Center=�$
��Wrap=�$��Center=��� $��3��$ 2��$# 15��$��0,20,0,0q����#A4A4A4._�� 1 �� �� .+��� $��0��$��Shortcut��#��[ $��Center=�$
��Left=�$��10,0,0,0q��� $��1��#��[ �� $��1��$��Ctrl + E��#��[ $��Center=�$��Center=�$3 False���� $��4��$��5q�#��[ $# 15��$��-10,-20,-10,-10q����� " ��.���  �@J�Opacity$	 0.0Q� �� # [  �� *CurrentDatatype+ �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.2D����� " ��.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $! 0Q�$" 0Q� �� # [  �� *CurrentDatatype+ �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)�	 -10.0$ 0:0:0.3D� �� .���� .��W� $,
 &RenderTransform.(TranslateTransform.Y)� 0.0$ 0:0:0.3D�� $��3��.+�� $��Right=�$	��210��$��
0,20,30,20q�.+��� #��[ ���� *NewFileCreation+�� $��Start Writing!��#��[ $��Right=�$��Center=�$��0,0,20,0q�$3 False���� $$��/Resource/StartWritingIcon.png��$
��Left=�$��Center=�$��
10,9,10,11q�	 ��%�  '�       �� " �.���  �@J�Opacity$ 0Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $! 70Q�$" 0Q� ��  �� *CanCreateFile+ True �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.5D�W� $,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.5D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.5D�W� $,
 &RenderTransform.(TranslateTransform.X)� 70.0$ 0:0:0.5D��� -   4 ��Name$4 FolderHighlight��$ 2��$'��!/Resource/DirectoryFolderOpen.png��$��42��$��42��$��3��$
��Left=�$��Center=�$��30,5,0,0q�$. 0Q�$3 False������ " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� ��  �� *CanShowDirectory+ True �� .���� .��W� $
 Opacity� 1.0$ 0:0:0.2D� �� .���� .��W� $
 Opacity� 0.0$ 0:0:0.2D�� $ 1��$��3��$
��Left=�$��Center=�$��30,5,0,0q�.+��� $��40��$��40������ *OpenDirectoryButton+.��� -   5 �Name$5 DirectoryPopup��6 � VerticalOffset$
6 -400��7 � HorizontalOffset$	7 100��8 � 	StaysOpen$
8 True��9 � AllowsTransparency$
9 True��$
��Fade=����� *CanShowDirectory+: � PlacementTarget: �� ; �� RelativeSource; �� < �� AncestorType
< Button.���� #��[ $	��700��$	��330��$# 15��$/ 0.5,0.5&�z�T� -   DirectoryBorderTranslate��$	! 0.0Q�$	" 0.0Q�._��  �� $��50 ��� $
��auto ��� .+�� $��0��.+��� $!��/Resource/HorusEyeBlack.png��$��40��$��40��$
��Left=�$	��Top=�$��	10,10,0,0q��� $��1�����#A5A5A5$��5q����#A5A5A5$# 15��$	��600��$��Center=�._�� .+��� $��Project Directory��#��[ $��Center=�$	��Top=�$��0,8,0,0q��� $ Vertical=�.+��� -	   $��50��$��50��$
��Left=����� *
PrevPage+���� " ��.���  � �� �#5E5E5E��  � �� �� = �� 
TargetType"= ��.R��� #��z��$# 15,0,0,0��._��� $4 	BackImage��$!��/Resource/LeftArrowGray.png��$��10q�$/ 0.5,0.5&�z�T� $! 0Q�$" 0Q�'  !System.Windows.VisualStateManager>   VisualStateGroups> %  System.Windows.VisualStateGroup  CommonStates��?   States.?    System.Windows.VisualState  
Normal��@   
Storyboard.@ �� .��W� A �� 
TargetName$A 	BackImage��$,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.2D��� $2
 ,(Control.Background).(SolidColorBrush.Color)� #5E5E5E$ 0:0:0.2D�  	MouseOver��.@ �� .��W� $A 	BackImage��$,
 &RenderTransform.(TranslateTransform.X)� -3.0$ 0:0:0.2D��� $2
 ,(Control.Background).(SolidColorBrush.Color)� #383838$ 0:0:0.2D�  Pressed��.@ �� .��W� $A 	BackImage��$,
 &RenderTransform.(TranslateTransform.X)� -3.0$ 0:0:0.2D��� $2
 ,(Control.Background).(SolidColorBrush.Color)� #383838$ 0:0:0.2D��� -
     NewFileDirectoryLayout#��[   NewFileDirectoryListItem#��[ $	��130��B �� SelectionMode$B Single=����� *Directories+C �� SelectedItemC �� *SelectedDirectory+&   $' MouseDoubleClick��.(   ) �� *
NextPage+� $��2��$	��Top=�$��0,15,0,0q�.+��� $ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��Center=�.+��� ���#FFFFFF���#A5A5A5$��2q�$# 10��$
��Left=�$��Center=�$	��500��$��40��$��10,0,0,0q�._�� .+�� -   $5 DirectoryMessageWarning��$ 2��$6 10��$7 0��$
8 True��$
9 True��$
��Fade=����� *CanShowWarningMessage+: �� ; �� 
< Border.��� $/ 0.5,0.5&�.+��� $��30�����#FF4242$# 5������ $+ WarningMessage��$0 ActualWidth�._��� -   $% WarningMessage��#��[ $��15(����#FFFFFF$��5,5,0,0q�F� ���#FF4242$��0����B� .��E� D E� 
StartPoint$D 40,40&�.���� E �� Point$E 40,50&��� $E 50,40&��� $E 40,40&��� $ 1��$
��Left=�$��40��{��� *IsLeftScrollButtonActive+���� *
ScrollLeft+���� $+ DirectoryNameScrollViewer������ " ��.���  � �� �� "= ��.R��� $# 7,0,0,7������ F �� 
StartPoint$F 0.0,0.5&�G �� EndPoint$G 1,0.5&�.��� H � Color$H #A5A5A5��� $H 	#33A5A5A5��I � Offset$I 1Q�._��� $4 	LeftArrow��$��/Resource/LeftArrow.png��$��5q�$/ 0.5,0.5&�z�T� $! 0Q�$" 0Q�>   CommonStates��.?   
Normal��.@ �� .��W� $A 	LeftArrow��$,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.2D�W� $
 Opacity� 1.0$ 0:0:0.2D�  	MouseOver��.@ �� .��W� $A 	LeftArrow��$,
 &RenderTransform.(TranslateTransform.X)� -3.0$ 0:0:0.2D�  Pressed��.@ �� .��W� $A 	LeftArrow��$,
 &RenderTransform.(TranslateTransform.X)� -3.0$ 0:0:0.2D�  Disabled��.@ �� .��W� $A 	LeftArrow��$,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.2D�W� $
 Opacity� 0.0$ 0:0:0.2D��� $ 1��$��Right=�$��40��{��� *IsRightScrollButtonActive+���� *ScrollRight+���� $+ DirectoryNameScrollViewer������ " ��.���  � �� �� "= ��.R��� $# 0,7,7,0������ $F 1.0,0.5&�$G 0.0,0.5&�.��� $H #A5A5A5��� $H 	#33A5A5A5��$I 1Q�._��� $4 
RightArrow��$��/Resource/RightArrow.png��$��5q�$/ 0.5,0.5&�z�T� $! 0Q�$" 0Q�>   CommonStates��.?   
Normal��.@ �� .��W� $A 
RightArrow��$,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.2D�W� $
 Opacity� 1.0$ 0:0:0.2D�  	MouseOver��.@ �� .��W� $A 
RightArrow��$,
 &RenderTransform.(TranslateTransform.X)� 3.0$ 0:0:0.2D�  Pressed��.@ �� .��W� $A 
RightArrow��$,
 &RenderTransform.(TranslateTransform.X)� 3.0$ 0:0:0.2D�  Disabled��.@ �� .��W� $A 
RightArrow��$,
 &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.2D�W� $
 Opacity� 0.0$ 0:0:0.2D�"  UDSH.MVVM.CustomScrollViewer  -   J  Name$J DirectoryNameScrollViewer��$��Disabled=�$��Hidden=�.���� $ 
Horizontal=�.+��� $)��#/Resource/DirectoryFolderNormal.png��$��30��$��30��$��Center=�$��Center=�$��8,0,0,0q�$3 False���� ���#A5A5A5$��Center=�$��3��$��30��K �� RadiusX$K 2��L �� RadiusY$L 2��$��7,0,0,0q��� #��[ $��18(�$��Center=�$��7,0,0,0q����� *DisplayDest+�� ���#393E46$��Center=�$# 5��$��0,0,20,0q�._�� .+��� 
������ $��Add New Directory(ies)��$��DemiBold"�$��18(����#DBAD2F$��10������ " ��.���  � y�$ 	Collapsed=� ��  �� *	InputDest+  .a���  � y�$ Visible=��� -   
������ $��0q�$��DemiBold"�$��18(����#DBAD2F$��10������ *	InputDest+$- PropertyChanged=�.��&   $' TextChanged��.(   ) �� *DirectoryTextChanged+.��&   $' ScrollChanged��.(   ) �� *DirectoryScrollChanged+* �� $+ DirectoryNameScrollViewer���� -   $��10,0,0,0q�$��38��$	��120������ *ConfirmButton+{��� *IsDirectoryButtonButtonActive+���� " ��.���  � �� �#FFFFFF��  � �� �� "= ��.R��� #��z��$# 5��._��� #��[ $��Confirm��$��Center=�$��Center=�>   CommonStates��.?   
Normal��.@ �� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)� #FFFFFF$ 0:0:0.2D�  	MouseOver��.@ �� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)� ! MainHoveredControlButtonColor# [ $ 0:0:0.2D�  Pressed��.@ �� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)�  MainPressedButtonColor# [ $ 0:0:0.2D�  Disabled��.@ �� .���� $2
 ,(Control.Background).(SolidColorBrush.Color)� #A5A5A5$ 0:0:0.2D��� $��Center=�#��[ $U��OUse “/” To Add Multiple Directories Or Right-Click Inside Project Directory�����#6A6A6A$��12(�$��0,5,0,0q�&   $' 
MouseEnter��.(   ) �� *MouseEnterDirectoryBorder+  $' 
MouseLeave��.(   ) �� *MouseLeaveDirectoryBorder+���� " ��.���  � ��
 ���� ��  � �� �� "= ��.R��� #��z��._�� .+��� $)��#/Resource/DirectoryFolderNormal.png��$��Center=�$��Center=�$��0,0,0,0q�$3 False���� $4 FolderHighlight��$,��&/Resource/DirectoryFolderHighlight.png��$��Center=�$��Center=�$��0,0,0,0q�$. 0Q�$3 False��>   CommonStates��.?   
Normal��.@ �� .��W� $A FolderHighlight��$
 Opacity� 0.0$ 0:0:0.3D�  	MouseOver��.@ �� .��W� $A FolderHighlight��$
 Opacity� 1.0$ 0:0:0.3D�  Pressed��  Disabled��&   $' Loaded��.(   ) �� *
LoadedGrid+* �� $+ WindowLayout��	 M W� From��%!  '�       �� " �.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $! 0Q�$" 0Q� :� $ Window.Loaded��.[��� .���� .��W� $
 Opacity�M 0.0 1.0$ 0:0:0.3D�W� $,
 &RenderTransform.(TranslateTransform.Y)�	M 100.0 0.0$ 0:0:0.3D�