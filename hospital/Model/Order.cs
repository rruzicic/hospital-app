using System;
namespace Model
{
    public class Order
    {
        private int _id;
        private DateTime _orderDate;
        private Equipment _equipment;

        public Order(DateTime orderDate, Equipment equipment)
        {
            _orderDate = orderDate;
            _equipment = equipment;
        }
        public Order() { }

        public int Id { get => _id; set => _id = value; }
        public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }
        public Equipment Equipment { get => _equipment; set => _equipment = value; }
    }
}
