   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  1clr-namespace:UDSH.ConvertersUDSH.Converters  /clr-namespace:UDSH.ViewModelUDSH.ViewModel  &     UDSH.View.SideContentUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:UDSH.View     .Convclr-namespace:UDSH.Converters     +VMclr-namespace:UDSH.ViewModel     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  -   $
|�True��  � 	Resources  %  UDSH.Converters.HeightConverter   HeightConverter��%   &	          .���  +� IsFocusScope$
 True�� � RowDefinitions �� �� $��50 ��� .+���  K� ZIndex$ 1��$��1��$
��Left=�$��10,0,0,0q�$ ��/Resource/CurrentFiles.png���� -    ��Name$ BorderCollision��$ 2��$��3��$	��200��$
��Left=�  HighlightCapture.Radius �� CornerRadius# [   TestThickness#��[   SideContentHighlight#��[  J� Opacity$	 0.0Q�  SideContentBar#��[ $
|�True�� � FocusVisualStyle Z� *  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger  	   	EventName$	 
MouseEnter��*  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *BorderMouseEnter+   CommandParameter ��  �� ElementName$ BorderCollision��  $	 
MouseLeave��.
    �� *BorderMouseLeave+ �� $ BorderCollision��  $	 MouseLeftButtonDown��.
      PassEventArgsToCommand$
 True�� �� *BorderMouseButtonDown+  $	 MouseLeftButtonUp��.
    �� *BorderMouseButtonUp+ �� $ BorderCollision��  $	 	MouseMove��.
   $
 True�� �� *BorderMouseMove+9� $��3��$ 2��$��	-22,0,0,0q�$��0q�$��LeftToRight=� 9� ExpandDirection$ Right=�$
��Left=� 9� 
IsExpanded �� *CanExpandSideContent+ Z� .��� -    �Name$ SideContentBackground������ *SideContentWidth+ �� UpdateSourceTrigger$ PropertyChanged=� Z�    $	 
MouseLeave��.
    �� *SideContentMouseLeave+ �� $ SideContentBackground��  $	 Loaded��.
    �� *SideContentLoad+ �� $ SideContentBackground��.+���   SideContentSCB#��[    $	 MouseLeftButtonDown��.
    �� **(SideContentBackgroundLeftMouseButtonDown+� $��Right=�$	��Top=�$��0,5,10,0q�$��30��$��30��.+�d�   
HoldButton#��[ ���� *PinSideContent+�� $��/Resource/Pin.png����  �� Orientation$ Vertical=� Z� .+��� $ 
Horizontal=�$��7,5,0,0q�.+��� $��/Resource/SideContent.png��$��40��$��40���� $��Monster��$
��Left=�$��Bottom=�$
��Bold"���   SideContentTitleBarSCB#��[ $��Stretch=�$��6,3,0,0q�$��3���� $��Stretch=�  SearchBarSCB#��[ $��2q� 	 SearchBarBorderSCB#��[	 $ 4��$��6,6,6,0q�$��40�� Z� ._��� $ 
Horizontal=�$��Stretch=�$��Center=� Z� .+��� $��/Resource/Search.png��$��7q�$ 0�� �� BitmapScalingMode$ HighQuality=�����  �� 
TargetType" �� �� Triggers ��  �� Binding �� *SearchGotFocused+ �� Value True N� EnterActions �� .���� .��W�  �� TargetProperty$ Opacity� W� From 1.0	 W� To 0.0 g� Duration$ 0:0:0.1D� N� ExitActions �� .���� .��W� $ Opacity� 1.0$ 0:0:0.1D�^�  ^� 
Conditions ��   �� Binding  �� *CanSearchBoxTextBeFocusable+! �� Value	! False��   �� *ResetSearchBox+! True��   �� *
SearchText+!   �� .���� .��W� $ Opacity� 0.0 1.0$ 0:0:0.1D� �� .���� .��W� $ Opacity� 0.0$ 0:0:0.1D�� $��4,0,0,0q�$��Center=�.+��� $��	Search...�� 
 SearchTextHeaderSCB#��[
 $ 0��$��18(�$��SemiBold"�$|�False������ " ��.��� " �� Property" � y�# �� Value$# 	Collapsed=� ��  �� *
SearchText+  .a��� " � y�$# Visible=��� -   $ ��Name$$ UserSearchBox��  MainBlankSCB#��[ $��18(�$
��Bold"�$ 1��$��0q����� *TextBoxWidth+���� *
SearchText+|��� *CanSearchBoxTextBeFocusable+ Z� .��   $	 GotFocus��.
    �� *SearchBoxFocus+  $	 TextChanged��.
    �� *SearchBoxTextChange+ �� $ UserSearchBox��  $	 MouseLeftButtonDown��.
    �� *SearchBoxLeftMouseButtonDown+ �� $ UserSearchBox��.��z�T� % T� X$% 0Q�& T� Y$& 0Q����� " � ��  �� *SearchGotFocused+ True �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0	 -35.0$ 0:0:0.1D� �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.1D�^�  ��   �� *CanSearchBoxTextBeFocusable+	! False��   �� *ResetSearchBox+! True��   �� *
SearchText+!   �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)�	 -35.0 0.0$ 0:0:0.1D� �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)�	 -35.0$ 0:0:0.1D�� .+�R� -   ' R�Name$' SideContentTreeView��$��5,5,5,0q�  SideContentTreeView#��[ ���� *Root.SubNodes+���� *ActualHeight+( �� RelativeSource( �� ) �� Mode$) FindAncestor=�* �� AncestorType* UserControl+ �� 	Converter#+ [  , �� ConverterParameter$	, 110������ - �� DataType   UDSH.ViewModel.Node"-  . �� ItemsSource. �� *
SubNodes+.R��� $ 
Horizontal=�$��5q�.+��� $��33��$��33��$��0,-3,0,3q����� *	NodeImage+��   DefaultText#��[ $��	-10,0,0,0q����� *Name+   $	 SelectedItemChanged��.
   $
 True�� �� *TreeViewSelectionChanged+  $	 MouseDoubleClick��.
    �� *TreeViewMouseDoubleClick+R� -   $' SideContentTreeViewFILES��$��5,5,5,0q�  Search_SideContentTreeView#��[ ���� *
RootSearch+/ J� IsHitTestVisible/ �� *CanChooseFromSearch+���� *ActualHeight+( �� $) FindAncestor=�* UserControl#+ [  $	, 110������ "-  . �� *
SubNodes+.R��� $ 
Horizontal=�$��5q�.+��� $��33��$��33��$��0,-3,0,3q����� *	NodeImage+�� #��[ $��	-10,0,0,0q����� *Name+   $	 SelectedItemChanged��.
   $
 True�� �� *TreeViewSelectionChanged+  $	 MouseDoubleClick��.
    �� *TreeViewMouseDoubleClick+�� $��3��$ 3��$
��Left=�$��Bottom=����� *SideContentWidth+