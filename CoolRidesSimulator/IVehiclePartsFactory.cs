using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    interface IVehiclePartsFactory
    {
        IChassis CreateChassis();
        IShell CreateShell();
        IWheel CreateWheels();
        ITrim TrimDecoration();

    }
}
