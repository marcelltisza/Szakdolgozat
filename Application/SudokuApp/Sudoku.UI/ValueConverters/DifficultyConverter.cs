using Sudoku.Models.Enumerations;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    public class DifficultyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DifficultyType difficulty = (DifficultyType)value;

            var dif = difficulty.ToString();

            return (difficulty.ToString() == (string)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DifficultyType difficulty = DifficultyType.Easy;

            switch ((string)parameter)
            {
                case "Easy":
                    difficulty = DifficultyType.Easy;
                    break;
                case "Medium":
                    difficulty = DifficultyType.Medium;
                    break;
                case "Hard":
                    difficulty = DifficultyType.Hard;
                    break;
            }

            if ((bool)value)
                return difficulty;
            else
                return Binding.DoNothing;
        }
    }
}
