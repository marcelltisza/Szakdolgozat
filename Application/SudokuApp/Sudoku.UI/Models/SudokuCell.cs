using System;

namespace Sudoku.UI.Models
{
    public class SudokuCell
    {
        public bool IsFixed { get; set; }
        public string Value { get; set; }
        public string Notes { get; set; } //e.g.: 100111001

        public SudokuCell(string value, string notes = "000000000", bool isFixed = false)
        {
            Value = value;
            Notes = notes;
            IsFixed = isFixed;
        }
    }
}
