   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.MVVM	UDSH.MVVM  1clr-namespace:UDSH.ConvertersUDSH.Converters  W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089%   UDSH.Converters.HeightConverter   WidthConverter  �� 
TargetType  PageScrollViewer �� Property �� Value �� 
TargetType � ColumnDefinitions � RowDefinitions �� CanContentScroll ��Name K� ZIndex  MainBlankSCB	 J� Opacity+  %UDSH.MVVM.ParagraphMouseClickBehavior
 ��Name �� VerticalOffset � Value �� ScrollableHeight � Maximum �� ViewportHeight �� ViewportSize* �� #ComputedVerticalScrollBarVisibility  	PaperPage �� Orientation �� HorizontalOffset �� ScrollableWidth �� ViewportWidth, �� %ComputedHorizontalScrollBarVisibility  OpenFilesScrollViewer  PaperPageTest  OpenFilesListScrollViewer  OpenFilesList  SideContentScrollViewer  	 TreeviewSideContentScrollBar 
 ContentScrollViewer  HiddenContentScrollViewer �� BasedOn  TestContentScrollViewer J� IsHitTestVisible �� RelativeSource �� AncestorType �� Path �� 	Converter �� ConverterParameter �� 
StartPoint �� EndPoint  � Color! � Offset" �� Triggers# �� Binding$ �� Value% N� EnterActions& �� TargetProperty	' W� To( g� Duration) N� ExitActions�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     !localclr-namespace:UDSH.MVVM  &Convclr-namespace:UDSH.Converters  %�  &	        &	      7  7  7  &	 }    7  7  &	 �    7  7  &	 �    7 	 7 	 &	
 A    7 	 7 	 &	 7
    7 	 7 	 &	 �    7   7 	 7 	    �� "  ��.���  �@�OverridesDefaultStyle$
 True����  � �� �� " ��.R��  �� $
��Auto ���  �� �� $
��Auto �.+��� $��1��$��0q�._��� # z���� $ CollisionHitRect��$��1��$ 0��$��Right=�$��30��8��  $		 0.2Q����� "  ��.���  �@ MouseEnterCommand �� *SideScrollCollisionMouseEnter+��  �@ MouseLeaveCommand �� *SideScrollCollisionMouseLeave+�� $
 PART_VerticalScrollBar��$��1��$��Right=����#D9D9D9# z # z # z #y�z 8�� �� $
 PART_HorizontalScrollBar��$ 
Horizontal=�$��1��$��1��# z # z # z #y�z 8�� �� "  ��.���  �@�OverridesDefaultStyle$
 True����  � ��
 ���� �� ! �@ ScrollViewerLoadedCommand �� *HorizontalScrollViewerLoaded+��  � �� �� " ��.R�� 
������  �� $
��Auto ���  �� �� $
��Auto �.+��� $��1��
������ $��0q�._��� # z���� $
 PART_VerticalScrollBar��$��1��$��Right=�
������ # z # z # z #y�z 8��  �� $
 PART_HorizontalScrollBar��$ 
Horizontal=�$��1��$��1��# z # z # z #y�z 8�� 
������ $��	0,-10,0,0q��� "  ��.���  �@�OverridesDefaultStyle$
 True���� ! �@ ScrollViewerLoadedCommand �� *)'VerticalOpenFilesListScrollViewerLoaded+��  � �� �� " ��.R��  �� $
��Auto ���  �� �� $
��Auto �.+��� $��1��$��0q�._��� # z��$��0,0,5,0q��� $
 PART_VerticalScrollBar��$��1��$��Right=����#D9D9D9# z # z # z #y�z 8��  $��0,5,5,5q��� $
 PART_HorizontalScrollBar��$ 
Horizontal=�$��1��$��1��# z # z # z #y�z 8�� �� "  ��.���  �@�OverridesDefaultStyle$
 True����  � �� �� " ��.R��  �� $
��Auto ���  �� $
��Auto ��� .+��� $��1��$��1��$��0q�$��0,5,0,0q�._��� # z���� $
 PART_VerticalScrollBar��$��1��$��1��$��Right=����#D9D9D9# z # z # z #y�z 8��  �� $
 PART_HorizontalScrollBar��$ 
Horizontal=�$��0��$��1��# z # z # z #y�z 8�� $��0,-5,0,0q��� "  ��.���  �@�OverridesDefaultStyle$
 True���� ! �@ ScrollViewerLoadedCommand �� *ContentScrollViewerLoaded+��  � �� �� " ��.R��  �� �� $
��Auto � �� �� $
��Auto �.+��� $!��PART_ScrollContentPresenter��$��0��$��0��# z��#��z��$��0q��� $��PART_VerticalScrollBar��$��0��$��1��$ Vertical=�# z # z # z #y�z 8��  �� $��PART_HorizontalScrollBar��$��1��$��0��$ 
Horizontal=�# z # z # z #y�z 8�� �� "  ��.���  �@�OverridesDefaultStyle$
 True���� ! �@ ScrollViewerLoadedCommand �� *!HiddenContentScrollViewerLoaded+��  � �� �� " ��.R��  �� �� $
��Auto � �� �� $
��Auto �.+��� $!��PART_ScrollContentPresenter��$��0��$��0��# z��#��z��$��0q��� $��PART_VerticalScrollBar��$��0��$��1��$ Vertical=�# z # z # z #y�z ���� "  ��8   .���  �@ ScrollBarLoadedCommand �� *VerticalScrollBarLoaded+�� $��PART_HorizontalScrollBar��$��1��$��0��$ 
Horizontal=�# z # z # z #y�z 8�� �� "  ��.���  �@�OverridesDefaultStyle$
 True���� ! �@ ScrollViewerLoadedCommand �� *#!VerticalContentScrollViewerLoaded+��  � �� �� " ��.R��  �� �� $
��Auto � �� �� $
��Auto �.+��� $!��PART_ScrollContentPresenter��$��0��$��0��# z��#��z��$��0q��� $ False��$��0��$��0��$
��Left=�$��Bottom=�$��30������  ��  ListView$ ActualWidth�8   $ 9������ $ 0.5,1&�$ 0.5,0&�.��� $  #DBDBDB��� $  	#00DBDBDB��$! 1Q����� "  ��" �� # �� *ReachedContentBottom+$ True% �� .���� .��W� $& Opacity�' 0$( 0:0:0.2D�) �� .���� .��W� $& Opacity�' 1$( 0:0:0.2D��� $��PART_VerticalScrollBar��$��0��$��1��$ Vertical=�# z # z # z #y�z 8�� �� $��PART_HorizontalScrollBar��$��1��$��0��$ 
Horizontal=�# z # z # z #y�z 8�� 