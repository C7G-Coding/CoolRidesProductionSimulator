using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public abstract class Vehicle
    {
        public string Model { get; protected set; }
        public string Colour { get; set; }

        public Chassis Chassis { get; set; }
        public Shell Shell { get; set; }
        public List<Wheel> Wheels { get; } = new List<Wheel>();
        public Trim Trim { get; set; }

      
        public override string ToString()
        {
            return $"{Model} ({Colour})";
        }


    }
}
