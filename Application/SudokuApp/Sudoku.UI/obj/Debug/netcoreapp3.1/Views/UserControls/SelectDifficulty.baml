   M S B A M L   `   `   `  ����D  @Sudoku.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Sudoku.UI.Views.UserControlsSudoku.UI.Views.UserControls  5      3    -Sudoku.UI.Views.UserControls.SelectDifficulty   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      <local*clr-namespace:Sudoku.UI.Views.UserControls     5      
�������.��5      ]� $��30q�6   .+�5   
   �� 
������$	��Top=�5      
��� � �$��5q�5      $��0 5 0 20q�5      $��70��5      $��Stretch=�5      $
��easy��5      ��5      ��   �� RelativeSource  ��  �� AncestorType UserControl �� AncestorLevel$ 2�� �� Path$&  DataContext.CreateNewGameCommand�.��5      �  � ColumnDefinitions 5      �� 5      $��80 �6'   �� 5      .+�5      �� $;��5..\..\Resources\Images\Difficulty\difficulty_easy.png��6   �� 5      $��20(�6   $��1��5      $��	UltraBold"�5      $��Center=�5      .��6:   EASY5!      5"      5#      �� 5$   
   
�������
��� ���$	��Top=�5&      $��5q�5'      $��0 5 0 20q�5(      $��70��5)      $��Stretch=�5*      $��medium��5,      ��5+      ��   ��  UserControl$ 2��$&  DataContext.CreateNewGameCommand�.��5-      ��  �� Orientation$ 
Horizontal=�6   .+�5.      �� $=��7..\..\Resources\Images\Difficulty\difficulty_medium.png��6   �� 5/      $��20(�6   $��	UltraBold"�50      $��Center=�51      .��6;   MEDIUM53      54      55      �� 56   
   
���\\��
���  ��$��5q�58      $��70��59      $��Stretch=�5:      $
��hard��5<      ��5;      ��   ��  UserControl$ 2��$&  DataContext.CreateNewGameCommand�.��5=      �� $ 
Horizontal=�6   $
��Left=�62   .+�5>      �� $;��5..\..\Resources\Images\Difficulty\difficulty_hard.png��6   �� 5?      $��20(�6   $��	UltraBold"�5@      $��Center=�5A      .��6>   HARD5C      5D      5E      5F      5G      