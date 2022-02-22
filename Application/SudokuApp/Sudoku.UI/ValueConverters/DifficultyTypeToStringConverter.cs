using Sudoku.Models.Enumerations;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class DifficultyTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DifficultyType difficulty = (DifficultyType)value;

            switch (difficulty)
            {
                case DifficultyType.Easy:
                    return "easy";
                case DifficultyType.Medium:
                    return "medium";
                case DifficultyType.Hard:
                    return "hard";
                default:
                    throw new Exception();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
