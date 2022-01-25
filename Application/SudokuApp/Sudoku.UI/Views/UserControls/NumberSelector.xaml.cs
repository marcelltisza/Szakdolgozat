﻿using Sudoku.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Sudoku.UI.Views.UserControls
{
    public partial class NumberSelector : UserControl
    {
        #region Properties

        public bool NoteMode
        {
            get { return (bool)GetValue(NoteModeProperty); }
            set { SetValue(NoteModeProperty, value); }
        }

        public static readonly DependencyProperty NoteModeProperty =
            DependencyProperty.Register("NoteMode", typeof(bool), typeof(NumberSelector), new PropertyMetadata(null));

        public string SelectedNumber
        {
            get { return (string)GetValue(SelectedNumberProperty); }
            set { SetValue(SelectedNumberProperty, value); }
        }

        public static readonly DependencyProperty SelectedNumberProperty =
            DependencyProperty.Register("SelectedNumber", typeof(string), typeof(NumberSelector), new PropertyMetadata(null));

        #endregion

        public NumberSelector()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var toggleButton = sender as ToggleButton;
            SelectedNumber = toggleButton.Content.ToString();

            foreach (ToggleButton button in NumbersGrid.Children)
            {
                if (button.Content.ToString() != SelectedNumber)
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}
