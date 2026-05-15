using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class MinibusPartsFactory : IVehiclePartsFactory
    {
        public Chassis CreateChassis()
        {
            Chassis part = new MiniBusChassis();
            Thread.Sleep((int)(part.BuildTimeSeconds * 1000));
            return part;
        }

        public Shell CreateShell()
        {
            Shell part = new MiniBusShell();
            Thread.Sleep((int)(part.BuildTimeSeconds * 1000));
            return part;
        }

        public Wheel CreateWheels()
        {
            Wheel wheel = new MiniBusWheel();
            Thread.Sleep((int)(wheel.BuildTimeSeconds * 1000));
            return wheel;
        }
        public Trim TrimDecoration()
        {
            Trim part = new MiniBusTrim();
            Thread.Sleep((int)(part.BuildTimeSeconds * 1000));
            return part;
        }
    }

}
