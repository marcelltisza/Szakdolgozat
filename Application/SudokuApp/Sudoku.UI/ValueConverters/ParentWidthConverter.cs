using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class ParentWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parentWidth = value as double?;

            return parentWidth - 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
