﻿using Sudoku.UI.ViewModels;
using System;

namespace Sudoku.UI.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;

        public event Action CurrentViewModelChanged;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public bool IsOpen => CurrentViewModel != null;

        public void Close()
        {
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}
