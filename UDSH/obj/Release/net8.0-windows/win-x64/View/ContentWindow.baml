   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  1clr-namespace:UDSH.ConvertersUDSH.Converters  /clr-namespace:UDSH.ViewModelUDSH.ViewModel      UDSH.View.ContentWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:UDSH.View     .Convclr-namespace:UDSH.Converters     +VMclr-namespace:UDSH.ViewModel     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors    � Title$  ContentWindow��$	��450��$
��1000�� � WindowStartupLocation$ CenterScreen=� � 	Resources %  UDSH.Converters.HeightConverter   HeightConverter��%   &	          .��� -    �Name$ GridContent��$
|�True������ *GridContentMargin+ � RowDefinitions �� $
��auto ��� �� $��70 �.+��� -    ��Name$ 	NavBorder��$��0�����#C5C5C5$��40��$
��Left=�$��Center=� �� CornerRadius$ 4��$��	250,0,0,0q�._���  �� Orientation$ 
Horizontal=�.+���   ContentBackButton#��[ ���� *
BackButton+{��� *IsNormalState+�� $$��/Resource/HorusEyeDarkGrey.png��$��	15,3,15,3q���   ContentNextButton#��[ ���� *ReturnButton+{��� *IsNormalState+�� $��0��$ 
Horizontal=�$��Right=�$��Center=�$��0,0,10,0q�.+��� -    ��Name$ AddComboBox��  AddComboBox#��[ $��0,0,10,0q�	 �� SelectedIndex	 �� *SelectedAddIndex+{��� *IsNormalState+.@��� .���� $ 
Horizontal=�.+��� $!��/Resource/FolderContent.png��$��15��$��15����   DefaultText#��[ $��Folder��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKB File��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKC File��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKM File��$��16(�$��5,0,0,0q�*  $Microsoft.Xaml.Behaviors.Interaction
   Triggers
 +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ SelectionChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *AddSelectionChanged+��   FilterComboBox#��[ $��0,0,10,0q�	 �� *SelectedFilterIndex+{��� *IsNormalState+.@��� .���� $ 
Horizontal=�.+��� $��/Resource/None.png��$��15��$��15���� #��[ $
��None��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKB Only��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKC Only��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/FileType.png��$��15��$��15���� #��[ $��MKM Only��$��16(�$��5,0,0,0q�
   $ SelectionChanged��.    �� *FilterSelectionChanged+��   SortComboBox#��[ $��0,0,10,0q�	 �� *SelectedSortIndex+{��� *IsNormalState+.@��� .���� $ 
Horizontal=�.+��� $��/Resource/AToZ.png��$��15��$��15���� #��[ $��(A-Z) Files First��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/ZToA.png��$��15��$��15���� #��[ $��(Z-A) Files First��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/AToZ.png��$��15��$��15���� #��[ $��(A-Z) Folders First��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/ZToA.png��$��15��$��15���� #��[ $��(Z-A) Folders First��$��16(�$��5,0,0,0q�
   $ SelectionChanged��.    �� *SortSelectionChanged+��   ViewComboBox#��[ $��0,0,20,0q�	 �� *SelectedViewIndex+{��� *IsNormalState+.@��� .���� $ 
Horizontal=�.+��� $��/Resource/Medium.png��$��15��$��15���� #��[ $��Large Icons��$��16(�$��5,0,0,0q��� .���� $ 
Horizontal=�.+��� $��/Resource/Small.png��$��15��$��15���� #��[ $��Details��$��16(�$��5,0,0,0q�
   $ SelectionChanged��.    �� *ViewSelectionChanged+�� $��Stretch=�  SearchBarSCB#��[ $��2q� 	 SearchBarBorderSCB#��[	 $ 4��$��40��$��40�� � FocusVisualStyle Z� {��� *IsNormalState+._��� $ 
Horizontal=�$��Stretch=�$��Center=� Z� .+��� $��/Resource/Search.png��$��7q� K� ZIndex$ 0�� J� IsHitTestVisible$ False�� �� BitmapScalingMode$ HighQuality=�����  �� 
TargetType" �� �� Triggers � $��4,0,0,0q�$��Center=�.+��� $��	Search...�� 
 SearchTextHeaderSCB#��[
 $ 0��$��18(�$��SemiBold"�$|�False������ " �� �� BasedOn# [ .���  �� Property � y� �� Value$ 	Collapsed=� ��  �� Binding �� *
SearchText+ �� Value  .a���  � y�$ Visible=��� -    ��Name$ UserSearchBox��  MainBlankSCB#��[ $��18(�$��DemiBold"�$	��350��$ 1��$��0q����� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=� Z� .��
   $ GotFocus��.    �� *SearchBoxGotFocus+   CommandParameter ��  �� ElementName$ UserSearchBox��.������ " �� ��  �� *IsSearchBoxBorderOpen+ True N� EnterActions �� .���� .��W�  �� TargetProperty$ Width�	 W� To 400  g� Duration$  0:0:0.5D�! N� ExitActions! �� .���� .��W� $ Width� 40$  0:0:0.5D�
   $ GotFocus��.    �� *SearchBoxFocus+  $ 
MouseEnter��.    �� *SearchBoxMouseEnter+  $ 
MouseLeave��.    �� *SearchBoxMouseLeave+� -   $ ListGrid��$��1��$��0,5,10,0q�" � ColumnDefinitions" �� $	��200 �$��20���� $��40 ��� .+�R� -   # R�Name$# SideContentTreeView��$��5,-5,5,0q�  SideContentTreeView#��[ ���� *Root.SubNodes+{��� *IsNormalState+���� *ActualHeight+$ �� RelativeSource$ �� % �� Mode$% FindAncestor=�& �� AncestorType& UserControl' �� 	Converter#' [  ( �� ConverterParameter$	( 110������ ) �� DataType   UDSH.ViewModel.Node")  * �� ItemsSource* �� *
SubNodes+.R��� $ 
Horizontal=�$��5q�.+��� $��16��$��16��$��0,0,0,0q����� *	NodeImage+�� #��[ $��16(�$��5,0,0,0q����� *Name+
   $ SelectedItemChanged��.   +   PassEventArgsToCommand$
+ True�� �� *TreeViewSelectionChanged+  $ MouseDoubleClick��.    �� *TreeViewMouseDoubleClick+�� -   $��1��$��40��$��0,0,0,0q�$��Center=����� " ��.���  � �� �#C5C5C5��  � �� �� , �� 
TargetType", ��.R��� $ 2��- J� SnapsToDevicePixels#- z- ._��  �� �� $
��auto ��� .+��� $ UpperBorder��$��0�����#C5C5C5$��10��$ 2��#- z- �� $��1��$��/Resource/HandHandle.png��$��40��$��40���� . ��Name$. ImageMouseOver��$��1��$'��!/Resource/HandHandleHighlight.png��$��40��$��40��/ J� Opacity$/ 0Q��� $��1�����#C5C5C5$/ 0Q�$��40��$ 2��#- z- �� $ BottomBorder��$��2�����#C5C5C5$��10��$ 2��#- z- 0 �� Triggers0 P� 1 P� Property1 �@J�IsMouseOver2 P� Value$
2 True�� �� .���� .���� 3 �� 
TargetName$3 UpperBorder��$2 ,(Control.Background).(SolidColorBrush.Color)�	4 �� To4 #2D2D2D$  0:0:0.2D��� $3 BottomBorder��$2 ,(Control.Background).(SolidColorBrush.Color)�4 #2D2D2D$  0:0:0.2D�W� $3 ImageMouseOver��$ Opacity� 1$  0:0:0.2D�! �� .���� .���� $3 UpperBorder��$2 ,(Control.Background).(SolidColorBrush.Color)�4 #C5C5C5$  0:0:0.2D��� $3 BottomBorder��$2 ,(Control.Background).(SolidColorBrush.Color)�4 #C5C5C5$  0:0:0.2D�W� $3 ImageMouseOver��$ Opacity� 0$  0:0:0.2D�� $��2��$��10,0,0,0q� �� $
��auto ��� .+�� $��0��.+��� $��5��$	��Top=�$ 2������ 5 �� 
StartPoint$5 0,0.5&�6 �� EndPoint$6 1,0.5&�.��� 7 � Color$7 	#FFC5C5C5��� $7 	#FFC5C5C5��8 � Offset$8 1Q�� $7 	#FF5F5F5F��$	8 0.5Q����� " �� ��  �� *IsContentLoading+ True �� 9 �� BeginStoryboardName$9 RenameAnimation���� $9 CanceledAnimation���� : ��Name$: LoadingAnimation��.���� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�4 #DBAD2F$  0:0:1D�; g� AutoReverse$
; True��< g� RepeatBehavior$< Forever���� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�4 #DBAD2F$  0:0:1D��� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�4 #DBAD2F$  0:0:1D�$
; True��$< Forever��! �� .���� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�4 	#FFC5C5C5$  0:0:1D��� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�4 	#FF5F5F5F$  0:0:1D��� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�4 	#FFC5C5C5$  0:0:1D���  �� *IsContentRenameProcess+ True �� $9 LoadingAnimation���� $9 CanceledAnimation���� $: RenameAnimation��.���� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�4 #2FA7DB$  0:0:1D�$
; True��$< Forever���� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�4 #2FA7DB$  0:0:1D��� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�4 #2FA7DB$  0:0:1D�$
; True��$< Forever����  �� *IsContentCanceledProcess+ True �� $9 LoadingAnimation���� $9 RenameAnimation���� $: CanceledAnimation��.���� .���� $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�4 #F83030$  0:0:1D�$
; True��$< 2x���� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�4 #F83030$  0:0:1D��� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�4 #F83030$  0:0:1D�$
; True��$< 2x���� $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�4 	#FFC5C5C5$  0:0:1D�= g� 	BeginTime	= 0:0:3�� $N H(Panel.Background).(GradientBrush.GradientStops)[2].(GradientStop.Color)�4 	#FF5F5F5F$  0:0:1D�	= 0:0:3�� $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�4 	#FFC5C5C5$  0:0:1D�	= 0:0:3� $��1��.+��� -   > J� 	AllowDrop$
> True��  ContentDirectoryLayout#��[   ContentDirectoryListItem#��[ / �� *DetailsListViewOpacity+ �� *DetailsListViewHitTest+{��� *IsNormalState+���� *CurrentFiles+? �� SelectedItem? �� *SelectedItem+@ �� View@ ��    Content_GridViewColumnHeader!A �� ColumnHeaderContainerStyle#A [ .P��� $
��Name��B �� WidthB �� *	ColWidth1+C �� Mode$C TwoWay=�D �� CellTemplateD �� .R�� " �� $
��auto ��� $��* �.+��� $��0��$��20��$��20��$
��Left=�$��Center=����� *Image+���� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False����  �@J�Opacity$ 1Q� ��  �� $ �� & ListViewItemE �� Path$E 
IsSelected� True.a���  �@J�Opacity$ 0Q��� $��0��$"��/Resource/HorusEyeOrange.png��$��20��$��20��$
��Left=�$��Center=����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False����  �@J�Opacity$ 0Q� ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  �@J�Opacity$ 1Q��� $��1��$��/Resource/RenameState.png��$��25��$��7,0,0,0q�$
��Left=�$��Center=����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False����  �@J�Opacity$ 0Q� ^� F ^� 
ConditionsF �� G �� BindingG �� $ �� & ListViewItem$E 
IsSelected�H �� ValueH True�� G �� *DataContext.OpenRenameTextBox+$ �� & ListViewH True �� .���� .��W� $ Opacity� 1$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� $��1��$��18(�$��7,0,0,0q�$
��Left=�$��Center=����� *Name+���� " ��# [ .���  � �� �#000000��  �@J�Opacity$ 1Q� ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  � �� �#DBAD2F �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 5.0$  0:0:0.3D�= 0:0:0.3! �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$  0:0:0.3D�^� F �� G �� $ �� & ListViewItem$E 
IsSelected�H True�� G �� *DataContext.OpenRenameTextBox+$ �� & ListViewH True �� .���� .��W� $ Opacity� 0$  0:0:0.3D�! �� .���� .��W� $ Opacity� 1$  0:0:0.3D��� $��Last Date Modification��B �� *	ColWidth2+$C TwoWay=�D �� .R��� I �� TextAlignment$I Right=�$��18(�$��0,0,0,0q�$
��Left=�$��Center=����� *LastDateModification+���� " ��# [ .���  � �� �#000000 ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  � �� �#DBAD2F�� $
��Type��B �� *	ColWidth3+$C TwoWay=�D �� .R��� $��18(�$��0,0,0,0q�$
��Left=�$��Center=����� *Type+���� " ��# [ .���  � �� �#000000 ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  � �� �#DBAD2F�� $
��Size��B �� *	ColWidth4+$C TwoWay=�D �� .R��� $I Right=�$��18(�$��0,0,0,0q�$
��Left=�$��Center=����� *Size+���� " ��# [ .���  � �� �#000000 ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  � �� �#DBAD2F�� $��Author��B �� *	ColWidth5+$C TwoWay=�D �� .R��� $��18(�$��0,0,0,0q�$
��Left=�$��Center=����� *Author+���� " ��# [ .���  � �� �#000000 ��  �� $ �� & ListViewItem$E 
IsSelected� True.a���  � �� �#DBAD2FJ � ContextMenuJ ��   ContentItemProperties#��[ .@�m� $��Delete Permanently�����#F83030K m� CommandK �� *
DeleteItem+m� $��Rename��K �� *
RenameItem+m� $��
Properties��
   $ SelectionChanged��.   $
+ True�� �� *ListSelectionChanged+  $ MouseDoubleClick��.    �� *ListViewMouseDoubleClick+  $  PreviewMouseLeftButtonDown��.   $
+ True�� �� *!ListViewPrevMoustLeftButtonDown+  $ PreviewMouseLeftButtonUp��.   $
+ True�� �� *ListViewPrevMoustLeftButtonUp+  $ 	MouseMove��.   $
+ True�� �� *ListMouseMove+  $ DragOver��.   $
+ True�� �� *ListDragOver+  $
 Drop��.   $
+ True�� �� *
ListDrop+�� -	   $
> True�� % !ContentDirectoryLayout_LargeIcons#��[  + 'ContentDirectoryListViewItem_LargeIcons#��[ / �� *LargeListViewOpacity+ �� *LargeListViewHitTest+{��� *IsNormalState+���� *CurrentFiles+? �� *SelectedItem+J �� #��[ .@�m� $��Delete Permanently�����#F83030K �� *
DeleteItem+m� $��Rename��K �� *
RenameItem+m� $��
Properties��
   $ SelectionChanged��.   $
+ True�� �� *ListSelectionChanged+  $ MouseDoubleClick��.    �� *ListViewMouseDoubleClick+  $  PreviewMouseLeftButtonDown��.   $
+ True�� �� *!ListViewPrevMoustLeftButtonDown+  $ PreviewMouseLeftButtonUp��.   $
+ True�� �� *ListViewPrevMoustLeftButtonUp+  $ 	MouseMove��.   $
+ True�� �� *ListMouseMove+  $ DragOver��.   $
+ True�� �� *ListDragOver+  $
 Drop��.   $
+ True�� �� *
ListDrop+� $��2��.+��� -
   $ 	StatusBox�����#383838$
��Left=�$��Center=�$ 5��$��20,5,0,5q�$
|�True��$ 0�� Z� ._��� $ ��/Resource/ContentState.png��$	��100��$��15q����� " ��.���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 0$  0:0:0.3D�! �� .���� .��W� $ Opacity� 1	= 0:0:1$  0:0:0.3D���  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 0$  0:0:0.3D�! �� .���� .��W� $ Opacity� 1	= 0:0:1$  0:0:0.3D���  �� *IsContentDeleteProcess+ True �� .���� .��W� $ Opacity� 0$  0:0:0.3D�! �� .���� .��W� $ Opacity� 1	= 0:0:1$  0:0:0.3D�
   $ Loaded��.    �� *StatusBorderLoaded+ �� $ 	StatusBox���� ���#393E46$��60��$	��130��$
��Left=�$��Center=�$ 5��$��20,5,0,5q�$ 1��._��� $ 
Horizontal=�$��Center=�$��10,0,0,0q�.+��� $��Rename�����#DBAD2F���� " ��# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:1.2$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� ���#DEDEDE$��30��$ 4��$��
10,-5,0,-5q�._�� .+��� $��5,0,0,0q����� *RenameNewName+$ PropertyChanged=�{��� *OpenRenameTextBox+.��
   $ TextChanged��.    �� *NameTextBoxChanged+.������ " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True���� -  'System.Windows.Input.KeyboardNavigation �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop$
 true����  � ��$ 18(���  � ��$ DemiBold"���  � �� �� ", ��.R��� $��Border��
������ $��0q�$ 4��L �� Padding$L 2q�._��� M ��Name$M PART_ContentHost��$��0q�$��Center=�.���� $ False��$��Enter New Name...�����#666666$��18(�$��Center=�$��9,0,0,0q����� " ��# [ .���  � y�$ 	Collapsed=� ��  �� *RenameNewName+  .a���  � y�$ Visible=����� " ��.���  �@J�Opacity$ 0Q� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:1.3$  0:0:0.3D�W� $ Width� 500= 0:0:1.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D�W� $ Width� 30= 0:0:0.4$  0:0:0.6D��� $	��100��$��
20,-5,0,-5q����� *ConfirmRename+���� " ��  RenameItemConfirmButton# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:2.3$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� $	��100��$��10,-5,10,-5q����� *CancelRename+���� " ��  RenameItemCancelButton# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:2.5$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D����� " ��.���  �@J�Opacity$ 0Q� ��  �� *OpenRenameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:0.3$  0:0:0.3D�W� $ Width� 833= 0:0:0.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0= 0:0:0.8$  0:0:0.3D�W� $ Width� 130= 0:0:0.2$  0:0:0.6D��� ���#F83030$��60��$	��130��$
��Left=�$��Center=�$ 5��$��20,5,0,5q�$ 1��._�� .+��� $��Center=�$��10,0,0,0q����#FFFFFF���� *DirectoryWrongInputMessage+���� " ��# [ .���  �@J�Opacity$ 0Q� ��  �� *ShowWarningMessage+ True �� .���� .��W� $ Opacity� 1= 0:0:1.2$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D����� " ��.���  �@J�Opacity$ 0Q� ��  �� *ShowWarningMessage+ True �� .���� .��W� $ Opacity� 1= 0:0:0.3$  0:0:0.3D�W� $ Width� 510= 0:0:0.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0= 0:0:0.8$  0:0:0.3D�W� $ Width� 130= 0:0:0.2$  0:0:0.6D��� ���#393E46$��60��$	��130��$
��Left=�$��Center=�$ 5��$��20,5,0,5q�$ 1��._��� $ 
Horizontal=�$��Center=�$��10,0,0,0q�.+��� $
��Name�����#DBAD2F���� " ��# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:1.2$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� ���#DEDEDE$��30��$ 4��$��
10,-5,0,-5q�._�� .+��� $��5,0,0,0q����� *ItemNewName+$ PropertyChanged=�{��� *OpenNameTextBox+.��
   $ TextChanged��.    �� *NameTextBoxChanged+.������ " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop$
 true����  � ��$ 18(���  � ��$ DemiBold"���  � �� �� ", ��.R��� $��Border��
������ $��0q�$ 4��$L 2q�._��� $M PART_ContentHost��$��0q�$��Center=�.���� $ False��$��Enter Name...�����#666666$��18(�$��Center=�$��9,0,0,0q����� " ��# [ .���  � y�$ 	Collapsed=� ��  �� *ItemNewName+  .a���  � y�$ Visible=����� " ��.���  �@J�Opacity$ 0Q� ��  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:1.3$  0:0:0.3D�W� $ Width� 500= 0:0:1.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D�W� $ Width� 30= 0:0:0.4$  0:0:0.6D��� $	��100��$��
20,-5,0,-5q����� *ConfirmNewItemName+���� " ��  NameItemConfirmButton# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:2.3$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� $	��100��$��10,-5,10,-5q����� *CancelNewItemName+���� " ��# [ .���  �@J�Opacity$ 0Q� ��  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:2.5$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D����� " ��.���  �@J�Opacity$ 0Q� ��  �� *OpenNameTextBox+ True �� .���� .��W� $ Opacity� 1= 0:0:0.3$  0:0:0.3D�W� $ Width� 833= 0:0:0.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0= 0:0:0.8$  0:0:0.3D�W� $ Width� 130= 0:0:0.2$  0:0:0.6D��� ���#393E46$��60��$	��130��$
��Left=�$��Center=�$ 5��$��20,5,0,5q�$ 1��._��� $ 
Horizontal=�$��Center=�$��10,0,0,0q�.+��� -   $ 
Horizontal=�.+��� #��[ $��Do You Want Item(s) �����#DBAD2F�� #��[ $��Deleted�����#F83030�� #��[ $��?�����#DBAD2F���� " ��.���  �@J�Opacity$ 0Q� ��  �� *IsContentDeleteProcess+ True �� .���� .��W� $ Opacity� 1= 0:0:1.2$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� $	��100��$��
20,-5,0,-5q����� *ConfirmDelete+���� " ��  DeleteConfirmButton# [ .���  �@J�Opacity$ 0Q� ��  �� *IsContentDeleteProcess+ True �� .���� .��W� $ Opacity� 1= 0:0:2.3$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D��� $	��100��$��10,-5,10,-5q����� *CancelDelete+���� " ��  DeleteCancelButton# [ .���  �@J�Opacity$ 0Q� ��  �� *IsContentDeleteProcess+ True �� .���� .��W� $ Opacity� 1= 0:0:2.5$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D����� " ��.���  �@J�Opacity$ 0Q� ��  �� *IsContentDeleteProcess+ True �� .���� .��W� $ Opacity� 1= 0:0:0.3$  0:0:0.3D�W� $ Width� 525= 0:0:0.6$  0:0:0.6D�! �� .���� .��W� $ Opacity� 0= 0:0:0.8$  0:0:0.3D�W� $ Width� 130= 0:0:0.2$  0:0:0.6D��� -   $ NameWarningMessage�����#F83030$
��Left=�$��Center=�$ 5��$��20,-100,0,0q�$
|�True��$ 0�� Z� ._��� $��Center=�$��5q����#FFFFFF���� *NameInputMessage+���� " ��.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � ��$ 0q���  �@J�Opacity$ 0Q���  � z� T� N T� X$N 0Q�O T� Y$O 30Q� ��  �� *ShowNameInputWarningMessage+ True �� .���� .��W� $ Opacity� 1$  0:0:0.3D�W� $, &RenderTransform.(TranslateTransform.Y)� 0$  0:0:0.3D�! �� .���� .��W� $ Opacity� 0$  0:0:0.3D�W� $, &RenderTransform.(TranslateTransform.Y)� 30$  0:0:0.3D����� " �.���  � �� �#1D1B20��  � �� " MainContentWindowBackgroundSCB# [ �� '  !System.Windows.Shell.WindowChrome �@ WindowChrome   P   UseAeroCaptionButtons$P False��Q   CaptionHeight$Q 70Q���  � �� �� ", �.R��� $ WindowBorder��#��z��#��z��._�� $ Layout��#��z�� �� $
��auto ��� $��* �.+�� $ WindowTitlePanel��$��70��
������ ���� *WindowChromeMargin+" �� $
��auto ��� $��* ��� $
��auto �.+��� $ 
Horizontal=�.+��� $!��/Resource/FolderContent.png��$��Center=�$��15,0,0,0q�$��30��$��30��R   IsHitTestVisibleInChrome$
R True���� #��[ $��Content��$��10,0,0,0q�$��Center=�$��Center=��� $ BorderContainer��-   $��1��$��	20,0,20,0q�$��Center=�$��40�����#C5C5C5$ 10��{��� *IsNormalState+._�� .+��� $ DirectoryTextBox��$��20(�$��DemiBold"�$��0,0,0,0q�$
R True������ *CurrentDirectory+$ PropertyChanged=�.������ " ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 True����  �@ TabNavigation$
 None=���  �@�FocusVisualStyle Z� ��  � ��$	 120����  � ��$ 20����  �@J�	AllowDrop$
 true����  � �� �� ", ��.R��� $��Border��$ 10��$L 2q�
������ $��0q�'	  !System.Windows.VisualStateManagerS 	  VisualStateGroupsS %
  System.Windows.VisualStateGroup
  CommonStates��T 
  States.T    System.Windows.VisualState  
Normal��  Disabled��U   
Storyboard.U �� .���� $3 Border��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�8  2System.Windows.Media.Animation.EasingColorKeyFrame  V �� KeyTime$V 0��W �� Value$W #C5C5C5��  ReadOnly��.U �� .���� $3 Border��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $V 0��$W #C5C5C5��  	MouseOver��._�� $��10,0,0,0q�$��Center=�" �� $
��auto ��� $��* �.+��� X ��Name$X ProjectName��$��0��#��[ ���� *ProjectName+�� $M PART_ContentHost��$��1��$��0,0,10,0q�.��Y J� InputBindingsY �� 
Z �� Key$Z Enter��[ �� Command[ �� *PressedEnter+.��
   $ Loaded��.    �� *DirectoryTextBoxLoaded+ �� $ DirectoryTextBox��  $ TextChanged��.    �� *DirectoryTextBoxTextChanged+.���� $ DirectoryHitCollision��$
R True��
������ $ 10�� �� *CanRegisterDragMode+
   $ MouseLeftButtonDown��.   $
+ True�� �� *DirectoryHitCollisionLMB+  $ MouseLeftButtonUp��.   $
+ True�� �� *DirectoryHitCollisionLMBUp+  $ 
MouseEnter��.    �� *!DirectoryHitCollisionMouseEnter+  $ 
MouseLeave��.    �� *!DirectoryHitCollisionMouseLeave+�� -   ���#393E46$
R True��$ False��$ 10��$/ 0Q�\ J� RenderTransformOrigin$\ 0.0,0.5&�._��� -   #��[ $��	DRAG MODE��$��10,0,0,0q�$
��Left=�$��Center=�$/ 0Q����#DBAD2F�� ] ��Name$] WindowCommandButtons��$��2��$ 
Horizontal=�$��0,0,10,0q�$��Right=�$��Stretch=�
������ $
R True��.+��� $|�False������ *FunctionBar+$ �� *TemplatedParent+$C OneWay=��� ^ ��Name$^ MinimizeButton��$��40��$��40��  Content_MinimizeButton#��[ ���� *MinimizeWindow+� $��40��$��40��.+��� $^ NormalButton��  Content_NormalButton#��[ y��� *CollapseResetButtonState+���� *NormalizeWindow+�� $^ 	MaxButton��  Content_MaxButton#��[ y��� *CollapseResizeButtonState+���� *MaximizeWindow+�� $^ CloseButton��$��40��$��40��  Content_CloseButton#��[ ���� *CloseWindow+�� $��1��_   	IsTabStop$_ False��.u��� ` ��Name$` MainContentPresenter��#��z��a   TabNavigation$a Cycle=��� b ��Name$b 
ResizeGrip��$��Right=�$��Bottom=�$��1��$��False��$y�Hidden=�c   ResizeGripDirection$c BottomRight=�