using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    class CarPartsFactory : IVehiclePartsFactory
    {
        public IChassis CreateChassis()
        {
            return new CarChassis();
        }
        public IShell CreateShell()
        {
            return new CarShell();
        }
        public IWheel CreateWheels()
        {
            return new CarWheel();
        }
        public ITrim TrimDecoration()
        {
            return new CarTrim();
        }
    }

}
