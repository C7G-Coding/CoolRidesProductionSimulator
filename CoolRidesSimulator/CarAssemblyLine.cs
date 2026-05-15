using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class CarAssemblyLine : AssemblyLineBase
    {
       public CarAssemblyLine()
       {
          Name = "Car Assembly Line";
       }

       protected override Vehicle CreateVehicle(string model, string colour)
       {
           return new Car(model, colour);
       }

       protected override IVehiclePartsFactory CreatePartsFactory()
       {
          return new CarPartsFactory();
       }

       protected override int GetAssemblyTimeMilliseconds()
       {
          return 2000;
       }
    }
}