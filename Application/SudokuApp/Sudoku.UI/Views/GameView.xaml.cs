using Sudoku.UI.Models;
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
        private bool processingNumberChecked = false;
        public bool NoteMode { get; set; }

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

                    Button button = new Button();
                    button.Content = CreateInputField(cell);
                    button.Background = Brushes.Transparent;
                    button.Click += Cell_Click;
                    button.Tag = cell;
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    InputGrid.Children.Add(button);

                    if (cell.IsFixed)
                    {
                        button.IsHitTestVisible = false;
                        button.FontWeight = FontWeights.Bold;
                        button.Background = Brushes.LightGray;
                    }
                }
            }
        }

        public Grid CreateInputField(SudokuCell cell)
        {
            Grid grid = new Grid();
            Label value = new Label();
            value.Content = cell.Value;
            grid.Children.Add(value);
            Grid notes = new Grid();
            grid.Children.Add(notes);
            for (int i = 0; i < 9; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label note = new Label();
                    note.Content = $"{i}{j}";
                    Grid.SetRow(note, i);
                    Grid.SetColumn(note, j);
                    notes.Children.Add(note);
                }
            }
            return grid;
        }

        public void CreateNoteGrid()
        {
            for (int i = 0; i < 9; i++)
            {
                NoteGrid.RowDefinitions.Add(new RowDefinition());
                NoteGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Label label = new Label();
                    Grid notes = new Grid();
                    label.Content = notes;
                    Grid.SetRow(notes, i);
                    Grid.SetColumn(notes, j);
                    for (int k = 0; k < 9; k++)
                    {
                        for (int l = 0; l < 9; l++)
                        {
                            notes.RowDefinitions.Add(new RowDefinition());
                            notes.ColumnDefinitions.Add(new ColumnDefinition());
                        }
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
                }
            }

            // Minigrids
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
            var button = (Button)sender;
            var cell = (SudokuCell)button.Tag;

            if (!string.IsNullOrEmpty(SelectedNumber))
            {
                ((Button)sender).Content = SelectedNumber;
                ((SudokuCell)((Button)sender).Tag).Value = SelectedNumber;
                
            }

            if (!gameViewModel.CheckErrorsForCell(Grid.GetRow(button), Grid.GetColumn(button)))
            {
                button.FontWeight = FontWeights.Bold;
                button.Background = Brushes.LightCoral;
                return;
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
            if (processingNumberChecked)
                return;
            processingNumberChecked = true;

            var selectedNumber = sender as ToggleButton;
            
            foreach (ToggleButton number in NumberGrid.Children)
            {
                if (number.Content != selectedNumber.Content)
                {
                    number.IsChecked = false;
                }
            }

            SelectedNumber = selectedNumber.IsChecked == true ? selectedNumber.Content.ToString() : "";
            processingNumberChecked = false;
        }

        private void NoteToggle_Checked(object sender, RoutedEventArgs e)
        {
            NoteMode = true;

            foreach (ToggleButton button in NumberGrid.Children)
            {
                button.Background = Brushes.LightGoldenrodYellow;
            }
        }

        private void NoteToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            NoteMode = false;

            foreach (ToggleButton button in NumberGrid.Children)
            {
                button.Background = Brushes.LightGray;
            }
        }
    }
}
