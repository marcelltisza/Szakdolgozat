using Sudoku.Models.Enumerations;
using Sudoku.Models.GameModels;
using Sudoku.Services;
using Sudoku.UI.Commands;
using Sudoku.UI.Stores;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

namespace Sudoku.UI.ViewModels
{
    public class SolverViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand NavigateToMenuCommand { get; }

        public RelayCommand SolvePuzzle { get; }
        public RelayCommand<SudokuCell> ClickCommand { get; }
        public RelayCommand ClearCommand { get; }
        public RelayCommand GenerateCommand { get; set; }

        public string LogMessages 
        { 
            get => logMessages; 
            set
            {
                if (logMessages != value)
                {
                    logMessages = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(logMessages)));
                }
            }
        }

        private DifficultyType selectedDifficulty;

        public DifficultyType SelectedDifficulty
        {
            get => selectedDifficulty;
            set
            {
                selectedDifficulty = value;
            }
        }


        private readonly NavigationStore _navigationStore;
        private PuzzleGenerator _generator = new PuzzleGenerator();
        private PuzzleSolver _solver = new PuzzleSolver();

        public SudokuBoard Board
        {
            get => board;
            set
            {
                if (board != value)
                {
                    board = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(board)));
                }
            }
        }

        public ObservableCollection<SudokuCell> Cells { get; set; }

        private bool noteMode;
        private string selectedNumber;
        private SudokuBoard board;
        private string logMessages;

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

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public SolverViewModel(NavigationStore navigationStore)
        {
            Board = new SudokuBoard();

            _navigationStore = navigationStore;
            NavigateToMenuCommand = new NavigateCommand<MenuViewModel>(_navigationStore, (object param) => new MenuViewModel(_navigationStore));
            SolvePuzzle = new RelayCommand(OnSolvePuzzle);
            ClearCommand = new RelayCommand(OnClear);
            ClickCommand = new RelayCommand<SudokuCell>(OnClick);
            GenerateCommand = new RelayCommand(OnGenerate);
        }

        private void OnGenerate()
        {
            Board = _generator.Generate(SelectedDifficulty);
            LogMessages = $"Generated under {Board.GenerationTime} ms\n" + LogMessages;
        }

        private void OnClear()
        {
            for (int i = 0; i < Board.Cells.Length; i++)
            {
                for (int j = 0; j < Board.Cells[i].Length; j++)
                {
                    Board.Cells[i][j].Value = "";
                    Board.Cells[i][j].IsFixed = false;
                }
            }
        }

        private void OnSolvePuzzle()
        {
            var startTime = DateTime.Now;
            _solver.SolveSudoku(Board);
            var endTime = DateTime.Now;
            var duration = (int)Math.Ceiling((endTime - startTime).TotalMilliseconds);
            LogMessages = $"Solved under {duration} ms\n" + LogMessages;
        }

        private void OnClick(SudokuCell cell)
        {
            cell.Value = SelectedNumber;
            if (cell.Value != "")
                cell.IsFixed = true;
            else
                cell.IsFixed = false;
        }
    }
}
