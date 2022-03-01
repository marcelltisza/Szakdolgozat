using Sudoku.Models.GameModels;
using System;
using System.Threading.Tasks;

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

        public bool SolveSudoku(SudokuBoard sudokuBoard, int n)
        {
            var board = sudokuBoard.Cells;
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
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

            for (int num = 1; num <= n; num++)
            {
                if (IsSafe(sudokuBoard, row, col, num))
                {
                    
                    board[row][col].Value = num.ToString();

                    if (SolveSudoku(sudokuBoard, n))
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
    }
}
