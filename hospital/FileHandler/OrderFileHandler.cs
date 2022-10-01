using Model;
using System.Collections.Generic;

namespace FileHandler
{
    public class OrderFileHandler
    {
        private readonly string path = @"../../Resources/Data/OrderData.txt";


        public List<Order> Read()
        {
            try
            {
                string serializedOrders = System.IO.File.ReadAllText(path);
                List<Order> orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Order>>(serializedOrders);
                return orders;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<Order> orders)
        {
            string serializedOrders = Newtonsoft.Json.JsonConvert.SerializeObject(orders);
            System.IO.File.WriteAllText(path, serializedOrders);
        }
    }
}
