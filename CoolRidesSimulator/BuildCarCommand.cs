using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class BuildCarCommand : ICommand
    {
        private CarAssemblyLine _assemblyLine;
        private string _colour;
        private string _name = "Build Car";

        public BuildCarCommand(CarAssemblyLine assemblyLine, string colour)
        {
            _assemblyLine = assemblyLine;
            _colour = colour;
        }

        public void Execute()
        {
            _assemblyLine.BuildCar(_colour);
        }

        public string GetName()
        {
            return $"{_name} ({_colour})";
        }
    }
}
