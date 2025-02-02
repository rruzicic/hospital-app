﻿using hospital.Repository;
using Model;
using Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Service
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly IRoomRepository _roomRepository;
        private ObservableCollection<Order> _orders;

        public OrderService(OrderRepository orderRepo, IRoomRepository roomRepo) { _orderRepository = orderRepo; _roomRepository = roomRepo; }

        public void Create(Order newOrder)
        {
            newOrder.Id = GetNewId();
            _orderRepository.Create(newOrder);
        }

        public int GetNewId()
        {
            _orders = _orderRepository.FindAll();
            if (_orders.Count == 0)
            {
                return 100;
            }
            else
            {
                return _orders[_orders.Count - 1].Id + 1;
            }
        }
        public ObservableCollection<Order> FindAll()
        {
            return _orderRepository.FindAll();
        }

        public void OrderTracker()
        {
            while (true)
            {
                try
                {
                    CheckTimeHasExpired();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private void CheckTimeHasExpired()
        {
            Thread.Sleep(10000);
            foreach (Order order in FindAll().ToList())
            {
                if (order.OrderDate.AddSeconds(3) < DateTime.Now)
                {
                    AddEquipmentInWarehouse(order);
                    _orderRepository.DeleteById(order.Id);
                }
            }
        }
        private void AddEquipmentInWarehouse(Order order)
        {
            Room warhouse = _roomRepository.FindById("9559");
            warhouse.AddEquipment(order.Equipment);
        }
    }
}
