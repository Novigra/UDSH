   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.MVVM	UDSH.MVVM  5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ScrollBarLineButton �� Property �� Value �� 
TargetType ��Name �� CornerRadius  RepeatButtonNormalSCB'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� TargetProperty  RepeatButtonPressedColor	
 �� To g� Duration F�Name �� RelativeSource  RepeatButtonArrowColor �� Color  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions  ScrollBarBackgroundSCB ]�Name ]� DecreaseRepeatButton ]� Thumb  MainBlankColor 	 ScrollBarTrackThumbColor ]� IncreaseRepeatButton �� ScaleX �� ScaleY �� Triggers �� Binding �� Value N� EnterActions �� BeginStoryboardName ��Name	 W� To 
 HorizontalScrollBar � ColumnDefinitions �� 
StartPoint �� EndPoint  � Color! � Offset  	PaperPage+  %UDSH.MVVM.ParagraphMouseClickBehavior" P� Property# P� Value  ScrollBarLineButtonTest   MainOpenPageRepeatButtonLeft8  2System.Windows.Media.Animation.EasingColorKeyFrame$ �� KeyTime% �� Value  ScrollBarPageButtonTest  ScrollBarThumbTest  VerticalScrollBarTest  HorizontalScrollBarTest  PaperPageTest  ScrollBarLineButtonOP  ScrollBarPageButtonOP  ScrollBarThumbOP  VerticalScrollBarOP  HorizontalScrollBarOP  OpenFilesList  TSScrollBarLineButton  TSScrollBarPageButton  TSScrollBarThumb  TSVerticalScrollBar  TSHorizontalScrollBar   TreeviewSideContentScrollBar�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      !localclr-namespace:UDSH.MVVM  5      %�L  &	        7  7  &	 L    &	 �    &	     7  7   7  7  7  7 	 7  7   &	
     7   7  7  7  7   &	 T    7 
 7  &	     7  &	 X    &	 �    &	 $    7  7  7  7  7  7 	 7  7  &	 &    7  7  7  7  7  &	 )    7  7  &	 :+    &	 .    &	 j/    &	 �0    7  7  7  7  7  7 	 7  7  &	 �4    7  7  7  7  7  &	 �8    7  7  &	 �:    &	 �=    &	 ,?    &	 �@    7  7  7  7  7 	 7  7  &	 �D    7  7  7  7  7 	 7  7  &	 mH    7  7  �� 5      "  ��6   .�5   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5   
    �@�OverridesDefaultStyle$
 true��62   �� 5	   
    � |�$ false��6&   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $ Border��6   $��1q�6-   $ 2��68   $��1q�6I   8��   5        5      CommonStates��. 5   "     
Normal��6.     5   "   	MouseOver��6.     5   "   Pressed��. 5   &   �� .��5   *   �� $2	 ,(Control.Background).(SolidColorBrush.Color)�69   8
  $ 0:0:0.1D�5   9   5   '   5   #     5   "   Disabled��6.   5      ._�5      F� $ Arrow��6   $��Center=�6.   $��Center=�6K   ��6f   �� *	Content+ �� *TemplatedParent+��5      �� 5    "   # �  62   5"      5#      5$      5&      5'      �� 5)      "  ��6   .�5*   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5+   
    �@�OverridesDefaultStyle$
 true��62   �� 5,   
    � ��$ false��6&   �� 5-   
    � |�$ false��6&   �� 5/   
    � �� 50      �� 51      " ��6"   .R�52      �� 
������ 6   53      55      56      �� 58      "  n�6#   .�59   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5:   
    �@�OverridesDefaultStyle$
 true��62   �� 5;   
    � ��$ false��6&   �� 5<   
    � |�$ false��6&   �� 5>   
    � �� 5?      �� 5@      " n�6"   .R�5A      �� $ 2��6   #��z��6.   #��z��6X   $��1q�6�   5B      5D      5E      �� 5G      " ��6   .R�5H   
   �  5I      �� 5J      $��70��6    �� 5K      $��0.00001* �6    �� 5L      $��70��6    .+�5O      �� $��3��6   $ 2��6&   8��  67   �� 5P      $��0��6   8�� $��70��6U   $��ScrollBar.LineUpCommand��6a   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5R      $ 
PART_Track��6   $��1��6(   $
��true��65    5S      �� 5T      8�� $��ScrollBar.PageUpCommand��6P    5W      n� 5X      8�� $��1,0,1,0q�6D   ��5Y      �� 5Z      8  6.   ��5\      �� 5]      8  6.   5_       5b      �� 5c      8�� $��ScrollBar.PageDownCommand��6P   5e      �� 5g      $��2��6   8�� $��70��6U   $��ScrollBar.LineDownCommand��6a   $��M 0 0 L 4 4 L 8 0 Z��6�   ��5i      �� 5j      "  �6   .�5k      ��  �@J�RenderTransformOrigin$ 1.0,0.5&�6>   �� 5l       � z� 5m      �� 5n      $	 0.5Q�6-   $	 1.0Q�6:   5p       5r      �� 5s       6&   �� *IsInsideScrollBarHitCollision+ True6X    5t      �� 5u   "   $ CloseScroll��63   �� 5v   "   $ 	OffScroll��63   �� 5w   "   $ 
OpenScroll��62   .��5x   &   �� .��5y   *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    1.05z   :   $ 0:0:0.2D�5{   :   5|   '   5}   #   5      �� 5�       6&   �� *IsInsideScrollBarHitCollision+	 False6X    5�      �� 5�   "   $ 
OpenScroll��63   �� 5�   "   $ OnScroll��63   �� 5�   "   $ CloseScroll��62   .��5�   &   �� .��5�   *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    0.55�   :   $ 0:0:0.2D�5�   :   5�   '   5�   #   5�      �� 5�       6&   �� *IsInsideScrollBar+ True6L    5�      �� 5�   "   $ 	OffScroll��63   �� 5�   "   $ CloseScroll��63   �� 5�   "   $ OnScroll��62   .��5�   &   �� .��5�   *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    1.05�   :   $ 0:0:0.2D�5�   :   5�   '   5�   #   5�      �� 5�       6&   �� *IsInsideScrollBar+	 False6L    5�      �� 5�   "   $ 	OffScroll��62   .��5�   &   �� .��5�   *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    0.55�   :   $ 0:0:0.2D�5�   :   5�   '   5�   #   5�      5�      5�      5�      �� 5�      " ��6   .R�5�   
   �  5�      �� 5�      $��18��6#   �� 5�      $��0.00001* �6#   �� 5�      $��18��6#   .+�5�      �� $��3��6   $ 2��6)   ���#F0F0F06:   �� 5�      $��0��6   8��  $��18��6X   $��ScrollBar.LineLeftCommand��6c   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5�      $ 
PART_Track��6   $��1��6(   $��False��68    5�      �� 5�      8�� $��ScrollBar.PageLeftCommand��6P    5�      n� 5�      8�� $��0,1,0,1q�6D   ��5�      �� 5�      $	 0,0&�62   $	 1,0&�6C   ��5�   &   � � 5�   *   $	  Red��67   $	! 0.0Q�6C   � 5�   *   $	  Red��67   $	! 1.0Q�6C   5�   '   5�      ��5�      �� 5�      $	 0,0&�62   $	 0,1&�6C   ��5�   &   � � 5�   *   $	  Red��67   $	! 0.0Q�6C   � 5�   *   $	  Red��67   $	! 1.0Q�6C   5�   '   5�      5�       5�      �� 5�      8�� $ ��ScrollBar.PageRightCommand��6P   5�      �� 5�      $��2��6   8�� $��18��6X   $ ��ScrollBar.LineRightCommand��6c   $��M 0 0 L 4 4 L 0 8 Z��6�   5�      5�      �� 5�      "  ��6   .�5�   
   �� " �@ MouseEnterScrollBarCommand 6Y   �� *SideScrollMouseEnter+�� 5�   
   " �@ MouseLeaveScrollBarCommand 6Y   �� *SideScrollMouseLeave+�� 5�   
    �@J�SnapsToDevicePixels$
 True��60   �� 5�   
    �@�OverridesDefaultStyle$
 true��62    5�   
   P� 5�      " �@��Orientation$# 
Horizontal=�6-   . �5�      ��  � ��$
 Auto��6*   �� 5�       � ��$ 18��6+   �� 5�       � ��8   6-   5�      P� 5�      " �@��Orientation$# Vertical=�6-   . �5�      ��  � ��$ 18��6*   �� 5�       � ��$
 Auto��6+   �� 5�       � ��8  6-   5�      5�      �� 5�      "  ��6   .�5�   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�   
    �@�OverridesDefaultStyle$
 true��62   �� 5   
    � |�$ false��6&   �� 5  
    � �� 5     �� 5     " ��6"   .R�5     �� $ Border��6   $��1q�6-   $ 2��68   $��0q�6I   8��   5       5     CommonStates��. 5  "     
Normal��. 5	  &   �� .��5
  *   �� $N	 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6G   .g�5  .     $$ 0:0:0.3��6B   $% 	#00DBAD2F��6T   5  +   5  '   5  #     5  "   	MouseOver��. 5  &   �� .��5  *   �� $N	 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6G   .g�5  .     $$ 0:0:0.3��6B   $% 	#FFDBAD2F��6T   5  +   5  '   5  #     5  "   Pressed��6.     5  "   Disabled��6.   5     ._�5     F� $ Arrow��6   $��Center=�6.   $��Center=�6K   ��6f   �� *	Content+ �� *TemplatedParent+��5     �� 5  "   # �  62   5     5      5!     5#     5$     �� 5&     "  ��6   .�5'  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5(  
    �@�OverridesDefaultStyle$
 true��62   �� 5)  
    � ��$ false��6&   �� 5*  
    � |�$ false��6&   �� 5,  
    � �� 5-     �� 5.     " ��6"   .R�5/     �� 
������ 6   50     52     53     �� 55     "  n�6   .�56  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 57  
    �@�OverridesDefaultStyle$
 true��62   �� 58  
    � ��$ false��6&   �� 59  
    � |�$ false��6&   �� 5;  
    � �� 5<     �� 5=     " n�6"   .R�5>     �� $ 2��6   
������ 
������ $��0q�6a   5?     5A     5B     �� 5D     " ��6   .R�5E  
   �  5F     �� 5G     $��70��6    �� 5H     $��0.00001* �6    �� 5I     $��70��6    .+�5L     �� $��3��6   $ 2��6&   8��  67   �� 5M     $��0��6   8�� $��70��6Y   $��ScrollBar.LineUpCommand��6e   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5O     $ 
PART_Track��6   $��1��6(   $
��true��65    5P     �� 5Q     8�� $��ScrollBar.PageUpCommand��6T    5T     n� 5U     8�� $��1,0,1,0q�6H   ��5V     �� 5W     8  6.   ��5Y     �� 5Z     8  6.   5\      5_     �� 5`     8�� $��ScrollBar.PageDownCommand��6T   5b     �� 5d     $��2��6   8�� $��70��6Y   $��ScrollBar.LineDownCommand��6e   $��M 0 0 L 4 4 L 8 0 Z��6�   ��5f     �� 5g     "  �6   .�5h     ��  �@J�RenderTransformOrigin$ 1.0,0.5&�6>   �� 5i      � z� 5j     �� 5k     $	 0.5Q�6-   $	 1.0Q�6:   5m      5o     �� 5p      6&   �� *IsInsideScrollBarHitCollision+ True6X    5q     �� 5r  "   $ CloseScroll��63   �� 5s  "   $ 	OffScroll��63   �� 5t  "   $ 
OpenScroll��62   .��5u  &   �� .��5v  *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    1.05w  6   $ 0:0:0.2D�5x  6   5y  '   5z  #   5|     �� 5~      6&   �� *IsInsideScrollBarHitCollision+	 False6X    5     �� 5�  "   $ 
OpenScroll��63   �� 5�  "   $ OnScroll��63   �� 5�  "   $ CloseScroll��62   .��5�  &   �� .��5�  *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    0.55�  6   $ 0:0:0.2D�5�  6   5�  '   5�  #   5�     �� 5�      6&   �� *IsInsideScrollBar+ True6L    5�     �� 5�  "   $ 	OffScroll��63   �� 5�  "   $ CloseScroll��63   �� 5�  "   $ OnScroll��62   .��5�  &   �� .��5�  *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    1.05�  6   $ 0:0:0.2D�5�  6   5�  '   5�  #   5�     �� 5�      6&   �� *IsInsideScrollBar+	 False6L    5�     �� 5�  "   $ 	OffScroll��62   .��5�  &   �� .��5�  *   W� $-	 'RenderTransform.(ScaleTransform.ScaleX)�6:    0.55�  6   $ 0:0:0.2D�5�  6   5�  '   5�  #   5�     5�     5�     5�     �� 5�     " ��6   .R�5�  
   �  5�     �� 5�     $��70��6#   �� 5�     $��0.00001* �6#   �� 5�     $��70��6#   .+�5�     �� $��3��6   $ 2��6)   
������ 6:   �� 5�     $��0��6   8��  $��70��6\   $��ScrollBar.LineLeftCommand��6h   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5�     $ 
PART_Track��6   $��1��6(   $��False��68    5�     �� 5�     8�� $��ScrollBar.PageLeftCommand��6T    5�     n� 5�     8�� 
������ 
������ $��0q�6{   $��0,1,0,1q�6�   5�      5�     �� 5�     8�� $ ��ScrollBar.PageRightCommand��6T   5�     �� 5�     $��2��6   8�� $��70��6\   $ ��ScrollBar.LineRightCommand��6h   $��M 0 0 L 4 4 L 0 8 Z��6�   5�     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62    5�  
   P� 5�     " �@��Orientation$# 
Horizontal=�6-   . �5�     ��  � ��$
 Auto��6*   �� 5�      � ��$ 70��6+   �� 5�      � ��
 ���� 6/   �� 5�      � ��8   6-   5�     P� 5�     " �@��Orientation$# Vertical=�6-   . �5�     ��  � ��$ 18��6*   �� 5�      � ��$
 Auto��6+   �� 5�      � ��8  6-   5�     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62   �� 5�  
    � |�$ false��6&   �� 5�  
    � �� 5�     �� 5�     " ��6"   .R�5�     �� $ Border��6   $��1q�6-   $ 2��68   $��1q�6I   
������  5�       5�     CommonStates��. 5�  "     
Normal��6.     5�  "   	MouseOver��6.     5�  "   Pressed��6.     5�  "   Disabled��6.   5�     ._�5�     F� $ Arrow��6   $��Center=�6.   $��Center=�6K   ��6f   �� *	Content+ �� *TemplatedParent+��5�     �� 5�  "   $ Transparent��62   5�     5�     5�     5�     5�     �� 5     "  ��6   .�5  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5  
    �@�OverridesDefaultStyle$
 true��62   �� 5  
    � ��$ false��6&   �� 5  
    � |�$ false��6&   �� 5  
    � �� 5     �� 5	     " ��6"   .R�5
     �� 
������ 6   5     5     5     �� 5     "  n�6   .�5  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5  
    �@�OverridesDefaultStyle$
 true��62   �� 5  
    � ��$ false��6&   �� 5  
    � |�$ false��6&   �� 5  
    � �� 5     �� 5     " n�6"   .R�5     �� $ 5��6   #��z��6.   #��z��6X   $��1q�6�   5     5     5     �� 5     " ��6   .R�5   
   �  5!     �� 5"     $��0��6    �� 5#     $��0.00001* �6    �� 5$     $��0��6    .+�5'     �� $��3��6   $ 2��6&   8��  67   �� 5(     $��0��6   8�� $��70��6W   $��ScrollBar.LineUpCommand��6c   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5*     $ 
PART_Track��6   $��1��6(   $
��true��65    5+     �� 5,     8�� $��ScrollBar.PageUpCommand��6R    5/     n� 50     8�� $��1,0,1,0q�6F   ��51     �� 52     8  6.   ��54     �� 55     8  6.   57      5:     �� 5;     8�� $��ScrollBar.PageDownCommand��6R   5=     �� 5?     $��2��6   8�� $��70��6W   $��ScrollBar.LineDownCommand��6c   $��M 0 0 L 4 4 L 8 0 Z��6�   ��5A     �� 5B     "  �6   .�5C     ��  �@J�RenderTransformOrigin$ 1.0,0.5&�6>   �� 5D      � z� 5E     �� 5F     $	 0.5Q�6-   $	 1.0Q�6:   5H     5I     5K     5L     �� 5O     " ��6   .R�5P  
   �  5Q     �� 5R     $��18��6#   �� 5S     $��0.00001* �6#   �� 5T     $��18��6#   .+�5W     �� $��3��6   $ 2��6)   ���#F0F0F06:   �� 5X     $��0��6   8��  $��18��6Z   $��ScrollBar.LineLeftCommand��6e   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5Z     $ 
PART_Track��6   $��1��6(   $��False��68    5\     �� 5]     8�� $��ScrollBar.PageLeftCommand��6R    5`     n� 5a     8�� $��0,1,0,1q�6F   ��5b     �� 5c     $	 0,0&�62   $	 1,0&�6C   ��5e  &   � � 5f  *   $	  Red��67   $	! 0.0Q�6C   � 5g  *   $	  Red��67   $	! 1.0Q�6C   5h  '   5j     ��5m     �� 5n     $	 0,0&�62   $	 0,1&�6C   ��5p  &   � � 5q  *   $	  Red��67   $	! 0.0Q�6C   � 5r  *   $	  Red��67   $	! 1.0Q�6C   5s  '   5u     5w      5z     �� 5{     8�� $ ��ScrollBar.PageRightCommand��6R   5}     �� 5~     $��2��6   8�� $��18��6Z   $ ��ScrollBar.LineRightCommand��6e   $��M 0 0 L 4 4 L 0 8 Z��6�   5     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62    5�  
   P� 5�     " �@��Orientation$# 
Horizontal=�6-   . �5�     ��  � ��$
 Auto��6*   �� 5�      � ��$ 18��6+   �� 5�      � ��8   6-   5�     P� 5�     " �@��Orientation$# Vertical=�6-   . �5�     ��  � ��$ 18��6*   �� 5�      � ��$
 Auto��6+   �� 5�      � ��8  6-   5�     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62   �� 5�  
    � |�$ false��6&   �� 5�  
    � �� 5�     �� 5�     " ��6"   .R�5�     �� $ Border��6   $��1q�6-   $ 2��68   $��1q�6I   
������  5�       5�     CommonStates��. 5�  "     
Normal��6.     5�  "   	MouseOver��6.     5�  "   Pressed��6.     5�  "   Disabled��6.   5�     ._�5�     F� $ Arrow��6   $��Center=�6.   $��Center=�6K   ��6f   �� *	Content+ �� *TemplatedParent+��5�     �� 5�  "   $ Transparent��62   5�     5�     5�     5�     5�     �� 5�     "  ��6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62   �� 5�  
    � ��$ false��6&   �� 5�  
    � |�$ false��6&   �� 5�  
    � �� 5�     �� 5�     " ��6"   .R�5�     �� 
������ 6   5�     5�     5�     �� 5�     "  n�6   .�5�  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5�  
    �@�OverridesDefaultStyle$
 true��62   �� 5�  
    � ��$ false��6&   �� 5�  
    � |�$ false��6&   �� 5�  
    � �� 5�     �� 5�     " n�6"   .R�5�     �� $ 5��6   #��z��6.   #��z��6X   $��1q�6�   5�     5�     5�     �� 5�     " ��6   .R�5�  
   �  5�     �� 5�     $��0��6    �� 5�     $��0.00001* �6    �� 5�     $��0��6    .+�5�     �� $��3��6   $ 2��6&   ���#ABABAB67   �� 5�     $��0��6   8��  $��70��6W   $��ScrollBar.LineUpCommand��6c   $��M 0 4 L 8 4 L 4 0 Z��6�   ]� 5�     $ 
PART_Track��6   $��1��6(   $
��true��65    5�     �� 5�     8�� $��ScrollBar.PageUpCommand��6R    5�     n� 5�     8�� $��1,0,1,0q�6F   ��5�     �� 5�     8  6.   ��5�     �� 5�     8  6.   5�      5�     �� 5�     8�� $��ScrollBar.PageDownCommand��6R   5�     �� 5�     $��2��6   8�� $��70��6W   $��ScrollBar.LineDownCommand��6c   $��M 0 0 L 4 4 L 8 0 Z��6�   ��5�     �� 5�     "  �6   .�5�     ��  �@J�RenderTransformOrigin$ 1.0,0.5&�6>   �� 5�      � z� 5�     �� 5�     $	 0.5Q�6-   $	 1.0Q�6:   5�     5�     5�     5�     �� 5      " ��6   .R�5  
   �  5     �� 5     $��0��6#   �� 5     $��0.00001* �6#   �� 5     $��0��6#   .+�5     �� $��3��6   $ 2��6)   ���#ABABAB6:   �� 5	     $��0��6   8��  $��18��6Z   $��ScrollBar.LineLeftCommand��6e   $��M 4 0 L 4 8 L 0 4 Z��6�   ]� 5     $ 
PART_Track��6   $��1��6(   $��False��68    5     �� 5     8�� $��ScrollBar.PageLeftCommand��6R    5     n� 5     8�� $��0,1,0,1q�6F   ��5     �� 5     8  6.   ��5     �� 5     8  6.   5      5     �� 5     8�� $ ��ScrollBar.PageRightCommand��6R   5      �� 5!     $��2��6   8�� $��18��6Z   $ ��ScrollBar.LineRightCommand��6e   $��M 0 0 L 4 4 L 0 8 Z��6�   ��5#     �� 5$     "  �6   .�5%     ��  �@J�RenderTransformOrigin$ 0.5,1.0&�6>   �� 5&      � z� 5'     �� 5(     $	 1.0Q�6-   $	 0.5Q�6:   5*     5+     5-     5.     �� 50     "  ��6   .�51  
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 52  
    �@�OverridesDefaultStyle$
 true��62    53  
   P� 54     " �@��Orientation$# 
Horizontal=�6-   . �55     ��  � ��$
 Auto��6*   �� 56      � ��$ 18��6+   �� 57      � ��8   6-   58     P� 59     " �@��Orientation$# Vertical=�6-   . �5:     ��  � ��$ 18��6*   �� 5;      � ��$
 Auto��6+   �� 5<      � ��$ 0,10,0,0q�6+   �� 5=      � ��8  6-   5>     5@     5B     