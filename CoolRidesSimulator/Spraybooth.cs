using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public sealed class Spraybooth
    {
        private static Spraybooth _instance;
        private static readonly object _lock = new object();

        private readonly object _sprayLock = new object();
        private bool _isSpraying = false;
        private string _currentVehicle = "None";

        private Spraybooth()
        {

        }

        public static Spraybooth Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new Spraybooth();
                    }
                }
                return _instance;
            }
        }

        public bool IsSpraying
        {
            get
            {
                lock (_sprayLock)
                {
                    return _isSpraying;
                }
            }
        }

        public string GetCurrentVehicle()
        {
            lock (_sprayLock)
            {
                return _currentVehicle;
            }
        }

        public void Spray(Vehicle vehicle)
        {
            lock (_sprayLock)
            {
                while (_isSpraying)
                {
                    Monitor.Wait(_sprayLock);
                }

                _isSpraying = true;
                _currentVehicle = vehicle.ToString();
            }

            Thread.Sleep(GetSprayTimeMilliseconds(vehicle));

            lock (_sprayLock)
            {
                _isSpraying = false;
                _currentVehicle = "None";
                Monitor.PulseAll(_sprayLock);
            }
        }

        private int GetSprayTimeMilliseconds(Vehicle vehicle)
        {
            if (vehicle is Car)
                return 5000;

            return 7000;

        }
    }
}
    