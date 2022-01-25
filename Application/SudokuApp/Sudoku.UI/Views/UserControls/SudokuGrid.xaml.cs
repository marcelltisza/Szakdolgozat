using Sudoku.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Sudoku.UI.Views.UserControls
{
    public partial class SudokuGrid : UserControl
    {
        #region Properties

        public SudokuGridViewModel sudokuGridViewModel { get; set; }

        public string SelectedNumber
        {
            get { return (string)GetValue(SelectedNumberProperty); }
            set { SetValue(SelectedNumberProperty, value); }
        }

        public static readonly DependencyProperty SelectedNumberProperty =
            DependencyProperty.Register("SelectedNumber", typeof(string), typeof(SudokuGrid), new PropertyMetadata(null));

        public bool NoteMode
        {
            get { return (bool)GetValue(NoteModeProperty); }
            set { SetValue(NoteModeProperty, value); }
        }

        public static readonly DependencyProperty NoteModeProperty =
            DependencyProperty.Register("NoteMode", typeof(bool), typeof(SudokuGrid), new PropertyMetadata(null));

        #endregion

        public SudokuGrid()
        {
            InitializeComponent();

            sudokuGridViewModel = new SudokuGridViewModel();

            GenerateUI();
        }

        private void GenerateUI()
        {
            for (int i = 0; i < PlayAreaGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < PlayAreaGrid.ColumnDefinitions.Count; j++)
                {
                    GridCell gridCell = new GridCell(sudokuGridViewModel.Board.Cells[i, j]);
                    gridCell.Background = Brushes.Transparent;
                    gridCell.SetBinding(GridCell.SelectedNumberProperty, new Binding { Source = this, Path = new PropertyPath(SelectedNumberProperty) });
                    gridCell.SetBinding(GridCell.NoteModeProperty, new Binding { Source = this, Path = new PropertyPath(NoteModeProperty) });
                    Grid.SetRow(gridCell, i);
                    Grid.SetColumn(gridCell, j);
                    PlayAreaGrid.Children.Add(gridCell);
                }
            }
        }
    }
}
