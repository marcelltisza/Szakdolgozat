using Sudoku.UI.ViewModels;
using Sudoku.UI.Views.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Sudoku.UI.Views
{
    public partial class GameView : Page
    {
        public string SelectedNumber { get; set; }
        private GameViewModel gameViewModel = new GameViewModel();

        public GameView()
        {
            InitializeComponent();

            DataContext = gameViewModel;

        }
    }
}
