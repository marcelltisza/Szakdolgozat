using Sudoku.UI.ViewModels;
using System.Windows.Controls;

namespace Sudoku.UI.Views
{
    public partial class GameView : Page
    {
        private GameViewModel gameViewModel = new GameViewModel();

        public GameView()
        {
            InitializeComponent();

            DataContext = gameViewModel;

        }
    }
}
