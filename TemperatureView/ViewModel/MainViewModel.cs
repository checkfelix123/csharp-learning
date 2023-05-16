using delegates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TemperatureView.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

      
        public event PropertyChangedEventHandler? PropertyChanged;
        private List<ISensor> _temperatureSensors = new List<ISensor>();



        private ObservableCollection<Temperature> _temperatureReadings  { get; }= new();

        public ObservableCollection<Temperature> TemperatureReadings
        {
            get { return _temperatureReadings; }
          
        }
        
        public MainViewModel()
        {
            StartSensorsAync();
        }
        private async Task StartSensorsAync()
        {
            _temperatureSensors.Add(new TemperatureSensor());
            _temperatureSensors.Add(new TemperatureSensor());
            _temperatureSensors.Add(new TemperatureSensor());

            foreach (var sensor in _temperatureSensors)
            {
                sensor.TemperatureTimerCallback += OnTemperatureTimerCallback;
                sensor.Start();
            }
        }

        private void OnTemperatureTimerCallback(Temperature temperature)

        {

            App.Current.Dispatcher.InvokeAsync(() =>
            {
                if (temperature != null)
                {
                    _temperatureReadings.Add(temperature);
                }
            }); 
        }

        public void OnChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

      

        

       
    }
}
