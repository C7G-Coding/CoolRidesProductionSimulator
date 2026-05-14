using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoolRidesSimulator
{
    public class MinibusAssemblyLine : IAssemblyLine
    {
        private Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private bool _isBusy = false;

        public string Name => "Minibus Assembly Line";
        public bool IsBusy => _isBusy;

        public void AddCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
            Console.WriteLine($"[MinibusAssemblyLine] Added to queue: {command.GetName()}");
        }

        public void ExecuteAllCommands()
        {
            while (_commandQueue.Count > 0)
            {
                _isBusy = true;
                ICommand command = _commandQueue.Dequeue();
                Console.WriteLine($"\n[MinibusAssemblyLine] Executing: {command.GetName()}");
                command.Execute();
                _isBusy = false;
            }
        }

        public void BuildMinibus(string colour)
        {
            Console.WriteLine($"\n STARTING MINIBUS PRODUCTION - {colour}");


            Console.WriteLine($"\n Step 1/6: Building Chassis...");
            Thread.Sleep(2000);
            Console.WriteLine($"   ✓ Minibus Chassis complete! (2 seconds)");

            Console.WriteLine($"\n Step 2/6: Building Shell...");
            Thread.Sleep(3000);
            Console.WriteLine($"   ✓ Minibus Shell complete! (3 seconds)");

            Console.WriteLine($"\n Step 3/6: Building 4 Wheels...");
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"   Building Wheel {i}...");
                Thread.Sleep(500);
                Console.WriteLine($"   ✓ Wheel {i} complete! (0.5 seconds)");
            }
            Console.WriteLine($"   ✓ All 4 wheels complete!");

            Console.WriteLine($"\n Step 4/6: Installing Interior Trim...");
            Thread.Sleep(2000);
            Console.WriteLine($"   ✓ Minibus Trim complete! (2 seconds)");

            Console.WriteLine($"\n Step 5/6: Assembling all parts...");
            Thread.Sleep(500);
            Console.WriteLine($"   ✓ Assembly complete!");

            Console.WriteLine($"\n Step 6/6: Sending to Spraybooth...");


            Console.WriteLine($"\n MINIBUS PRODUCTION COMPLETE - {colour}");


            Spraybooth.Instance.Spray("Minibus", colour);
        }

    }
}
