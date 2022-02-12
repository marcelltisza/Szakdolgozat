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
            DependencyProperty.Register("SelectedNumber", typeof(string), typeof(NumberSelector), new PropertyMetadata(""));

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

            for (int i = 0; i < 9; i++)
            {
                var button = NumbersGrid.Children[i] as ToggleButton;
                if (button.Content.ToString() != SelectedNumber && button.Name != "NoteToggle")
                {
                    button.IsChecked = false;
                }
            }
        }
    }
}
