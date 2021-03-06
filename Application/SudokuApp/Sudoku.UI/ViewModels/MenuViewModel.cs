using Sudoku.Models.Enumerations;
using Sudoku.Models.GameModels;
using Sudoku.Services;
using Sudoku.UI.Commands;
using Sudoku.UI.Properties;
using Sudoku.UI.Stores;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Sudoku.UI.ViewModels
{
    public class MenuViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand NavigateToGameCommand { get; }
        public ICommand NavigateToSolverCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand CreateNewGameCommand { get; }
        public ICommand ContinueCommand { get; }
        public RelayCommand ExitCommand { get; }

        private readonly ModalNavigationStore _modalNavigationStore;
        private bool isOpen;
        private PuzzleGenerator _puzzleGenerator;
        private SudokuBoard gameBoard;
        private XmlHandler _xmlHandler = new XmlHandler();

        public event PropertyChangedEventHandler PropertyChanged;

        public SudokuBoard GameBoard 
        { 
            get => gameBoard; 
            set
            {
                if (value != gameBoard)
                {
                    gameBoard = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(GameBoard)));
                }
            } 
        }

        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (value != isOpen)
                {
                    isOpen = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(IsOpen)));
                }
            }
        }

        public MenuViewModel(NavigationStore navigationStore)
        {
            NewGameCommand = new RelayCommand(NewGameExecute);
            ContinueCommand = new RelayCommand(OnContinue, CanContinue);
            ExitCommand = new RelayCommand(OnExit);
            
            CreateNewGameCommand = new RelayCommand<string>(CreateNewGame);
            NavigateToGameCommand = new NavigateCommand<GameViewModel>(navigationStore, (object board) => new GameViewModel(navigationStore, (SudokuBoard)board));
            NavigateToSolverCommand = new NavigateCommand<SolverViewModel>(navigationStore, (object param) => new SolverViewModel(navigationStore));

            _modalNavigationStore = new ModalNavigationStore();
            _modalNavigationStore.CurrentViewModelChanged += OnCurrenModalViewModelChanged;
            _puzzleGenerator = new PuzzleGenerator();
        }

        private void OnExit()
        {
            Application.Current.MainWindow.Close();
        }

        private bool CanContinue()
        {
            return File.Exists("savedGame.xml");
        }

        private void OnContinue()
        {
            GameBoard = _xmlHandler.Read<SudokuBoard>("savedGame.xml");
            NavigateToGameCommand.Execute(GameBoard);
        }

        private void CreateNewGame(string difficulty)
        {
            switch (difficulty)
            {
                case "easy":
                    GameBoard = _puzzleGenerator.Generate(DifficultyType.Easy);
                    break;
                case "medium":
                    GameBoard = _puzzleGenerator.Generate(DifficultyType.Medium);
                    break;
                case "hard":
                    GameBoard = _puzzleGenerator.Generate(DifficultyType.Hard);
                    break;
            }
            NavigateToGameCommand.Execute(GameBoard);
        }

        private void NewGameExecute()
        {
            IsOpen = true;
        }

        private void OnCurrenModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }
    }
}
