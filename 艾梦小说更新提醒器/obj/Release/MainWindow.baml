   M S B A M L   `   `   `  ���� V  R艾梦小说更新提醒器, Version=1.1.1.1, Culture=neutral, PublicKeyToken=null#clr-namespace:MyWindowMyWindow  ,    &艾梦小说更新提醒器.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     +controlsclr-namespace:MyWindow        MyWindow.WindowBase    Closing$  WindowBase_Closing��   FocusableChanged$! WindowBase_FocusableChanged�� � Title$! 艾梦小说更新提醒器��$	��650��$	��800�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=� � Icon$ Images/clog.png�� � 	Resources    
myBtnStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType � ColumnDefinitions ��Name ��Name �� RadiusY �� RadiusX J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  bookMsgDetails A�Name
 � Tag �� Mode �� ImageSource �Name
 � Tag��%�  &	        &	 �    �� " ��.���  � ��	 �� "
 ��.R��  �� $ columnDefinition1��$��0.982* ��� $ columnDefinition��$��0.018* �.+��� $ 	rectangle��$ 2��$ 2�����	#FF6dcff6$��2����Z� �� #��z����. # z #��z�� P�  �@J�	IsFocused . . ��� $ 	rectangle��	 Z�  � ���� $ 	rectangle�� � ��	 �	#ff00aeefP�  �@��IsDefaulted . . ��� $ 	rectangle�� � ��	 �	#FF00aeef�� $ 	rectangle��	 Z�  � ��P�  �@J�IsMouseOver . . ��� $ 	rectangle��	 Z�  � ���� $ 	rectangle�� � ��	 �	#FF00aeefP�  � �� . . ��� $ 	rectangle��	 Z�  � ���� $ 	rectangle�� � ��	 �	#FF6dcff6�� $ columnDefinition�� � ��$
	 Auto ��� $ columnDefinition�� � ��$	 0���� $ columnDefinition1�� � ��$	 * �P�  � {� .  . ��� $ 	rectangle��	 Z�  � ���� $ 	rectangle��	 Z�  � ���� .R��� ���#ccc$��0,0,0,1q�._�� .+�A� $ ellipseWithImageBrush�����	#FFcccccc$
��Left=�$	��110��$��	44,18,0,0q�$	��Top=�$	��110�� �� *Id+$ TwoWay=�����  �� *ImgUrl+$ TwoWay=��� $��BookName��-   $
��Left=�$	��330��$��206,40,0,77q����� *Name+$ TwoWay=� �� *Id+$ TwoWay=��� $��lastChapter��-   $
��Left=�$	��330��$��206,80,0,29q����� *LastChapter+$ TwoWay=� �� *Id+$ TwoWay=��� $��IsRead��$	��330��$��206,120,0,29q����	#FFF10D0D���� *IsRead+$ TwoWay=� �� *Id+$ TwoWay=��� $
��Left=�$
��Wrap=�$	��104��$��602,122,0,29q� �� *Id+$ TwoWay=��� $ HypeLinkMore��-    �� *Name+$ TwoWay=�.�查看贴吧更新情况.���� $
��Left=�$	��800��$��0,0,0,0q�.+�� $��80��.+��� -    ��Name$ MoreDingyue��$��更多订阅��#���   $��80,15,555,15q� J� RenderTransformOrigin$ 	0.485,0.9&��� $��筛选书籍：��$
��Left=�$��
442,28,0,0q�$	��Top=�$ 1.129,-0.253&��� -   $��	CheckBook��$
��Left=�$��
546,28,0,0q�$	��Top=�$	��120�����	#FF04D3D3���	#FFFBFBFB��  �� Orientation$ 
Horizontal=�$��0,0,0,0q�$	��800��$	��435��$	��Top=�.+��� -    ��Name$ listBox��#��[ $	��800��$	��435��$	��Top=�� $��0,0,0,0q�$	��100��.+��� -   $ TopPage��$��	上一页��#���   $��150,25,535,30q��� -   $ DownPage��$��	下一页��#���   $��535,25,150,30q�