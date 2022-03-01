using Sudoku.Models.Enumerations;
using Sudoku.Models.GameModels;
using System;

namespace Sudoku.Services
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
            return new SudokuBoard { Cells = GenerateTestInput(),  Difficulty = DifficultyType.Easy };
        }

        private SudokuBoard GenerateMediumPuzzle()
        {
            return new SudokuBoard { Cells = GenerateTestInput(), Difficulty = DifficultyType.Medium };
        }

        private SudokuBoard GenerateHardPuzzle()
        {
            return new SudokuBoard { Cells = GenerateTestInput(), Difficulty = DifficultyType.Hard };
        }

        private SudokuCell[][] GenerateTestInput()
        {
            SudokuCell[][] cells = new SudokuCell[9][];

            cells[0] = new SudokuCell[9];
            cells[0][0] = new SudokuCell { Value = "8", IsFixed =  true };
            cells[0][1] = new SudokuCell();
            cells[0][2] = new SudokuCell();
            cells[0][3] = new SudokuCell { Value = "4", IsFixed = true };
            cells[0][4] = new SudokuCell();
            cells[0][5] = new SudokuCell();
            cells[0][6] = new SudokuCell();
            cells[0][7] = new SudokuCell();
            cells[0][8] = new SudokuCell { Value = "9", IsFixed = true };

            cells[1] = new SudokuCell[9];
            cells[1][0] = new SudokuCell();
            cells[1][1] = new SudokuCell();
            cells[1][2] = new SudokuCell();
            cells[1][3] = new SudokuCell();
            cells[1][4] = new SudokuCell();
            cells[1][5] = new SudokuCell { Value = "6", IsFixed = true };
            cells[1][6] = new SudokuCell { Value = "2", IsFixed = true };
            cells[1][7] = new SudokuCell();
            cells[1][8] = new SudokuCell();

            cells[2] = new SudokuCell[9];
            cells[2][0] = new SudokuCell();
            cells[2][1] = new SudokuCell { Value = "6", IsFixed = true };
            cells[2][2] = new SudokuCell { Value = "2", IsFixed = true };
            cells[2][3] = new SudokuCell();
            cells[2][4] = new SudokuCell { Value = "7", IsFixed = true };
            cells[2][5] = new SudokuCell();
            cells[2][6] = new SudokuCell { Value = "8", IsFixed = true };
            cells[2][7] = new SudokuCell { Value = "4", IsFixed = true };
            cells[2][8] = new SudokuCell();

            cells[3] = new SudokuCell[9];
            cells[3][0] = new SudokuCell();
            cells[3][1] = new SudokuCell { Value = "8", IsFixed = true };
            cells[3][2] = new SudokuCell();
            cells[3][3] = new SudokuCell { Value = "6", IsFixed = true };
            cells[3][4] = new SudokuCell();
            cells[3][5] = new SudokuCell();
            cells[3][6] = new SudokuCell { Value = "4", IsFixed = true };
            cells[3][7] = new SudokuCell { Value = "5", IsFixed = true };
            cells[3][8] = new SudokuCell();

            cells[4] = new SudokuCell[9];
            cells[4][0] = new SudokuCell { Value = "7", IsFixed = true };
            cells[4][1] = new SudokuCell();
            cells[4][2] = new SudokuCell();
            cells[4][3] = new SudokuCell { Value = "9", IsFixed = true };
            cells[4][4] = new SudokuCell();
            cells[4][5] = new SudokuCell { Value = "1", IsFixed = true };
            cells[4][6] = new SudokuCell();
            cells[4][7] = new SudokuCell();
            cells[4][8] = new SudokuCell { Value = "8", IsFixed = true };

            cells[5] = new SudokuCell[9];
            cells[5][0] = new SudokuCell();
            cells[5][1] = new SudokuCell { Value = "4", IsFixed = true };
            cells[5][2] = new SudokuCell { Value = "1", IsFixed = true };
            cells[5][3] = new SudokuCell();
            cells[5][4] = new SudokuCell();
            cells[5][5] = new SudokuCell { Value = "2", IsFixed = true };
            cells[5][6] = new SudokuCell();
            cells[5][7] = new SudokuCell { Value = "7", IsFixed = true };
            cells[5][8] = new SudokuCell();

            cells[6] = new SudokuCell[9];
            cells[6][0] = new SudokuCell();
            cells[6][1] = new SudokuCell { Value = "2", IsFixed = true };
            cells[6][2] = new SudokuCell { Value = "7", IsFixed = true };
            cells[6][3] = new SudokuCell();
            cells[6][4] = new SudokuCell { Value = "1", IsFixed = true };
            cells[6][5] = new SudokuCell();
            cells[6][6] = new SudokuCell { Value = "6", IsFixed = true };
            cells[6][7] = new SudokuCell { Value = "8", IsFixed = true };
            cells[6][8] = new SudokuCell();

            cells[7] = new SudokuCell[9];
            cells[7][0] = new SudokuCell();
            cells[7][1] = new SudokuCell();
            cells[7][2] = new SudokuCell { Value = "4", IsFixed = true };
            cells[7][3] = new SudokuCell { Value = "7", IsFixed = true };
            cells[7][4] = new SudokuCell();
            cells[7][5] = new SudokuCell();
            cells[7][6] = new SudokuCell();
            cells[7][7] = new SudokuCell();
            cells[7][8] = new SudokuCell();

            cells[8] = new SudokuCell[9];
            cells[8][0] = new SudokuCell { Value = "5", IsFixed = true };
            cells[8][1] = new SudokuCell();
            cells[8][2] = new SudokuCell();
            cells[8][3] = new SudokuCell();
            cells[8][4] = new SudokuCell();
            cells[8][5] = new SudokuCell { Value = "3", IsFixed = true };
            cells[8][6] = new SudokuCell();
            cells[8][7] = new SudokuCell();
            cells[8][8] = new SudokuCell { Value = "4", IsFixed = true };

            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    cells[i][j].Row = i;
                    cells[i][j].Column = j;
                }
            }

            return cells;
        }
    }
}
