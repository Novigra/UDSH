   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  &     UDSH.View.WelcomeLastUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     )localclr-namespace:UDSH.View     .���    NewFileProcessBackgroundSCB#��[    � RowDefinitions  �� $
��auto ��� .+���  K� ZIndex$ 3��$��0��$	��Top=�$��/Resource/StoryHeader.png��$	��150��$��38��$��325,10,325,0q��� $ 1��$��2������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFEEEEEE��� $ 	#FFDADADA�� � Offset$ 0.963Q�� $ 	#FFE7E7E7��$
 0.67Q�� $ 	#00E0E0E0��$	 0.8Q�� $ 2��$��0��.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��
20,-30,0,0q�.+��� $��/Resource/LogoLarge.png��$	��200��$	��200��� $��Right=�$	��Top=�$��	0,10,30,0q�.+��� -   $��40��$��40��  NewFileCreationCloseButton#��[ �� $��/Resource/Close.png��$��10��$��10���� -    ��Name$ 	UpperPara��$��1��$��my reflection.��$��34(�$��SemiBold"����#616161$
��Left=�$��Bottom=�$��
15,0,0,141q�����  �� 
TargetType" ��.��� 	 �� Property	 �@J�Opacity
 �� Value$	
 0.0Q��� 	 �@��BitmapScalingMode$
 HighQuality=��� 	 �@J�IsHitTestVisible$
 False�� �� Triggers :�  :� RoutedEvent$ UserControl.Loaded��.[��� .���� .��W�  �� TargetProperty$ Opacity�	 W� To 1.0 g� Duration$ 0:0:0.5D� g� 	BeginTime 0:0:1.0�� $ 0��$��1��$
��Left=�$��Bottom=�$��	15,0,0,50q�.+��� -   $
 Para��$�� ��$	��423��$��34(�$��SemiBold"����#616161$
��Wrap=�� $ 1��$��1��.+�� $��Right=�$��Bottom=�$	��130��$��50��$��
0,20,30,20q�.+��� -    ��Name$ StartButton��  NewFileCreationButton#��[ �� $��Start��  DefaultText#��[ $��Right=�$��Center=�$��0,0,20,0q� J� IsHitTestVisible$ False���� $$��/Resource/StartWritingIcon.png��$
��Left=�$��Center=�$��
10,9,10,11q� � 	Resources  T� X T� Y��%?  '�       �� " �.��� 	 �@J�Opacity$	
 0.0Q��� 	 �@J�RenderTransformOrigin$
 0.5,0.5&��� 	 � z�
 T� $	 125Q�$ 0Q� :� $ UserControl.Loaded��.[��� .���� 	 0:0:4.��W� $ Opacity� 1.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.5D�