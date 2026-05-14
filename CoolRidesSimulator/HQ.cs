using System;
using System.Collections.Generic;

namespace CoolRidesSimulator
{
    public class HQ
    {
        private CarAssemblyLine _carLine;
        private MinibusAssemblyLine _busLine;
        private List<string> _orderHistory = new List<string>();

        public HQ(CarAssemblyLine carLine, MinibusAssemblyLine busLine)
        {
            _carLine = carLine;
            _busLine = busLine;
        }

        public void OrderCar(string colour)
        {
            var command = new BuildCarCommand(_carLine, colour);
            _carLine.AddCommand(command);
            _orderHistory.Insert(0, $"[{DateTime.Now:HH:mm:ss}] ORDER: Car LUX1000 in {colour}");

            // Also log to console for debugging
            Console.WriteLine($"[HQ] Car order placed for {colour}");
        }

        public void OrderMinibus(string colour)
        {
            var command = new BuildMinibusCommand(_busLine, colour);
            _busLine.AddCommand(command);
            _orderHistory.Insert(0, $"[{DateTime.Now:HH:mm:ss}] ORDER: Minibus MV500 in {colour}");

            Console.WriteLine($"[HQ] Minibus order placed for {colour}");
        }

        public void AddActivityLog(string activity)
        {
            _orderHistory.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {activity}");
            // Keep only last 50 entries
            if (_orderHistory.Count > 50)
                _orderHistory.RemoveAt(_orderHistory.Count - 1);
        }

        public List<string> GetOrderHistory()
        {
            return _orderHistory;
        }
    }
}