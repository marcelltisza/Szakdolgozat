using Sudoku.UI.Models;
using System.IO;

namespace Sudoku.Services
{
    public class IOServices
    {
        // basic | will be replaced
        public SudokuBoard ReadPuzzleFromFile()
        {
            SudokuBoard board = new SudokuBoard();
            StreamReader reader = new StreamReader("test.txt");
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
            }

            return board;
        }
    }
}
