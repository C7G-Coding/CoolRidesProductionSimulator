using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class Car
    {
        public string Model { get; set; }
        public string Colour { get; set; }

        public Car(string model, string colour)
        {
            Model = model;
            Colour = colour;
        }
    }
}
