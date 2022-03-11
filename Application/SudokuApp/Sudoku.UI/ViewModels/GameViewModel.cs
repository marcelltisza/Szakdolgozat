using Sudoku.Models.Enumerations;
using Sudoku.Models.Extensions;
using Sudoku.Models.GameModels;
using Sudoku.Services;
using Sudoku.UI.Commands;
using Sudoku.UI.Properties;
using Sudoku.UI.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        public RelayCommand RedoCommand { get; set; }
        public RelayCommand SolveCommand { get; set; }

        public HistoryEntry SelectedHistoryEntry { get; set; }
        public ObservableCollection<HistoryEntry> History { get; set; }
        public ObservableCollection<HistoryEntry> RedoHistory { get; set; }
        private DispatcherTimer _timer;
        private TimeSpan playTime;
        private bool isGameOver;
        private bool noteMode;
        private string selectedNumber;
        private bool gameOverViewIsOpen;
        private readonly NavigationStore navigationStore;

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

        public bool GameOverViewIsOpen
        {
            get => gameOverViewIsOpen;
            set
            {
                if (gameOverViewIsOpen != value)
                {
                    gameOverViewIsOpen = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(GameOverViewIsOpen)));
                }
            }
        }

        public DifficultyType Difficulty { get; set; }

        public SudokuBoard Board { get; set; }
        public string SelectedNumber
        {
            get => selectedNumber;
            set
            {
                if (selectedNumber != value)
                {
                    selectedNumber = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(SelectedNumber)));
                }
            }
        }
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

        private XmlHandler _xmlHandler = new XmlHandler();
        private PuzzleSolver _solver = new PuzzleSolver();
        private string logMessage;

        public string LogDifficultyLevel { get; set; }

        public string LogMessage 
        { 
            get => logMessage;
            set 
            {
                if (logMessage != value)
                {
                    logMessage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(LogMessage)));
                }
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        #region Constructor
        public GameViewModel(NavigationStore navigationStore, SudokuBoard board)
        {
            LogDifficultyLevel = $"Difficulty: {board.Difficulty}";
            LogMessage = $"It took less than {board.GenerationTime.ToString()} ms to generate the puzzle.";
            Difficulty = board.Difficulty;
            PlayTime = board.PlayTime;
            this.navigationStore = navigationStore;
            Board = board;
            ClickCommand = new RelayCommand<SudokuCell>(OnClick);
            ResetCommand = new RelayCommand(OnReset);
            UndoCommand = new RelayCommand(OnUndo);
            RedoCommand = new RelayCommand(OnRedo);
            SolveCommand = new RelayCommand(OnSolve);

            UndoPreceedingCommand = new RelayCommand(OnUndoPreceeding);
            NavigateToMenuCommand = new NavigateCommand<MenuViewModel>(navigationStore, OnNavigateToMenu);

            History = Board.History;
            RedoHistory = Board.RedoHistory;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void OnSolve()
        {
            var startTime = DateTime.Now;
            _solver.SolveSudoku(Board);
            var endTime = DateTime.Now;
            var duration = (int)Math.Ceiling((endTime - startTime).TotalMilliseconds);
            LogMessage = $"It took less than {duration} ms to solve the puzzle";
        }

        private void OnRedo()
        {
            var firstEntry = RedoHistory.FirstOrDefault();
            firstEntry.Redo(Board);
            History.Insert(0, firstEntry);
            RedoHistory.Remove(firstEntry);
            History.Remove(History.First());
        }
        #endregion

        #region Methods

        private MenuViewModel OnNavigateToMenu(object param)
        {
            Board.PlayTime = PlayTime;
            if (!GameOverViewIsOpen)
                _xmlHandler.Write<SudokuBoard>(Board, "savedGame.xml");
            return new MenuViewModel(navigationStore);
        }

        private void OnReset()
        {
            while (History.Count > 0)
            {
                var firstEntry = History.FirstOrDefault();
                firstEntry.Undo(Board);
                RedoHistory.Insert(0, firstEntry);
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
            if (SelectedHistoryEntry == null)
            {
                var firstEntry = History.FirstOrDefault();
                firstEntry.Undo(Board);
                RedoHistory.Insert(0, firstEntry);
                History.Remove(firstEntry);
                History.Remove(History.First());
            }
            else
            {
                OnUndoPreceeding();
            }
        }

        private void OnUndoPreceeding()
        {
            int maxIndex = History.IndexOf(SelectedHistoryEntry);

            for (int i = 0; i <= maxIndex; i++)
            {
                History.First().Undo(Board);
                RedoHistory.Insert(0, History.First());
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

            var full = Board.IsFull();
            var solved = Board.IsSolved();

            if (Board.IsFull() && Board.IsSolved())
            {
                _timer.Stop();
                File.Delete("savedGame.xml");
                GameOverViewIsOpen = true;
            }
        }
        #endregion
    }
}