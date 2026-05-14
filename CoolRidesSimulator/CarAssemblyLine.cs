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

        public void BuildCar(string colour)
        {
<<<<<<< HEAD
            // Update task - Starting
            SetTaskDescription($"🚗 Car ({colour}) - Starting assembly...");

            Console.WriteLine($"  🚗 Building CAR in {colour}");

            // Chassis (2 seconds)
            SetTaskDescription($"🚗 Car ({colour}) - Building Chassis (2s)");
            Console.WriteLine("    🔩 Building Chassis...");
            Thread.Sleep(2000);

            // Shell (2 seconds)
            SetTaskDescription($"🚗 Car ({colour}) - Building Shell (2s)");
            Console.WriteLine("    🚘 Building Shell...");
            Thread.Sleep(2000);

            // 4 Wheels (0.5 seconds each = 2 seconds total)
            SetTaskDescription($"🚗 Car ({colour}) - Building 4 Wheels (2s total)");
            Console.WriteLine("    ⚙️ Building 4 Wheels...");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"      Wheel {i + 1}");
                Thread.Sleep(500);
            }

            // Interior Trim (1 second)
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

        // Async queue processor for GUI responsiveness
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

                await Task.Delay(100); // Small delay to prevent CPU spinning
=======

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
>>>>>>> 4821d6a310ab36e882904da991b75243da894913
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

<<<<<<< HEAD
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
=======
>>>>>>> 4821d6a310ab36e882904da991b75243da894913
    }
}