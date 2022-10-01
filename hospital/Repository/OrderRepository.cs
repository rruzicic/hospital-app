using FileHandler;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class OrderRepository
    {
        public OrderFileHandler orderFileHandler;
        public ObservableCollection<Order> orders;

        public OrderRepository()
        {
            orderFileHandler = new OrderFileHandler();


            List<Order> deserializedList = orderFileHandler.Read();
            if (deserializedList != null)
            {
                orders = new ObservableCollection<Order>(orderFileHandler.Read());
            }
            else
            {
                orders = new ObservableCollection<Order>();
            }

        }


        public void Create(Order newOrder)
        {
            orders.Add(newOrder);
            orderFileHandler.Write(orders.ToList());
        }
        public ObservableCollection<Order> FindAll()
        {
            return orders;
        }

        public Order FindById(int id)
        {
            foreach (Order o in orders)
            {
                if (o.Id == id)
                {
                    return o;
                }
            }
            return null;
        }
        public bool DeleteById(int id)
        {
            bool retVal = orders.Remove(FindById(id));
            orderFileHandler.Write(orders.ToList());
            return retVal;
        }
    }
}
