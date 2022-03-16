using System.Windows;
using System.Windows.Controls;

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
    }
}
