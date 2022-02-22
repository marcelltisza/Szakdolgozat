using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class HitTestConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string content = (string)values[0];
                bool isFixed = (bool)values[1];
                bool noteMode = (bool)values[2];

                if (isFixed)
                    return false;

                if (!string.IsNullOrEmpty(content) && noteMode)
                    return false;
            }
            catch (Exception) {}

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
