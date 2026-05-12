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

        public void UndoLastCommand()
        {
            // Optional: implement if needed
            Console.WriteLine("[CarAssemblyLine] Undo feature - to be implemented");
        }

        public void BuildCar(string colour)
        {
            Console.WriteLine($"  🚗 Building CAR in {colour}");

            // Simulate building parts with delays
            Console.WriteLine("    🔩 Building Chassis...");
            Thread.Sleep(2000);  // Chassis: 2 seconds

            Console.WriteLine("    🚘 Building Shell...");
            Thread.Sleep(2000);  // Shell: 2 seconds

            Console.WriteLine("    ⚙️ Building 4 Wheels...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"      Wheel {i + 1}");
                Thread.Sleep(500);  // Each wheel: 0.5 seconds
            }

            Console.WriteLine("    💺 Installing Interior Trim...");
            Thread.Sleep(1000);  // Trim: 1 second

            Console.WriteLine($"  ✅ Car assembly complete!");

            // Send to spraybooth
            Spraybooth.Instance.Spray("Car", colour);           //Waiting on this method from Person Person 4 (Motheo)
        }

        public void AddToQueue(ICommand command)
        {
            AddCommand(command);
        }
    }
}
