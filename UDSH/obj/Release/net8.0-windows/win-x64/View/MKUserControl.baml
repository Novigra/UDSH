   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View      UDSH.View.MKUserControl   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  #localclr-namespace:UDSH.View  .��� -     �Name$  
ParentGrid�� � RowDefinitions �� $��40 ��� .+��� $��0�� K� ZIndex$ 2��   MainTopHeaderCSFSCB#��[  ��  �� Orientation$ 
Horizontal=�$��5,0,0,0q�$ 2��.+�� .+��� $��5q����� *
SaveButton+�� $��7q�$(��"/Resource/White/SaveFile-White.png��� $��-2,0,0,0q�.+��� $��5q����� *DeleteButton+�� $��7q�$*��$/Resource/White/DeleteFile-White.png���� ���#FFFFFF$��2,3,0,3q� �� RadiusX$ 3�� �� RadiusY$ 3��$��3��� .+��� $��5q����� *	AlignLeft+�� $��6q�$#��/Resource/White/AlignLeft.png��� .+��� $��5q����� *AlignCenter+�� $��6q�$"��/Resource/White/AlignMid.png��� .+��� $��5q����� *
AlignRight+�� $��6q�$$��/Resource/White/AlignRight.png���� ���#FFFFFF$��2,3,0,3q�$ 3��$ 3��$��3��� $��-3,0,0,0q�.+��� $��7q����� *
BoldText+�� $��7,7,7,5q�$��/Resource/White/Bold.png��� $��	-16,0,0,0q�.+��� $��7q����� *
ItalicText+�� $��7,7,7,4q�$ ��/Resource/White/Italic.png��� $��	-10,0,0,0q�.+��� $��7q����� *StrikethroughText+�� $��7,7,7,4q�$'��!/Resource/White/Strikethrough.png��� $��	-10,0,0,0q�.+��� $��7q����� *UnderlineText+�� $��7q�$#��/Resource/White/Underline.png���� ���#FFFFFF$��2,3,0,3q�$ 3��$ 3��$��3��� .+��� $��5q��� $��8q�$%��/Resource/White/Photo-White.png��� .+��� -    ��Name$ 
NoteButton��$��5q�*  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$  PreviewMouseLeftButtonDown��*  $Microsoft.Xaml.Behaviors.TriggerBase	   Actions.	 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  
   PassEventArgsToCommand$

 True��   Command �� *	AddNote+  $ Loaded��.	    �� *NoteButtonLoaded+   CommandParameter ��  �� ElementName$ 
NoteButton���� $��8q�$'��!/Resource/White/AddNote-White.png��� $��1��$ 1�� � ColumnDefinitions �� $��* ��� $	��816 ��� $��* �.+�� -   $  	PaperGrid��$��1�� J� RenderTransformOrigin$ 0.5,0.5&�.+��� $ 0��  MainPaperCSFSCB#��[ �� -    ��Name$ RTB_ScrollViewer��  PageScrollViewer#��[ $��Visible=�$ 2��$��	0,30,3,40q�.���� -    ��Name$ MKContentLayout�� 
 MKText#��[  �� 	IsEnabled$
 True��$��	30,0,30,0q� �� AcceptsReturn$
 True�� �� 
AcceptsTab$
 True��   $ Loaded��.	    �� *RichTextLoaded+ �� $ MKContentLayout��  $ GotMouseCapture��.	    �� *ParagraphFocus+ �� $ MKContentLayout��. �/�  /� TextAlignment$
 Left=� J� InputBindings �� 
 �� Key$ Enter�� �� Command �� *PressedEnter+��  �� 	Modifiers$
 Ctrlf�$ A�� �� *	DeleteAll+   $ ScrollChanged��.	    �� *ScrollChanged+ �� $ RTB_ScrollViewer��z�T�  T� X$ 0Q� T� Y$ 20Q�����  �� 
TargetType" � �� Triggers ��  �� Binding �� *FirstStartAnimPlayed+ �� Value True  N� EnterActions  �� ! �� BeginStoryboardName$! DownAnim���� " ��Name$" UpAnim��.���� .��W� # �� TargetProperty$,# &RenderTransform.(TranslateTransform.Y)�	$ W� To$ 0.0% g� Duration$% 0:0:0.3D�& g� 	BeginTime& 0:0:0.3��  �� *FirstStartAnimPlayed+	 False  �� $! UpAnim���� $" DownAnim��.���� .��W� $,# &RenderTransform.(TranslateTransform.Y)�$ 20.0$% 0:0:0.3D�& 0:0:0.3   $ Loaded��.	    �� *
GridLoaded+ �� $ 	PaperGrid��� -   $  SideContentGrid��$��1��$
��Left=�$ 1��   $ Loaded��.	    �� *SideContentGridLoaded+ �� $ SideContentGrid���� $��2��  MainBackgroundCSFSCB#��[    $ Loaded��.	    �� *ParentGridLoaded+ �� $ 
ParentGrid��  $ 	MouseMove��.	   $

 True�� �� *NoteButtonMouseMove+  $ PreviewMouseLeftButtonUp��.	   $

 True�� �� *StopAddingNoteProcess+