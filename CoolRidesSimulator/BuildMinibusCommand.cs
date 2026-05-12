using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class BuildMinibusCommand
    {
        private MinibusAssemblyLine _assemblyLine;
        private string _colour;
        private string _name = "Build Minibus";

        public BuildMinibusCommand(MinibusAssemblyLine assemblyLine, string colour)
        {
            _assemblyLine = assemblyLine;
            _colour = colour;
        }

        public void Execute()
        {
            _assemblyLine.BuildMinibus(_colour);
        }

        public string GetName()
        {
            return $"{_name} ({_colour})";
        }
    }
}
