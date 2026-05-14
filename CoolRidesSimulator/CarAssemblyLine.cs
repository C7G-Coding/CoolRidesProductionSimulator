using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public class CarAssemblyLine : IAssemblyLine
    {
        private Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private bool _isBusy = false;

        // For thread-safe task tracking
        private string _currentTaskDescription = "None";
        private readonly object _taskLock = new object();
        private readonly object _queueLock = new object();

        public string Name => "Car Assembly Line";
        public bool IsBusy => _isBusy;

        public void AddCommand(ICommand command)
        {
            lock (_queueLock)
            {
                _commandQueue.Enqueue(command);
            }
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
            Console.WriteLine("[CarAssemblyLine] Undo feature - to be implemented");
        }

        public void BuildCar(string colour)
        {
            // Update task - Chassis
            SetTaskDescription($"🚗 Car ({colour}) - Building Chassis (2s)");
            Console.WriteLine($"  🚗 Building CAR in {colour}");
            Console.WriteLine("    🔩 Building Chassis...");
            Thread.Sleep(2000);

            // Update task - Shell
            SetTaskDescription($"🚗 Car ({colour}) - Building Shell (2s)");
            Console.WriteLine("    🚘 Building Shell...");
            Thread.Sleep(2000);

            // Update task - Wheels
            SetTaskDescription($"🚗 Car ({colour}) - Building 4 Wheels (2s total)");
            Console.WriteLine("    ⚙️ Building 4 Wheels...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"      Wheel {i + 1}");
                Thread.Sleep(500);
            }
            Console.WriteLine($"   ✓ All 4 wheels complete!");

            // Update task - Trim
            SetTaskDescription($"🚗 Car ({colour}) - Installing Interior Trim (1s)");
            Console.WriteLine("    💺 Installing Interior Trim...");
            Thread.Sleep(1000);

            // Assembly complete
            SetTaskDescription($"🚗 Car ({colour}) - Assembly complete!");
            Console.WriteLine($"  ✅ Car assembly complete!");

            // Send to spraybooth
            SetTaskDescription($"🚗 Car ({colour}) - Sending to Spraybooth (5s drying)");
            Spraybooth.Instance.Spray("Car", colour);

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
                    Console.WriteLine($"[CarAssemblyLine] Executing: {command.GetName()}");
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