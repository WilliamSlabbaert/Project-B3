   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      "    PresentationLayer.ViewOrders   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6<   .��5
      � $��
20 0 20 20q�6     � RowDefinitions  5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      	� -   5         	�Name$ rTab1��5      $��4q�6)    d� 	IsChecked$
 TrueY�64    #  MaterialDesignFlatLightBgButton#��[  6E   $
��LIST��6~   	� 5      -   5        $ rTab2��5      $��4q�6)   #��[  64   $��ADD NEW��6m   5      #  MaterialDesignThemes.Wpf.Card  5   
   $��1��6   $��16q�6+   .��5      � .+�5      � y�6   �� *	IsChecked+ �� ElementName$ rTab1��   BooleanToVisibilityConverter �� 	Converter# [   5      �� 5      $��35 �6(   �� 5      $��* �6(   .+�5      � $��0��6   .+�5      ��  $  MaterialDesignHeadline6TextBlock#���  6$   $
��Left=�6_   Orders6�   �� 5      $ 
Horizontal=�6%   $��Right=�6>   .+�5       �� -   5         ��Name$ Button_EditOrder��5    �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j    & "MaterialDesignFloatingActionButton#���  6u   ��6�   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Pencil+6�   �� 5!      -   5        $ Button_DeleteOrders��5!   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   5"      5#      &   System.Windows.Controls.DataGrid  5%      -   5          Name$ 
OrdersGrid��5%   ,   $��1��6      
IsReadOnly$
 True��6@   	   SelectionMode$	 Extended=�6R   ���	#FFFFFFFF
 �� SelectedItem
 6k   ��  �� Path$ SelectedItem� �� Mode$ TwoWay=�6�   5&      � 5'      y�6   �� *	IsChecked+$ rTab2��# [   5(      �� 5)      $��35 �6(   �� 5*      $��* �6(   �� 5+      $��50 �6(   .+�5.      � $��0��6   .+�5/      �� #���  6$   $
��Left=�6_   	Add Order6�   50      �� 52      $��1��6#   .��53      �� .+�54      �� $
��Left=�6*   $	��350��6E   .+�55   "   �� $��5 0 0 0q�6,   $��
First name��6=   �� 56   "   -   5         ��Name$ Input_OrderFirstname��56   *   $��5q�6H   $��25��6S   57      �� 59      $
��Left=�6*   $	��350��6E   .+�5:   "   �� $��5 0 0 0q�6,   $��	Last name��6=   �� 5;   "   -   5        $ Input_OrderLasttname��5;   *   $��5q�6H   $��25��6S   5<      �� 5>      $
��Left=�6*   $	��350��6E   .+�5?   "   �� $��5 0 0 0q�6,   $��Email��6=   �� 5@   "   -   5        $ Input_OrderEmail��5@   *   $��5q�6D   $��25��6O   5A      �� 5C      $
��Left=�6*   $	��350��6E   .+�5D   "   �� $��5 0 0 0q�6,   $��Phone��6=   �� 5E   "   -	   5        $ Input_OrderPhone��5E   *   $��5q�6D   $��25��6O   5F      �� 5H      $
��Left=�6)   $	��350��6D   .+�5I   "   �� $��5 0 0 0q�6,   $��Add Item��6=   �� 5J   "   -
   5         ��Name$ Input_OrderItem��5J   +   $��5q�6D   $��25��6O   5K      �� 5M      $
��Left=�6*   $	��350��6E   .+�5N   "   �� $��5 0 0 0q�6,   $��Quantity��6=   �� 5O   "   -   5        $ Input_OrderItemQuantity��5O   *   $��1��6K   $��5q�6T   $��25��6_   5P      �� 5R      $
��Left=�6*   $	��350��6E   .+�5S   "   �� -   5        $ Button_AddOrderItem��5S   Y   $��4q�6)   ${�False��64   $��ADD ITEM��6F   6Y   5T      �� 5V      $
��Left=�6)   $	��350��6D   .+�5W   "   �� $��5 0 0 0q�6,   $��Delivery Items��6=     5X   "   -   5        $ Input_OrderItems��5X   +   $
 True��6E   $	 Extended=�6W   ���	#FFFFFFFF
 6p   �� $ SelectedItem�$ TwoWay=�6�   5Y      5Z      5[      �� 5]      $��2��6!   $
��Left=�6.   $��Center=�6I   .+�5^      �� -   5        $ Button_CreateOrder��5^   R   $��4q�6!   ${�False��6,   $��	ADD ORDER��6>   6R   5_      5`      5a      5b      5c      5d      