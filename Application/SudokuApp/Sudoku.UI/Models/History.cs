using System.Collections.Generic;
using System.Linq;

namespace Sudoku.UI.Models
{
    public class History
    {
        private List<HistoryEntry> _items;
        public History()
        {
            _items = new List<HistoryEntry>();
        }

        public HistoryEntry Pop()
        {
            var item = _items.Last();
            _items.RemoveAt(_items.Count - 1);

            return item;
        }

        public HistoryEntry Pop(int index)
        {
            var item = _items[index];
            _items.RemoveAt(index);

            return item;
        }

        public void Push(HistoryEntry entry)
        {
            _items.Add(entry);
        }
    }
}
