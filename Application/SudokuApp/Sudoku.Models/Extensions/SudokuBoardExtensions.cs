using Sudoku.Models.GameModels;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Models.Extensions
{
    public static class SudokuBoardExtensions
    {
        public static bool CheckErrorsForCell(this SudokuBoard board, int row, int column)
        {
            var rowResult = RowErrorFound(board, row);
            var columnResult = ColumnErrorFound(board, column);
            var mingridResult = MingridErrorFound(board, row, column);
            var result = rowResult && columnResult && mingridResult;
            return result;
        }

        public static bool IsFull(this SudokuBoard board)
        {
            for (int i = 0; i < board.Cells.Length; i++)
            {
                for (int j = 0; j < board.Cells[i].Length; j++)
                {
                    if (string.IsNullOrEmpty(board.Cells[i][j].Value))
                        return false;
                }
            }
            return true;
        }

        public static bool IsSolved(this SudokuBoard board)
        {
            for (int i = 0; i < board.Cells.Length; i++)
            {
                for (int j = 0; j < board.Cells[i].Length; j++)
                {
                    if (board.CheckErrorsForCell(i, j) == false)
                        return false;
                }
            }
            return true;
        }

        private static bool RowErrorFound(SudokuBoard board, int row)
        {
            var NotEmptyCells = board.Rows[row].Where(cell => cell.Value != "").Select(cell => cell.Value);
            var distinctItems = NotEmptyCells.Distinct().Count();
            int allItems = NotEmptyCells.Count();

            if (distinctItems != allItems)
            {
                return false;
            }
            return true;
        }

        private static bool ColumnErrorFound(SudokuBoard board, int column)
        {
            var NotEmptyCells = board.Columns[column].Where(cell => cell.Value != "").Select(cell => cell.Value);
            var distinctItems = NotEmptyCells.Distinct().Count();
            int allItems = NotEmptyCells.Count();

            if (distinctItems != allItems)
            {
                return false;
            }
            return true;
        }

        private static bool MingridErrorFound(SudokuBoard board, int row, int column)
        {
            var options = new List<int>();
            switch (row)
            {
                case 0:
                case 1:
                case 2:
                    options.AddRange(new List<int> { 0, 1, 2 });
                    break;
                case 3:
                case 4:
                case 5:
                    options.AddRange(new List<int> { 3, 4, 5 });
                    break;
                case 6:
                case 7:
                case 8:
                    options.AddRange(new List<int> { 6, 7, 8 });
                    break;
            }

            switch (column)
            {
                case 0:
                case 1:
                case 2:
                    options = options.Intersect(new List<int> { 0, 3, 6 }).ToList();
                    break;
                case 3:
                case 4:
                case 5:
                    options = options.Intersect(new List<int> { 1, 4, 7 }).ToList();
                    break;
                case 6:
                case 7:
                case 8:
                    options = options.Intersect(new List<int> { 2, 5, 8 }).ToList();
                    break;
            }

            var NotEmptyCells = board.Minigrids[options.First()].Where(cell => cell.Value != "").Select(cell => cell.Value);
            var distinctItems = NotEmptyCells.Distinct().Count();
            var distinct = NotEmptyCells.Distinct();
            int allItems = NotEmptyCells.Count();

            if (distinctItems != allItems)
            {
                return false;
            }

            return true;
        }
    }
}
