   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.MVVM	UDSH.MVVM  5      1clr-namespace:UDSH.ConvertersUDSH.Converters  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089%   UDSH.Converters.HeightConverter   WidthConverter  �� 
TargetType  PageScrollViewer �� Property �� Value �� 
TargetType � ColumnDefinitions � RowDefinitions �� CanContentScroll ��Name K� ZIndex  MainBlankSCB	 J� Opacity+  %UDSH.MVVM.ParagraphMouseClickBehavior
 ��Name �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility  	PaperPage �� Orientation �� HorizontalOffset �� ScrollableWidth �� ViewportWidth, �� %ComputedHorizontalScrollBarVisibility  OpenFilesScrollViewer  PaperPageTest  OpenFilesListScrollViewer  OpenFilesList  SideContentScrollViewer  	 TreeviewSideContentScrollBar 
 ContentScrollViewer  HiddenContentScrollViewer �� BasedOn  TestContentScrollViewer J� IsHitTestVisible �� RelativeSource �� AncestorType �� Path �� 	Converter �� ConverterParameter �� 
StartPoint �� EndPoint  � Color! � Offset" �� Triggers# �� Binding$ �� Value% N� EnterActions& �� TargetProperty	' W� To( g� Duration) N� ExitActions�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      !localclr-namespace:UDSH.MVVM  5      &Convclr-namespace:UDSH.Converters  5      %b  &	        &	      7  7  7  &	 a    7  7  &	      7  7  &	 }    7 	 7 	 &	
 �    7 	 7 	 &	 :    7 	 7 	 &	 \    7   7 	 7 	    5      6   �� 5	      "  ��6   .�5
   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �  5      �� 5      $
��Auto �6/   �� 5       5      �� 5      �� 5      $
��Auto �6,   .+�5      �� $��1��6!   $��0q�61   ._�5      �� # z��65   5      �� 5      $ CollisionHitRect��6$   $��1��6>   $ 0��6N   $��Right=�6_   $��30��6{   8��  $		 0.2Q�6�   ��5      �� 5    "   "  ��6(   .�5!   &   ��  �@ MouseEnterCommand 6l   �� *SideScrollCollisionMouseEnter+�� 5"   &    �@ MouseLeaveCommand 6l   �� *SideScrollCollisionMouseLeave+5#   #   5%      �� 5'      $
 PART_VerticalScrollBar��6$   $��1��6D   $��Right=�6T   ���#D9D9D9# z 5(   $   # z 5)   $   # z 5*   $   #y�z 5+   $   8�� 5,      �� 5.      $
 PART_HorizontalScrollBar��6$   $ 
Horizontal=�5/   $   $��1��50   $   $��1��51   $   # z 52   $   # z 53   $   # z 54   $   #y�z 55   $   8�� 6i   57      58      5:      5;      �� 5=      "  ��6   .�5>   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5?   
    � ��
 ���� 6'   �� 5@   
   ! �@ ScrollViewerLoadedCommand 6X   �� *HorizontalScrollViewerLoaded+�� 5A   
    � �� 5B      �� 5C      " ��6"   .R�5D      � 
������  5E      �� 5F      $
��Auto �6/   �� 5G       5J      �� 5K      �� 5L      $
��Auto �6,   .+�5O      �� $��1��6!   
������ $��0q�6J   ._�5P      �� # z��65   5Q      �� 5S      $
 PART_VerticalScrollBar��6$   $��1��6D   $��Right=�6T   
������ # z 5T       # z 5U       # z 5V       #y�z 5W       8��  5X      �� 5Z      $
 PART_HorizontalScrollBar��6$   $ 
Horizontal=�5[       $��1��5\       $��1��5]       # z 5^       # z 5_       # z 5`       #y�z 5a       8�� 
������ $��	0,-10,0,0q�6�   5c      5d      5f      5g      �� 5k      "  ��6   .�5l   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5m   
   ! �@ ScrollViewerLoadedCommand 6X   �� *)'VerticalOpenFilesListScrollViewerLoaded+�� 5n   
    � �� 5o      �� 5p      " ��6"   .R�5q      �  5r      �� 5s      $
��Auto �6/   �� 5t       5w      �� 5x      �� 5y      $
��Auto �6,   .+�5|      �� $��1��6!   $��0q�61   ._�5}      �� # z��65   $��0,0,5,0q�6k   5~      �� 5�      $
 PART_VerticalScrollBar��6$   $��1��6D   $��Right=�6T   ���#D9D9D9# z 5�       # z 5�       # z 5�       #y�z 5�       8��  $��0,5,5,5q�6�   5�      �� 5�      $
 PART_HorizontalScrollBar��6$   $ 
Horizontal=�5�       $��1��5�       $��1��5�       # z 5�       # z 5�       # z 5�       #y�z 5�       8�� 6e   5�      5�      5�      5�      �� 5�      "  ��6   .�5�   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �  5�      �� 5�      $
��Auto �6/   �� 5�       5�      �� 5�      $
��Auto �6,   �� 5�      .+�5�      �� $��1��6!   $��1��6.   $��0q�6>   $��0,5,0,0q�6R   ._�5�      �� # z��65   5�      �� 5�      $
 PART_VerticalScrollBar��6$   $��1��6D   $��1��6Q   $��Right=�6a   ���#D9D9D9# z 5�       # z 5�       # z 5�       #y�z 5�       8��  5�      �� 5�      $
 PART_HorizontalScrollBar��6$   $ 
Horizontal=�5�       $��0��5�       $��1��5�       # z 5�       # z 5�       # z 5�       #y�z 5�       8�� $��0,-5,0,0q�6�   5�      5�      5�      5�      �� 5�      "  ��6   .�5�   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5�   
   ! �@ ScrollViewerLoadedCommand 6X   �� *ContentScrollViewerLoaded+�� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �  5�      �� 5�      �� 5�      $
��Auto �6/    5�      �� 5�      �� 5�      $
��Auto �6,   .+�5�      �� $!��PART_ScrollContentPresenter��61   $��0��5�   1   $��0��5�   1   # z��5�   1   #��z��5�   1   $��0q�5�   1   �� 5�      $��PART_VerticalScrollBar��6$   $��0��5�   $   $��1��5�   $   $ Vertical=�5�   $   # z 5�   $   # z 5�   $   # z 5�   $   #y�z 5�   $   8��  5�   $   �� 5�      $��PART_HorizontalScrollBar��6$   $��1��5�   $   $��0��5�   $   $ 
Horizontal=�5�   $   # z 5�   $   # z 5�   $   # z 5�   $   #y�z 5�   $   8�� 5�   $   5�      5�      5�      5�      �� 5�      "  ��6   .�5�   
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5�   
   ! �@ ScrollViewerLoadedCommand 6X   �� *!HiddenContentScrollViewerLoaded+�� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �  5�      �� 5�      �� 5�      $
��Auto �6/    5�      �� 5�      �� 5      $
��Auto �6,   .+�5     �� $!��PART_ScrollContentPresenter��61   $��0��5  -   $��0��5  -   # z��5  -   #��z��5  -   $��0q�5  -   �� 5
     $��PART_VerticalScrollBar��6$   $��0��5      $��1��5      $ Vertical=�5      # z 5      # z 5      # z 5      #y�z 5      ��5     �� 5  "   "  ��6(   8   .�5  &   ��  �@ ScrollBarLoadedCommand 6q   �� *VerticalScrollBarLoaded+5  #   5     �� 5     $��PART_HorizontalScrollBar��6$   $��1��5      $��0��5      $ 
Horizontal=�5      # z 5      # z 5      # z 5      #y�z 5       8�� 5!      5"     5#     5%     5&     �� 5(     "  ��6   .�5)  
   ��  �@�OverridesDefaultStyle$
 True��62   �� 5*  
   ! �@ ScrollViewerLoadedCommand 6X   �� *#!VerticalContentScrollViewerLoaded+�� 5+  
    � �� 5,     �� 5-     " ��6"   .R�5.     �  5/     �� 50     �� 51     $
��Auto �6/    54     �� 55     �� 56     $
��Auto �6,   .+�59     �� $!��PART_ScrollContentPresenter��61   $��0��5:  )   $��0��5;  )   # z��5<  )   #��z��5=  )   $��0q�5>  )   �� 5@     $ False��6!   $��0��6:   $��0��6G   $
��Left=�6W   $��Bottom=�6r   $��30��6�   ��5A  !   ��  ��  ListView$ ActualWidth�8   $ 9����5B     �� 5C  "   $ 0.5,1&�66   $ 0.5,0&�6I   .��5D  &   � $  #DBDBDB��63   � 5E  &   $  	#00DBDBDB��63   $! 1Q�6E   5F  #   ��5I     �� 5J  "   "  ��6(   " 5K  &   �� 5L  *   # 66   �� *ReachedContentBottom+$ True6_   % 5M  .   �� 5N  2   .��5O  6   �� .��5P  :   W� $& Opacity�6J   ' 06n   $( 0:0:0.2D�6u   5Q  7   5R  3   ) 5T  .   �� 5U  2   .��5V  6   �� .��5W  :   W� $& Opacity�6J   ' 16n   $( 0:0:0.2D�6u   5X  7   5Y  3   5[  +   5]  #   5_     �� 5a     $��PART_VerticalScrollBar��6$   $��0��5b     $��1��5c     $ Vertical=�5d     # z 5e     # z 5f     # z 5g     #y�z 5h     8�� 5i     �� 5k     $��PART_HorizontalScrollBar��6$   $��1��5l     $��0��5m     $ 
Horizontal=�5n     # z 5o     # z 5p     # z 5q     #y�z 5r     8�� 5s     5v     5w     5y     5z     5|     