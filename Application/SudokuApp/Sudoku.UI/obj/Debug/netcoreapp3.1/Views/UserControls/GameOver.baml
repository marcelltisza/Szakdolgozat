   M S B A M L   `   `   `  ����D  @Sudoku.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Sudoku.UI.ValueConvertersSudoku.UI.ValueConverters  5      +    %Sudoku.UI.Views.UserControls.GameOver   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >
converters'clr-namespace:Sudoku.UI.ValueConverters     5      
�������  � 	Resources  5      ?   9Sudoku.UI.ValueConverters.DifficultyTypeToStringConverter #  DifficultyTypeToStringConverter��%   &	          5   
   65   5      .��5      � $��10q�6    � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��0.25* �6   �� 5      $��1* �6   .+�5   
   �� $��Center=�6   $��Center=�5      $��	UltraBold"�5      $��2��5      
���  ��$��30(�5        J� Effect 5!      3  -System.Windows.Media.Effects.DropShadowEffect  5"         Color$ Orange��6#      
BlurRadius$ 10Q�5#   #      ShadowDepth$ 0Q�5$   #   .��5%      !C o n g r a t u l a t i o n s !5'      �� 5(   
   $��Center=�6   $��Right=�5)      $��1��5*      $��25(�5+      $��0 0 10 0q�5,      $
��Bold"�5-      .��6"   Difficulty:5/      �� 50   
   $��1��6   $��1��51      $��Center=�52      $��25(�54      $��10 0 0 0q�55      $��DemiBold"�56      ��53      ��  �� RelativeSource ��  �� AncestorType UserControl	 �� AncestorLevel$	 2��
 �� Path$
 DataContext.Difficulty� �� 	Converter# [  56      �� 57   
   $��2��6   $��2��58      $��0 1 0 4q�59      
���   �._�5;      �  5<      �� 5=      �� 5>      .+�5@      �� $��Right=�6   $��Center=�5A      $��25(�5B      $��0 0 10 0q�5C      $
��Bold"�5D      
�������.��5E   /   Time:5G      �� 5H      $��1��6   $��Center=�5I      $��25(�5J      $��10 0 0 0q�5K      $��DemiBold"�5L      
���������5N      ��  ��  UserControl$	 2��$
 DataContext.PlayTime�5O      5P      �� 5R   
   $��4��6   $��1��5S      $��5q�5T      $��20(�5U      $��	UltraBold"�5V      $��10q�5W      ��5X      ��  ��  UserControl$	 2��$'
 !DataContext.NavigateToMenuCommand�.��6�   Back to Menu5Z      5[      5]      