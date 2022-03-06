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

        private readonly NavigationStore _navigationStore;

        public SudokuBoard Board { get; set; }

        public ObservableCollection<SudokuCell> Cells { get; set; }

        private bool noteMode;
        private string selectedNumber;

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
            Board = new SudokuBoard { Cells = GenerateTestInput(), Difficulty = DifficultyType.Easy };

            _navigationStore = navigationStore;
            NavigateToMenuCommand = new NavigateCommand<MenuViewModel>(_navigationStore, (object param) => new MenuViewModel(_navigationStore));
            SolvePuzzle = new RelayCommand(OnSolvePuzzle);
            ClearCommand = new RelayCommand(OnClear);
            ClickCommand = new RelayCommand<SudokuCell>(OnClick);

            Cells = new ObservableCollection<SudokuCell>();
            for (int i = 0; i < Board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Cells.GetLength(1); j++)
                {
                    Cells.Add(Board.Cells[i][j]);
                }
            }
        }

        private void OnClear()
        {
            for (int i = 0; i < Board.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Cells.GetLength(1); j++)
                {
                    Board.Cells[i][j].Value = "";
                    Board.Cells[i][j].IsFixed = false;
                }
            }
        }

        private void OnSolvePuzzle()
        {
            PuzzleSolver solver = new PuzzleSolver();
            solver.SolveSudoku(Board);
        }

        private void OnClick(SudokuCell cell)
        {
            cell.Value = SelectedNumber;
            if (cell.Value != "")
                cell.IsFixed = true;
            else
                cell.IsFixed = false;
        }

        private SudokuCell[][] GenerateTestInput()
        {
            SudokuCell[][] cells = new SudokuCell[9][];

            cells[0] = new SudokuCell[9];
            cells[0][0] = new SudokuCell { Value = "8", IsFixed = true };
            cells[0][1] = new SudokuCell();
            cells[0][2] = new SudokuCell();
            cells[0][3] = new SudokuCell { Value = "4", IsFixed = true };
            cells[0][4] = new SudokuCell();
            cells[0][5] = new SudokuCell();
            cells[0][6] = new SudokuCell();
            cells[0][7] = new SudokuCell();
            cells[0][8] = new SudokuCell { Value = "9", IsFixed = true };

            cells[1] = new SudokuCell[9];
            cells[1][0] = new SudokuCell();
            cells[1][1] = new SudokuCell();
            cells[1][2] = new SudokuCell();
            cells[1][3] = new SudokuCell();
            cells[1][4] = new SudokuCell();
            cells[1][5] = new SudokuCell { Value = "6", IsFixed = true };
            cells[1][6] = new SudokuCell { Value = "2", IsFixed = true };
            cells[1][7] = new SudokuCell();
            cells[1][8] = new SudokuCell();

            cells[2] = new SudokuCell[9];
            cells[2][0] = new SudokuCell();
            cells[2][1] = new SudokuCell { Value = "6", IsFixed = true };
            cells[2][2] = new SudokuCell { Value = "2", IsFixed = true };
            cells[2][3] = new SudokuCell();
            cells[2][4] = new SudokuCell { Value = "7", IsFixed = true };
            cells[2][5] = new SudokuCell();
            cells[2][6] = new SudokuCell { Value = "8", IsFixed = true };
            cells[2][7] = new SudokuCell { Value = "4", IsFixed = true };
            cells[2][8] = new SudokuCell();

            cells[3] = new SudokuCell[9];
            cells[3][0] = new SudokuCell();
            cells[3][1] = new SudokuCell { Value = "8", IsFixed = true };
            cells[3][2] = new SudokuCell();
            cells[3][3] = new SudokuCell { Value = "6", IsFixed = true };
            cells[3][4] = new SudokuCell();
            cells[3][5] = new SudokuCell();
            cells[3][6] = new SudokuCell { Value = "4", IsFixed = true };
            cells[3][7] = new SudokuCell { Value = "5", IsFixed = true };
            cells[3][8] = new SudokuCell();

            cells[4] = new SudokuCell[9];
            cells[4][0] = new SudokuCell { Value = "7", IsFixed = true };
            cells[4][1] = new SudokuCell();
            cells[4][2] = new SudokuCell();
            cells[4][3] = new SudokuCell { Value = "9", IsFixed = true };
            cells[4][4] = new SudokuCell();
            cells[4][5] = new SudokuCell { Value = "1", IsFixed = true };
            cells[4][6] = new SudokuCell();
            cells[4][7] = new SudokuCell();
            cells[4][8] = new SudokuCell { Value = "8", IsFixed = true };

            cells[5] = new SudokuCell[9];
            cells[5][0] = new SudokuCell();
            cells[5][1] = new SudokuCell { Value = "4", IsFixed = true };
            cells[5][2] = new SudokuCell { Value = "1", IsFixed = true };
            cells[5][3] = new SudokuCell();
            cells[5][4] = new SudokuCell();
            cells[5][5] = new SudokuCell { Value = "2", IsFixed = true };
            cells[5][6] = new SudokuCell();
            cells[5][7] = new SudokuCell { Value = "7", IsFixed = true };
            cells[5][8] = new SudokuCell();

            cells[6] = new SudokuCell[9];
            cells[6][0] = new SudokuCell();
            cells[6][1] = new SudokuCell { Value = "2", IsFixed = true };
            cells[6][2] = new SudokuCell { Value = "7", IsFixed = true };
            cells[6][3] = new SudokuCell();
            cells[6][4] = new SudokuCell { Value = "1", IsFixed = true };
            cells[6][5] = new SudokuCell();
            cells[6][6] = new SudokuCell { Value = "6", IsFixed = true };
            cells[6][7] = new SudokuCell { Value = "8", IsFixed = true };
            cells[6][8] = new SudokuCell();

            cells[7] = new SudokuCell[9];
            cells[7][0] = new SudokuCell();
            cells[7][1] = new SudokuCell();
            cells[7][2] = new SudokuCell { Value = "4", IsFixed = true };
            cells[7][3] = new SudokuCell { Value = "7", IsFixed = true };
            cells[7][4] = new SudokuCell();
            cells[7][5] = new SudokuCell();
            cells[7][6] = new SudokuCell();
            cells[7][7] = new SudokuCell();
            cells[7][8] = new SudokuCell();

            cells[8] = new SudokuCell[9];
            cells[8][0] = new SudokuCell { Value = "5", IsFixed = true };
            cells[8][1] = new SudokuCell();
            cells[8][2] = new SudokuCell();
            cells[8][3] = new SudokuCell();
            cells[8][4] = new SudokuCell();
            cells[8][5] = new SudokuCell { Value = "3", IsFixed = true };
            cells[8][6] = new SudokuCell();
            cells[8][7] = new SudokuCell();
            cells[8][8] = new SudokuCell { Value = "4", IsFixed = true };

            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells[i].Length; j++)
                {
                    cells[i][j].Row = i;
                    cells[i][j].Column = j;
                }
            }

            return cells;
        }
    }
}
