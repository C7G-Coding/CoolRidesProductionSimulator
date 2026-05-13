using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class MinibusPartsFactory : IVehiclePartsFactory
    {
        public Chassis CreateChassis()
        {
            return new MiniBusChassis();
        }
        public Shell CreateShell()
        {
            return new MiniBusShell();
        }
        public Wheel CreateWheels()
        {
            return new MiniBusWheel();
        }
        public Trim TrimDecoration()
        {
            return new MiniBusTrim();
        }
    }

}
