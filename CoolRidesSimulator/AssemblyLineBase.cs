using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public abstract class AssemblyLineBase : IAssemblyLine
    {
        private readonly Queue<ICommand> _commandQueue = new Queue<ICommand>();
        private readonly object _queueLock = new object();
        private readonly object _statusLock = new object();

        private bool _isBusy = false;
        private string _currentTaskDescription = "None";
        private int _completedCount = 0;

        public string Name { get; protected set; }

        public bool IsBusy
        {
            get
            {
                lock (_statusLock)
                {
                    return _isBusy;
                }
            }
        }

        public void AddCommand(ICommand command)
        {
            lock (_queueLock)
            {
                _commandQueue.Enqueue(command);
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
            lock (_statusLock)
            {
                return _currentTaskDescription;
            }
        }

        public int GetCompletedCount()
        {
            lock (_statusLock)
            {
                return _completedCount;
            }
        }

        protected void SetBusy(bool busy)
        {
            lock (_statusLock)
            {
                _isBusy = busy;
            }
        }

        protected void SetTaskDescription(string description)
        {
            lock (_statusLock)
            {
                _currentTaskDescription = description;
            }
        }

        protected void IncrementCompleted()
        {
            lock (_statusLock)
            {
                _completedCount++;
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
                    {
                        command = _commandQueue.Dequeue();
                    }
                }

                if (command != null)
                {
                    SetBusy(true);
                    SetTaskDescription(command.GetName());
                    command.Execute();
                    SetBusy(false);
                    SetTaskDescription("None");
                }

                await Task.Delay(100);
            }
        }

        public void BuildVehicle(string model, string colour)
        {
            Vehicle vehicle = CreateVehicle(model, colour);
            IVehiclePartsFactory partsFactory = CreatePartsFactory();

            SetTaskDescription($"{vehicle} - Building Chassis");
            vehicle.Chassis = partsFactory.CreateChassis();

            SetTaskDescription($"{vehicle} - Building Shell");
            vehicle.Shell = partsFactory.CreateShell();

            SetTaskDescription($"{vehicle} - Building Wheel 1 of 4");
            vehicle.Wheels.Add(partsFactory.CreateWheels());

            SetTaskDescription($"{vehicle} - Building Wheel 2 of 4");
            vehicle.Wheels.Add(partsFactory.CreateWheels());

            SetTaskDescription($"{vehicle} - Building Wheel 3 of 4");
            vehicle.Wheels.Add(partsFactory.CreateWheels());

            SetTaskDescription($"{vehicle} - Building Wheel 4 of 4");
            vehicle.Wheels.Add(partsFactory.CreateWheels());

            SetTaskDescription($"{vehicle} - Installing Interior Trim");
            vehicle.Trim = partsFactory.TrimDecoration();

            SetTaskDescription($"{vehicle} - Assembling Vehicle");
            Thread.Sleep(GetAssemblyTimeMilliseconds());

            SetTaskDescription($"{vehicle} - Sending to Spraybooth");
            Spraybooth.Instance.Spray(vehicle);

            SetTaskDescription($"{vehicle} - Completed");
            IncrementCompleted();
            Thread.Sleep(300);
        }

        protected abstract Vehicle CreateVehicle(string model, string colour);
        protected abstract IVehiclePartsFactory CreatePartsFactory();
        protected abstract int GetAssemblyTimeMilliseconds();
    }

}

