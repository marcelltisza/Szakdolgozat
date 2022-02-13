using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    class CountToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = value as int?;

            if (count == 0 || count == null)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
