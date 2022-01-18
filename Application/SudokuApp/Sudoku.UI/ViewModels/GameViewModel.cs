using Sudoku.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.UI.ViewModels
{
    public class GameViewModel
    {
        public SudokuBoard Board { get; set; }

        public GameViewModel()
        {
            Board = new SudokuBoard(GenerateTestInput()); // DELETE LATER
        }

        public SudokuCell[,] GenerateTestInput()
        {
            SudokuCell[,] cells = new SudokuCell[9, 9];

            cells[0, 0] = new SudokuCell("8", isFixed: true);
            cells[0, 1] = new SudokuCell("");
            cells[0, 2] = new SudokuCell("");
            cells[0, 3] = new SudokuCell("4", isFixed: true);
            cells[0, 4] = new SudokuCell("");
            cells[0, 5] = new SudokuCell("");
            cells[0, 6] = new SudokuCell("");
            cells[0, 7] = new SudokuCell("");
            cells[0, 8] = new SudokuCell("9", isFixed: true);

            cells[1, 0] = new SudokuCell("");
            cells[1, 1] = new SudokuCell("");
            cells[1, 2] = new SudokuCell("");
            cells[1, 3] = new SudokuCell("");
            cells[1, 4] = new SudokuCell("");
            cells[1, 5] = new SudokuCell("6", isFixed: true);
            cells[1, 6] = new SudokuCell("2", isFixed: true);
            cells[1, 7] = new SudokuCell("");
            cells[1, 8] = new SudokuCell("");

            cells[2, 0] = new SudokuCell("");
            cells[2, 1] = new SudokuCell("6", isFixed: true);
            cells[2, 2] = new SudokuCell("2", isFixed: true);
            cells[2, 3] = new SudokuCell("");
            cells[2, 4] = new SudokuCell("7", isFixed: true);
            cells[2, 5] = new SudokuCell("");
            cells[2, 6] = new SudokuCell("8", isFixed: true);
            cells[2, 7] = new SudokuCell("4", isFixed: true);
            cells[2, 8] = new SudokuCell("");

            cells[3, 0] = new SudokuCell("");
            cells[3, 1] = new SudokuCell("8", isFixed: true);
            cells[3, 2] = new SudokuCell("");
            cells[3, 3] = new SudokuCell("6", isFixed: true);
            cells[3, 4] = new SudokuCell("");
            cells[3, 5] = new SudokuCell("");
            cells[3, 6] = new SudokuCell("4", isFixed: true);
            cells[3, 7] = new SudokuCell("5", isFixed: true);
            cells[3, 8] = new SudokuCell("");

            cells[4, 0] = new SudokuCell("7", isFixed: true);
            cells[4, 1] = new SudokuCell("");
            cells[4, 2] = new SudokuCell("");
            cells[4, 3] = new SudokuCell("9", isFixed: true);
            cells[4, 4] = new SudokuCell("");
            cells[4, 5] = new SudokuCell("1", isFixed: true);
            cells[4, 6] = new SudokuCell("");
            cells[4, 7] = new SudokuCell("");
            cells[4, 8] = new SudokuCell("8", isFixed: true);

            cells[5, 0] = new SudokuCell("");
            cells[5, 1] = new SudokuCell("4", isFixed: true);
            cells[5, 2] = new SudokuCell("1", isFixed: true);
            cells[5, 3] = new SudokuCell("");
            cells[5, 4] = new SudokuCell("");
            cells[5, 5] = new SudokuCell("2", isFixed: true);
            cells[5, 6] = new SudokuCell("");
            cells[5, 7] = new SudokuCell("7", isFixed: true);
            cells[5, 8] = new SudokuCell("");

            cells[6, 0] = new SudokuCell("");
            cells[6, 1] = new SudokuCell("2", isFixed: true);
            cells[6, 2] = new SudokuCell("7", isFixed: true);
            cells[6, 3] = new SudokuCell("");
            cells[6, 4] = new SudokuCell("1", isFixed: true);
            cells[6, 5] = new SudokuCell("");
            cells[6, 6] = new SudokuCell("6", isFixed: true);
            cells[6, 7] = new SudokuCell("8", isFixed: true);
            cells[6, 8] = new SudokuCell("");

            cells[7, 0] = new SudokuCell("");
            cells[7, 1] = new SudokuCell("");
            cells[7, 2] = new SudokuCell("4", isFixed: true);
            cells[7, 3] = new SudokuCell("7", isFixed: true);
            cells[7, 4] = new SudokuCell("");
            cells[7, 5] = new SudokuCell("");
            cells[7, 6] = new SudokuCell("");
            cells[7, 7] = new SudokuCell("");
            cells[7, 8] = new SudokuCell("");

            cells[8, 0] = new SudokuCell("5", isFixed: true);
            cells[8, 1] = new SudokuCell("");
            cells[8, 2] = new SudokuCell("");
            cells[8, 3] = new SudokuCell("");
            cells[8, 4] = new SudokuCell("");
            cells[8, 5] = new SudokuCell("3", isFixed: true);
            cells[8, 6] = new SudokuCell("");
            cells[8, 7] = new SudokuCell("");
            cells[8, 8] = new SudokuCell("4", isFixed: true);

            return cells;
        }

        public bool CheckErrorsForCell(int row, int column)
        {
            var rowResult = RowErrorFound(row);
            var columnResult = ColumnErrorFound(column);
            var mingridResult = MingridErrorFound(row, column);
            var result = rowResult && columnResult && mingridResult;
            return result;
        }

        public bool RowErrorFound(int row)
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

        public bool ColumnErrorFound(int column)
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

        public bool MingridErrorFound(int row, int column)
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