using Sudoku.UI.Models;
using Sudoku.UI.ViewModels;
using System;
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

            CreateNoteGrid();
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
                    button.Content = cell.Value;
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
                    var cell = gameViewModel.Board.Cells[i, j];

                    Grid notes = new Grid();
                    notes.Margin = new Thickness(2);
                    Grid.SetRow(notes, i);
                    Grid.SetColumn(notes, j);
                    NoteGrid.Children.Add(notes);

                    for (int k = 0; k < 3; k++)
                    {
                        notes.ColumnDefinitions.Add(new ColumnDefinition());
                        notes.RowDefinitions.Add(new RowDefinition());
                    }

                    for (int k = 0; k < 3; k++)
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            Button note = new Button();
                            note.FontSize = 6;
                            note.Background = Brushes.Transparent;
                            note.BorderThickness = new Thickness(0);
                            note.Content = cell.Notes[3 * k + l] == '0' ? ' ' : cell.Notes[3 * k + l];
                            Grid.SetRow(note, k);
                            Grid.SetColumn(note, l);
                            notes.Children.Add(note);
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
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);
            Grid selectedNotes = new Grid();

            if (NoteMode && !string.IsNullOrEmpty(SelectedNumber))
            {
                foreach (Grid notes in NoteGrid.Children)
                {
                    if (Grid.GetRow(notes) == row && Grid.GetColumn(notes) == column)
                    {
                        selectedNotes = notes;
                        break;
                    }
                }

                if (cell.Notes[Convert.ToInt32(SelectedNumber) - 1] != '0')
                {
                    cell.SetNote(Convert.ToInt32(SelectedNumber) - 1, "0");
                    ((Button)selectedNotes.Children[Convert.ToInt32(SelectedNumber) - 1]).Content = "";
                    return;
                }

                

                string newNotes = "";
                for (int i = 0; i < cell.Notes.Length; i++)
                {
                    if (i == Convert.ToInt32(SelectedNumber) - 1)
                    {
                        newNotes += SelectedNumber;
                    }
                    else
                    {
                        newNotes += cell.Notes[i];
                    }
                }

                ((Button)selectedNotes.Children[Convert.ToInt32(SelectedNumber) - 1]).Content = SelectedNumber;
                cell.Notes = newNotes;
                return;
            }

            if (!string.IsNullOrEmpty(SelectedNumber))
            {
                if (cell.Value == SelectedNumber)
                {
                    button.Content = "";
                    cell.Value = "";
                    return;
                }

                ((Button)sender).Content = SelectedNumber;
                ((SudokuCell)((Button)sender).Tag).Value = SelectedNumber;
                if (!gameViewModel.CheckErrorsForCell(row, column))
                {
                    button.FontWeight = FontWeights.Bold;
                    button.Background = Brushes.LightCoral;
                    return;
                }
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
