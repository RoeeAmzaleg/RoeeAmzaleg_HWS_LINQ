using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RoeeAmzaleg_HWS_LINQ
{
    internal class Order
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int StockNumber { get; set; }


        public Order(string productName, int price, int stockNumber)
        {
            ProductName=productName;
            Price=price;
            StockNumber=stockNumber;
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
