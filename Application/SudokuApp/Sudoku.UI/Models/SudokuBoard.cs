using System.Collections.Generic;

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
            SetRows();
            SetColumns();
            SetMinigrids();
        }

        private void SetRows()
        {
            Rows = new SudokuCell[9][];
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                Rows[i] = new SudokuCell[9];
                var row = Rows[i];
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    row[j] = Cells[i, j];
                }
            }
        }

        private void SetColumns()
        {
            Columns = new SudokuCell[9][];
            for (int i = 0; i < Cells.GetLength(1); i++)
            {
                Columns[i] = new SudokuCell[9];
                var column = Columns[i];
                for (int j = 0; j < Cells.GetLength(0); j++)
                {
                    column[j] = Cells[j, i];
                }
            }
        }

        private void SetMinigrids()
        {
            var minigrids = new List<SudokuCell[]>();


            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    minigrids.Add(GetMingrid(i, j));
                }
            }

            Minigrids = minigrids.ToArray();
        }

        private SudokuCell[] GetMingrid(int row, int column)
        {
            var minigrid = new List<SudokuCell>();

            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                if (3 * row <= i && 3 * row + 3 > i)
                {
                    for (int j = 0; j < Cells.GetLength(1); j++)
                    {
                        if (3 * column <= j && 3 * column + 3 > j)
                        {
                            minigrid.Add(Cells[i, j]);
                        }
                    }
                }
            }

            return minigrid.ToArray();
        }
    }
}
