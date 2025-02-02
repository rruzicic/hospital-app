using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Controller
{
    public class RoomController
    {
        private readonly RoomService roomService;

        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }

        public void Create(Room room)
        {
            if (room.purpose.ToLower().Equals("warehouse") && doesWarehouseExists())
            {
                throw new Exception("Warehouse already exists");
            }
            if (room.floor < 0 || room.floor > 5)
            {
                throw new Exception("Floor must be between 0 and 5");
            }

            if (!isNewId(room.id))
            {
                throw new Exception("Room with that ID already exists");
            }

            roomService.Create(room);
        }

        public Room FindRoomById(string id)
        {
            return roomService.FindRoomById(id);
        }

        public Room FindRoomByName(string name)
        {
            return roomService.FindRoomByName(name);
        }

        public Room FindRoomByPurpose(string purpose)
        {
            return roomService.FindRoomByPurpose(purpose);
        }
        public List<Room> FindRoomsByPurpose(string purpose)
        {
            return roomService.FindRoomsByPurpose(purpose);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentType(string type)
        {
            return roomService.FindRoomsByEquipmentType(type);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentQuantity(int quantity)
        {
            return roomService.FindRoomsByEquipmentQuantity(quantity);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentTypeAndQuantity(string type, int quantity)
        {
            return roomService.FindRoomsByEquipmentTypeAndQuantity(type, quantity);
        }

        public ref ObservableCollection<Room> FindAll()
        {
            return ref roomService.FindAll();
        }

        public bool UpdateById(string id, Room room)
        {
            Room r = FindRoomById(id);
            if (r.purpose.ToLower().Equals("warehouse"))
            {
                throw new Exception("Warehouse cant be edited");
            }
            if (room.purpose.ToLower().Equals("warehouse") && doesWarehouseExists())
            {
                throw new Exception("Warehouse already exists");
            }

            if (room.floor < 0 || room.floor > 5)
            {
                throw new Exception("Floor must be between 0 and 5");
            }

            return roomService.UpdateById(id, room);
        }

        public bool DeleteById(string id)
        {
            if (FindRoomById(id).purpose == "warehouse")
            {
                throw new Exception("Warehouse cant be deleted");
            }

            return roomService.DeleteById(id);
        }

        private bool doesWarehouseExists()
        {
            List<Room> rooms = FindAll().ToList();
            foreach (Room r in rooms)
            {
                if (r.purpose.ToLower().Equals("warehouse"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool isNewId(string id)
        {
            List<Room> rooms = FindAll().ToList();
            foreach (Room room in rooms)
            {
                if (room.id.Equals(id.Trim()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}