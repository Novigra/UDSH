   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  0    *UDSH.View.WelcomeProfilePictureUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:UDSH.View       � 	Resources     ShowParagraphs �� TargetProperty	 W� To g� Duration g� 	BeginTime��%G   &	        �� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:1.3.���   NewFileProcessBackgroundSCB#��[  � RowDefinitions �� $
��auto ��� .+���  K� ZIndex$ 3��$��0��$	��Top=�$��/Resource/StoryHeader.png��$	��150��$��38��$��325,10,325,0q��� $ 1��$��2������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.��� 	 � Color$	 	#FFEEEEEE��� $	 	#FFDADADA��
 � Offset$
 0.963Q�� $	 	#FFE7E7E7��$

 0.67Q�� $	 	#00E0E0E0��$	
 0.8Q�� $ 2��$��0��.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��
20,-30,0,0q�.+��� $��/Resource/LogoLarge.png��$	��200��$	��200��� $��Right=�$	��Top=�$��	0,10,30,0q�.+��� -   $��40��$��40��  NewFileCreationCloseButton#��[ �� $��/Resource/Close.png��$��10��$��10���� $ 1��$��2��$ 
Horizontal=�.+�� -    �Name$ LeftGrid��$��110,140,0,0q�.+��� -    ��Name$ ProfilePictureLabel��$��Profile Picture��$��20(�$��SemiBold"�����  �� 
TargetType" ��.���  �� Property �@J�Opacity �� Value$	 0.0Q���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� �� Triggers :�  :� RoutedEvent$ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:4.0� -   $ PickedImageGrid��$��
0,40,0,185q�.+�A� -    A�Name$ PickedImage�� �� BitmapScalingMode$ HighQuality=�$	��150�� J� Opacity$ 0Q� J� IsHitTestVisible$ False������  �� ImageSource$ /Resource/emojitest.png��A� -   $ PickedProfileCollision��$	��150�� J� RenderTransformOrigin$ 0.5,1.0&�$ 0Q�$ False������ $ 0.5,1&�$ 0.5,0&�.��� $	 	#FF393E46��$
 1Q�� $	 Transparent��$
 0Q�� $	 	#B2393E46��$
 0.706Q� � Triggers :� $ Ellipse.MouseEnter��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D�:� $ Ellipse.MouseLeave��.[��� .���� .��W� $ Opacity� 0.0$ 0:0:0.3D��� -    ��Name$ PickImageBorder�����#FFFFFF �� CornerRadius$ 20��$��2q�$	��Top=�$��
0,40,0,185q���!�  !� Visual ��  �� StrokeDashArray$	 4 4R����#A5A5A5$��2�� �� RadiusX$ 20�� �� RadiusY$ 20������   �� RelativeSource  �� ! �� AncestorType"! ��" �� Path$" ActualWidth�����   �� "! ��$" ActualHeight�._�� .+��� $ 
Horizontal=�.+��� $��/Resource/HorusEye.png��$��80��$��80��$��30,0,0,0q��� ���#A5A5A5$��2��$	 2 2R�	# �� Y1$	# 130��$��40,0,0,0q�$��Center=��� -   $ 	SizeLabel��$��Square Sizes Recommended�����#A5A5A5$��15(�$��SemiBold"�$	��160��$
��Wrap=�$��Center=�$ �� TextAlignment$$ Center=�$��0,0,0,0q��� -	   $ HighlightProfileBorder�����#393E46$��0��$ 20��$
��Left=�._��� $ 
Horizontal=�.+��� $"��/Resource/HorusEyeOrange.png��$��80��$��80��$��30,0,0,0q��� ���#FFFFFF$��2��$	 2 2R�$	# 130��$��40,0,0,0q�$��Center=��� $��Square Sizes Recommended�����#FFFFFF$��15(�$��SemiBold"�$	��160��$
��Wrap=�$��Center=�$$ Center=�$��0,0,0,0q��� -
   % ��Name$ % ProfilePictureHitCollision��  MainBlankSCB#��[  :� $ Rectangle.MouseEnter��.[��� .���� .��W� & �� 
TargetName$& HighlightProfileBorder��$ Width� 311$ 0:0:1D�' W� EasingFunction' .  (System.Windows.Media.Animation.CubicEase  7  1System.Windows.Media.Animation.EasingFunctionBase(   
EasingMode$( EaseOut=�:� $ Rectangle.MouseLeave��.[��� .���� .��W� $& HighlightProfileBorder��$ Width� 0$ 0:0:0.5D�'   $( EaseIn=����� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:4.3�� -   $% M_Border��  ProfilePicBar#��[ $	��Top=�$��6��$	��380��$��
72,120,0,0q�  	BorderLBG#��[ $ 2��$ 2��� -   $ 	RightGrid��$��
72,140,0,0q�.+��� $ 
Horizontal=�.+��� -   $ CustomIconLabel��$��Custom Icons��$��20(�$��SemiBold"����� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:4.7�� $ 1��$ ��/Resource/More Details.png��$��17��$��17��$	��Top=�$��10,7,0,0q����� " ��.���  �@J�Opacity$	 0.0Q���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:4.7�� -   $ 0��$��20��$��20��$	��Top=�$��-18.5,5.5,0,0q�.��� -   ) �Name$) 	GuidPopup��* � 	StaysOpen$
* True��+ � AllowsTransparency$
+ True��$��Mouse=�$
��Fade=�, � VerticalOffset$, 5��- � HorizontalOffset$
- -100��.���� ���#FFFFFF$��3q����#A5A5A5$	��300��$	��210��$ 10��._��� $ Vertical=�.+��� -   $ TextGuidance��  DefaultText#��[ $��15(�$��
10,10,10,0q�$
��Wrap=��� -   $ TipGuidance��#��[ $��15(����#FF0000$��
10,5,10,10q�$
��Wrap=��� ���#CDCDCD$ 	0,0,10,10��$��40��._��� $ 
Horizontal=�$��Center=�.+��� #��[ $��UDSH | Hamo��$��20,0,0,0q��� $��/Resource/Egyptflag.png��$��30��$��30��$��10,0,0,0q��� $��/Resource/Olive.png��$��30��$��30��$��10,0,0,0q��� $��/Resource/SmileEmoji.png��$��30��$��30��$��10,0,0,0q��� $��
0,40,0,185q�$ 
Horizontal=�$	��Top=�$
��Left=�.+��� -   $ BorderIcon1�����#FFFFFF$ 5��$��2q�$��50��$��30����!�  �� $	 4 4R����#A5A5A5$��2��$ 5��$ 5������   �� "! ��$" ActualWidth�����   �� "! ��$" ActualHeight�._�� .+��� $��/Resource/Add.png��$��20��$��20���� -   . ��Name$. IconOne���� -   $% IconHitColl1��#��[ ���� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D�	 0:0:5�� -   $ BorderIcon2�����#FFFFFF$ 5��$��2q�$��50��$��30��$��10,0,0,0q���!�  �� $	 4 4R����#A5A5A5$��2��$ 5��$ 5������   �� "! ��$" ActualWidth�����   �� "! ��$" ActualHeight�._�� .+��� $��/Resource/Add.png��$��20��$��20���� -   $. IconTwo���� -   $% IconHitColl2��#��[ ���� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:5.3�� -   $ BorderIcon3�����#FFFFFF$ 5��$��2q�$��50��$��30��$��10,0,0,0q���!�  �� $	 4 4R����#A5A5A5$��2��$ 5��$ 5������   �� "! ��$" ActualWidth�����   �� "! ��$" ActualHeight�._�� .+��� $��/Resource/Add.png��$��20��$��20���� -   $. 	IconThree���� -   $% IconHitColl3��#��[ ���� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:5.6�� -   $ 	UpperPara��$��1��$��remains is my name.��$��34(�$��SemiBold"����#616161$
��Left=�$��Bottom=�$��
15,0,0,141q����� " ��.���  �@J�Opacity$	 0.0Q���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:1.0�� $ 0��$��1��$
��Left=�$��Bottom=�$��
15,0,0,-40q�.+��� -   $
 Para��$�� ��$	��423��$��34(�$��SemiBold"����#616161$
��Wrap=��� -   $ 
BottomPara��$8��2But I will speak, for my words are my last weapon.��$	��423��$��34(�$��SemiBold"����#616161$
��Wrap=����� " ��.���  �@J�Opacity$	 0.0Q���  �@��BitmapScalingMode$ HighQuality=���  �@J�IsHitTestVisible$ False�� :� $ TextBlock.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:1.0�� -   $ ButtonContainer��$ 2��$��1��$��Right=�$��Bottom=�$��	0,0,35,20q�._�� .+��� -   / ��Name$/ 
NextButton��  StartupButton#��[ $ False��$��	-8,0,-4,0q� :� $ Button.MouseEnter��.[��� .���� .��W� $& NextImg��$, &RenderTransform.(TranslateTransform.X)� 5$ 0:0:0.3D�:� $ Button.MouseLeave��.[��� .���� .��W� $& NextImg��$, &RenderTransform.(TranslateTransform.X)� 0$ 0:0:0.3D��� $ False��$ 
Horizontal=�.+��� -    $ NextTextBlock��$
��Skip��$��25(�$��SemiBold"�$��0,0,3,5q��� -!   $. NextImg��$��/Resource/RightArrow.png��$ 0.5,0.5&�$��20��$��20��z�T� 0 T� X$0 0Q�1 T� Y$1 0Q����� " ��.���  �@J�Opacity$	 0.0Q� :� $ UserControl.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.3D� 0:0:5.9