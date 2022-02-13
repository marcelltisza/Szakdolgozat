using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class NoteConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
