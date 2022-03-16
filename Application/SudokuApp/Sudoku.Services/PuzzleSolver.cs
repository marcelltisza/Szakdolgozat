using Sudoku.Models.GameModels;
using System;

namespace Sudoku.Services
{
    public class PuzzleSolver
    {
        public bool IsSafe(SudokuBoard sudokuBoard, int row, int col, int num)
        {
            var board = sudokuBoard.Cells;
            for (int d = 0; d < board.GetLength(0); d++)
            {
                if (board[row][d].Value == num.ToString())
                {
                    return false;
                }
            }

            for (int r = 0; r < board.GetLength(0); r++)
            {

                if (board[r][col].Value == num.ToString())
                {
                    return false;
                }
            }

            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                 r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                     d < boxColStart + sqrt; d++)
                {
                    if (board[r][d].Value == num.ToString())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool SolveSudoku(SudokuBoard sudokuBoard)
        {
            var board = sudokuBoard.Cells;
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i][j].Value == "")
                    {
                        row = i;
                        col = j;

                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            if (isEmpty)
            {
                return true;
            }

            for (int num = 1; num <= 9; num++)
            {
                if (IsSafe(sudokuBoard, row, col, num))
                {

                    board[row][col].Value = num.ToString();

                    if (SolveSudoku(sudokuBoard))
                    {
                        return true;
                    }
                    else
                    {
                        board[row][col].Value = "";
                    }
                }
            }
            return false;
        }

        //public int SolveSudoku(SudokuBoard board, int i=0, int j=0, int count=0 /*initailly called with 0*/)
        //{
        //    var cells = board.Cells;
        //    if (i == 9)
        //    {
        //        i = 0;
        //        if (++j == 9)
        //            return 1 + count;
        //    }
        //    if (cells[i][j].Value != "")  // skip filled cells
        //        return SolveSudoku(board, i + 1, j, count);
        //    // search for 2 solutions instead of 1
        //    // break, if 2 solutions are found
        //    for (int val = 1; val <= 9 && count < 2; ++val)
        //    {
        //        if (IsSafe(board, i, j, val))
        //        {
        //            cells[i][j].Value = val.ToString();
        //            // add additional solutions
        //            count = SolveSudoku(board, i + 1, j, count);
        //        }
        //    }
        //    cells[i][j].Value = ""; // reset on backtrack
        //    return count;
        //}
    }
}
