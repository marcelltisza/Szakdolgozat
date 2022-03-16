using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class NumberSelectorConverter : IValueConverter
    {
        private string _lastValue;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string selectedNumber = (string)value;
            _lastValue = selectedNumber;
            return (selectedNumber == (string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isChecked = (bool)value;

            if (isChecked)
                return (string)parameter;
            else if (_lastValue == (string)parameter)
                return "";
            return Binding.DoNothing;
        }
    }
}
