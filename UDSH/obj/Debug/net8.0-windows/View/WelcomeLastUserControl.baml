   M S B A M L   `   `   `  ����?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  5      &     UDSH.View.WelcomeLastUserControl   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:UDSH.View     5      .��5
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
20,-30,0,0q�6f   .+�5      �� $��/Resource/LogoLarge.png��6   $	��200��69   $	��200��6E   5      � 5       $��Right=�6   $	��Top=�6/   $��	0,10,30,0q�6G   .+�5!      �� -   5        $��40��5!      $��40��6$     NewFileCreationCloseButton#��[ 60   6d   �� 5"      $��/Resource/Close.png��6   $��10��65   $��10��6@   5#      5$      �� 5&   
   -   5         ��Name$ 	UpperPara��5&      $��1��6'   $��my reflection.��64   $��34(�6J   $��SemiBold"�6X   ���#616161$
��Left=�6�   $��Bottom=�6�   $��
15,0,0,141q�6�   ��5'      �� 5(       �� 
TargetType" ��6   .�5)      �� 	 �� Property	 �@J�Opacity
 �� Value$	
 0.0Q�60   �� 5*      	 �@��BitmapScalingMode$
 HighQuality=�6H   �� 5+      	 �@J�IsHitTestVisible$
 False��69    �� Triggers 5-      :� 5.       :� RoutedEvent$ UserControl.Loaded��6'   .[�5/      �� .��50   "   �� .��51   &   W�  �� TargetProperty$ Opacity�66   	 W� To 1.052   2    g� Duration$ 0:0:0.5D�53   2    g� 	BeginTime 0:0:1.06E   54   #   55      56      58      5:      �� 5;   
   $ 0��6   $��1��6&   $
��Left=�63   $��Bottom=�6N   $��	15,0,0,50q�6i   .+�5<      �� -   5        $
 Para��5<      $�� ��6&   $	��423��6.   $��34(�6:   $��SemiBold"�6H   ���#616161$
��Wrap=�6s   5>      � 5@   
   $ 1��6   $��1��6    .+�5A      � $��Right=�6   $��Bottom=�6/   $	��130��6J   $��50��6V   $��
0,20,30,20q�6b   .+�5B      �� -   5         ��Name$ StartButton��5B        NewFileCreationButton#��[ 6.   6]   �� 5C      $��Start��6     DefaultText#��[ 6)   $��Right=�6N   $��Center=�6j   $��0,0,20,0q�6�    J� IsHitTestVisible$ False��6�   �� 5D      $$��/Resource/StartWritingIcon.png��6   $
��Left=�6@   $��Center=�6[   $��
10,9,10,11q�6v   5E       � 	Resources 5G       T� X T� Y��%)  '�       �� 5H      " �6   .�5I      �� 	 �@J�Opacity$	
 0.0Q�60   �� 5J      	 �@J�RenderTransformOrigin$
 0.5,0.5&�6>   �� 5K      	 � z�
 5L      T� 5M      $	 125Q�61   $ 0Q�69   5O       5P      :� 5Q      $ UserControl.Loaded��6'   .[�5R      �� .��5S   "   �� 	 0:0:46-   .��5T   &   W� $ Opacity�66    1.05U   6   $ 0:0:0.5D�5V   6   W� 5W   &   $, &RenderTransform.(TranslateTransform.X)�66    0.05X   6   $ 0:0:0.5D�5Y   6   5Z   #   5[      5\      5^      5_      5`      5a      5b      