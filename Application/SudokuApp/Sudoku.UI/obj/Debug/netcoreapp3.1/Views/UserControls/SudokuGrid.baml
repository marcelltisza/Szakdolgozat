   M S B A M L   `   `   `  ����D  @Sudoku.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Sudoku.UI.ValueConvertersSudoku.UI.ValueConverters  5      ;"clr-namespace:Sudoku.UI.ViewModelsSudoku.UI.ViewModels  5      -    'Sudoku.UI.Views.UserControls.SudokuGrid   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      =	converter'clr-namespace:Sudoku.UI.ValueConverters     5      9
viewModels"clr-namespace:Sudoku.UI.ViewModels     5      +   %Sudoku.UI.ViewModels.ViewModelLocator    AutoWireViewModel$
  True��5      
������� � 	Resources 5      -   'Sudoku.UI.ValueConverters.NoteConverter   NoteConverter7   1Sudoku.UI.ValueConverters.NoteVisibilityConverter  NoteVisibilityConverter8   2Sudoku.UI.ValueConverters.FixedBackgroundConverter  FixedbackGroundConverter/   )Sudoku.UI.ValueConverters.BooleanInverter  BooleanIverter3   -Sudoku.UI.ValueConverters.FontWeightConverter  FontWeightConverter/  )Sudoku.UI.ValueConverters.ToListConverter  ToListConverter2   ,Sudoku.UI.ValueConverters.HighlightConverter  HighlightConverter<	   6Sudoku.UI.ValueConverters.ForegroundHighlightConverter   ForegroundHighlightConverter0
   *Sudoku.UI.ValueConverters.HitTestConverter  HitTestConverter �� 
TargetType 	 ValueHighlightStyle �� Property �� Value `� 	Converter `� Mode �� Path �� RelativeSource	 �� Mode
 �� AncestorType �� AncestorLevel 
 NoteHighlightStyle  ValueButtonStyle��%A  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     7  7  &	
 �    7  &	 {    7    5   
   6"     5   
   6,     5   
   6-     5   
   6$     5   
   6(     5   
   6$     5   
   6'   	  5   
   61   
  5   
   6%   �� 5   
   " ��6   .�5      ��  �@J�Effect 5      `� 5      8   $ OneWay=�6S   .1�5      �� $
 Text�6N    6"   �� $
	 Self=�6N   �� 5      $  DataContext.SelectedNumber�6n    6"   �� 
 UserControl$ 2��6n   5      5!      �� 5#       � �� 5$      `� 5%      8  $ OneWay=�6]   .1�5&      �� $
 Text�6N    6"   �� $
	 Self=�6N   �� 5'      $  DataContext.SelectedNumber�6n    6"   �� 
 UserControl$ 2��6n   5(      5*      5+      �� 5,   
   " ��6   .�5-      ��  � �� 5.      `� 5/      8   $ OneWay=�6]   .1�50      �� $ Content�6N    6"   �� $
	 Self=�6N   �� 51      $  DataContext.SelectedNumber�6n    6"   �� 
 UserControl$ 2��6n   52      54      �� 55       � �� 56      #�57      Bold"�6'   59      �� 5:       � ��$ 10(�6)   �� 5;       � ��$ 1 -1 1 1q�6(   5<      �� 5=   
   " ��6   .�5>      ��  �@J�IsHitTestVisible 5?      `� 5@      8   .1�5A      �� $ Content.Text�6N    6"   �� $
	 Self=�6N   �� 5B      $ IsFixed�6"   �� 5C      $ DataContext.NoteMode�6n    6"   �� 
 UserControl$ 2��6n   5D      5F      5G      5H      .��5J      �� ��6   ��  �� 
 UserControl$ 2��$ DataContext.Board.Cells� �� 	Converter# [ ��5K   
   �� 5L      .R�5M      �� $��0q�6   
���   �._�5P      �  �Name$ CellGrid��6   .+�5R      ��  ��Name$ ValueButton��6!   #��[ 5S   !   $��24(�5T   !   
���   ���5V   !   ��  �� 
 UserControl$ 2��$ DataContext.ClickCommand���5W   !   �� ��5Y   !   �� *	IsFixed+# [ ��5Z   !   �� *	IsFixed+# [ .��5[      ��  ��Name$ 	ValueText��6(   #��[	 5]   (    �� TextAlignment$ Center=�5^   (   ��5\   (   �� *Value+5^   (   5_      � 5a      $ NoteGrid��6    J� IsHitTestVisible$ False��5b       � RowDefinitions 5c      �� 5d   "   �� 5e   "   �� 5f   "    � ColumnDefinitions 5h      �� 5i   "   �� 5j   "   �� 5k   "   .+�5n      �� $��0��6%   $��0��5o   %   
������ #��[
 5r   %   $��10(�5s   %   $��0q�5t   %   y�5p   %   ��  �� ElementName$ 	ValueText��$
 Text�# [ ��5u   %   �� *
Notes[0]+# [  �� 5w      $��1��6%   $��0��5x   %   #��[
 5y   %   
������ $��0q�5|   %   y�5z   %   �� $ 	ValueText��$
 Text�# [ ��5}   %   �� *
Notes[1]+# [  �� 5      $��2��6%   $��0��5�   %   #��[
 5�   %   
������ $��0q�5�   %   y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[2]+# [  �� 5�      $��0��6%   $��1��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[3]+# [  �� 5�      $��1��6%   $��1��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[4]+# [  �� 5�      $��2��6%   $��1��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[5]+# [  �� 5�      $��0��6%   $��2��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[6]+# [  �� 5�      $��1��6%   $��2��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[7]+# [  �� 5�      $��2��6%   $��2��5�   %   #��[
 5�   %   $��0q�5�   %   
������ y�5�   %   �� $ 	ValueText��$
 Text�# [ ��5�   %   �� *
Notes[8]+# [  5�      5�      5�      5�      ��5�   
   �� 5�      .R�5�      A�  A� Columns$ 9��6    A� Rows$ 9��6*   5�      5�      5�      5�      