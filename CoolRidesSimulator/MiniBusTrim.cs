using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class MiniBusTrim : Trim
    {
        public override string PartName => "Minibus Interior Trim";
        public override double BuildTimeSeconds => 2;
    }
}
