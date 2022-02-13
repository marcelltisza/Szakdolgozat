using Sudoku.Models.Enumerations;
using Sudoku.Models.Events;
using System;
using System.ComponentModel;

namespace Sudoku.Models.GameModels
{
    public class SudokuCell : INotifyPropertyChanged
    {
        public event EventHandler<CellChangedEventArgs> CellChanged = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string value;
        private string notes;

        public bool IsFixed { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public string Value
        {
            get => value;
            set
            {
                var oldValue = this.value;
                if (this.value != value)
                {
                    this.value = value;
                }
                else
                {
                    this.value = "";
                }
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
                CellChanged(this, new CellChangedEventArgs
                {
                    OldValue = oldValue,
                    NewValue = this.value,
                    TargetContent = TargetContentType.CellValue
                });
            }
        }
        public string Notes //e.g.: 100111001
        { 
            get => notes;
            set
            {
                if (this.notes != value)
                {
                    this.notes = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(Notes)));
                }
            }
        }

        public SudokuCell(string value, string notes = "000000000", bool isFixed = false)
        {
            Value = value;
            Notes = notes;
            IsFixed = isFixed;
        }

        public void SetNote(int index, string value)
        {
            string newNotes = "";
            string oldValue = Notes[index].ToString();
            for (int i = 0; i < Notes.Length; i++)
            {
                if (i == index)
                {
                    if (Notes[i].ToString() == value)
                        newNotes += '0';
                    else
                        newNotes += value;
                }
                else
                {
                    newNotes += Notes[i];
                }
            }
            Notes = newNotes;
            CellChanged(this, new CellChangedEventArgs
            {
                OldValue = oldValue,
                NewValue = Notes[index].ToString(),
                TargetContent = TargetContentType.Note
            });
        }
    }
}
