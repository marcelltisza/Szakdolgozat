using Sudoku.Models.Enumerations;
using Sudoku.Models.Extensions;
using Sudoku.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.Services
{
    public class PuzzleGenerator
    {
        private const int TotalNumberOfCells = 41;

        protected struct CellToDig 
        {
            public int Row;
            public int Column;
        }

        private PuzzleSolver _solver = new PuzzleSolver();

        public SudokuBoard Generate(DifficultyType difficulty)
        {
            switch (difficulty)
            {
                case DifficultyType.Easy:
                    return GenerateEasyPuzzle();
                case DifficultyType.Medium:
                    return GenerateMediumPuzzle();
                case DifficultyType.Hard:
                    return GenerateHardPuzzle();
                default:
                    throw new Exception();
            }
        }

        private SudokuBoard GenerateEasyPuzzle()
        {
            var startTime = DateTime.Now;
            var board = new SudokuBoard();
            board.Difficulty = DifficultyType.Easy;
            Random random = new Random();
            var numberOfHoles = TotalNumberOfCells - random.Next(18, 25);
            var lowerBound = 4;
            var cellsToDig = InitializeCellsToDig(board);

            InitializeBoard(board, 11);
            _solver.SolveSudoku(board);
            DigHoles(board, cellsToDig, numberOfHoles, lowerBound);
            FinalizeBoard(board);
            var endTime = DateTime.Now;

            board.GenerationTime = (int)Math.Ceiling((endTime - startTime).TotalMilliseconds);

            return board;
        }

        private SudokuBoard GenerateMediumPuzzle()
        {
            var startTime = DateTime.Now;
            var board = new SudokuBoard();
            board.Difficulty = DifficultyType.Medium;
            Random random = new Random();
            var numberOfHoles = random.Next(16, 18);
            var lowerBound = 3;
            var cellsToDig = InitializeCellsToDig(board);

            InitializeBoard(board, 11);
            _solver.SolveSudoku(board);
            DigHoles(board, cellsToDig, numberOfHoles, lowerBound);
            FinalizeBoard(board);
            var endTime = DateTime.Now;

            board.GenerationTime = (int)Math.Ceiling((endTime - startTime).TotalMilliseconds);

            return board;
        }

        private SudokuBoard GenerateHardPuzzle()
        {
            var startTime = DateTime.Now;
            var board = new SudokuBoard();
            board.Difficulty = DifficultyType.Hard;
            Random random = new Random();
            var numberOfHoles = TotalNumberOfCells - random.Next(14, 16);
            var lowerBound = 2;
            var cellsToDig = InitializeCellsToDig(board);

            InitializeBoard(board, 11);
            _solver.SolveSudoku(board);
            DigHoles(board, cellsToDig, numberOfHoles, lowerBound);
            FinalizeBoard(board);
            var endTime = DateTime.Now;

            board.GenerationTime = (int)Math.Ceiling((endTime - startTime).TotalMilliseconds);

            return board;
        }

        private void InitializeBoard(SudokuBoard board, int initialCells)
        {
            Random random = new Random();
            for (int i = 0; i < initialCells; i++)
            {
                int row = random.Next(0, 9);
                int column = random.Next(0, 9);
                if (!string.IsNullOrEmpty(board.Cells[row][column].Value))
                    i--;
                else
                {
                    do
                    {
                        board.Cells[row][column].Value = random.Next(1, 10).ToString();
                    } while (board.CellHasError(row, column));
                }
            }
        }

        private List<CellToDig> InitializeCellsToDig(SudokuBoard board)
        {
            var cellsToDig = new List<CellToDig>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < board.Rows[i].Length; j++)
                {
                    cellsToDig.Add(new CellToDig { Row = i, Column = j });
                }
            }
            cellsToDig.Add(new CellToDig { Row = 4, Column = 0 });
            cellsToDig.Add(new CellToDig { Row = 4, Column = 1 });
            cellsToDig.Add(new CellToDig { Row = 4, Column = 2 });
            cellsToDig.Add(new CellToDig { Row = 4, Column = 3 });
            cellsToDig.Add(new CellToDig { Row = 4, Column = 4 });

            return cellsToDig;
        }

        private void DigHoles(SudokuBoard board, List<CellToDig> cellsToDig, int numberOfHoles, int lowerBound)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfHoles; i++)
            {
                if (cellsToDig.Count == 0)
                {
                    break;
                }

                var index = random.Next(0, cellsToDig.Count);
                int row = cellsToDig[index].Row;
                int column = cellsToDig[index].Column;
                var oldValue = board.Cells[row][column].Value;

                if (DigWillResultUniqueSolution(board, row, column) == false && DigWillResultUniqueSolution(board, 8 - row, 8 - column) == false)
                {
                    i--;
                    var isFull = board.IsFull();
                    var hasErrors = board.HasErrors();
                    cellsToDig.RemoveAt(index);
                    continue;
                }

                // if (DigWillResultUniqueSolution(board, 8 - row, 8 - column) == false)
                //{

                //}

                board.Cells[row][column].Value = "";
                if (CheckRestrictions(board, lowerBound) == false)
                {
                    i--;
                    board.Cells[row][column].Value = oldValue;
                    cellsToDig.RemoveAt(index);
                    continue;
                }

                board.Cells[8 - row][8 - column].Value = "";
                cellsToDig.RemoveAt(index);
            }
        }

        private bool CheckRestrictions(SudokuBoard board, int lowerBound)
        {
            var rowsSatisfyCondition = board.Rows.All(cells => cells.Count(cell => !string.IsNullOrEmpty(cell.Value)) >= lowerBound);
            var columnsSatisfyCondition = board.Columns.All(cells => cells.Count(cell => !string.IsNullOrEmpty(cell.Value)) >= lowerBound);

            return rowsSatisfyCondition && columnsSatisfyCondition;
        }

        private bool DigWillResultUniqueSolution(SudokuBoard board, int row, int column)
        {
            var unchangedVersionOfBoard = CopyCells(board.Cells);
            bool hasUniqueSolution = true;
            var oldValue = board.Cells[row][column].Value;

            for (int i = 1; i <= 9; i++)
            {
                if (oldValue != i.ToString())
                {
                    board.Cells[row][column].Value = i.ToString();
                    if (!board.CellHasError(row, column))
                    {
                        _solver.SolveSudoku(board);
                        if (board.IsSolved())
                        {
                            hasUniqueSolution = false;
                            break;
                        }
                    }  
                    board.Cells = CopyCells(unchangedVersionOfBoard);
                }
            }

            board.Cells = CopyCells(unchangedVersionOfBoard);
            if (hasUniqueSolution)
                return true;
            return false;
        }

        private void FinalizeBoard(SudokuBoard board)
        {
            for (int i = 0; i < board.Cells.Length; i++)
            {
                for (int j = 0; j < board.Cells[i].Length; j++)
                {
                    if (!string.IsNullOrEmpty(board.Cells[i][j].Value))
                    {
                        board.Cells[i][j].IsFixed = true;
                    }
                    board.Cells[i][j].Row = i;
                    board.Cells[i][j].Column = j;
                }
            }
            board.History.Clear();
            board.RedoHistory.Clear();
        }

        private SudokuCell[][] CopyCells(SudokuCell[][] cellsToCopy)
        {
            var cells = new SudokuCell[9][];
            for (int i = 0; i < cellsToCopy.Length; i++)
            {
                cells[i] = new SudokuCell[9];
                for (int j = 0; j < cellsToCopy[i].Length; j++)
                {
                    cells[i][j] = new SudokuCell { Value = cellsToCopy[i][j].Value };
                }
            }

            return cells;
        }
    }
}
