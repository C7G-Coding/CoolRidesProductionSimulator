using System;
using System.Collections.Generic;

namespace CoolRidesSimulator
{

    public class HQ
    {
        private readonly CarAssemblyLine _carLine;
        private readonly MinibusAssemblyLine _busLine;
        private readonly List<string> _orderHistory = new List<string>();
        private readonly object _historyLock = new object();

        public HQ(CarAssemblyLine carLine, MinibusAssemblyLine busLine)
        {
            _carLine = carLine;
            _busLine = busLine;
        }

        public void OrderVehicle(string model, string colour)
        {
            if (model == "LUX1000")
            {
                var command = new BuildCarCommand(_carLine, model, colour);
                _carLine.AddCommand(command);
                AddActivityLog($"ORDER: {model} in {colour}");
            }
            else if (model == "MV500")
            {
                var command = new BuildMinibusCommand(_busLine, model, colour);
                _busLine.AddCommand(command);
                AddActivityLog($"ORDER: {model} in {colour}");
            }
            else
            {
                throw new ArgumentException("Unknown vehicle model.");
            }
        }

        public void AddActivityLog(string activity)
        {
            lock (_historyLock)
            {
                _orderHistory.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {activity}");

                if (_orderHistory.Count > 100)
                {
                    _orderHistory.RemoveAt(_orderHistory.Count - 1);
                }
            }
        }

        public List<string> GetOrderHistory()
        {
            lock (_historyLock)
            {
                return new List<string>(_orderHistory);
            }
        }

    }
}
