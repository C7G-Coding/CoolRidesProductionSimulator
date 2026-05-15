using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public abstract class Chassis
    {
     public abstract string PartName { get; }
     public abstract double BuildTimeSeconds { get; }
    }
}
