using Sudoku.Models.GameModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Sudoku.UI.ValueConverters
{
    class ToListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SudokuCell[][] matrix = value as SudokuCell[][];
            List<SudokuCell> list = new List<SudokuCell>();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    list.Add(matrix[i][j]);
                }
            }

            return list;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
