using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class MinibusPartsFactory : IVehiclePartsFactory
    {
        public IChassis CreateChassis()
        {
            return new MiniBusChassis();
        }
        public IShell CreateShell()
        {
            return new MiniBusShell();
        }
        public IWheel CreateWheels()
        {
            return new MiniBusWheel();
        }
        public ITrim TrimDecoration()
        {
            return new MiniBusTrim();
        }
    }

}
