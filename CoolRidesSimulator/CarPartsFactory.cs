using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarPartsFactory : IVehiclePartsFactory
    {
        public Chassis CreateChassis()
        {
            return new CarChassis();
        }
        public Shell CreateShell()
        {
            return new CarShell();
        }
        public Wheel CreateWheels()
        {
            return new CarWheel();
        }
        public Trim TrimDecoration()
        {
            return new CarTrim();
        }
    }

}
