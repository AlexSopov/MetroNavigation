   M S B A M L   `   `   `  ����D  @MetroDemo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MetroDemo.MetroDataMetroDemo.MetroData  5      #    MetroDemo.MetroStationControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:MetroDemo.MetroData     5        � Cursor$
  Hand��5	       � 	Resources 5
         	fillBrush �� Color  	textBrush��%R   &	        &	      �� 5   
   $ White��6,   �� 5   
   $ Black��6,   5      .��5      �� -   5        $��PanelDirection��5   *    �� Orientation$ 
Horizontal=�6   .+�5   
   A� -   5        $��marker��5        StationCircleDiameter#��[ 5      #��[ 5      #���   5      $��3��5   4    � DataContext 5      ��  �� Path$ StationLine���5      �� $ 	LineColor�64   �� 5   
   
������� �� CornerRadius$ 5��6$    J� Opacity$	 0.5Q�65   $��3 0 0 0q�6C   ._�5      �� -   5        $��stationNameBlock��5      #���  6K   $��Center=�6t    �� Padding$ 0 0 0 1q�6�   ��60   �� $
 Name�6�   5      5      5      