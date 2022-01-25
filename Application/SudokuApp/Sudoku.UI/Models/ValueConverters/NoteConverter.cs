using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.Models.ValueConverters
{
    public class NoteConverter : IValueConverter
    {
        // char => string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "")
            {
                return "";
            }
            if ((value.ToString())[0] == '0')
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }

        // string => char
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
