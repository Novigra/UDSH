   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   SelectedUnfocusedColor  
GlyphColor  GlyphMouseOver  �� 
TargetType  SideContentTreeView �� Property �� Value T� X T� Y �� 
TargetType �� CornerRadius ��Name �� PanningMode  SideContentScrollViewer	 J� SnapsToDevicePixels
 �� Triggers �� Binding �� Value N� EnterActions �� TargetProperty	 W� To g� Duration N� ExitActions  Search_SideContentTreeView  ExpandCollapseToggleStyle'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName R� KeyTime   System.Windows.Visibility   Hidden R� Value   Visible F�Name �� Color  TreeViewItemFocusVisual �� StrokeDashArray J� Opacity �� Path �� RelativeSource  �� AncestorType! � ColumnDefinitions" � RowDefinitions8   2System.Windows.Media.Animation.EasingColorKeyFrame# �� KeyTime ! MainHoveredControlButtonColor$ �� Value% d�Name& �� 	ClickMode' d� 	IsChecked( ��Name) �� Padding* ��Name+ ��Name, �� Triggers- P� Property. P� Value/ �� 
TargetName0 ]� 
Conditions1 �� Property2 �� Value�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %  &	        &	      &	 &     &	 [     7  &	 X    7  &	 X    &	 �    'Q� �    7  7  7   7  ��	#FFDDDDDD����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124���� "  R�.���  �@�OverridesDefaultStyle$
 True����  �@J�SnapsToDevicePixels$
 True����  � ��$
 Auto=���  � ��$
 Auto=���  �@J�Opacity$ 1Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $	 0.0Q�$	 0.0Q���  � �� �� " R�.R��� $��Border��
������ $ 1��$��0q�._��� $ 
TreeScroll��$
��Auto=�$
��Auto=�$
 Both=�$��4q�$��False��$��0q�8��  .���� ._��� #	 z	 
 ��  �� *
SearchText+   �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.3D�W� $ Opacity� 1.0$ 0:0:0.3D� �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 10.0$ 0:0:0.3D�W� $ Opacity� 0.0$ 0:0:0.3D��� "  R�.���  �@�OverridesDefaultStyle$
 True����  �@J�SnapsToDevicePixels$
 True����  � ��$
 Auto=���  � ��$
 Auto=���  �@J�Opacity$ 0Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $ -10.0Q�$	 0.0Q���  � �� �� " R�.R��� $��Border��
������ $ 1��$��0q�._��� $ 
TreeScroll��$
��Auto=�$
��Auto=�$
 Both=�$��4q�$��False��$��0q�8��  .���� ._��� #	 z	 
 ��  �� *
SearchText+   �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)�	 -10.0$ 0:0:0.3D�W� $ Opacity� 0.0$ 0:0:0.3D� �� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.3D�W� $ Opacity� 1.0$ 0:0:0.3D��� "  d�.���  � |�$ False����  � �� �� " d�.R�� $��15��$��13��
������    CheckStates��.   Checked��. �� .��T� $ (UIElement.Visibility)�$ 	Collapsed��..�i� $ 0��# Z T� $ (UIElement.Visibility)�$ Expanded��..�i� $ 0��# Z   	Unchecked��  Indeterminate��.+�F� $ 	Collapsed��$
��Left=�$��Center=�$��1,1,1,1q�"���� 	=     � z  	= � 	=  z 	���� # �  F� $ Expanded��$
��Left=�$��Center=�$��1,1,1,1q�"����     	= � z  	= � 	=  z 	$y�Hidden=����� # �  �� .���  � �� �� .R��� ._��� $��0,0,0,0q�$��5��
���   �$	 1 2R�$ 0Q��� "  Q�.���  � ��
 ���� ��  � �� �� $  HorizontalContentAlignment� �� "  ����  � �� �� $ VerticalContentAlignment� �� "  ����  � ��$ 1,0,0,0q���  � ��# � ����  � ��$ DemiBold"���  �@�FocusVisualStyle8   ��  � �� �� " Q�.R�� ! �� $��19��$
��Auto ��� $
��Auto ��� $��* �" �� $
��Auto ���    SelectionStates��.   Selected��. �� .���� $ Bd��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $# 0��8$    
Unselected��  SelectedInactive��. �� .���� $ Bd��$0 *(Panel.Background).(SolidColorBrush.Color)�.g�  $# 0��8$    ExpansionStates��.   Expanded��. �� .��T� $ (UIElement.Visibility)�$ 	ItemsHost��..�i� $ 0��# Z   	Collapsed��.+�d� $% Expander��8�� $& Press=�' �� *
IsExpanded+ �� *TemplatedParent+���� *DataContext.TestButton+ ��   TreeView�� $( Bd��$��1��#��z��#��z��#��z��#) z��$ 5��._��� $* PART_Header��$��Header��#��z���� $+ 	ItemsHost��$��1��$��1��$��2��$y�	Collapsed=�, P� - �@��HasItems$. false��. ��� $/ Expander�� � y�$ Hidden=�]� 0 �� 1 � ��$2 false���� 1 � ��$
2 Auto��./��� $/ PART_Header�� � ��$ 75��]� 0 �� 1 � ��$2 false���� 1 � ��$
2 Auto��./��� $/ PART_Header�� � ��$ 19��