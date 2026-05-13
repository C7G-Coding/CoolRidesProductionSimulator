using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarChassis : Chassis
    {
        public override string PartName => "Car Chassis";
        public override double BuildTimeSeconds => 2;
    }

}
