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

        public void UndoLastCommand()
        {
            // Optional: implement if needed
            Console.WriteLine("[MinibusAssemblyLine] Undo feature - to be implemented");
        }

        public void BuildMinibus(string colour)
        {
            Console.WriteLine($"  🚐 Building MINIBUS in {colour}");

            // Simulate building parts with delays
            Console.WriteLine("    🔩 Building Chassis...");
            Thread.Sleep(2000);  // Chassis: 2 seconds

            Console.WriteLine("    🚘 Building Shell...");
            Thread.Sleep(3000);  // Shell: 3 seconds (longer than car)

            Console.WriteLine("    ⚙️ Building 4 Wheels...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"      Wheel {i + 1}");
                Thread.Sleep(500);  // Each wheel: 0.5 seconds
            }

            Console.WriteLine("    💺 Installing Interior Trim...");
            Thread.Sleep(2000);  // Trim: 2 seconds (longer than car)

            Console.WriteLine($"  ✅ Minibus assembly complete!");

            // Send to spraybooth
            Spraybooth.Instance.Spray("Minibus", colour);               //Waiting for Person 4 (Motheo)
        }

        public void AddToQueue(ICommand command)
        {
            AddCommand(command);
        }
    }
}
