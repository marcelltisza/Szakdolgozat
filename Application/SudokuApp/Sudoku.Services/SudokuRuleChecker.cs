using Sudoku.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services
{
    public class SudokuRuleChecker
    {
        public SudokuBoard Board { get; set; }
        // Make it an extension method over the SudokuBoard class
        public SudokuRuleChecker()
        {

        }

        public bool CheckErrorsForCell(int row, int column)
        {
            var rowResult = RowErrorFound(row);
            var columnResult = ColumnErrorFound(column);
            var mingridResult = MingridErrorFound(row, column);
            var result = rowResult && columnResult && mingridResult;
            return result;
        }

        private bool RowErrorFound(int row)
        {
            var NotEmptyCells = Board.Rows[row].Where(cell => cell.Value != "").Select(cell => cell.Value);
            var distinctItems = NotEmptyCells.Distinct().Count();
            int allItems = NotEmptyCells.Count();

            if (distinctItems != allItems)
            {
                return false;
            }
            return true;
        }

        private bool ColumnErrorFound(int column)
        {
            var NotEmptyCells = Board.Columns[column].Where(cell => cell.Value != "").Select(cell => cell.Value);
            var distinctItems = NotEmptyCells.Distinct().Count();
            int allItems = NotEmptyCells.Count();

            if (distinctItems != allItems)
            {
                return false;
            }
            return true;
        }

        private bool MingridErrorFound(int row, int column)
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

            var NotEmptyCells = Board.Minigrids[options.First()].Where(cell => cell.Value != "").Select(cell => cell.Value);
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
