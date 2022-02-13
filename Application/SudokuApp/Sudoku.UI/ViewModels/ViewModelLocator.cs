using System;
using System.ComponentModel;
using System.Windows;

namespace Sudoku.UI.ViewModels
{
    public static class ViewModelLocator
    {


        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), 
                typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            var viewTypeName = d.GetType().Name;
            string viewModelTypeName;

            if (viewTypeName.Contains("View"))
                viewModelTypeName = "Sudoku.UI.ViewModels." + viewTypeName + "Model";
            else
                viewModelTypeName = "Sudoku.UI.ViewModels." + viewTypeName + "ViewModel";

            var viewModelType = Type.GetType(viewModelTypeName);
            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
