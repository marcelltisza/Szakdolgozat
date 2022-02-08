using Sudoku.Models.Enumerations;
using System;

namespace Sudoku.Models.Events
{
    public class CellChangedEventArgs : EventArgs
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public TargetContentType TargetContent { get; set; }
    }
}
