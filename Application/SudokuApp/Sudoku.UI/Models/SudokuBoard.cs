namespace Sudoku.UI.Models
{
    public class SudokuBoard
    {
        public SudokuCell[][] Rows { get; set; }
        public SudokuCell[][] Columns { get; set; }
        public SudokuCell[][] Minigrids { get; set; }
        public SudokuCell[,] Cells { get; set; }

        public SudokuBoard(SudokuCell[,] cells)
        {
            Cells = cells;
        }
    }
}
