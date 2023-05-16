using Swack.UI.ViewModels;
using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using TimerApp.Commands;

namespace TimerApp.ViewModel
{
    public delegate void InvokeableDelegateHandler();
    public class MainViewModel : INotifyPropertyChanged
    {
        InvokeableDelegateHandler TimerDelegate;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand IncreaseDateCommand { get; private set; }

        public ICommand DecreaseDateCommand { get; private set; }

        public ICommand StartCommand { get; private set; }

        private Timer timer { get; set; }

        private TimeSpan time;
        public TimeSpan Time
        {
            get
            {
                return time;
            }
            set
            {
                if (time != value)
                {
                    time = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
                }
            }
        }

        public MainViewModel()
        {

            timer = new System.Timers.Timer(1000); // DispatcherTImer
            Time = TimeSpan.FromMinutes(1);

            this.DecreaseDateCommand = new AsyncDelegateCommand((object? obj) => Task.Run(() => { Time = Time.Add(TimeSpan.FromSeconds(-1)); }));

            this.IncreaseDateCommand = new AsyncDelegateCommand((object? obj) => Task.Run(() => { Time = Time.Add(TimeSpan.FromSeconds(1)); }));

            this.StartCommand = new AsyncDelegateCommand((object? _) => Task.Run(() => timer.Start()));


             
            timer.Elapsed += (sender, e) => TimerDelegate?.Invoke();
            timer.AutoReset = true; 
           


            TimerDelegate += () =>
            {
                Time = Time.Add(TimeSpan.FromSeconds(-1));
            };
        }

    }
 }
