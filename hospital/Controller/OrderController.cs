using Model;
using Service;

namespace Controller
{
    public class OrderController
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService _service) { _orderService = _service; }

        public void Create(Order newOrder)
        {
            _orderService.Create(newOrder);
        }
    }
}
