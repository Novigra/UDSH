   M S B A M L   `   `   `  ���� W  SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ComboBoxToggleButton  �� 
TargetType � ColumnDefinitions ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName �� Property	 �� Value  ComboBoxTextBox
 �� 
TargetType  MainToggleTextSCB �� 	ClickMode d� 	IsChecked �� Path �� Mode �� RelativeSource J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight K� IsItemsHost-   'System.Windows.Input.KeyboardNavigation    DirectionalNavigation P� 
SourceName �� Triggers �� Padding  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor 	 SelectedBackgroundColor 
 SelectedUnfocusedColor  ControlLightColor  ControlMediumColor  ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush  �� EndPoint! �� 
StartPoint" � Color# � Offset $  ProgressBarIndicatorAnimatedFill   ViewToggleButton'  !System.Windows.VisualStateManager$   VisualStateGroups%  System.Windows.VisualStateGroup%   States   System.Windows.VisualState&   
Storyboard' �� TargetProperty( �� 
TargetName	) W� To* g� Duration	+ �� To, �� Orientation ! DefaultText- ��Name. J� RenderTransformOrigin/ T� X0 T� Y1 �� Angle " SortToggleButton # FilterToggleButton $ AddToggleButton % DFComboBoxTextBox & ViewComboBox8   2System.Windows.Media.Animation.EasingColorKeyFrame2 �� KeyTime3 �� Value4 R� KeyTime   System.Windows.Visibility5   Visible6 R� Value7   Hidden8 d�Name9 �Name: �Name ' SortComboBox ( FilterComboBox ) AddComboBox * DFComboBoxItem�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %%:  &	        &	 �    '�� �    7  7   7  '�� N    7  &	 h    &	 {    &	 �    &	 �    &	 �    &	 �    &		 �    &	
 �    &	  	    &	 	    &	 "	    &	 5	    &	 H	    &	 [	    &	 n	    &	 �	    &	 �	    &	 �	    &	 �	    &	 �	    &	 
    &	 
    &	 (
    &	 7
    &	 J
    &	 Y
    &	 l
    &	 
    &	 �
    &	  w    7 ! &	" !    7 ! &	# �    7 ! &	$ �    7 ! &	% o    &	& �    7  7   7 % &	' �$    7  7 " 7 % &	( s*    7  7 # 7 % &	) W0    7  7 $ 7 % &	* ;6    7 	 7 
 �� "  d�.R��  �� �� $��30 �.+��� $ Border��$��2��$ 2�����#EEEEEE�� $��0��$ 2��$��1q����#EEEEEE�� $��1��$��/Resource/Dropdown.png��$��Center=�$��Center=�$��5,10,8,3q� P�  �@J�IsMouseOver$
 true��. ��� $ Border�� � ��	 �#FFFFFFP�  �@d�	IsChecked$
 trueY�. ��� $ Border�� � ��	 �#E0E0E0P�  � {�$ False��. ���  � ��	 �#888888�� "  ��.R��� $ PART_ContentHost��$|�False��#��z���� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��$	 Disabled=���  � ��$	 Disabled=���  � ��$	 false����  � ��$		 170����  � ��8	   ��  � ��$	 5,-2,5,0q���  � ��	 �� "  ��.R�� .+�d� $��ToggleButton��8�� $��2��$|�false��$ Press=� �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+�� $��ContentSite��$ False��#��z #��z #��z��$��3,0,0,0q�$��Center=�$
��Left=��� $ PART_EditableTextBox��8�� $
��Left=�$��Center=�$��3,0,0,0q�$
|�True�����#EEEEEE���#EEEEEE$y�Hidden=�# z ��Z� � $��Popup��$��Bottom=�#��z $
 True��$|�False��$
��Fade=�.��� $��DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����#EEEEEE$��0q��� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$	 95��P�  �@��
IsGrouping$
 true��. ���  � ��$	 false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$	 4���� $ DropDownBorder�� � ��$	 0,2,0,0q�P�  �@��
IsEditable$
 true��. ���  � ��$	 false���� $ PART_EditableTextBox�� � y�$	 Visible=��� $ ContentSite�� � y�$	 Hidden=� �� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  � ��8	   ��  �@�OverridesDefaultStyle$
	 true����  � ��	 �� "  ��.R��� $��Border��$ 2q�$
 true��._���  P�  �@��IsHighlighted$
 true��. ��� $ Border�� � ��	 �#BFBFBF��	#FFE8EDF9����	#FFC5CBF9����	#FF7381F9����	#FFE8EDF9����	#FFC5CBF9����	#FF888888����	#FFC5CBF9����	#FFDDDDDD����	White����	#FF7381F9����	#FF211AA9����	#FF3843C4����	#FF211AA9����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124����	#FFCCCCCC����	#FF888888����	#FF444444����	#FF888888����	#FF444444����	#FFAAAAAA����	#FF888888����	Black����	#FFC5CBF9����	Black����	#FFC5CBF9����	#FF3843C4���� $  0.5,1&�$! 0.5,0&�.��� #" �  $# 0Q�� #" �  $	# 0.5Q�� #" �  $# 1Q��� $	! 0,0&�$	  1,0&���� � $" 	#000000FF��$# 0Q�� $" 	#600000FF��$	# 0.4Q�� $" 	#600000FF��$	# 0.6Q�� $" 	#000000FF��$# 1Q��� "  d�.R�� $   CommonStates��.%   
Normal��.& �� .��W� $' X�$( ArrowTranslate��) 0$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #C5C5C5$* 0:0:0.3D�  	MouseOver��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  Pressed��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  CheckStates��.%   Checked��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  	Unchecked��  Indeterminate��.+��� $ Border�����#C5C5C5$ 4��$��0q�._��� $, 
Horizontal=�$
��Left=�$��Center=�$��10,0,0,0q�.+��� $��/Resource/View.png��$��25��$��25���� 8��  $
��View��$��18(�$��5,0,0,0q��� $- Arrow��$��/Resource/RightArrow.png��$��15��$��15��$. 0.5,0.5&�$��10,3,0,0q�z�V� .~�T� ArrowTranslate��$/ 0Q�$0 0Q��� $1 90Q��� "  d�.R�� $   CommonStates��.%   
Normal��.& �� .��W� $' X�$( ArrowTranslate��) 0$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #C5C5C5$* 0:0:0.3D�  	MouseOver��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  Pressed��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  CheckStates��.%   Checked��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  	Unchecked��  Indeterminate��.+��� $ Border�����#C5C5C5$ 4��$��0q�._��� $, 
Horizontal=�$
��Left=�$��Center=�$��10,0,0,0q�.+��� $��/Resource/Sort.png��$��25��$��25��$. 0.5,0.5&�z�V� .~�T� $/ 0Q�$0 -2Q��� 8��  $
��Sort��$��18(�$��5,0,0,0q��� $- Arrow��$��/Resource/RightArrow.png��$��15��$��15��$. 0.5,0.5&�$��10,3,0,0q�z�V� .~�T� ArrowTranslate��$/ 0Q�$0 0Q��� $1 90Q��� "  d�.R�� $   CommonStates��.%   
Normal��.& �� .��W� $' X�$( ArrowTranslate��) 0$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #C5C5C5$* 0:0:0.3D�  	MouseOver��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  Pressed��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  CheckStates��.%   Checked��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  	Unchecked��  Indeterminate��.+��� $ Border�����#C5C5C5$ 4��$��0q�._��� $, 
Horizontal=�$
��Left=�$��Center=�$��10,0,0,0q�.+��� $��/Resource/Filter.png��$��25��$��25���� 8��  $��Filter��$��18(�$��5,0,0,0q��� $- Arrow��$��/Resource/RightArrow.png��$��15��$��15��$. 0.5,0.5&�$��10,3,0,0q�z�V� .~�T� ArrowTranslate��$/ 0Q�$0 0Q��� $1 90Q��� "  d�.R�� $   CommonStates��.%   
Normal��.& �� .��W� $' X�$( ArrowTranslate��) 0$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #C5C5C5$* 0:0:0.3D�  	MouseOver��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  Pressed��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  CheckStates��.%   Checked��.& �� .��W� $' X�$( ArrowTranslate��) 2$* 0:0:0.3D��� $2' ,(Control.Background).(SolidColorBrush.Color)�$( Border��+ #A7A7A7$* 0:0:0.3D�  	Unchecked��  Indeterminate��.+��� $ Border�����#C5C5C5$ 4��$��0q�._��� $, 
Horizontal=�$
��Left=�$��Center=�$��10,0,0,0q�.+��� $��/Resource/AddFile.png��$��25��$��25��z�T� $/ 0Q�$0 -1Q��� 8��  $	��Add��$��18(�$��5,0,0,0q��� $- Arrow��$��/Resource/RightArrow.png��$��15��$��15��$. 0.5,0.5&�$��10,3,0,0q�z�V� .~�T� ArrowTranslate��$/ 0Q�$0 0Q��� $1 90Q��� "  ��.R��� $ PART_ContentHost��$|�False��#��z���� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��$
	 Auto=���  � ��$
	 Auto=���  � ��$
	 true����  � ��$		 115����  � ��$	 20����  � ��	 �� "  ��.R�� $   CommonStates��.%   
Normal��  	MouseOver��  Disabled��.& �� .���� $( PART_EditableTextBox��$6' 0(TextElement.Foreground).(SolidColorBrush.Color)�.g�  $2 0��83     
EditStates��.%   Editable��.& �� .��T� $' (UIElement.Visibility)�$( PART_EditableTextBox��..�i� $4 0��#6 Z5 T� $' (UIElement.Visibility)�$( ContentSite��..�i� $4 0��#6 Z7   
Uneditable��.+�d� $8 ToggleButton��8�� $��2��$|�false��$ Press=� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $ PART_EditableTextBox��8�� $
��Left=�$��Bottom=�$��3,3,23,3q�$
|�True��
������ $y�Hidden=�# z ��Z� � $9 Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $: DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����#DADADA���#A7A7A7$��2q��� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$	 95��P�  �@��
IsGrouping$
 true��. ���  � ��$	 false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$	 4���� $ DropDownBorder�� � ��$	 0,2,0,0q��� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��$
	 Auto=���  � ��$
	 Auto=���  � ��$
	 true����  � ��$		 110����  � ��$	 20����  � ��	 �� "  ��.R�� $   CommonStates��.%   
Normal��  	MouseOver��  Disabled��.& �� .���� $( PART_EditableTextBox��$6' 0(TextElement.Foreground).(SolidColorBrush.Color)�.g�  $2 0��83     
EditStates��.%   Editable��.& �� .��T� $' (UIElement.Visibility)�$( PART_EditableTextBox��..�i� $4 0��#6 Z5 T� $' (UIElement.Visibility)�$( ContentSite��..�i� $4 0��#6 Z7   
Uneditable��.+�d� $8 ToggleButton��8�� $��2��$|�false��$ Press=� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $ PART_EditableTextBox��8�� $
��Left=�$��Bottom=�$��3,3,23,3q�$
|�True��
������ $y�Hidden=�# z ��Z� � $9 Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $: DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����#DADADA���#A7A7A7$��2q��� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$	 95��P�  �@��
IsGrouping$
 true��. ���  � ��$	 false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$	 4���� $ DropDownBorder�� � ��$	 0,2,0,0q��� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��$
	 Auto=���  � ��$
	 Auto=���  � ��$
	 true����  � ��$		 115����  � ��$	 20����  � ��	 �� "  ��.R�� $   CommonStates��.%   
Normal��  	MouseOver��  Disabled��.& �� .���� $( PART_EditableTextBox��$6' 0(TextElement.Foreground).(SolidColorBrush.Color)�.g�  $2 0��83     
EditStates��.%   Editable��.& �� .��T� $' (UIElement.Visibility)�$( PART_EditableTextBox��..�i� $4 0��#6 Z5 T� $' (UIElement.Visibility)�$( ContentSite��..�i� $4 0��#6 Z7   
Uneditable��.+�d� $8 ToggleButton��8�� $��2��$|�false��$ Press=� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $ PART_EditableTextBox��8�� $
��Left=�$��Bottom=�$��3,3,23,3q�$
|�True��
������ $y�Hidden=�# z ��Z� � $9 Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $: DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����#DADADA���#A7A7A7$��2q��� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$	 95��P�  �@��
IsGrouping$
 true��. ���  � ��$	 false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$	 4���� $ DropDownBorder�� � ��$	 0,2,0,0q��� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��$
	 Auto=���  � ��$
	 Auto=���  � ��$
	 true����  � ��$		 110����  � ��$	 20����  � ��	 �� "  ��.R�� $   CommonStates��.%   
Normal��  	MouseOver��  Disabled��.& �� .���� $( PART_EditableTextBox��$6' 0(TextElement.Foreground).(SolidColorBrush.Color)�.g�  $2 0��83     
EditStates��.%   Editable��.& �� .��T� $' (UIElement.Visibility)�$( PART_EditableTextBox��..�i� $4 0��#6 Z5 T� $' (UIElement.Visibility)�$( ContentSite��..�i� $4 0��#6 Z7   
Uneditable��.+�d� $8 ToggleButton��8�� $��2��$|�false��$ Press=� �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $ PART_EditableTextBox��8�� $
��Left=�$��Bottom=�$��3,3,23,3q�$
|�True��
������ $y�Hidden=�# z ��Z� � $9 Popup��$��Bottom=�#��z $
 True��$|�False��$��Slide=�.��� $: DropDown��$
 True��#��z #��z .+��� $ DropDownBorder�����#DADADA���#A7A7A7$��2q��� $��4,6,4,6q�$
 True��.���� $
 True��$ 	Contained=� P�  �@��HasItems$ false��. ��� $ DropDownBorder�� � ��$	 95��P�  �@��
IsGrouping$
 true��. ���  � ��$	 false��P� $ Popup�� �@�AllowsTransparency$
 true��. ��� $ DropDownBorder�� �@��CornerRadius$	 4���� $ DropDownBorder�� � ��$	 0,2,0,0q��� "
 ��.���  �@J�SnapsToDevicePixels$
	 true����  �@�OverridesDefaultStyle$
	 true����  � ��	 �� "  ��.R��� $ Border��$ 2q�$
 true��
������ $   SelectionStates��.%   
Unselected��  Selected��.& �� .���� $( Border��$0' *(Panel.Background).(SolidColorBrush.Color)�.g�  $2 0��83     SelectedUnfocused��.& �� .���� $( Border��$0' *(Panel.Background).(SolidColorBrush.Color)�.g�  $2 0��83  ._��� 