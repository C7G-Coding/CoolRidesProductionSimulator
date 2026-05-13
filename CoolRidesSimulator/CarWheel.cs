using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarWheel : Wheel
    {
        public override string PartName => "Car Wheel";
        public override double BuildTimeSeconds => 0.5;
    }
}
