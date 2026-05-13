using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class MiniBusWheel : Wheel
    {

        public override string PartName => "Minibus Wheel";
        public override double BuildTimeSeconds => 0.5;
    }
}
