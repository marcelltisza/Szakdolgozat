using Sudoku.Models.Enumerations;
using System;

namespace Sudoku.Models.GameModels
{
    public class HistoryEntry
    {
        private string imageSource;
        private string description;
        private ActionType action;
        private string newvalue;
        private string newValue;
        private string oldValue;

        public int Row { get; set; }
        public int Column { get; set; }
        public string OldValue { get => oldValue; set => oldValue = value == "0" ? "" : value; }
        public string NewValue { get => newValue; set => newValue = value == "0" ? "" : value; }
        public TargetContentType Content { get; set; }
        public ActionType Action
        {
            get
            {
                if (string.IsNullOrEmpty(OldValue) && !string.IsNullOrEmpty(NewValue))
                {
                    return ActionType.Add;
                }
                else if (!string.IsNullOrEmpty(OldValue) && string.IsNullOrEmpty(NewValue))
                {
                    return ActionType.Delete;
                }
                else if (!string.IsNullOrEmpty(OldValue) && !string.IsNullOrEmpty(NewValue))
                {
                    return ActionType.Change;
                }
                return ActionType.Error;
            }
            set => action = value;
        }

        public string ImageSource
        {
            get
            {
                switch (Action)
                {
                    case ActionType.Add:
                        return "../../Resources/Images/History/history_add.png";
                    case ActionType.Change:
                        return "../../Resources/Images/History/history_change.png";
                    case ActionType.Delete:
                        return "../../Resources/Images/History/history_delete.png";
                    default:
                        throw new Exception(message: "Invalid action type");
                }
            }
        }

        public string Description
        {
            get
            {
                var contentString = Content == TargetContentType.Note ? "Note" : "Value";
                switch (Action)
                {
                    case ActionType.Add:
                    case ActionType.Change:
                        return $"{contentString} {NewValue} inserted at ({Row + 1}, {Column + 1})";
                    case ActionType.Delete:
                        return $"{contentString} {OldValue} deleted from ({Row + 1}, {Column + 1})";
                    default:
                        throw new Exception(message: "Invalid action type");
                }
            }
        }

        public void Undo(SudokuBoard board)
        {
            switch (Content)
            {
                case TargetContentType.CellValue:
                    board.Cells[Row][Column].Value = OldValue;
                    break;
                case TargetContentType.Note:
                    if (OldValue == "")
                        board.Cells[Row][Column].SetNote(Convert.ToInt32(NewValue) - 1, "0");
                    else
                        board.Cells[Row][Column].SetNote(Convert.ToInt32(OldValue) - 1, OldValue);
                    break;
            }
        }

        public void Redo(SudokuBoard board)
        {
            switch (Content)
            {
                case TargetContentType.CellValue:
                    board.Cells[Row][Column].Value = NewValue;
                    break;
                case TargetContentType.Note:
                    if (NewValue == "")
                        board.Cells[Row][Column].SetNote(Convert.ToInt32(OldValue) - 1, "0");
                    else
                        board.Cells[Row][Column].SetNote(Convert.ToInt32(NewValue) - 1, NewValue);
                    break;
            }
        }
    }
}