   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  5      1clr-namespace:UDSH.ConvertersUDSH.Converters  5      /clr-namespace:UDSH.ViewModelUDSH.ViewModel  5	      &     UDSH.View.SideContentUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:UDSH.View     5      .Convclr-namespace:UDSH.Converters     5      +VMclr-namespace:UDSH.ViewModel     5	      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5
      -   5        $
|�True��5        � 	Resources  5      %  UDSH.Converters.HeightConverter   HeightConverter��%   &	          5   
   6   5      .��5      �  +� IsFocusScope$
 True��6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   �� 5      .+�5   
   ��  K� ZIndex$ 1��6   $��1��6!   $
��Left=�6.   $��10,0,0,0q�6I   $ ��/Resource/CurrentFiles.png��6[   �� 5   
   -   5         ��Name$ BorderCollision��5      $ 2��6*   $��3��6;   $	��200��6L   $
��Left=�6X     HighlightCapture.Radius �� CornerRadius# [ 5        TestThickness#��[ 6I     SideContentHighlight#��[ 6z    J� Opacity$	 0.0Q�6�     SideContentBar#��[ 5      $
|�True��6?    � FocusVisualStyle 6P   Z� *  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5       +  %Microsoft.Xaml.Behaviors.EventTrigger  5!      	   	EventName$	 
MouseEnter��6!   *  $Microsoft.Xaml.Behaviors.TriggerBase
   Actions.
 5"      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6,   �� *BorderMouseEnter+   CommandParameter 6Q   ��  �� ElementName$ BorderCollision��5#        5%      $	 
MouseLeave��6!   .
 5&         6,   �� *BorderMouseLeave+ 6Q   �� $ BorderCollision��5'        5)      $	 MouseLeftButtonDown��6!   .
 5*           PassEventArgsToCommand$
 True��6V    6,   �� *BorderMouseButtonDown+6V   5+        5-      $	 MouseLeftButtonUp��6!   .
 5.         6,   �� *BorderMouseButtonUp+ 6T   �� $ BorderCollision��5/        51      $	 	MouseMove��6!   .
 52        $
 True��6P    6,   �� *BorderMouseMove+6P   53      55      9� 58   
   $��3��6   $ 2��6$   $��	-22,0,0,0q�65   $��0q�6H   $��LeftToRight=�6\    9� ExpandDirection$ Right=�6x   $
��Left=�6�    9� 
IsExpanded 59      �� *CanExpandSideContent+ 6?   Z� .��5;      � -   5         �Name$ SideContentBackground��5;      ��62   �� *SideContentWidth+ �� UpdateSourceTrigger$ PropertyChanged=� 6z   Z�  5<        5=      $	 
MouseLeave��6%   .
 5>         60   �� *SideContentMouseLeave+ 6Z   �� $ SideContentBackground��5?        5A      $	 Loaded��6%   .
 5B         60   �� *SideContentLoad+ 6T   �� $ SideContentBackground��5C      .+�5G      ��   SideContentSCB#��[ 6    5H        5I      $	 MouseLeftButtonDown��6)   .
 5J         64   �� **(SideContentBackgroundLeftMouseButtonDown+5K      5M      � 5P      $��Right=�6   $	��Top=�63   $��0,5,10,0q�6K   $��30��6]   $��30��6h   .+�5Q      d�   
HoldButton#��[ 6F   ��6#   �� *PinSideContent+6F   �� 5R      $��/Resource/Pin.png��6   5S      �� 5V       �� Orientation$ Vertical=�6    64   Z� .+�5X      �� $ 
Horizontal=�6!   $��7,5,0,0q�6:   .+�5Y      �� $��/Resource/SideContent.png��6    $��40��6C   $��40��6N   �� 5Z      $��Monster��6$   $
��Left=�63   $��Bottom=�6N   $
��Bold"�6i   5[      �� 5^        SideContentTitleBarSCB#��[ 6    $��Stretch=�6O   $��6,3,0,0q�6m   $��3��6~   �� 5a      $��Stretch=�6     SearchBarSCB#��[ 6;   $��2q�6f    	 SearchBarBorderSCB#��[	 6z   $ 4��5b      $��6,6,6,0q�6.   $��40��6?    6K   Z� ._�5d      �� $ 
Horizontal=�6%   $��Stretch=�6>   $��Center=�6\    6w   Z� .+�5f      �� $��/Resource/Search.png��6$   $��7q�6B   $ 0��6M    �� BitmapScalingMode$ HighQuality=�6^   ��5g   "   �� 5h   &    �� 
TargetType" ��6,    �� Triggers 5i   *   �� 5j   .    �� Binding 6:   �� *SearchGotFocused+ �� Value True6_    N� EnterActions 5k   2   �� 5l   6   .��5m   :   �� .��5n   >   W�  �� TargetProperty$ Opacity�6N    W� From 1.05o   N   	 W� To 0.05p   N    g� Duration$ 0:0:0.1D�5q   N   5r   ;   5s   7    N� ExitActions 5u   2   �� 5v   6   .��5w   :   �� .��5x   >   W� $ Opacity�6N    1.05y   N   $ 0:0:0.1D�5z   N   5{   ;   5|   7   5~   /   ^� 5�   .    ^� 
Conditions 5�   2   �� 5�   6     �� Binding  6@   �� *CanSearchBoxTextBeFocusable+! �� Value	! False6p   �� 5�   6     6@   �� *ResetSearchBox+! True6c   �� 5�   6     6@   �� *
SearchText+!  6_    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $ Opacity�6N    0.05�   N    1.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $ Opacity�6N    0.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7   5�   /   5�   '   5�      � 5�      $��4,0,0,0q�6#   $��Center=�64   .+�5�   "   �� $��	Search...��6,    
 SearchTextHeaderSCB#��[
 6=   $ 0��6o   $��18(�6�   $��SemiBold"�6�   $|�False��6�   ��5�   &   �� 5�   *   " ��60   .�5�   .   �� " �� Property" � y�# �� Value$# 	Collapsed=�6K    5�   .   �� 5�   2    6>   �� *
SearchText+  6]   .a�5�   6   �� " � y�$# Visible=�6S   5�   3   5�   +   5�   #   �� 5�   "   -   5        $ ��Name$$ UserSearchBox��5�   *     MainBlankSCB#��[ 6A   $��18(�6l   $
��Bold"�6z   $ 1��5�   F   $��0q�6�   ��5�   �   �� *TextBoxWidth+��5�   *   �� *
SearchText+|�6W   �� *CanSearchBoxTextBeFocusable+ 6�   Z� .��6�    5�   &     5�   *   $	 GotFocus��69   .
 5�   .      6D   �� *SearchBoxFocus+5�   +     5�   *   $	 TextChanged��69   .
 5�   .      6D   �� *SearchBoxTextChange+ 6l   �� $ UserSearchBox��5�   +     5�   *   $	 MouseLeftButtonDown��69   .
 5�   .      6D   �� *SearchBoxLeftMouseButtonDown+ 6u   �� $ UserSearchBox��5�   +   .��5�   >   5�   #   z�5�   "   T� 5�   &   % T� X$% 0Q�69   & T� Y$& 0Q�6?   ��5�   "   �� 5�   &   " �6,    5�   *   �� 5�   .    6:   �� *SearchGotFocused+ True6_    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $, &RenderTransform.(TranslateTransform.X)�6N    0.05�   N   	 -35.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $, &RenderTransform.(TranslateTransform.X)�6N    0.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7   5�   /   ^� 5�   .    5�   2   �� 5�   6     6@   �� *CanSearchBoxTextBeFocusable+	! False6p   �� 5�   6     6@   �� *ResetSearchBox+! True6c   �� 5�   6     6@   �� *
SearchText+!  6_    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $, &RenderTransform.(TranslateTransform.X)�6N   	 -35.05�   N    0.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7    5�   2   �� 5�   6   .��5�   :   �� .��5�   >   W� $, &RenderTransform.(TranslateTransform.X)�6N   	 -35.05�   N   $ 0:0:0.1D�5�   N   5�   ;   5�   7   5�   /   5�   '   5�      5�      5�      � 5�      .+�5�      R� -   5        ' R�Name$' SideContentTreeView��5�   #   $��5,5,5,0q�6f     SideContentTreeView#��[ 6w   ��6@   �� *Root.SubNodes+��5   #   �� *ActualHeight+( �� RelativeSource( �� ) �� Mode$) FindAncestor=�* �� AncestorType* UserControl+ �� 	Converter#+ [  , �� ConverterParameter$	, 110����5     �� 5  "   - �� DataType   UDSH.ViewModel.Node"-  6;   . �� ItemsSource. 6W   �� *
SubNodes+.R�5  &   �� $ 
Horizontal=�61   $��5q�6J   .+�5  *   �� $��33��6M   $��33��6X   $��0,-3,0,3q�6d   ��60   �� *	NodeImage+6d   �� 5  *     DefaultText#��[ 64   $��	-10,0,0,0q�6o   ��6Y   �� *Name+6o   5  '   5  #    5
       5  "   $	 SelectedItemChanged��61   .
 5  &     $
 True��6i    6<   �� *TreeViewSelectionChanged+6i   5  #     5  "   $	 MouseDoubleClick��61   .
 5  &      6<   �� *TreeViewMouseDoubleClick+5  #   5     R� 5     -   5        $' SideContentTreeViewFILES��5  #   $��5,5,5,0q�6h     Search_SideContentTreeView#��[ 6y   ��6E   �� *
RootSearch+/ J� IsHitTestVisible/ 6�   �� *CanChooseFromSearch+��5  #   �� *ActualHeight+( �� $) FindAncestor=�* UserControl#+ [  $	, 110����5     �� 5  "   "-  6;   . 6W   �� *
SubNodes+.R�5  &   �� $ 
Horizontal=�61   $��5q�6J   .+�5  *   �� $��33��6M   $��33��6X   $��0,-3,0,3q�6d   ��60   �� *	NodeImage+6d   �� 5  *   #��[ 64   $��	-10,0,0,0q�6o   ��6Y   �� *Name+6o   5  '   5  #    5!       5"  "   $	 SelectedItemChanged��61   .
 5#  &     $
 True��6i    6<   �� *TreeViewSelectionChanged+6i   5$  #     5&  "   $	 MouseDoubleClick��61   .
 5'  &      6<   �� *TreeViewMouseDoubleClick+5(  #   5*     5+     5,     5-     5.     �� 51  
   $��3��6   $ 3��6!   $
��Left=�6T   $��Bottom=�6o   ��62   �� *SideContentWidth+6o   53     54     