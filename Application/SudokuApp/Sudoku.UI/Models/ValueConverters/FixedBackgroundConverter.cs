using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Sudoku.UI.Models.ValueConverters
{
    public class FixedBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFixed = (bool)value;
            if (isFixed)
            {
                return Brushes.LightGray;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
