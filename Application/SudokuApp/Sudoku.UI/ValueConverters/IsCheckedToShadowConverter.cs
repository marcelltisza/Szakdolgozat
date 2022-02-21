using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Sudoku.UI.ValueConverters
{
    public class IsCheckedToShadowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;

            if (isChecked)
                return new DropShadowEffect() { Color = Colors.Orange, BlurRadius = 10, ShadowDepth = 0 };
            return new DropShadowEffect() { Color = Colors.Transparent, BlurRadius = 10, ShadowDepth = 0 };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
