   M S B A M L   `   `   `  ����   艾梦小说更新提醒器3    -艾梦小说更新提醒器.CMessageBoxWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  window��$	��200��$	��300�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$��1,0,1,1q����	#FF3C74D6
������  � AllowsTransparency .  � 	Resources  �� 
TargetType   NormalButtonStyle �� Property �� Value	 �� 
TargetType
 ��Name �� Triggers P� Property P� Value �� 
TargetName  NotNormalButtonStyle  StoryboardWinLoaded �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value   EasingFunction-  'System.Windows.Media.Animation.BackEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime p� Value��%|  &	        &	 �    &	 9    �� " ��.���  � ��
 �������  � ��$ 0q���  � ��
 ���� ��  � �� �	#FF069DD4��  � �� �� "	 ��.R��� $
 border��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 ������� $ border�� � �� �	#FF4C6C99P�  � �� . . ���  � ��
 ������� $ border�� � �� �	#FF2E5387�� " ��.���  � ��
 �������  � ��$ 0q���  � ��
 ���� ��  �@J�Opacity$	 0.8Q���  � �� �	#FF40A6A6��  � �� �� "	 ��.R��� $
 border��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 ������� $ border�� � �� �	#FF4C6C99P�  � �� . . ���  � ��
 ������� $ border�� � �� �	#FF2E5387�� .��U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�$
 grid��.\�  $ 0��$ 0Q�   $ 	EaseInOut=�  $ 0:0:0.4��$ 1Q�   $ 	EaseInOut=�U� $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�$
 grid��.\�  $ 0��$ 0Q�   $ 	EaseInOut=�  $ 0:0:0.4��$ 1Q�   $ 	EaseInOut=�r� $ (Border.BorderThickness)�$ window��.	�  $ 0��$ 0q�  $ 0:0:0.3��$ 0q�  $ 0:0:0.4��$ 1,0,1,1q� � Triggers :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� #��[ .��� -    �Name$
 grid�� J� RenderTransformOrigin$ 0.5,0.5&�
�������z�V� .~��� �� �� T�  � RowDefinitions �� $��30 ��� .+�� -   $��0�����	#FF3C74D6 � ColumnDefinitions �� �� $��30 ��� $��30 �.+��� $��0��$��Center=�
�������$
��Bold"�$��15(�$��	22,6,33,6q����� *MessageBoxTitle+ � ToolTip ��  �� RelativeSource �� *Self+ �� Path$
 Text��� -   $��1��#��[  .���� 	  �� X1$  0��	! �� X2$! 15��
�������
�������$��1���� -   $��2��#��[  .���� .+��� $	  -10��	" �� Y1$	" -10��$! 10��	# �� Y2$# 10��
�������
�������$��1���� $	  -10��$" 10��$! 10��$	# -10��
�������
�������$��1��� $��1��.+��  �� �� $��50 �.+��� $��0��$��Center=�$ �� Orientation$$ 
Horizontal=�.+��� ���� *	ImagePath+�� $��WrapWithOverflow=�$	��259��$��20,0,0,0q����� *MessageBoxText+ �� *Text+ �� *Self+�� $��1��$$ 
Horizontal=�$��Center=�.+��� -   % ��Name$% OKButton��$��确定��$��70��$��35��$��	20,0,20,0q�#��[  y��� *OKButtonVisibility+& �� Mode$& OneWay=��� -   $% 	YesButton��$	��是��$��70��$��35��$��	20,0,20,0q�#��[  y��� *YesButtonVisibility+$& OneWay=��� -   $% NoButton��$	��否��$��70��$��35��$��	20,0,20,0q�#��[  y��� *NoButtonVisibility+$& OneWay=��� -	   $% CancelButton��$��取消��$��70��$��35��$��	20,0,20,0q�#��[  y��� *CancelButtonVisibility+$& OneWay=�