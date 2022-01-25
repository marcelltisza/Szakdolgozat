using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sudoku.UI.Models.ValueConverters
{
    public class FontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFixed = (bool)value;
            if (isFixed)
            {
                return FontWeights.Bold;
            }
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
