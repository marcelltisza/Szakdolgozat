using Sudoku.UI.Models.GameModels;
using Sudoku.UI.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Sudoku.UI.Views.UserControls
{
    public partial class GridCell : UserControl
    {
        #region Properties

        private GridCellViewModel gridCellViewModel;

        public string SelectedNumber
        {
            get { return (string)GetValue(SelectedNumberProperty); }
            set { SetValue(SelectedNumberProperty, value); }
        }

        public static readonly DependencyProperty SelectedNumberProperty =
            DependencyProperty.Register("SelectedNumber", typeof(string), typeof(GridCell), new PropertyMetadata(null));

        public bool NoteMode
        {
            get { return (bool)GetValue(NoteModeProperty); }
            set { SetValue(NoteModeProperty, value); }
        }

        public static readonly DependencyProperty NoteModeProperty =
            DependencyProperty.Register("NoteMode", typeof(bool), typeof(GridCell), new PropertyMetadata(null));

        #endregion

        public GridCell(SudokuCell sudokuCell)
        {
            InitializeComponent();

            gridCellViewModel = new GridCellViewModel(sudokuCell);
            DataContext = gridCellViewModel;
        }

        private void ValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (NoteMode)
            {
                gridCellViewModel.SudokuCell.SetNote(Convert.ToInt32(SelectedNumber) - 1, SelectedNumber);
            }
            else
            {
                gridCellViewModel.SudokuCell.Value = SelectedNumber;
            }
        }
    }
}
