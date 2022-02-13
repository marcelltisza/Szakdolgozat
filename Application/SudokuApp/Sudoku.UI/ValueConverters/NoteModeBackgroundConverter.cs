using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Sudoku.UI.ValueConverters
{
    public class NoteModeBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool noteMode = (bool)value;
            if (noteMode)
            {
                return new SolidColorBrush(Color.FromRgb(252, 249, 139));
            }
            return Brushes.WhiteSmoke;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
