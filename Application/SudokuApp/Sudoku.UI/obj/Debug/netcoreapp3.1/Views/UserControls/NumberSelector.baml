   M S B A M L   `   `   `  ����D  @Sudoku.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Sudoku.UI.ValueConvertersSudoku.UI.ValueConverters  5      1    +Sudoku.UI.Views.UserControls.NumberSelector   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      =	converter'clr-namespace:Sudoku.UI.ValueConverters     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      
������   � 	Resources  5      ;   5Sudoku.UI.ValueConverters.NoteModeBackgroundConverter   NoteModeBackgroundConverter>   8Sudoku.UI.ValueConverters.IsCheckedToForegroundConverter " IsCheckedToForegroundConverter:   4Sudoku.UI.ValueConverters.IsCheckedToShadowConverter  IsCheckedToShadowConverter7   1Sudoku.UI.ValueConverters.NumberSelectorConverter  NumberSelectorConverter  SelectorTextStyle �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType �� Path �� 	Converter��%  &	        &	      &	 &     &	 9     &	 L     7  7    5   
   60     5   
   63     5   
   6/     5   
   6,   �� 5   
   " ��6*   .�5      ��  � �� 6+   ��  ��  ToggleButton$ 	IsChecked�8   �� 5       �@J�Effect 5      �� 5      8  $ 	IsChecked�5       5      ��  ToggleButton5      5      5      .��5      � -   5         �Name$ NumbersGrid��5      	 � ColumnDefinitions	 5   
   �� 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      �� 5'      �� 5(      �� 5)      $
��0.5* �6   �� 5*      .+�5-   
   d� $��0��6   $��5q�5.      $��24(�50      $
��Bold"�51      ��5/      �� 
 �� ElementName$
 
NoteToggle��$ 	IsChecked�# [   d� 	IsChecked 52      �� *SelectedNumber+# [  �� ConverterParameter$ 1��.��53      �� $��24(�6   #��[ 54      $��1��55      56      d� 58   
   $��1��6   $��5q�59      $
��Bold"�5;      ��5:      �� $
 
NoteToggle��$ 	IsChecked�# [   5<      �� *SelectedNumber+# [ $ 2��.��5=      �� $��24(�6   #��[ 5>      $��2��5?      5@      d� 5B   
   $��2��6   $��5q�5C      $��24(�5E      $
��Bold"�5F      ��5D      �� $
 
NoteToggle��$ 	IsChecked�# [   5G      �� *SelectedNumber+# [ $ 3��.��5H      �� $��24(�6   #��[ 5I      $��3��5J      5K      d� 5M   
   $��3��6   $��5q�5N      $��24(�5P      $
��Bold"�5Q      ��5O      �� $
 
NoteToggle��$ 	IsChecked�# [   5R      �� *SelectedNumber+# [ $ 4��.��5S      �� $��24(�6   #��[ 5T      $��4��5U      5V      d� 5X   
   $��4��6   $��5q�5Y      $��24(�5[      $
��Bold"�5\      ��5Z      �� $
 
NoteToggle��$ 	IsChecked�# [   5]      �� *SelectedNumber+# [ $ 5��.��5^      �� $��24(�6   #��[ 5_      $��5��5`      5a      d� 5c   
   $��5��6   $��5q�5d      $��24(�5f      $
��Bold"�5g      ��5e      �� $
 
NoteToggle��$ 	IsChecked�# [   5h      �� *SelectedNumber+# [ $ 6��.��5i      �� $��24(�6   #��[ 5j      $��6��5k      5l      d� 5n   
   $��6��6   $��5q�5o      $��24(�5q      $
��Bold"�5r      ��5p      �� $
 
NoteToggle��$ 	IsChecked�# [   5s      �� *SelectedNumber+# [ $ 7��.��5t      �� $��24(�6   #��[ 5u      $��7��5v      5w      d� 5y   
   $��7��6   $��5q�5z      $��24(�5|      $
��Bold"�5}      ��5{      �� $
 
NoteToggle��$ 	IsChecked�# [   5~      �� *SelectedNumber+# [ $ 8��.��5      �� $��24(�6   #��[ 5�      $��8��5�      5�      d� 5�   
   $��8��6   $��5q�5�      $��24(�5�      $
��Bold"�5�      ��5�      �� $
 
NoteToggle��$ 	IsChecked�# [   5�      �� *SelectedNumber+# [ $ 9��.��5�      �� $��24(�6   #��[ 5�      $��9��5�      5�      �� 5�   
   $��10��6   $��5q�5�      ��5�      �� $
 
NoteToggle��$ 	IsChecked�# [  d� 5�   
   -   5         d�Name$ 
NoteToggle��5�      $��1��5�      $��10��5�      $��5q�5�      $��Center=�5�      $��Center=�5�      
������� 5�      �� *
NoteMode+.��5�      �� $3��-..\..\Resources\Images\PlayArea\game_note.png��6   $��Uniform=�6K   5�      5�      5�      