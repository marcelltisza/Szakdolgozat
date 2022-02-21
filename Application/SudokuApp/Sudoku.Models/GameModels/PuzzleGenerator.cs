using Sudoku.Models.Enumerations;
using System;

namespace Sudoku.Models.GameModels
{
    public class PuzzleGenerator
    {
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
            return new SudokuBoard(GenerateTestInput());
        }

        private SudokuBoard GenerateMediumPuzzle()
        {
            return new SudokuBoard(GenerateTestInput());
        }

        private SudokuBoard GenerateHardPuzzle()
        {
            return new SudokuBoard(GenerateTestInput());
        }

        private SudokuCell[,] GenerateTestInput()
        {
            SudokuCell[,] cells = new SudokuCell[9, 9];

            cells[0, 0] = new SudokuCell("8", isFixed: true);
            cells[0, 1] = new SudokuCell("", "000000000");
            cells[0, 2] = new SudokuCell("");
            cells[0, 3] = new SudokuCell("4", isFixed: true);
            cells[0, 4] = new SudokuCell("");
            cells[0, 5] = new SudokuCell("", "000000000");
            cells[0, 6] = new SudokuCell("");
            cells[0, 7] = new SudokuCell("");
            cells[0, 8] = new SudokuCell("9", isFixed: true);

            cells[1, 0] = new SudokuCell("", "000000000");
            cells[1, 1] = new SudokuCell("");
            cells[1, 2] = new SudokuCell("");
            cells[1, 3] = new SudokuCell("", "000000000");
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
            cells[2, 8] = new SudokuCell("", "000000000");

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

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    cells[i, j].Row = i;
                    cells[i, j].Column = j;
                }
            }

            return cells;
        }
    }
}
