using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public interface IVehiclePartsFactory
    {
        Chassis CreateChassis();
        Shell CreateShell();
        Wheel CreateWheels();
        Trim TrimDecoration();

    }
}
