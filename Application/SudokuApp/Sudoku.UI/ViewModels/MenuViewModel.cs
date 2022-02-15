using Sudoku.UI.Commands;
using Sudoku.UI.Stores;
using System.Windows.Input;

namespace Sudoku.UI.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ICommand NavigateToGameCommand { get; }

        public MenuViewModel(NavigationStore navigationStore)
        {
            NavigateToGameCommand = new NavigateCommand<GameViewModel>(navigationStore, () => new GameViewModel(navigationStore));
        }
    }
}
