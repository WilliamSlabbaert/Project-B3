   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      &     PresentationLayer.ViewDeliveries   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6<   .��5
      � $��
20 0 20 20q�6     � RowDefinitions  5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      	� -   5         	�Name$ rTab1��5      $��4q�6)    d� 	IsChecked$
 TrueY�64    #  MaterialDesignFlatLightBgButton#��[  6E   $
��LIST��6~   	� 5      -   5        $ rTab2��5      $��4q�6)   #��[  64   $��ADD NEW��6m   5      #  MaterialDesignThemes.Wpf.Card  5   
   $��1��6   $��16q�6+   .��5      � .+�5      � y�6   �� *	IsChecked+ �� ElementName$ rTab1��   BooleanToVisibilityConverter �� 	Converter# [   5      �� 5      $��35 �6(   �� 5      $��* �6(   .+�5      � $��0��6   .+�5      ��  $  MaterialDesignHeadline6TextBlock#���  6$   $
��Left=�6_   
Deliveries6�   �� 5      $ 
Horizontal=�6%   $��Right=�6>   .+�5       �� -   5         ��Name$ Button_EditDelivery��5    �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j    & "MaterialDesignFloatingActionButton#���  6u   ��6�   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Pencil+6�   �� 5!      -   5        $ Button_DeleteDeliveries��5!   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   5"      5#      &   System.Windows.Controls.DataGrid  5%      -   5          Name$ DeliveriesGrid��5%   ,   $��1��6      
IsReadOnly$
 True��6D   	   SelectionMode$	 Extended=�6V   ���	#FFFFFFFF
 �� SelectedItem
 6o   ��  �� Path$ SelectedItem� �� Mode$ TwoWay=�6�   5&      � 5'      y�6   �� *	IsChecked+$ rTab2��# [   5(      �� 5)      $��35 �6(   �� 5*      $��* �6(   �� 5+      $��50 �6(   .+�5.      � $��0��6   .+�5/      �� #���  6$   $
��Left=�6_   Add Delivery6�   50      �� 52      $��1��6#   .��53      �� .+�54      �� $
��Left=�6*   $	��350��6E   .+�55   "   �� $��5 0 0 0q�6,   $��Supplier��6=   �� 56   "   -   5         ��Name$ Input_DeliverySupplier��56   *   $��5q�6J   $��25��6U   57      �� 59      $
��Left=�6)   $	��350��6D   .+�5:   "   �� $��5 0 0 0q�6,   $��Add Item��6=   �� 5;   "   -   5         ��Name$ Input_DeliveryItem��5;   +   $��5q�6G   $��25��6R   5<      �� 5>      $
��Left=�6*   $	��350��6E   .+�5?   "   �� $��5 0 0 0q�6,   $��Quantity��6=   �� 5@   "   -   5        $  Input_DeliveryItemQuantity��5@   *   $��1��6N   $��5q�6W   $��25��6b   5A      �� 5C      $
��Left=�6*   $	��350��6E   .+�5D   "   �� -	   5        $ Button_AddItem��5D   Y   $��4q�6)   ${�False��64   $��ADD ITEM��6F   6Y   5E      �� 5G      $
��Left=�6)   $	��350��6D   .+�5H   "   �� $��5 0 0 0q�6,   $��Delivery Items��6=     5I   "   -
   5        $ Input_DeliveryItems��5I   +   $
 True��6H   $	 Extended=�6Z   ���	#FFFFFFFF
 6s   �� $ SelectedItem�$ TwoWay=�6�   5J      5K      5L      �� 5N      $��2��6!   $
��Left=�6.   $��Center=�6I   .+�5O      �� -   5        $ Button_CreateDelivery��5O   U   $��4q�6!   ${�False��6,   $��ADD DELIVERY��6>   6U   5P      5Q      5R      5S      5T      5U      