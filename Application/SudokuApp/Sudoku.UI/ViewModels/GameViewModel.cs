using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Sudoku.UI.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public TimeSpan PlayTime 
        { 
            get => playTime;
            set 
            {
                if (playTime != value)
                {
                    playTime = value;
                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(PlayTime)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private DispatcherTimer _timer = new DispatcherTimer();
        private TimeSpan playTime;

        public GameViewModel()
        {
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            PlayTime = PlayTime.Add(new TimeSpan(0, 0, 1));
        }
    }
}