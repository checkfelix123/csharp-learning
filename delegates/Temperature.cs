using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates
{
    public class Temperature
    {
        double CurrentValue { get; set; } = 0;
        String Region { get; set; } = "";

        public Temperature(double temp, string region) {
            CurrentValue = temp;
            Region = region;    
        }

        public override string? ToString()
        {
            return "Current Value: " + CurrentValue + " Region: " + Region;
        }
    }
}
