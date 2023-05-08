using delegates;
using System.Timers;

public delegate void ProgramConverterCallback(double value);
public delegate void TimerEmittedValueCallback(string s);
public delegate void TemperatureTimerCallback(Temperature s);
public class TemperatureSensor
{
    public event ProgramConverterCallback EmittedValueCallback;

    public event TemperatureTimerCallback TemperatureTimerCallback;




    public TemperatureSensor(ProgramConverterCallback pic)
    {
        this.EmittedValueCallback = pic;
       

        System.Timers.Timer timer = new System.Timers.Timer(5000);

        timer.Elapsed += (object? sender, ElapsedEventArgs elapsedEventArgs) =>
        {
            Random random = new Random();
            double d;
           if(Double.TryParse(random.Next(0, 100).ToString(), out d))
            {
                this.EmittedValueCallback?.Invoke(d);
            }

           Temperature t = new Temperature(((random.NextDouble() * 20.0) + 4.0), "Traismauer");
            this.TemperatureTimerCallback?.Invoke(t);

        };
        timer.Start();


    }

    static void Main(String[] args)
    {
        //cration of a TemperatureSenson
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


        Console.ReadLine();

    }

    
}