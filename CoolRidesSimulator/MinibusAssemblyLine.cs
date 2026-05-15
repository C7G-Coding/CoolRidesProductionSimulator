using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoolRidesSimulator
{
    public class MinibusAssemblyLine : AssemblyLineBase
    {
        public MinibusAssemblyLine()
        {
            Name = "Minibus Assembly Line";
        }

        protected override Vehicle CreateVehicle(string model, string colour)
        {
            return new MiniBus(model, colour);
        }

        protected override IVehiclePartsFactory CreatePartsFactory()
        {
            return new MinibusPartsFactory();
        }

        protected override int GetAssemblyTimeMilliseconds()
        {
            return 3000;
        }
    }
}
