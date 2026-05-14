using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class CarAssemblyLine : IAssemblyLine
    {
        private Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private bool _isBusy = false;

        public string Name => "Car Assembly Line";
        public bool IsBusy => _isBusy;

        public void AddCommand(ICommand command)
        {
            _commandQueue.Enqueue(command);
            Console.WriteLine($"[CarAssemblyLine] Added to queue: {command.GetName()}");
        }

        public void ExecuteAllCommands()
        {
            while (_commandQueue.Count > 0)
            {
                _isBusy = true;
                ICommand command = _commandQueue.Dequeue();
                Console.WriteLine($"\n[CarAssemblyLine] Executing: {command.GetName()}");
                command.Execute();
                _isBusy = false;
            }
        }

        public void BuildCar(string colour)
        {

            Console.WriteLine($"\n STARTING CAR PRODUCTION - {colour}");
            

            Console.WriteLine($"\n Step 1/6: Building Chassis...");
            Thread.Sleep(2000);
            Console.WriteLine($"   ✓ Car Chassis complete! (2 seconds)");

            Console.WriteLine($"\n Step 2/6: Building Shell...");
            Thread.Sleep(2000);
            Console.WriteLine($"   ✓ Car Shell complete! (2 seconds)");

            Console.WriteLine($"\n Step 3/6: Building 4 Wheels...");
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"   Building Wheel {i}...");
                Thread.Sleep(500);
                Console.WriteLine($"   ✓ Wheel {i} complete! (0.5 seconds)");
            }
            Console.WriteLine($"   ✓ All 4 wheels complete!");

            Console.WriteLine($"\n Step 4/6: Installing Interior Trim...");
            Thread.Sleep(1000);
            Console.WriteLine($"   ✓ Car Trim complete! (1 second)");

            Console.WriteLine($"\n Step 5/6: Assembling all parts...");
            Thread.Sleep(500);
            Console.WriteLine($"   ✓ Assembly complete!");

            Console.WriteLine($"\n Step 6/6: Sending to Spraybooth...");

            Console.WriteLine($"\n    CAR PRODUCTION COMPLETE - {colour}");


            Spraybooth.Instance.Spray("Car", colour);
        }

    }
}
