using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarPartsFactory : IVehiclePartsFactory
    {
        public Chassis CreateChassis()
        {
            Chassis chassis = new CarChassis();
            Thread.Sleep((int)(chassis.BuildTimeSeconds * 1000));
            return chassis;
        }

        public Shell CreateShell()
        {
            Shell part = new CarShell();
            Thread.Sleep((int)(part.BuildTimeSeconds * 1000));
            return part;
        }

        public Wheel CreateWheels()
        {
            Wheel wheel = new CarWheel();
            Thread.Sleep((int)(wheel.BuildTimeSeconds * 1000));
            return wheel;
        }

        public Trim TrimDecoration()
        {
            Trim trim = new CarTrim();
            Thread.Sleep((int)(trim.BuildTimeSeconds * 1000));
            return trim;
        }
    }

}
