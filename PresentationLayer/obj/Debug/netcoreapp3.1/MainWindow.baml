   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   "    PresentationLayer.MainWindow   5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	      MaterialDesignFont#���   5   	     � AllowsTransparency$
  True��5
   	   
������  � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�5   	     MaterialDesignBodyLight#���  5   	   $��1q�5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	   $	��824��5   	   $	��500��5   	    � Title$
 Home��5   	   $	��600��6   $
��1024��6#   .��5      �   MaterialDesignPaper#���  6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      $��* �6   .+�5   
   � $��0��6   $	��250��6   ���	#FF222222.+�5      ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  -   5          Name$ TransitioningContentSlide��5   2   C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6U   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  *SlideInFromLeft+   Duration 0:0:0.2.��5      � -   5        	 �Name$	 
GridCursor��5      $��	0 100 0 0q�6+   ���	#FF0069C0$��10��6U   $
��Left=�6`   $��60��6{   $	��Top=�6�   5      �� 5      -   5        
 ��Name$
 ListViewMenu��5      $��0 100q�6-   
�������$��Global Monospace)�6S   $��20(�6q   .@�5      �� $��60��6   .��5       ��  �� Orientation$ 
Horizontal=�6!   .+�5!      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Home=�62   $��30��6>   $��30��6I   $��Center=�6U   $��5q�6p   �� 5"      $
��Home��6$   $��Center=�60   $
��20 0q�6K   5#      5$      �� 5%      $��60��6   .��5&      �� $ 
Horizontal=�6!   .+�5'        $
 Plus=�62   $��30��6>   $��30��6I   $��Center=�6U   $��5q�6p   �� 5(      $��Add New��6$   $��Center=�63   $
��20 0q�6N   5)      5*      �� 5+      $��60��6   .��5,      �� $ 
Horizontal=�6!   .+�5-        $ Truck=�62   $��30��6?   $��30��6J   $��Center=�6V   $��5q�6q   �� 5.      $��
Deliveries��6$   $��Center=�66   $
��20 0q�6Q   5/      50      �� 51      $��60��6   .��52      �� $ 
Horizontal=�6!   .+�53        $
 Cart=�62   $��30��6>   $��30��6I   $��Center=�6U   $��5q�6p   �� 54      $��Orders��6$   $��Center=�62   $
��20 0q�6M   55      56      �� 57      $��60��6   .��58      �� $ 
Horizontal=�6!   .+�59        $ 	CloudJson=�62   $��30��6C   $��30��6N   $��Center=�6Z   $��5q�6u   �� 5:      $��Ex- & Import��6$   $��Center=�6<   $
��20 0q�6W   5;      5<      5=      5>      � 5@   
   $��1��6    � RowDefinitions 5A      �� 5B      $��30 �6    �� 5C      $��* �6    .+�5F      � -   5        $��0��5F      $	��Top=�6    ���	#00000000.+�5G      �� $ 
Horizontal=�6   $��Right=�66   $��0,0,5,0q�6R   .+�5H      �� $��30��6   $��30��6(     MaterialDesignToolButton#���  64   .��5I        $ Settings=�62   5J      �� 5K      -   5         ��Name$ BtnClose��5K      $��30��6/   $��30��6:   #���  6F   .��5L        $ PowerStandby=�62   5M      5N      5O      � 5Q      -   5        $	 GridPrincipal��5Q       $��1��6   5S      5T      5U      5V      