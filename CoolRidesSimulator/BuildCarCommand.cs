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
        private readonly string _colour;
        private readonly string _model;

        public BuildCarCommand(CarAssemblyLine assemblyLine,string model,  string colour)
        {
            _assemblyLine = assemblyLine;
            _model = model;
            _colour = colour;
        }

        public void Execute()
        {
            _assemblyLine.BuildVehicle(_model, _colour);
        }

        public string GetName()
        {
            return $"{_model} ({_colour})";
        }
    }
}
