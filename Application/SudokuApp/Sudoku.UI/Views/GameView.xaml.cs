using Sudoku.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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

            CreateInputGrid();
            CreateGridlines();
            
            CreateNumberGrid();
        }

        public void CreateInputGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                InputGrid.RowDefinitions.Add(new RowDefinition());
                InputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cell = gameViewModel.Board.Cells[i, j];

                    if (!cell.IsFixed)
                    {
                        Button button = new Button();
                        button.Content = cell.Value;
                        button.Background = Brushes.Transparent;
                        button.BorderThickness = new Thickness(0);
                        button.Margin = new Thickness(1);
                        button.Click += Cell_Click;
                        button.Tag = cell;
                        Grid.SetRow(button, i);
                        Grid.SetColumn(button, j);
                        InputGrid.Children.Add(button);
                    }
                }
            }
        }

        public void CreateGridlines()
        {
            for (int i = 0; i < 9; i++)
            {
                SudokuGrid.RowDefinitions.Add(new RowDefinition());
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var cell = gameViewModel.Board.Cells[i, j];
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(0.5);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    SudokuGrid.Children.Add(border);
                    if (cell.IsFixed)
                    {
                        Label label = new Label();
                        label.Content = cell.Value;
                        label.FontWeight = FontWeights.Bold;
                        label.Background = Brushes.LightGray;
                        label.VerticalContentAlignment = VerticalAlignment.Center;
                        label.HorizontalContentAlignment = HorizontalAlignment.Center;
                        border.Child = label;
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var border = new Border
                    {
                        BorderThickness = new Thickness(1.5),
                        BorderBrush = Brushes.Black
                    };
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    Minigrids.Children.Add(border);
                    var label = new Label();
                    label.Background = Brushes.Transparent;
                    border.Child = label;
                }
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedNumber))
            {
                ((Button)sender).Content = SelectedNumber;
            }
        }

        public void CreateNumberGrid()
        {
            for (int i = 1; i <= 9; i++)
            {
                NumberGrid.ColumnDefinitions.Add(new ColumnDefinition());
                ToggleButton button = new ToggleButton();
                button.Content = i;
                button.Margin = new Thickness(1);
                button.Checked += Number_Checked;
                button.Unchecked += Number_Checked;
                Grid.SetColumn(button, i - 1);
                NumberGrid.Children.Add(button);
            }
        }

        private void Number_Checked(object sender, RoutedEventArgs e)
        {
            var selectedNumber = sender as ToggleButton;
            foreach (ToggleButton number in NumberGrid.Children)
            {
                if (number.Content != selectedNumber.Content)
                {
                    number.IsChecked = false;
                }
            }

            SelectedNumber = selectedNumber.IsChecked == true ? selectedNumber.Content.ToString() : "";
        }


    }
}
