   M S B A M L   `   `   `  ����D  @Sudoku.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:Sudoku.UI.Views.UserControlsSudoku.UI.Views.UserControls  5      E'clr-namespace:Sudoku.UI.ValueConvertersSudoku.UI.ValueConverters  5      G CModalControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA0clr-namespace:ModalControl;assembly=ModalControlModalControl 5      ;"clr-namespace:Sudoku.UI.ViewModelsSudoku.UI.ViewModels  5	          Sudoku.UI.Views.GameView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?controls*clr-namespace:Sudoku.UI.Views.UserControls     5      >
converters'clr-namespace:Sudoku.UI.ValueConverters     5      Ccustom0clr-namespace:ModalControl;assembly=ModalControl     5      9
viewModels"clr-namespace:Sudoku.UI.ViewModels     5	        � 	Resources  5      4  .Sudoku.UI.ValueConverters.CountToBoolConverter   CountToBoolConverter4   .Sudoku.UI.ValueConverters.ParentWidthConverter  ParentWidthConverter��%:   &	        &	        5   
   6*     5   
   6*   5      .��5      � .+�5   
     ModalControl.Modal   K� ZIndex$ 1��6=      IsOpen 6   �� *GameOverViewIsOpen+.��5      +   %Sudoku.UI.Views.UserControls.GameOver  $	��300��6    5      � 5   
    � RowDefinitions 5      �� 5      $
��1.2* �6    �� 5      $��8* �6    �� 5      $��2* �6    �� 5      $
��auto �6    .+�5"      �  5#      �� 5$      $
��2.5* �6$   �� 5%      $��5* �6$   .+�5'      � $��1��6    � ColumnDefinitions 5(      �� 5)      �� 5*      �� 5+      �� 5,      �� 5-      �� 5.      $	��10* �6+   �� 5/      �� 50      �� 51       53      �� 54      $��40 �6(   .+�56      �� -   5         ��Name$ 
MenuButton��56      $��0��57      $
��Bold"�58      $��90��59      $��15(�5:      $��3q�5;      $��0 0 0 0q�5<      $��3��5=      ��5>      �� *NavigateToMenuCommand+.��6G   Back5@      �� 5A      -   5        $ SolveButton��5A      $��3q�5B      $��6��5C      $	��110��5D      $��3��5E      ��5F      �� *SolveCommand+.��5G      ��  �� Orientation$ 
Horizontal=�6%   .+�5H      �� $3��-..\..\Resources\Images\PlayArea\game_hint.png��6$   �� 5J      $
��Bold"�6$   $��15(�5K   $   $��Center=�5L   $   .��6F   Solve5N      5O      5P      5Q      5R      � 5U      -   5         �Name$ PlayArea��5U      $��1��5V       5W      �� 5X      $��2* �6'   �� 5Y      $��8* �6'   �� 5Z      �� 5[      $��5* �6'   �� 5\      $��2* �6'    5^      �� 5_      $��1* �6$   �� 5`      $��6* �6$   �� 5a      $��1* �6$   .+�5d      �� -   5         ��Name$ 	TimeLabel��5d      $��Center=�5e      $��Center=�5f      $��5��5g      $
��Bold"�5h      $��24(�5i      ��5j      �� *
PlayTime+-   'Sudoku.UI.Views.UserControls.SudokuGrid  5l      $��1��6&   $��1��5m   "   0   *Sudoku.UI.Views.UserControls.SudokuBorders  5o      $��1��6)   $��1��5p   %   	 J� IsHitTestVisible$	 False��5q   %   �� 5r      $��1��6   $��3��5s      $	��1.5q�5t      
���   �._�5v      ]� -   5        
 ]�Name$
 HistoryPanel��5v       
�������.+�5x      � 
�������$	��Top=�66    5y      �� 5z   "   �� 5{   "   $��2* �63   �� 5|   "   �� 5}   "   �� 5~   "   .+�5�      �� $��2��6$   $��20(�5�       $
��Bold"�5�       $��	10 10 0 5q�5�       .��64   	History5�      �� 5�      -   5        $ ResetButton��5�   %   
������ $��0q�5�   %   $��2��5�   !   $��
10 15 10 5q�5�   !   	 5�   !   �� *History.Count+ �� 	Converter# [  ��5�   !   �� *ResetCommand+.��5�   "   �� $4��.../../Resources/Images/PlayArea/game_reset.png��6(   5�      �� 5�      -   5        $ 
UndoButton��5�   %   $��3��5�   !   $��0q�5�   !   $��
10 15 10 5q�5�   !   
������ 	 5�   !   �� *History.Count+# [  ��5�   !   �� *UndoCommand+.��5�   "   �� $5��/../../Resources/Images/History/history_undo.png��6(   5�      �� 5�      -   5        $ 
RedoButton��5�   %   $��
10 15 10 5q�5�   %   $��4��5�   %   
������ $��0q�5�   %   	 5�   %   �� *RedoHistory.Count+# [  ��5�   %   �� *RedoCommand+.��5�   "   �� $5��/../../Resources/Images/History/history_redo.png��6(   5�      5�      �� 5�      -	   5         ��Name$ HistoryList��5�   #   $��0q�5�      $��0 0 2 0q�5�      $
��True��5�      $��Hidden=�5�      ��5�      �� *	History+ �� SelectedItem 5�      �� *SelectedHistoryEntry+  5�        ItemContextMenu m� Command �� Path �� RelativeSource �� AncestorType �� AncestorLevel��%�   &	       �� 5�   "   .@�5�   &   m� $��
Delete all��6/   
������� 6C   �� $' !DataContext.UndoPreceedingCommand� ��  UserControl$ 2��6�   5�   #   5�      ��5�      �� 5�   "    �� 
TargetType" ��6(   .�5�   &   ��  �� Property �@�ContextMenu �� Value# [ 6D   5�   #   ��5�      �� 5�   "   .R�5�   &   �� 
�������$��1q�5�   -   $��-4 -1.8q�5�   -   ._�5�   *   �� $ 
Horizontal=�65   ��5�   1   ��  �� ElementName$ HistoryPanel��$ ActualWidth�# [ .+�5�   .   �� $��27��5�   0   $��27��5�   0   $��5q�5�   0   ��5�   4   �� *ImageSource+5�   0   �� 5�   .   $
��Wrap=�5�   4   $��17(�5�   4   $��Center=�5�   4   $��Italic$�5�   4   $��5 4 4 4q�5�   4   ��5�   8   �� *Description+5�   4   5�   +   5�   '   5�   #   5�      5�      5�      5�      � 5�      $��2��6    5�      �� 5�      $��4* �6$   �� 5�      $��5* �6$    5�      �� 5�      $
��140* �6'   �� 5�      $
��330* �6'   �� 5�      $
��140* �6'   .+�5�      1   +Sudoku.UI.Views.UserControls.NumberSelector  -
   5          Name$ NumberSelector��5�   *   $��0��5�   &   $��1��5�   &      SelectedNumber 5�   &   ��  ��  UserControl$  DataContext.SelectedNumber� �� Mode$ OneWayToSource=�   NoteMode 5�   &   ��  ��  UserControl$ DataContext.NoteMode�$ OneWayToSource=�5�      �� 5�      $��3��6   
�������$��0 2 0 0q�5�      
������� �� Padding$	 5 0q�5�      ._�5�      �  5�      �� 5�      �� 5�      .+�5�      �� $��12(�5�      $��DemiBold"�5�      
���   ���5�      �� *
LogMessage+5�      �� 5�      $��12(�5�      $��1��5�      $��Right=�5�      $��DemiBold"�5�      
���   ���5�      �� *LogDifficultyLevel+5�      5�      5�      5�      5�      5�      