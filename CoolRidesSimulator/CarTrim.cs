using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarTrim : Trim
    {
        public override string PartName => "Car Interior Trim";
        public override double BuildTimeSeconds => 1;
    }
}
