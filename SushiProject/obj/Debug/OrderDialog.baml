   M S B A M L   `   `   `  ����E  ASushiSushi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:SushiSushi
SushiSushi  5          SushiSushi.OrderDialog   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:SushiSushi     5      .��5	   
   � $��0,0,0,0q�6   ���	#FFDAAB68$	��980��67   $	��650��6C   .+�5
   
   �� .+�5      � .+�5      �� 
���   �$��2q�6-   $��Center=�6A   $	��400��6^   $��10q�6k   $	��Top=�6w   $	��940��6�   ���	#FFF3EFEA  �� CornerRadius$  5��6�   �� 5      $	��380��6   $	��920��6,   $��Hidden=�68    �� PanningMode$ VerticalOnly=�6]   .��5      �� -   5         ��Name$ SelectedList��5   =   $��10q�6#   $
��Auto��6/   ��. ��6x   ��  �� ElementName$ 
mainWindow�� �� Path$ SelectedItems���5      �� 5      .R�5   "   �  K� IsItemsHost .  � Orientation$ 
Horizontal=�6�   ��6?   �� $ SelectedList��$ Width���6v   �� $ SelectedList��$ Height�6�   5      ��5      �� 5      .R�5   "   #   SushiSushi.SidebarItemControl     MainBackground$ Salmon��6;   5      5      5      5      � 5       � ColumnDefinitions 5       �� 5!      $
��Auto �6'   �� 5"      $	��220 �6'   �� 5#      $	��400 �6'   	 � RowDefinitions	 5%      �� 5&      $
��0.5* �6$   �� 5'      $
��0.5* �6$   .+�5)      �� -   5        $��Cancel��5)      $��3��6+   $��Right=�6;   $��50,10q�6W   $	��120��6f   $��50��6s   $
��Bold"�6   
�������6�   �� 5*      -   5        $��Confirm Order��5*      $��3��61   $
��Left=�6A   $��70,10q�6\   $	��120��6k   $��50��6w   $
��Bold"�6�   
�������6�   �� 5+      $#��Your total for this order is:��6   $
��Left=�6@   $��50,10q�6[   $	��Top=�6j   $��20(�6�   $
��Bold"�6�   �� 5,      -   5        
 ��Name$
 SelectedCost��5,      $��20(�6.   $
��Left=�6<   $��1��6W   $
��0,14q�6g   $��$0.00��6v   $	��Top=�6�   �� 5-      $��Overall total:��6   $��20(�62   $
��Left=�6@   $��50,10q�6[   $��1��6j   $	��Top=�6w   $
��Bold"�6�   �� 5.      -   5        $
 TotalMealCost��5.      $��20(�6/   $
��Left=�6=   $��1��6X   $��1��6h   $
��0,14q�6u   $��$0.00��6�   $	��Top=�6�   5/      50      52      53      