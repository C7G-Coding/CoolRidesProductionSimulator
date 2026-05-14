using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public interface IAssemblyLine
    {
        string Name { get; }
        bool IsBusy { get; }
        void AddCommand(ICommand command);
        void ExecuteAllCommands();

    }
}
