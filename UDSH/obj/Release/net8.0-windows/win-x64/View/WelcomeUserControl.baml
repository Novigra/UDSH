   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  "    UDSH.View.WelcomeUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:UDSH.View     .���    NewFileProcessBackgroundSCB#��[    � RowDefinitions  �� $
��auto ��� .+���  K� ZIndex$ 3��$��0��$	��Top=�$��/Resource/StoryHeader.png��$	��150��$��38��$��325,10,325,0q��� $ 1��$��2������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFEEEEEE��� $ 	#FFDADADA�� � Offset$ 0.963Q�� $ 	#FFE7E7E7��$
 0.67Q�� $ 	#00E0E0E0��$	 0.8Q�� $ 2��$��0��.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��
20,-30,0,0q�.+��� $��/Resource/LogoLarge.png��$	��200��$	��200������  �� 
TargetType" ��.���  �� Property �@J�Opacity	 �� Value$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 �� Triggers
 :�  :� RoutedEvent$ Window.Loaded��.[��� .���� .��W�  �� TargetProperty$ Opacity�	 W� To 1.0 g� Duration$ 0:0:0.5D� g� 	BeginTime 0:0:1.2� $��Right=�$	��Top=�$��	0,10,30,0q�.+��� -   $��40��$��40��  NewFileCreationCloseButton#��[ �� $��/Resource/Close.png��$��10��$��10������ " ��.���  �@J�Opacity$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D�	 0:0:1� $ 2��$��2��.+��� -    ��Name$ WelcomeTitle��$��Welcome To UDSH��$��40(�$
��Bold"�$��Center=�$��Center=����� " ��.���  �@J�Opacity$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:2.8�� -   $ WelcomeSubTitle��$��A Screenwriting Software��$��20(�$��SemiBold"�$��Center=�$��Center=�$��0,70,0,0q����� " ��.���  �@J�Opacity$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D�	 0:0:3�� -   $ 	UpperPara��$��1��$"��I know. My love is this what��$��34(�$��SemiBold"����#616161$
��Left=�$��Bottom=�$��
15,0,0,141q����� " ��.���  �@J�Opacity$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:1.6�� $ 0��$��1��$
��Left=�$��Bottom=�$��
15,0,0,-40q�.+��� -   $
 Para��$�� ��$	��423��$��34(�$��SemiBold"����#616161$
��Wrap=��� -   $ 
BottomPara��$4��.Life consumed me; all that remains is my name.��$	��423��$��34(�$��SemiBold"����#616161$
��Wrap=����� " ��.���  �@J�Opacity$		 0.0Q���  �@��BitmapScalingMode$	 HighQuality=���  �@J�IsHitTestVisible$	 False��
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:1.8�� -    ��Name$ ButtonContainer��$ 2��$��1��$��Right=�$��Bottom=�$��	0,0,35,20q�._�� .+��� -    ��Name$ 
NextButton��  StartupButton#��[ $��	-8,0,-4,0q� � Triggers :� $ Button.MouseEnter��.[��� .���� .��W�  �� 
TargetName$ NextImg��$, &RenderTransform.(TranslateTransform.X)� 5$ 0:0:0.3D�:� $ Button.MouseLeave��.[��� .���� .��W� $ NextImg��$, &RenderTransform.(TranslateTransform.X)� 0$ 0:0:0.3D���  J� IsHitTestVisible$ False��$ 
Horizontal=�.+��� $
��Next��$��25(�$��SemiBold"�$��0,0,3,5q��� -	    ��Name$ NextImg��$��/Resource/RightArrow.png��$��20�� J� RenderTransformOrigin$ 0.5,0.5&�$��20��z�T�  T� X$ 0Q� T� Y$ 0Q����� " �.���  �@J�Opacity$		 0.0Q�
 :� $ Window.Loaded��.[��� .���� .��W� $ Opacity� 1.0$ 0:0:0.5D� 0:0:4.5