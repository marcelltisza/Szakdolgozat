using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku.UI.Views
{
    public partial class GameView : Page
    {
        public string SelectedNumber { get; set; }

        public GameView()
        {
            InitializeComponent();

            CreateSudokuGrid();
            CreateNumberGrid();
        }

        public void CreateSudokuGrid()
        {
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

            for (int i = 0; i < 9; i++)
            {
                SudokuGrid.RowDefinitions.Add(new RowDefinition());
                SudokuGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(0.5);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    SudokuGrid.Children.Add(border);
                }
            }

            for (int i = 0; i < 9; i++)
            {
                InputGrid.RowDefinitions.Add(new RowDefinition());
                InputGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Button button = new Button();
                    button.Background = Brushes.Transparent;
                    button.BorderThickness = new Thickness(0);
                    button.Click += Cell_Click;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    InputGrid.Children.Add(button);
                }
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Content = SelectedNumber;
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
