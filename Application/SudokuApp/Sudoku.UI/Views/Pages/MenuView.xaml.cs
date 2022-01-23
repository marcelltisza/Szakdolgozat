using System.Windows;
using System.Windows.Controls;

namespace Sudoku.UI.Views
{
    public partial class MenuView : Page
    {
        private Frame _frame;
        public MenuView(Frame mainFrame)
        {
            InitializeComponent();

            _frame = mainFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _frame.Content = new GameView();
        }
    }
}
