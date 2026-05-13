using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolRidesSimulator
{
    public sealed class Spraybooth
    {
        private static Spraybooth _instance;
        private static readonly object _lock = new object();

        private bool _isSpraying = false;
        private string _currentVehicle = "None";

        private Spraybooth()
        {
            Console.WriteLine("[Spraybooth] Singleton instance created");
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

        public bool IsSpraying => _isSpraying;
        public string GetCurrentVehicle() => _currentVehicle;

        public void Spray(string vehicleType, string colour)
        {
            lock (_lock)
            {
                // Wait if spraybooth is busy
                while (_isSpraying)
                {
                    Monitor.Wait(_lock);
                }
                _isSpraying = true;
                _currentVehicle = $"{vehicleType} ({colour})";
            }

            int sprayTime = (vehicleType.ToLower() == "car") ? 5000 : 7000;
            Console.WriteLine($"[Spraybooth] 🎨 Spraying {vehicleType} {colour}... ({sprayTime / 1000} seconds)");

            // Simulate spraying time
            Thread.Sleep(sprayTime);

            Console.WriteLine($"[Spraybooth] ✅ {vehicleType} painted {colour} and dried!");

            lock (_lock)
            {
                _isSpraying = false;
                _currentVehicle = "None";
                Monitor.Pulse(_lock); // Wake up waiting thread
            }
        }
    }
}