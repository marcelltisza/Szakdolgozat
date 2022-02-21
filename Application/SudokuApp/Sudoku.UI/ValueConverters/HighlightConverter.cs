using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Sudoku.UI.ValueConverters
{
    public class HighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DropShadowEffect dropShadow = new DropShadowEffect
            {
                BlurRadius = 10,
                ShadowDepth = 0,
                Color = Colors.Transparent
            };
            try
            {
                var cellValue = values[0].ToString();
                var selectedValue = values[1].ToString();

                if (cellValue == selectedValue)
                {
                    dropShadow.Color = Colors.Orange;
                }
                else
                {
                    dropShadow.Color = Colors.Transparent;
                }
            }
            catch (Exception)
            {
                return dropShadow;
            }

            return dropShadow;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
