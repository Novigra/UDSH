   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  5      &     UDSH.View.WelcomeNameUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:UDSH.View     5      .��5
      �    NewFileProcessBackgroundSCB#��[  6     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      .+�5   
   ��  K� ZIndex$ 3��6   $��0��6!   $	��Top=�6.   $��/Resource/StoryHeader.png��6F   $	��150��6i   $��38��6u   $��325,10,325,0q�6�   �� 5   
   $ 1��6   $��2��6"   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FFEEEEEE��6#   � 5      $ 	#FFDADADA��6#    � Offset$ 0.963Q�65   � 5      $ 	#FFE7E7E7��6#   $
 0.67Q�65   � 5      $ 	#00E0E0E0��6#   $	 0.8Q�65   5      5      � 5   
   $ 2��6   $��0��6    .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�63   $	��Top=�6N   $��
20,-30,0,0q�6f   .+�5      �� $��/Resource/LogoLarge.png��6   $	��200��69   $	��200��6E   5      � 5       $��Right=�6   $	��Top=�6/   $��	0,10,30,0q�6G   .+�5!      �� -   5        $��40��5!      $��40��6$     NewFileCreationCloseButton#��[ 60   6d   �� 5"      $��/Resource/Close.png��6   $��10��65   $��10��6@   5#      5$      � 5&   
   $ 2��6   $��2��6    $��Center=�61   $��Center=�6N   .+�5'      �� -   5         ��Name$ 
NameBorder��5'      $
��Left=�6)     SearchBarSCB#��[ 6D   $��2q�6o     SearchBarBorderSCB#��[ 6�    �� CornerRadius$ 4��5(      $��40��6&   $��50��62   	 J� Opacity$	 0Q�6Y   
 � FocusVisualStyle
 6=   Z� ._�5*      �� $ 
Horizontal=�6   $��Stretch=�66   $��Center=�6T   
 6o   Z� .+�5,      � $��4,0,4,0q�6   $��Center=�6,   .+�5.      �� -   5         ��Name$ HighlightText��5.   $   $�� ��6;     SearchTextHeaderSCB#��[ 6C   $ 0��6u   $��20(�6�   $��SemiBold"�6�   $|�False��6�   5/      �� 52      -   5         ��Name$ NameText��52   "    J� IsHitTestVisible$ False��64     MainBlankSCB#��[ 6p   $��20(�6�   $
��Bold"�6�   $	��500��6�   $�� ��53   "   $ 1��6*   $��0q�6;   
 6O   Z� .��6k   54      55      56      57      58      �� 5:   
   -   5        $ 	UpperPara��5:      $��1��6'   $��hoping to find myself.��64   $��34(�6R   $��SemiBold"�6`   ���#616161$
��Left=�6�   $��Bottom=�6�   $��
15,0,0,141q�6�   ��5;      �� 5<       �� 
TargetType" ��6   .�5=      ��  �� Property �@J�Opacity �� Value$	 0.0Q�60   �� 5>       �@��BitmapScalingMode$ HighQuality=�6H   �� 5?       �@J�IsHitTestVisible$ False��69    �� Triggers 5A      :� 5B       :� RoutedEvent$ TextBlock.Loaded��6'   .[�5C      �� .��5D   "   �� .��5E   &   W�  �� TargetProperty$ Opacity�66   	 W� To 1.05F   6    g� Duration$ 0:0:0.5D�5G   6    g� 	BeginTime 0:0:1.36I   5H   #   5I      5J      5L      5N      �� 5O   
   $ 0��6   $��1��6&   $
��Left=�63   $��Bottom=�6N   $��
15,0,0,-40q�6i   .+�5P      �� -   5        $
 Para��5P      $�� ��6&   $	��423��6.   $��34(�6:   $��SemiBold"�6H   ���#616161$
��Wrap=�6s   �� 5Q      -   5        $ 
BottomPara��5Q      $3��-I cried a lot and began to see my reflection.��6,   $	��423��6a   $��34(�6m   $��SemiBold"�6{   ���#616161$
��Wrap=�6�   ��5R      �� 5S      " ��6   .�5T      ��  �@J�Opacity$	 0.0Q�64   �� 5U       �@��BitmapScalingMode$ HighQuality=�6L   �� 5V       �@J�IsHitTestVisible$ False��6=    5X      :� 5Y      $ TextBlock.Loaded��6+   .[�5Z   "   �� .��5[   &   �� .��5\   *   W� $ Opacity�6:    1.05]   *   $ 0:0:0.5D�5^   *    0:0:1.36=   5_   '   5`   #   5a      5c      5e      5f      �� 5h   
   -   5        $ ButtonContainer��5h      $ 2��6*   $��1��6;   $��Right=�6H   $��Bottom=�6d   $��	0,0,35,20q�6   $	 0Q�6�   ._�5i      � .+�5j      �� -	   5         ��Name$ 
NextButton��5j        StartupButton#��[ 6-   $ False��6T   $��	-8,0,-4,0q�6�    � Triggers 5k      :� 5l      $ Button.MouseEnter��6'   .[�5m      �� .��5n   "   �� .��5o   &   W�  �� 
TargetName$ NextImg��66   $, &RenderTransform.(TranslateTransform.X)�5p   "    55q   "   $ 0:0:0.3D�5r   "   5s   #   5t      5u      :� 5w      $ Button.MouseLeave��6'   .[�5x      �� .��5y   "   �� .��5z   &   W� $ NextImg��66   $, &RenderTransform.(TranslateTransform.X)�5{   "    05|   "   $ 0:0:0.3D�5}   "   5~   #   5      5�      5�      �� 5�      $ False��6   $ 
Horizontal=�66   .+�5�      �� $
��Next��6    $��25(�6,   $��SemiBold"�6:   $��0,0,3,5q�6P   �� 5�      -
   5         ��Name$ NextImg��5�      $��/Resource/RightArrow.png��6-    J� RenderTransformOrigin$ 0.5,0.5&�6O   $��20��6o   $��20��6z   z�5�      T� 5�       T� X$ 0Q�61    T� Y$ 0Q�67   5�      5�      5�      5�      5�      5�      