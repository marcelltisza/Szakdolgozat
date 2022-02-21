using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Sudoku.UI.ValueConverters
{
    public class ForegroundHighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Brush brush = new SolidColorBrush(Colors.Black);
            try
            {
                var cellValue = values[0].ToString();
                var selectedValue = values[1].ToString();

                if (cellValue == selectedValue)
                {
                    brush = new SolidColorBrush(Colors.Red);
                }
                else
                {
                    brush = new SolidColorBrush(Colors.Black);
                }
            }
            catch (Exception)
            {
                return brush;
            }

            return brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
