   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  %clr-namespace:UDSH.MVVM	UDSH.MVVM  1clr-namespace:UDSH.ConvertersUDSH.Converters  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   SelectedFile.Radius  �� TopLeft �� 
BottomLeft �� TopRight �� BottomRight  SideContentHighlight.Radius%   UDSH.Converters.HeightConverter  WidthConverter �� 
TargetType  OpenedPagesLayout �� Property �� Value � Orientation �� 
TargetType	 J� SnapsToDevicePixels"   UDSH.MVVM.CustomScrollViewer
  Name �� PanningMode  OpenFilesScrollViewer  OpenedPages  MainUnselectedButtonSCB �� Padding �� CornerRadius �� Orientation ��Name K� ZIndex J� Opacity  DefaultText  CloseOpenedPageButton �� RelativeSource �� AncestorType �� Triggers P� Property P� Value N� EnterActions �� 
TargetName �� TargetProperty	 W� To g� Duration N� ExitActions �� Triggers 	 MainSelectedButtonColor	 �� To 
 MainSelectedItemTextColor  MainUnselectedButtonColor  MainStandardTextColor  OpenedPagesListLayout  OpenFilesListScrollViewer  OpenedPagesListItem �� Path  �� 	Converter! �� ConverterParameter  NewFileDirectoryLayout  NewFileDirectoryListItem" ��Name'  !System.Windows.VisualStateManager#   VisualStateGroups%  System.Windows.VisualStateGroup$   States   System.Windows.VisualState%   
Storyboard  SideContentDirectoryLayout  SideContentScrollViewer   SideContentDirectoryListItem   Content_GridViewColumnHeader& ��Name' �� 
StartPoint( �� EndPoint) � Color* � Offset+ n�Name, � Cursor- ��Name. J� IsHitTestVisible/ �� 
TargetName  ContentDirectoryLayout  ContentScrollViewer0 � RowDefinitions1 �� AllowsColumnReorder2 �� Columns  TestContentScrollViewer3 �� 	Resources4 �� BasedOn  ContentDirectoryListItem % !ContentDirectoryLayout_LargeIcons + 'ContentDirectoryListViewItem_LargeIcons5 �Name6 �� Value7 �� Binding8 ^� 
Conditions9 �� Binding: �� Value  CSFContentLayout  MainBlankSCB  
CSFContent  SceneHeadingTemplate   
ScriptText; �� UpdateSourceTrigger ! HighlightScriptText " ActionTemplate # CharacterTemplate $ DialogueTemplate % BranchNodeTemplate & MainInputNodeSCB1  +UDSH.View.ActiveElementDataTemplateSelector ' AEDataSelector<   SceneHeadingTemplate=   ActionTemplate>   CharacterTemplate?   DialogueTemplate@   BranchNodeTemplate�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     !localclr-namespace:UDSH.View   MVVMclr-namespace:UDSH.MVVM  &Convclr-namespace:UDSH.Converters  %3  &	        &	 %     &	 P     &	 U     7  &	     7  7   7  7  7 	 7 
 7  7  &	 �    7  &	 ^	    7  7  7  7  7 	 7 
 7  7  &	 �    7  &	 u    7  7  &	 .    7  &	 �    7  7  &	 �    &	 w    7  7  7  &	 	!    &	 E#    7  &	 �$    7  7  &	 �+    7  &	 �,    7  7  &	 �-    7   7  7 ! 7  &	" k.    7   7  7 ! &	# T/    7   7  7 ! &	$ H0    7   7  7 ! &	% 
1    7 & 7   7  &	' �1    7  7 " 7 # 7 $ 7 % �� $  5Q�$ 0Q�$ 5Q�$ 0Q��� $  0Q�$ 0Q�$
 4000Q�$
 4000Q�   �� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ Hidden=���  � ��$ Disabled=���  � ��$
 True����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�� $ 
Horizontal=���  � �� �� " ��.R��� 
������ $��0q�$
	 True��._�  $
 
OpenScroll��$��Hidden=�$��Disabled=�$ HorizontalOnly=�#��z��$��0q�8��  .���� #	 z	 �� " ��.���  � ��8   ��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $��40��$��0q�$ 0q�#��z��8  ._��� $ 
Horizontal=�.+�� $��Center=�$��Center=�$��3q�.+��� $ ImageNormal��$ 0������ *fileImageNormal+�� $ ImageSelected��$ 1��$ 0Q����� *fileImageSelected+�� 8�� $��18(�$��Center=�$��Center=�$��3,5,5,5q����� *
FileName+� $��Center=�$��Center=�.+��� $ 0��8�� ���� *DataContext.CloseOpenedFile+ ��  ListView����  ��  ListViewItem�� $ 1��$��13,15,13,11q�$��/Resource/Close.png���� $ ImageCloseSelected��$ 2��$��13,15,13,11q�$!��/Resource/CloseSelected.png��$ 0Q� P�  �@��
IsSelected$
 True�� �� .���� .��W� $ ImageSelected��$ Opacity� 1.0$ 0:0:0.1D�W� $ ImageCloseSelected��$ Opacity� 1.0$ 0:0:0.1D� �� .���� .��W� $ ImageSelected��$ Opacity� 0.0$ 0:0:0.1D�W� $ ImageCloseSelected��$ Opacity� 0.0$ 0:0:0.1D� P�  �@��
IsSelected$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� $2 ,(Control.Foreground).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� $2 ,(Control.Foreground).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ Disabled=���  � ��$
 Auto=���  � ��$
 True����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�� $ Vertical=���  � �� �� " ��.R��� 
������ $��0q�$
	 True��._�  $
 OpenFilesListScrollviewer��$��Disabled=�$
��Auto=�$ VerticalOnly=�#��z��$��0q�8��  .���� #	 z	 �� " ��.���  � ��8   ��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $��40��$��0q�$ 5,0,20,0q�#��z��$ 5������  ��  ListView$ ActualWidth�._�� .+��� $ 
Horizontal=�.+�� $��Center=�$��Center=�$��3q�.+��� $ ImageNormal��$ 0������ *fileImageNormal+�� $ ImageSelected��$ 1��$ 0Q����� *fileImageSelected+�� 8�� $��CharacterEllipsis=�$��18(�$��Center=�$��Center=�$��3,5,5,5q����� *
FileName+����  ��  ListView$ ActualWidth�8   $! 95��� $��Center=�$��Right=�.+��� $ 0��8�� ���� *DataContext.CloseOpenedFile+ ��  ListView����  ��  ListViewItem�� $ 1��$��13,15,13,11q�$��/Resource/Close.png���� $ ImageCloseSelected��$ 2��$��13,15,13,11q�$!��/Resource/CloseSelected.png��$ 0Q� P�  �@��
IsSelected$
 True�� �� .���� .��W� $ ImageSelected��$ Opacity� 1.0$ 0:0:0.1D�W� $ ImageCloseSelected��$ Opacity� 1.0$ 0:0:0.1D� �� .���� .��W� $ ImageSelected��$ Opacity� 0.0$ 0:0:0.1D�W� $ ImageCloseSelected��$ Opacity� 0.0$ 0:0:0.1D� P�  �@��
IsSelected$
 True�� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� $2 ,(Control.Foreground).(SolidColorBrush.Color)�8  $ 0:0:0.1D� �� .���� .���� $2 ,(Control.Background).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� $2 ,(Control.Foreground).(SolidColorBrush.Color)�8  $ 0:0:0.1D��� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ Disabled=���  � ��$
 Auto=���  � ��$
 True����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�� $ Vertical=���  � �� �� " ��.R��� ���#FFFFFF$��0q�$
	 True��$ 5,5,0,5q�$ 	0,0,15,15��._�  $
 OpenFilesListScrollviewer��$��Disabled=�$��Visible=�$ VerticalOnly=�#��z��$��0q�8��  .���� #	 z	 �� " ��.���  � �� �#FFFFFF��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $��40��$��0q�$��0,0,0,0q�#��z��$ 5������  ��  ListView$ ActualWidth�8    $! 25��._��� $" 
FolderName��8�� ���#6A6A6A$��18(�$��Center=�$
��Left=�$��5,0,0,0q����� #   CommonStates��.$   
Normal��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #6A6A6A$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #FFFFFF$ 0:0:0.2D�  	MouseOver��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #6A6A6A$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #BFBFBF$ 0:0:0.2D�  Selected��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$
 Auto=���  � ��$
 Auto=���  � ��$ False����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�� $ Vertical=���  � �� �� " ��.R��� 
������ $��0q�$
	 True��$ 4q�$ 1��._�  $
 OpenFilesListScrollviewer��$
��Auto=�$
��Auto=�$
 Both=�#��z��$��0q�8��  .���� #	 z	 �� " ��.���  � ��
 ���� ��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $��40��$��0q�$��0,0,0,0q�#��z��$ 5������  ��  ListView$ ActualWidth�8    $! 25��._��� $ 
Horizontal=�$��5q�.+��� $��33��$��33��$��0,-3,0,3q����� *	NodeImage+�� 8�� $��	-10,0,0,0q����� *Name+#   CommonStates��.$   
Normal��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #6A6A6A$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #FFFFFF$ 0:0:0.2D�  	MouseOver��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #6A6A6A$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #BFBFBF$ 0:0:0.2D�  Selected��.% �� .���� $ 
FolderName��$( "Foreground.(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.2D��� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� " ��.���  � ��
 ���� ��  � �� �#000000��  � ��$ 18(���  � ��$ DemiBold"���  � ��$ 0,0,0,3q���  � �� �#C5C5C5��  � ��$
 Left=���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� .+��� #��z��#��z��#��z��._��� $��5,0,0,3q�#��z��#��z���� $& LinearBorder��$��Right=�$��0,0,2,3q�$��20������ $' 1,0.5&�$( 0,0.5&�.��� $) #EAEAEA��� $) 	#33EAEAEA��$* 1Q�n� $+ PART_HeaderGripper��$��4��$��Right=�
������ $, SizeWE��$��0,0,-1,0q�$y�Visible=�$��0q�$ 0Q��� $- GripperVisuals��$. False��$��3��$��Right=����#C5C5C5$��0,0,-1,0q�$y�Visible=� P�  � ��$ False��. ��� $/ GripperVisuals�� �@J�Opacity$ 0Q��� $/ LinearBorder�� �@J�Opacity$ 0Q��� $/ PART_HeaderGripper�� �@J�Opacity$ 0Q��� $/ PART_HeaderGripper�� �@J�IsHitTestVisible$ False���� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$
 Auto=���  � ��$
 Auto=���  � ��$ False����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�$� ��  � �� �� " ��.R�� .+�  $
 OuterContentScrollviewer��$��Hidden=�$��Disabled=�$
 Both=�#��z��$��0q�8��  .��� 
������ $
	 True��0 �� $
��auto ��� .+�� $��0��.+��� #	 z	 $1 False��2 �� *View.Columns+ �� *TemplatedParent+  8�� $��0,37,0,0q�$
��Auto=�$
��Auto=�#��z��.���� #	 z	 $ 1Q�$
��Left=�$��0,0,0,0q�3 ���� '��       " ��84  �� " ��.���  � ��
 ���� ��  �@�FocusVisualStyle Z� ��  � �� �� " ��.R��� $��40��$��0q�$��0,0,0,3q�$
��Left=�#��z��$ 5��._��� #	 z	 $
��Left=�$��Center=�#   CommonStates��.$   
Normal��.% �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� Transparent$ 0:0:0.2D�  	MouseOver��.% �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #A7A7A7$ 0:0:0.2D�  Selected��.% �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.2D��� " ��.���  � ��
 ���� ��  � ��$ 0q���  � ��$ Disabled=���  � ��$ Visible=���  � ��$ False����  �@%�
ScrollUnit$ Pixel=���  �@�FocusVisualStyle Z� ��  � �� �� .R�� $ 
Horizontal=���  � �� �� " ��.R��� 
������ $��0q�$
	 True��._�  $
 
OpenScroll��$��Disabled=�$��Visible=�$ HorizontalOnly=�#��z��$��0q�8��  .���� #	 z	 �� " ��.���  � ��8   ��  �@�FocusVisualStyle Z� ��  � |�$
 True����  � �� �� " ��.R��� $	��200��$	��200��$��0q�$ 5q�#��z��$ 5��$��10q�._��� $ Vertical=�$��0,10,0,0q�.+�� $5 IconGrid��$	��140��$	��150��$��Center=�.+��� $ NormalImage��$ 0������ *LargeNormalImage+�� $ HighlightImage��$ 1��$ 0Q����� *LargeHighlightImage+� $5 	TitleGrid��$��0,5,0,0q�.+��� $" Title��8�� ���#000000$��Center=����� *Name+�� $ RenameImage��$��/Resource/RenameState.png��$��30��$ 0Q�#   SelectionStates��.$   
Unselected��.% �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� Transparent$ 0:0:0.1D�W� $ HighlightImage��$ Opacity� 0.0$ 0:0:0.3D��� $ Title��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #000000$ 0:0:0.3D�  Selected��.% �� .���� $2 ,(Control.Background).(SolidColorBrush.Color)� #393E46$ 0:0:0.1D�W� $ HighlightImage��$ Opacity� 1.0$ 0:0:0.3D��� $ Title��$2 ,(Control.Foreground).(SolidColorBrush.Color)� #DBAD2F$ 0:0:0.3D� �� $6 Folder��7 �� *Type+.a��� $/ 	TitleGrid�� � ��$ 	0,-10,0,0q��� $/ IconGrid�� � ��$	 150���� $/ IconGrid�� � ��$	 150��^� 8 �� 9 �� $ 
IsSelected� �� *Self+: True�� 9 �� *DataContext.OpenRenameTextBox+ ��  ListView: True �� .���� .��W� $ RenameImage��$ Opacity� 1.0$ 0:0:0.3D�W� $ Title��$ Opacity� 0.0$ 0:0:0.3D� �� .���� .��W� $ RenameImage��$ Opacity� 0.0$ 0:0:0.3D�W� $ Title��$ Opacity� 1.0$ 0:0:0.3D��� " ��.���  � ��8   ��  � ��$ 0q���  � ��$ Disabled=���  � ��$ False����  � �� �� .R�� $ Vertical=���  � �� �� " ��.R��� #��z��$
	 True��._��� $|�False��#��z��.���� #	 z	 �� " ��.���  � ��8   ��  � ��$ 144,96,96,0q���  � �� �� " ��.R��� $��0q�$ 0q�8�� ._���  P�  �@J�IsKeyboardFocusWithin$
 True��. ���  �@��
IsSelected$
 True���� .R�� .+��� 8��  $ 1��$��16(�$
��Bold"�8��  �/� .T�J� .*�.*��� ��� *	Content+$; PropertyChanged=��� 8�� $ 0��$��19(�$��Scene Heading��
�������$��0q�$��Italic$�8�� �� .R�� $��Stretch=�.+��� 8��  $ 2��$��16(�8�� $��Stretch=� �/� .T�J� .*�.*��� ��� *	Content+$; PropertyChanged=��� 8�� $ 1��$��19(�$��Action��
�������$��0q�$��Italic$�$��Stretch=��� .R��� $ 
Horizontal=�$��355, 18, 96, 0q�.+�� $	��365��.+��� 8��  $ 2��$	��Top=�$
��Left=�$��16(�8�� �� 8�� $ 1��$	��Top=�$
��Left=�$��19(�$��	Character��
�������$��0q�$��Italic$��� $��50��$��50���� .R�� $��240, 6, 96, 0q�$	��480��.+��� 8��  $ 2��$	��Top=�$
��Left=�$��16(�8�� �� 8�� $ 1��$	��Top=�$
��Left=�$��19(�$��Dialogue��
�������$��0q�$��Italic$��� .R��� 8��  $��Center=�$��0q�$ 4��$	��365��._�� $��Center=�.+��� 8�� $ 2��$��Center=�$��16(�8�� �� $��Right=�  8<   8=  8>  8?  8@  