using System;
using System.Collections.Generic;

namespace CoolRidesSimulator
{
    public class HQ
    {
        private CarAssemblyLine _carAssemblyLine;
        private MinibusAssemblyLine _minibusAssemblyLine;

        // Track order history for display
        private List<string> _orderHistory = new List<string>();

        public HQ(CarAssemblyLine carLine, MinibusAssemblyLine minibusLine)
        {
            _carAssemblyLine = carLine;
            _minibusAssemblyLine = minibusLine;
        }

        public void OrderCar(string colour)
        {
            var command = new BuildCarCommand(_carAssemblyLine, colour);
            _carAssemblyLine.AddCommand(command);
            _orderHistory.Insert(0, $"[CAR] Ordered {colour} LUX1000 at {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"HQ: Car order placed for {colour}");
        }

        public void OrderMinibus(string colour)
        {
            var command = new BuildMinibusCommand(_minibusAssemblyLine, colour);
            _minibusAssemblyLine.AddCommand(command);
            _orderHistory.Insert(0, $"[MINIBUS] Ordered {colour} MV500 at {DateTime.Now:HH:mm:ss}");
            Console.WriteLine($"HQ: Minibus order placed for {colour}");
        }

        public List<string> GetOrderHistory()
        {
            return _orderHistory;
        }
    }
}