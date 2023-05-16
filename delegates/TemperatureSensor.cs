using delegates;
using System;
using System.Runtime.CompilerServices;
using System.Timers;

public delegate void ProgramConverterCallback(double value);
public delegate void TimerEmittedValueCallback(string s);
public delegate void TemperatureTimerCallback(Temperature s);

public interface ISensor
{
    event TemperatureTimerCallback TemperatureTimerCallback;
    public void Start();
    public void Stop();
}

public class TemperatureSensor : ISensor
{
    public event ProgramConverterCallback EmittedValueCallback;
    public event TemperatureTimerCallback TemperatureTimerCallback;

    System.Timers.Timer Timer;



    public TemperatureSensor(ProgramConverterCallback pic)
    {
        this.EmittedValueCallback = pic;


        Timer = new System.Timers.Timer(5000);

        Timer.Elapsed += OnTimerElapsed;
       /*
        (object? sender, ElapsedEventArgs elapsedEventArgs) =>
        {
            Random random = new Random();
            double d;
           if(Double.TryParse(random.Next(0, 100).ToString(), out d))
            {
                this.EmittedValueCallback?.Invoke(d);
            }

           Temperature t = new Temperature(((random.NextDouble() * 20.0) + 4.0), "Traismauer");
            this.TemperatureTimerCallback?.Invoke(t);

        }; */
        Timer.Start();


    }

    public TemperatureSensor()
    {
       

        Timer = new System.Timers.Timer(5000);

        Timer.Elapsed += OnTimerElapsed;


    }
    private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        Random random = new Random();
        Temperature t = new Temperature(((random.NextDouble() * 20.0) + 4.0), "Traismauer");
        this.TemperatureTimerCallback?.Invoke(t);
    }
    static void Main(String[] args)
    {
        TemperatureSensor p = new TemperatureSensor((double value) =>
        {
            Console.WriteLine("converted Value " + value * 3);
        });
        //listening to the callback
        p.EmittedValueCallback += (double d) =>
        {
            Console.WriteLine("original value: " + d);
        };

        // listening to the other callback which emitts the Temperature
        p.TemperatureTimerCallback += (Temperature t) => {
            Console.WriteLine("Emitted Temperature from sensor: " + t);
        };




        List<ISensor> temperatureSensors = new List<ISensor>
        {
            new TemperatureSensor(),
            new TemperatureSensor(),
            new TemperatureSensor()

        };
        foreach(var sensor in temperatureSensors)
        {
            sensor.TemperatureTimerCallback += OnTimerCallback;
            sensor.Start();
        }


        Console.ReadLine();

        foreach (var sensor in temperatureSensors)
        {
            Console.WriteLine("Sensor stopped");
            sensor.Stop();
        }

    }

    private static void OnTimerCallback(Temperature s)
    {
        Console.WriteLine("Temperature: " + s);
    }

    public void Start()
    {
        this.Timer.Start();
    }

    public void Stop()
    {
        this.Timer.Stop(); 
    }
}