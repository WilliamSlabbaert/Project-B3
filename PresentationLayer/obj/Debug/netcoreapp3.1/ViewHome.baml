   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5           PresentationLayer.ViewHome   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  6<   .��5
      � $��
20 0 20 20q�6     � RowDefinitions  5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   .+�5      	� -   5         	�Name$ rTab1��5      $��4q�6)    d� 	IsChecked$
 TrueY�64    #  MaterialDesignFlatLightBgButton#��[  6E   $��COMICSTRIPS��6~   	� 5      -   5        $ rTab2��5      $��4q�6)   #��[  64   $��AUTHORS��6m   	� 5      -   5        $ rTab3��5      $��4q�6)   #��[  64   $��
PUBLISHERS��6m   	� 5      -   5        $ rTab4��5      $��4q�6)   #��[  64   $��BUNDLES��6m   5      #  MaterialDesignThemes.Wpf.Card  5   
   $��1��6   $��16q�6+   .��5      � .+�5      � y�6   �� *	IsChecked+ �� ElementName$ rTab1��   BooleanToVisibilityConverter �� 	Converter# [   5      �� 5      $��35 �6(   �� 5      $��* �6(   .+�5      � $��0��6   .+�5       ��  $  MaterialDesignHeadline6TextBlock#���  6$   $
��Left=�6_   Comicstrips6�   �� 5!      $ 
Horizontal=�6%   $��Right=�6>   .+�5"      �� -   5         ��Name$ Button_EditStrip��5"   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j    & "MaterialDesignFloatingActionButton#���  6u   ��6�   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Pencil+6�   �� 5#      -   5        $ Button_DeleteStrips��5#   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   5$      5%      &   System.Windows.Controls.DataGrid  5'      -   5          Name$ 
StripsGrid��5'   ,   $��1��6      
IsReadOnly$
 True��6@   	   SelectionMode$	 Extended=�6R   ���	#FFFFFFFF
 �� SelectedItem
 6k   ��  �� Path$ SelectedItem� �� Mode$ TwoWay=�6�   5(      � 5)      y�6   �� *	IsChecked+$ rTab2��# [   5*      �� 5+      $��35 �6(   �� 5,      $��* �6(   .+�5/      � $��0��6   .+�50      �� #���  6$   $
��Left=�6_   	Authors6�   �� 51      $ 
Horizontal=�6%   $��Right=�6>   .+�52      �� -   5        $ Button_EditAuthor��52   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Pencil+6�   �� 53      -	   5        $ Button_DeleteAuthors��53   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   54      55        57      -
   5        $ AuthorsGrid��57   ,   $��1��6   $
 True��6A   $	 Extended=�6S   ���	#FFFFFFFF
 6l   �� $ SelectedItem�$ TwoWay=�6�   58      � 59      y�6   �� *	IsChecked+$ rTab3��# [   5:      �� 5;      $��35 �6(   �� 5<      $��* �6(   .+�5?      � $��0��6   .+�5@      �� #���  6$   $
��Left=�6_   
Publishers6�   �� 5A      $ 
Horizontal=�6%   $��Right=�6>   .+�5B      �� -   5        $ Button_EditPublisher��5B   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Pencil+6�   �� 5C      -   5        $ Button_DeletePublishers��5C   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   5D      5E        5G      -   5        $ PublishersGrid��5G   ,   $��1��6   $
 True��6D   $	 Extended=�6V   ���	#FFFFFFFF
 6o   �� $ SelectedItem�$ TwoWay=�6�   5H      � 5I      y�6   �� *	IsChecked+$ rTab4��# [   5J      �� 5K      $��35 �6(   �� 5L      $��* �6(   .+�5O      � $��0��6   .+�5P      �� #���  6$   $
��Left=�6_   Comicstrip Bundles6�   �� 5Q      $ 
Horizontal=�6%   $��Right=�6>   .+�5R      �� -   5        $ Button_EditBundles��5R   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Pencil+6�   �� 5S      -   5        $ Button_DeleteBundles��5S   �   $��4q�6%   $��Right=�60   ${�False��6L   $��25��6^   $��25��6j   #���  6u   ��6�     *Delete+6�   5T      5U        5W      -   5        $ BundlesGrid��5W   ,   $��1��6   $
 True��6A   $	 Extended=�6S   ���	#FFFFFFFF
 6l   �� $ SelectedItem�$ TwoWay=�6�   5X      5Y      5Z      5[      5\      