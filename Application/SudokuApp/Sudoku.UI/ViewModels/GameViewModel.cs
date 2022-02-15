using Sudoku.Models.Extensions;
using Sudoku.Models.GameModels;
using Sudoku.UI.Commands;
using Sudoku.UI.Stores;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sudoku.UI.ViewModels
{
    public class GameViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region Properties
        public RelayCommand<SudokuCell> ClickCommand { get; set; }
        public RelayCommand UndoCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }
        public RelayCommand UndoPreceedingCommand { get; set; }

        public HistoryEntry SelectedHistoryEntry { get; set; }
        public ObservableCollection<HistoryEntry> History { get; set; }
        private DispatcherTimer _timer;
        private TimeSpan playTime;
        private bool isGameOver;
        private bool noteMode;
        public ICommand NavigateToMenuCommand { get; }

        public TimeSpan PlayTime
        {
            get => playTime;
            set
            {
                if (playTime != value)
                {
                    playTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(PlayTime)));
                }
            }
        }
        public SudokuBoard Board { get; set; }
        public string SelectedNumber { get; set; }
        public bool NoteMode 
        { 
            get => noteMode;
            set
            {
                if (value != noteMode)
                {
                    noteMode = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(NoteMode)));
                }

            }
        }
        public bool IsGameOver
        {
            get => isGameOver;
            set
            {
                if (value != isGameOver)
                {
                    isGameOver = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsGameOver)));
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        #region Constructor
        public GameViewModel(NavigationStore navigationStore)
        {
            Board = new SudokuBoard(GenerateTestInput());
            ClickCommand = new RelayCommand<SudokuCell>(OnClick);
            ResetCommand = new RelayCommand(OnReset);
            UndoCommand = new RelayCommand(OnUndo);
            UndoPreceedingCommand = new RelayCommand(OnUndoPreceeding);
            NavigateToMenuCommand = new NavigateCommand<MenuViewModel>(navigationStore, () => new MenuViewModel(navigationStore));

            History = Board.History;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }
        #endregion

        #region Methods
        private void OnReset()
        {
            while (History.Count > 0)
            {
                var firstEntry = History.FirstOrDefault();
                firstEntry.Undo();
                History.Remove(firstEntry);
                History.Remove(History.First());
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            PlayTime = PlayTime.Add(new TimeSpan(0, 0, 1));
        }

        private void OnUndo()
        {
            if (History.Count > 0)
            {
                if (SelectedHistoryEntry == null)
                {
                    var firstEntry = History.FirstOrDefault();
                    firstEntry.Undo();
                    History.Remove(firstEntry);
                }
                else
                {
                    SelectedHistoryEntry.Undo();
                    History.Remove(SelectedHistoryEntry);
                }
                History.Remove(History.First());
            }
        }

        private void OnUndoPreceeding()
        {
            int maxIndex = History.IndexOf(SelectedHistoryEntry);

            for (int i = 0; i <= maxIndex; i++)
            {
                History.First().Undo();
                History.Remove(History.First());
                History.Remove(History.First());
            }
        }

        private void OnClick(SudokuCell cell)
        {
            if (NoteMode)
            {
                cell.SetNote(Convert.ToInt32(SelectedNumber) - 1, SelectedNumber);
            }
            else
            {
                cell.Value = SelectedNumber;
            }

            if (Board.IsFull())
            {
                IsGameOver = true;
            }
            else
            {
                IsGameOver = false;
            }
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
        #endregion
    }
}