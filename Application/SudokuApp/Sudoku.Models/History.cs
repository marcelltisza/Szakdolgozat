using System.Collections.ObjectModel;

namespace Sudoku.Models
{
    public class History
    {
        public ObservableCollection<HistoryEntry> Entries { get; set; }

        public History()
        {
            Entries = new ObservableCollection<HistoryEntry>();
        }
    }
}
