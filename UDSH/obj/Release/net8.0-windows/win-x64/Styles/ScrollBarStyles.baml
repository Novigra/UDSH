   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.MVVM	UDSH.MVVM  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ScrollBarLineButton �� Property �� Value �� 
TargetType ��Name �� CornerRadius  RepeatButtonNormalSCB'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard	 �� TargetProperty  RepeatButtonPressedColor	
 �� To g� Duration F�Name �� RelativeSource  RepeatButtonArrowColor �� Color  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions  ScrollBarBackgroundSCB ]�Name ]� DecreaseRepeatButton ]� Thumb  MainBlankColor 	 ScrollBarTrackThumbColor ]� IncreaseRepeatButton �� ScaleX �� ScaleY �� Triggers �� Binding �� Value N� EnterActions �� BeginStoryboardName ��Name	 W� To 
 HorizontalScrollBar � ColumnDefinitions �� 
StartPoint �� EndPoint  � Color! � Offset  	PaperPage+  %UDSH.MVVM.ParagraphMouseClickBehavior" P� Property# P� Value  ScrollBarLineButtonTest   MainOpenPageRepeatButtonLeft8  2System.Windows.Media.Animation.EasingColorKeyFrame$ �� KeyTime% �� Value  ScrollBarPageButtonTest  ScrollBarThumbTest  VerticalScrollBarTest  HorizontalScrollBarTest  PaperPageTest  ScrollBarLineButtonOP  ScrollBarPageButtonOP  ScrollBarThumbOP  VerticalScrollBarOP  HorizontalScrollBarOP  OpenFilesList  TSScrollBarLineButton  TSScrollBarPageButton  TSScrollBarThumb  TSVerticalScrollBar  TSHorizontalScrollBar   TreeviewSideContentScrollBar�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     !localclr-namespace:UDSH.MVVM  % .  &	        7  7  &	 �    &	 �    &	 �    7  7   7  7  7  7 	 7  7   &	
 �    7   7  7  7  7   &	 >    7 
 7  &	     7  &	 �    &	 �    &	 h    7  7  7  7  7  7 	 7  7  &	 �    7  7  7  7  7  &	 y    7  7  &	 �    &	 �    &	 L    &	 &    7  7  7  7  7  7 	 7  7  &	 V    7  7  7  7  7  &	 �!    7  7  &	 #    &	 �$    &	 |%    &	 V&    7  7  7  7  7 	 7  7  &	 �(    7  7  7  7  7 	 7  7  &	 �*    7  7  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � |�$ false����  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��1q�8��     CommonStates��.   
Normal��  	MouseOver��  Pressed��. �� .���� $2	 ,(Control.Background).(SolidColorBrush.Color)�8
  $ 0:0:0.1D�  Disabled��._�F� $ Arrow��$��Center=�$��Center=����� *	Content+ �� *TemplatedParent+���� # �  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " n�.R��� $ 2��#��z��#��z��$��1q��� " ��.R��  �� $��70���� $��0.00001* ��� $��70��.+��� $��3��$ 2��8��  �� $��0��8�� $��70��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $ 
PART_Track��$��1��$
��true�� �� 8�� $��ScrollBar.PageUpCommand�� n� 8�� $��1,0,1,0q����� 8  ���� 8   �� 8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��70��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z������ "  �.���  �@J�RenderTransformOrigin$ 1.0,0.5&���  � z� �� $	 0.5Q�$	 1.0Q� ��  �� *IsInsideScrollBarHitCollision+ True �� $ CloseScroll���� $ 	OffScroll���� $ 
OpenScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 1.0$ 0:0:0.2D���  �� *IsInsideScrollBarHitCollision+	 False �� $ 
OpenScroll���� $ OnScroll���� $ CloseScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 0.5$ 0:0:0.2D���  �� *IsInsideScrollBar+ True �� $ 	OffScroll���� $ CloseScroll���� $ OnScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 1.0$ 0:0:0.2D���  �� *IsInsideScrollBar+	 False �� $ 	OffScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 0.5$ 0:0:0.2D��� " ��.R��  �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $ 
PART_Track��$��1��$��False�� �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� $��0,1,0,1q����� $	 0,0&�$	 1,0&���� � $	  Red��$	! 0.0Q�� $	  Red��$	! 1.0Q����� $	 0,0&�$	 0,1&���� � $	  Red��$	! 0.0Q�� $	  Red��$	! 1.0Q� �� 8�� $ ��ScrollBar.PageRightCommand���� $��2��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� "  ��.��� " �@ MouseEnterScrollBarCommand �� *SideScrollMouseEnter+�� " �@ MouseLeaveScrollBarCommand �� *SideScrollMouseLeave+��  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true�� P� " �@��Orientation$# 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P� " �@��Orientation$# Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��8  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � |�$ false����  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��0q�8��     CommonStates��.   
Normal��. �� .���� $N	 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $$ 0:0:0.3��$% 	#00DBAD2F��  	MouseOver��. �� .���� $N	 H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $$ 0:0:0.3��$% 	#FFDBAD2F��  Pressed��  Disabled��._�F� $ Arrow��$��Center=�$��Center=����� *	Content+ �� *TemplatedParent+���� # �  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " n�.R��� $ 2��
������ 
������ $��0q��� " ��.R��  �� $��70���� $��0.00001* ��� $��70��.+��� $��3��$ 2��8��  �� $��0��8�� $��70��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $ 
PART_Track��$��1��$
��true�� �� 8�� $��ScrollBar.PageUpCommand�� n� 8�� $��1,0,1,0q����� 8  ���� 8   �� 8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��70��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z������ "  �.���  �@J�RenderTransformOrigin$ 1.0,0.5&���  � z� �� $	 0.5Q�$	 1.0Q� ��  �� *IsInsideScrollBarHitCollision+ True �� $ CloseScroll���� $ 	OffScroll���� $ 
OpenScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 1.0$ 0:0:0.2D���  �� *IsInsideScrollBarHitCollision+	 False �� $ 
OpenScroll���� $ OnScroll���� $ CloseScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 0.5$ 0:0:0.2D���  �� *IsInsideScrollBar+ True �� $ 	OffScroll���� $ CloseScroll���� $ OnScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 1.0$ 0:0:0.2D���  �� *IsInsideScrollBar+	 False �� $ 	OffScroll��.���� .��W� $-	 'RenderTransform.(ScaleTransform.ScaleX)� 0.5$ 0:0:0.2D��� " ��.R��  �� $��70���� $��0.00001* ��� $��70��.+��� $��3��$ 2��
������ �� $��0��8��  $��70��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $ 
PART_Track��$��1��$��False�� �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� 
������ 
������ $��0q�$��0,1,0,1q� �� 8�� $ ��ScrollBar.PageRightCommand���� $��2��8�� $��70��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true�� P� " �@��Orientation$# 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 70����  � ��
 ���� ��  � ��8   P� " �@��Orientation$# Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��8  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � |�$ false����  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��1q�
������    CommonStates��.   
Normal��  	MouseOver��  Pressed��  Disabled��._�F� $ Arrow��$��Center=�$��Center=����� *	Content+ �� *TemplatedParent+���� $ Transparent���� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " n�.R��� $ 5��#��z��#��z��$��1q��� " ��.R��  �� $��0���� $��0.00001* ��� $��0��.+��� $��3��$ 2��8��  �� $��0��8�� $��70��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $ 
PART_Track��$��1��$
��true�� �� 8�� $��ScrollBar.PageUpCommand�� n� 8�� $��1,0,1,0q����� 8  ���� 8   �� 8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��70��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z������ "  �.���  �@J�RenderTransformOrigin$ 1.0,0.5&���  � z� �� $	 0.5Q�$	 1.0Q��� " ��.R��  �� $��18���� $��0.00001* ��� $��18��.+��� $��3��$ 2�����#F0F0F0�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $ 
PART_Track��$��1��$��False�� �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� $��0,1,0,1q����� $	 0,0&�$	 1,0&���� � $	  Red��$	! 0.0Q�� $	  Red��$	! 1.0Q����� $	 0,0&�$	 0,1&���� � $	  Red��$	! 0.0Q�� $	  Red��$	! 1.0Q� �� 8�� $ ��ScrollBar.PageRightCommand���� $��2��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z���� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true�� P� " �@��Orientation$# 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P� " �@��Orientation$# Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��8  �� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � |�$ false����  � �� �� " ��.R��� $ Border��$��1q�$ 2��$��1q�
������    CommonStates��.   
Normal��  	MouseOver��  Pressed��  Disabled��._�F� $ Arrow��$��Center=�$��Center=����� *	Content+ �� *TemplatedParent+���� $ Transparent���� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " ��.R��� 
������ �� "  n�.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true����  � ��$ false����  � |�$ false����  � �� �� " n�.R��� $ 5��#��z��#��z��$��1q��� " ��.R��  �� $��0���� $��0.00001* ��� $��0��.+��� $��3��$ 2�����#ABABAB�� $��0��8��  $��70��$��ScrollBar.LineUpCommand��$��M 0 4 L 8 4 L 4 0 Z��]� $ 
PART_Track��$��1��$
��true�� �� 8�� $��ScrollBar.PageUpCommand�� n� 8�� $��1,0,1,0q����� 8  ���� 8   �� 8�� $��ScrollBar.PageDownCommand���� $��2��8�� $��70��$��ScrollBar.LineDownCommand��$��M 0 0 L 4 4 L 8 0 Z������ "  �.���  �@J�RenderTransformOrigin$ 1.0,0.5&���  � z� �� $	 0.5Q�$	 1.0Q��� " ��.R��  �� $��0���� $��0.00001* ��� $��0��.+��� $��3��$ 2�����#ABABAB�� $��0��8��  $��18��$��ScrollBar.LineLeftCommand��$��M 4 0 L 4 8 L 0 4 Z��]� $ 
PART_Track��$��1��$��False�� �� 8�� $��ScrollBar.PageLeftCommand�� n� 8�� $��0,1,0,1q����� 8  ���� 8   �� 8�� $ ��ScrollBar.PageRightCommand���� $��2��8�� $��18��$ ��ScrollBar.LineRightCommand��$��M 0 0 L 4 4 L 0 8 Z������ "  �.���  �@J�RenderTransformOrigin$ 0.5,1.0&���  � z� �� $	 1.0Q�$	 0.5Q��� "  ��.���  �@J�SnapsToDevicePixels$
 True����  �@�OverridesDefaultStyle$
 true�� P� " �@��Orientation$# 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 18����  � ��8   P� " �@��Orientation$# Vertical=�. ���  � ��$ 18����  � ��$
 Auto����  � ��$ 0,10,0,0q���  � ��8  