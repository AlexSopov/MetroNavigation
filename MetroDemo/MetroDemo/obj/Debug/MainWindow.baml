   M S B A M L   `   `   `  ����D  @MetroDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MetroDemo	MetroDemo  5   	   9!clr-namespace:MetroDemo.MetroDataMetroDemo.MetroData  5   	   J FTWWPFUtilityLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullH5clr-namespace:ZoomBoxLibrary;assembly=TWWPFUtilityLibZoomBoxLibrary 5	   	       MetroDemo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MetroDemo     5   	   =local.metroData!clr-namespace:MetroDemo.MetroData     5   	   K	zoomPanel5clr-namespace:ZoomBoxLibrary;assembly=TWWPFUtilityLib     5	   	     � Title$  Metro navigation��5   	   $	��700��6"   $
��1000��6/   $	��700��5   	   $	��500��6    � WindowStartupLocation$ CenterScreen=�5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.4* �6   $	��225��6,   $	��300��6<   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      .+�5      � $��0��6   ���#4285F4 5      �� 5       �� 5!      �� 5"      �� 5#      �� 5$       5&      �� 5'      $
��auto �6'   �� 5(      $��* �6'   �� 5)      $
��auto �6'    � 	Resources 5+       �� DataType&   MetroDemo.MetroData.MetroStation   comboBoxItem �� Orientation � DataContext �� Path	 �� BasedOn
 �� 
TargetType �� Property �� Value��%  &	        '�� &    7�'��     �� 5,      "  6#   .R�5-      �� $��2q�6%   $ 
Horizontal=�60   .+�5.      A� $��10��6&   $��10��61    6=   �� $ StationLine���6f   �� $ 	LineColor��� 5/      $��14(�6(   $
��Left=�6Q   $��3 0 0 0q�6l   ��66   �� $
 Name�6l   50      51      �� 52      8	   "
 ��6[   .�53      ��  � ��$ 
10 10 0 10q�63   �� 54       � ��$ 30��63   �� 55       � ��$ Stretch=�6@   �� 56       � |� .  66   �� 57       � ��# �   69   58      �� 59      "
 ��6   .�5:      ��  � �� �#F9F9F96A   �� 5;       � ��$ 12(�65   �� 5<       � ��$ Center=�6>   �� 5=       � ��$
 Bold"�67   �� 5>       � ��$ 5 0 0 0q�63   5?      5@      .+�5B      �� $��0��6   $��3��6)   $��Kyiv metro navigaton��6=    �� TextAlignment$ Center=�6Y   $��20(�5C      $
��Bold"�6*   $��	10 5 10 5q�6<   �� 5D      $��1��6   $��0��6)   $��From:��69   �� 5E      -   5         ��Name$ cbFrom��5E   8   $��1��6   $��1��6(   6H   �� 5G      $��2��6   $��0��6)   $	��To:��69   �� 5H      -   5        $
 cbTo��5H   8   $��2��6   $��1��6(    �� Text$ To��6F   6P   �� 5J      -   5        $��2��5J      $��1��6)   $��2��66   $��30��6G   $��30��6S   $��10q�6^   $��Center=�5K        urrow_up_down#��[ 66   |�.  #��[" �5L   1    � ToolTip$ Swap stations��5M      5N      �� 5Q      -   5        $��	routeInfo��5Q      $��1��6+   ���#F7F7F7$y�	Collapsed=�6M   .+�5R      �� -   5        $��routeInfoDescription��5R      $��Center=�68   $��10 10q�6S   $��16(�6b   5S      � 5V      -   5        $��commandTextContainer��5V      $��2��6/   ���#F7F7F7.+�5W      �� -   5        $��commandText��5W       �� Padding$ 5q�6/   $��Center=�6;   $��Center=�6V   
������� J� Opacity$	 0.8Q�5X   /   $��16(�6=   5Y      �� 5Z      -   5        $��routeStationsContainer��5Z      $��2��69   $
��Auto=�6F   $y�	Collapsed=�6i   .��5[      �� -	   5        $��routeStationsView��5[      $ Vertical=�66   
�������$��0 0 0 10q�6`   5\      5]      �� 5`   
   $��1��6   $��3��6'   $��Center=�61   $��Stretch=�6N   !  ZoomBoxLibrary.ZoomBoxPanel  5a   
   -
   5          Name$ zoomBox��5a   G   $��2��6!      ZoomMode$ 
FitVisible=�61   $��2��6X      MinZoom$ 75Q�6e      MaxZoom$	 200Q�6r      	MouseMode$	 Pan=�6�      	WheelMode$
 Zoom=�6�   
�������.+�5b      �� -   5         ��Name$ MetroMap��5b   .   $	��750��6   $	��750��6!   6.   5c      5d      5e      