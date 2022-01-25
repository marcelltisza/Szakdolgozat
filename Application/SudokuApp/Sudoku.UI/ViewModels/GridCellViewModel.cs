using Sudoku.UI.Models.GameModels;
using System.ComponentModel;

namespace Sudoku.UI.ViewModels
{
    public class GridCellViewModel
    {
        public SudokuCell SudokuCell { get; set; }

        public bool NoteMode { get; set; }

        public GridCellViewModel(SudokuCell sudokuCell)
        {
            SudokuCell = sudokuCell;
        }
    }
}
