   M S B A M L   `   `   `  ���� K  GAfronewsRecorder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:AfronewsRecorderAfronewsRecorder  !    AfronewsRecorder.MainWindow   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:AfronewsRecorder     -     � Title$  Afronews Recorder��$	��620��$	��584�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ CanMinimize=� � Icon$ /record_icon_64.png�� � 	Resources    FocusVisual �� Property �� Value �� StrokeDashArray J� SnapsToDevicePixels  Button.Static.Background	 �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonOpenFolder
 �� 
TargetType �� 
TargetType ��Name ��Name �� Triggers P� Property P� Value �� 
TargetName  FocusVisual1  Button.Static.Background1  Button.Static.Border1   Button.MouseOver.Background1  Button.MouseOver.Border1  Button.Pressed.Background1  Button.Pressed.Border1  Button.Disabled.Background1  Button.Disabled.Border1  Button.Disabled.Foreground1  ButtonRecordStyle��%D	  &	        &	 R     &	 g     &	 |     &	 �     &	 �     &	 �     &	 �     &	 �     &		 �     &	
     7   7  7  7  7  7  7  7 	 &	     &	 j    &	     &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 '    7  7  7  7  7  7  7  7  �� .���  � �� �� .R��� $��2q�$	 1 2R�$
 true��$��1��#��� ���� $	 	#FFDDDDDD���� $	 	#FF707070���� $	 	#FFBEE6FD���� $	 	#FF3C7FB1���� $	 	#FFC4E5F6���� $	 	#FF2C628B���� $	 	#FFF4F4F4���� $	 	#FFADB2B5���� $	 	#FF838383���� "
 ��.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��8  ��  � ��# � ����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z��$
 true��._��� $ contentPresenter��$|�False��#��z��#��z��$
��True��# z #��z�� P�  �@��IsDefaulted$
 true��. ��� $ border��# � �� � ��P�  �@J�IsMouseOver$
 true��. ��� $ border�� � �� �#9b505b�� $ border��8   � ��P�  � ��$
 true��. ��� $ border�� � �� �#9b505b�� $ border��8   � ��P�  � {�$ false��. ��� $ border��8   � ���� $ border��8   � ���� $ contentPresenter��8   � ���� .���  � �� �� .R��� $��2q�$	 1 2R�$
 true��$��1��#��� ���� $	 	#FFDDDDDD���� $	 	#FF707070���� $	 	#FFBEE6FD���� $	 	#FF3C7FB1���� $	 	#FFC4E5F6���� $	 	#FF2C628B���� $	 	#FFF4F4F4���� $	 	#FFADB2B5���� $	 	#FF838383���� "
 ��.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��8  ��  � ��# � ����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z��$
 true��._��� $ contentPresenter��$|�False��#��z��#��z��$
��True��# z #��z�� P�  �@��IsDefaulted$
 true��. ��� $ border��# � �� � ��P�  �@J�IsMouseOver$
 true��. ��� $ border�� � ��
 ���� �� $ border��8   � ��P�  � ��$
 true��. ��� $ border�� � ��
 ���� �� $ border��8   � ��P�  � {�$ false��. ��� $ border��8   � ���� $ border��8   � ���� $ contentPresenter��8   � ��.���  � RowDefinitions �� $��75 ��� $��* ��� $��75 �.+�� ���#7C4D96$��0�� � ColumnDefinitions �� $
��177* ��� $
��407* �.+��� $��Afronews Recorder��$
��Left=�$��Center=�$��30(�
�������$��DemiBold"�$��2��$��	163,0,0,0q�� $��1��.+��  �� $��* ��� $
��1.5* ��� $��* � �� $	��0.* ��� $
��1.5* ��� $��* �.+��� $��/background.png��$
��Fill=�$��3��$��3���� -    ��Name$ ButtonRecord��#���  $��1��$��1��$
��Left=�$��Bottom=�$��	14,0,0,60q�
������ $��0q� � Cursor$
 Hand��.��� 
������ .+�F� $	��120�� �� Stretch$ Uniform=�$��0�����#8C4852���  � FillRule$ EvenOdd=�.��@�  @� RadiusX$	 100Q� @� RadiusY$	 100Q� @� Center$	 0,0&�@� $	 130Q�$	 130Q�$	 0,0&�F� -    F�Name$ CircleRecord��$��60��$ Uniform=�$��0�����#7C4D96��� $ EvenOdd=�.��@� $ 60Q�$ 60Q�$	 0,0&�� ���#7C4D96$��2�� �� $
��1.2* ��� $��* �.+��� -    ��Name$ TextBlockRecordingPath��$��Recording path:��$
��Wrap=�$��Center=�$��0,8,0,0q�$��18(�
�������� $��1��.+��� -   $ TextBlockRecordTime��$
��Wrap=�$
��Left=�$	��Top=�$��8,0,0,0q�$��16(�$��DemiBold"�
���������  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $ ButtonOpenFolder��#��� 
 $	��100��$��0,0,30,8q����#8C4852
�������$��0q�$
 Hand��$��Open Folder���� -   $ ButtonBrowse��#��� 
 $	��100��$��0,0,0,8q����#8C4852
�������$��0q�$
 Hand��$��Browse��