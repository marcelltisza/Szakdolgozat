using System.ComponentModel;
using System.Windows;

namespace Sudoku.UI.Models.GameModels
{
    public delegate void CellChangedHandler(string value);

    public class SudokuCell : INotifyPropertyChanged
    {
        public event CellChangedHandler CellChanged = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string value;
        private string notes;

        public bool IsFixed { get; set; }

        public string Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                }
                else
                {
                    this.value = "";
                }
                CellChanged(value);
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Value)));
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
        }
    }
}
