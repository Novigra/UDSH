   M S B A M L   `   `   `  ���� ?  ;UDSH, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:UDSH.View	UDSH.View  (    "UDSH.View.NewProjectCreationWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  #localclr-namespace:UDSH.View    � Title$  NewProjectCreationWindow��$	��550��$
��1000�� � WindowStyle$
 None=�
������  � AllowsTransparency$
 True�� � WindowStartupLocation$ CenterOwner=� J� Clip ��  �� Rect$ 0,0,1000,550� �� RadiusX$ 10Q� �� RadiusY$ 10Q�.��� -    �Name$ 
WindowGrid��   NewFileProcessBackgroundSCB#��[  	 � RowDefinitions	 �� $
��auto ��� $
��auto ��� $	��.8* ��� $	��.2* �.+�� $��0��.+��� 
 �� Orientation$
 
Horizontal=�$
��Left=�$	��Top=�$��	10,10,0,0q�.+��� $$��/Resource/FolderNewContent.png��$��30��$��30���� $"��New Project Creation Process��$��17(�$��Center=�  DefaultText#��[ $��8,0,0,0q�� $��Right=�$	��Top=�$��	0,10,30,0q�.+��� $��40��$��40��  NewFileCreationCloseButton#��[ ���� *CloseWindow+����  �� ElementName$ 
WindowGrid���� $��/Resource/Close.png��$��10��$��10���� $��1��$
 
Horizontal=�$��15,5,0,0q�.+��� $!��Let's Create Your Project, ��$��40(�#��[ �� -    ��Name$ UserName��$�� ��$��40(�#��[ � $��2��$��	15,15,0,0q�.+��� $
 
Horizontal=�$
��Left=�$��0,0,0,0q�.+��� $
 Vertical=�$��0,7,0,0q�.+��� $��Project Name�� 	 LRow1#��[ �� $��	Secure It�� 	 LRow2#��[ $��0,43,0,0q�� $��0,44,0,0q�.+��� $��Password��  SecureOptionText#��[ $��0,0,0,0q��� $��Project Version�� 	 LRow3#��[ $��0,67,0,0q�����  �� 
TargetType" �.���  �� Property �@J�RenderTransformOrigin �� Value$ 0.5,0.5&���  � z� T�  T� X$ 0Q� T� Y$	 -67Q� �� Triggers ��  �� Binding �� *	CanSecure+ �� Value True N� EnterActions �� .���� .��W�  �� TargetProperty$, &RenderTransform.(TranslateTransform.Y)�	 W� To 0.0 g� Duration$ 0:0:0.3D� N� ExitActions �� .����  g� 	BeginTime 0:0:0.3.��W� $, &RenderTransform.(TranslateTransform.Y)�	 -67.0$ 0:0:0.3D���   NewProjectBar#��[ $��6��$��	20,-5,0,0q�  	BorderLBG#��[  �� RadiusX$ 2�� �� RadiusY$ 2���� $
 Vertical=�$��20,0,0,0q�.+��� -    ��Name$ TitleBorder��$
��Left=� 	 SearchBarSCB#��[	 $��2q� 
 SearchBarBorderSCB#��[
  �� CornerRadius$ 4��$��40�� � FocusVisualStyle Z� ._��� $
 
Horizontal=�$��Stretch=�$��Center=� Z� .+�� $��4,0,4,0q�$��Center=�.+��� -   $ HighlightText��$�� ��  SearchTextHeaderSCB#��[   K� ZIndex$  0��$��20(�$��SemiBold"�$|�False������ " ��.���  � y�$ 	Collapsed=� ��  �� *NewProjectName+  .a���  � y�$ Visible=�*  $Microsoft.Xaml.Behaviors.Interaction!   Triggers! +  %Microsoft.Xaml.Behaviors.EventTrigger  "   	EventName$" Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase#   Actions.# 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  $   Command$ �� *
LoadedText+%   CommandParameter% �� $ HighlightText���� -   & ��Name$& UserSearchBox��  MainBlankSCB#��[ $��20(�$
��Bold"�$	��400��$  1��$��0q����� *NewProjectName+' �� UpdateSourceTrigger$' PropertyChanged=� Z� .��!   $" TextChanged��.#   $ �� *ProjectNameChanged+% �� $ UserSearchBox��.������ " ��.���  �@J�Opacity$	 0.0Q���  � ��$ 50����  �@J�RenderTransformOrigin$ 0.0,0.5&���  � z� T� $ 67Q�$ 0Q� :� ( :� RoutedEvent$( Border.Loaded��.[��� .���� .��W� $, &RenderTransform.(TranslateTransform.X)� 0.75$ 0:0:0.3D� 0:0:0.7W� $ Opacity� 1$ 0:0:0.3D� 0:0:0.7W� $ Width� 412$ 0:0:0.3D� 0:0:1.2!   $" Loaded��.#   $ �� *TitleLoaded+% �� $ HighlightText���� $
 
Horizontal=�$��0,35,0,0q�$  1��.+��� $��0,0,0,0q�) d� 	IsChecked) �� *	CanSecure+$' PropertyChanged=�!   $ " PreviewMouseLeftButtonDown��.#   $ �� *CheckboxMouseLeftButtonDown+% �� $ HighlightPassText��� $��0,25,0,0q�$  0��.+��� -   $ 
PassBorder��$
��Left=�#��[	 $��2q�#��[
 $ 4��$��40��$��	0,10,0,61q� Z� * J� IsHitTestVisible* �� *	CanSecure+._��� $
 
Horizontal=�$��Stretch=�$��Center=� Z� .+�� $��4,0,4,0q�$��Center=�.+��� -   $ HighlightPassText��$�� ��#��[ $  0��$��20(�$��SemiBold"�$|�False������ " ��.���  � y�$ 	Collapsed=� ��  �� *NewPassword+  .a���  � y�$ Visible=�!   $" Loaded��.#   $ �� *
LoadedText+% �� $ HighlightText���� -   $& PassText��#��[ $��20(�$
��Bold"�$	��400��$  1��$��0q����� *NewPassword+$' PropertyChanged=� Z� .��!   $" TextChanged��.#   $ �� *ProjectPassChanged+.������ " ��.���  �@J�Opacity$	 0.0Q���  � ��$ 50����  �@J�RenderTransformOrigin$ 0.0,0.5&���  � z� V� .~�T� $ 60Q�$ 0Q� ��  �� *	CanSecure+ True �� .����  0:0:0.3.��W� $ Opacity� 1.0$ 0:0:0.3D�W� $4 .RenderTransform.(TransformGroup.Children)[0].X� 0.0$ 0:0:0.3D�W� $ Width�	 412.0$ 0:0:0.3D� 0:0:0.7 �� .���� .��W� $ Opacity� 0.0$ 0:0:0.3D�W� $4 .RenderTransform.(TransformGroup.Children)[0].X� 60.0$ 0:0:0.3D�W� $ Width� 50.0$ 0:0:0.3D� 0:0:0.4�� $
 
Horizontal=�$��0,84,0,0q�.+���  	 RRow3#��[ ���� *ProjectVersion+���� " �.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $ 0Q�$	 -67Q� ��  �� *	CanSecure+ True �� .���� .��W� $, &RenderTransform.(TranslateTransform.Y)� 0.0$ 0:0:0.3D� �� .���� .��W� $, &RenderTransform.(TranslateTransform.Y)�	 -67.0$ 0:0:0.3D� 0:0:0.3� $��3��.+�� $��Right=�$	��210��$��
0,20,30,20q�.+���   NewFileCreationButton#��[ ���� *NewProjectCreation+���� $ 
WindowGrid���� $��Create Project��#��[ $��Right=�$��Center=�$��0,0,20,0q�$* False���� $$��/Resource/StartWritingIcon.png��$
��Left=�$��Center=�$��
10,9,10,11q�+ � 	Resources+ , ^� 
Conditions- �� Binding. �� Value/ �� BeginStoryboardName0 ��Name��%�  '�       �� " �.���  �@J�Opacity$	 0.0Q���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $	 125Q�$ 0Q� ^� , �� - �� *	CanSecure+	. False�� - �� *ContainsTitleText+. True �� $
/ Dev2���� $
/ Dev3���� $
/ Dev4���� $
/ Dev5���� $
0 Dev1��.���� .��W� $ Opacity� 1.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.5D�^� , �� - �� *	CanSecure+	. False�� - �� *ContainsTitleText+	. False �� $
/ Dev1���� $
/ Dev3���� $
/ Dev4���� $
/ Dev5���� $
0 Dev2��.���� .��W� $ Opacity� 0.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)�	 125.0$ 0:0:0.5D�^� , �� - �� *ContainsPassText+	. False�� - �� *	CanSecure+. True�� - �� *ContainsTitleText+. True �� $
/ Dev1���� $
/ Dev2���� $
/ Dev4���� $
/ Dev5���� $
0 Dev3��.���� .��W� $ Opacity� 0.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)�	 125.0$ 0:0:0.5D�^� , �� - �� *ContainsPassText+. True�� - �� *	CanSecure+. True�� - �� *ContainsTitleText+	. False �� $
/ Dev1���� $
/ Dev2���� $
/ Dev3���� $
/ Dev5���� $
0 Dev4��.���� .��W� $ Opacity� 0.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)�	 125.0$ 0:0:0.5D�^� , �� - �� *ContainsPassText+. True�� - �� *	CanSecure+. True�� - �� *ContainsTitleText+. True �� $
/ Dev1���� $
/ Dev2���� $
/ Dev3���� $
/ Dev4���� $
0 Dev5��.���� .��W� $ Opacity� 1.0$ 0:0:0.5D�W� $, &RenderTransform.(TranslateTransform.X)� 0.0$ 0:0:0.5D�+ 1 W� From��%!  '�       �� " �.���  �@J�RenderTransformOrigin$ 0.5,0.5&���  � z� T� $ 0Q�$ 0Q� :� $( Window.Loaded��.[��� .���� .��W� $ Opacity�1 0.0 1.0$ 0:0:0.3D�W� $, &RenderTransform.(TranslateTransform.Y)�	1 100.0 0.0$ 0:0:0.3D�!   $" Loaded��.#   $ �� *
GridLoaded+% �� $ UserName��