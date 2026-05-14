using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class MinibusAssemblyLine : IAssemblyLine
    {
        private Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private bool _isBusy = false;

        // For thread-safe task tracking
        private string _currentTaskDescription = "None";
        private readonly object _taskLock = new object();
        private readonly object _queueLock = new object();

        public string Name => "Minibus Assembly Line";
        public bool IsBusy => _isBusy;

        public void AddCommand(ICommand command)
        {
            lock (_queueLock)
            {
                _commandQueue.Enqueue(command);
            }
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
            Console.WriteLine("[MinibusAssemblyLine] Undo feature - to be implemented");
        }

        public void BuildMinibus(string colour)
        {
            // Update task - Chassis
            SetTaskDescription($"🚐 Minibus ({colour}) - Building Chassis (2s)");
            Console.WriteLine($"  🚐 Building MINIBUS in {colour}");
            Console.WriteLine("    🔩 Building Chassis...");
            Thread.Sleep(2000);

            // Update task - Shell
            SetTaskDescription($"🚐 Minibus ({colour}) - Building Shell (3s)");
            Console.WriteLine("    🚘 Building Shell...");
            Thread.Sleep(3000);

            // Update task - Wheels
            SetTaskDescription($"🚐 Minibus ({colour}) - Building 4 Wheels (2s total)");
            Console.WriteLine("    ⚙️ Building 4 Wheels...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"      Wheel {i + 1}");
                Thread.Sleep(500);
            }
            Console.WriteLine($"   ✓ All 4 wheels complete!");

            // Update task - Trim
            SetTaskDescription($"🚐 Minibus ({colour}) - Installing Interior Trim (2s)");
            Console.WriteLine("    💺 Installing Interior Trim...");
            Thread.Sleep(2000);

            // Assembly complete
            SetTaskDescription($"🚐 Minibus ({colour}) - Assembly complete!");
            Console.WriteLine($"  ✅ Minibus assembly complete!");

            // Send to spraybooth
            SetTaskDescription($"🚐 Minibus ({colour}) - Sending to Spraybooth (7s drying)");
            Spraybooth.Instance.Spray("Minibus", colour);

            // Done
            SetTaskDescription("None");
        }

        public void AddToQueue(ICommand command)
        {
            AddCommand(command);
        }

        // Thread-safe task description setter
        private void SetTaskDescription(string description)
        {
            lock (_taskLock)
            {
                _currentTaskDescription = description;
            }
        }

        public async Task ProcessQueueAsync()
        {
            while (true)
            {
                ICommand command = null;
                lock (_queueLock)
                {
                    if (_commandQueue.Count > 0)
                        command = _commandQueue.Dequeue();
                }

                if (command != null)
                {
                    _isBusy = true;
                    SetTaskDescription(command.GetName());
                    Console.WriteLine($"[MinibusAssemblyLine] Executing: {command.GetName()}");
                    command.Execute();
                    _isBusy = false;
                }

                await Task.Delay(100);
            }
        }

        public int GetQueueCount()
        {
            lock (_queueLock)
            {
                return _commandQueue.Count;
            }
        }

        public string GetCurrentTaskDescription()
        {
            lock (_taskLock)
            {
                return _currentTaskDescription;
            }
        }
    }
}