using hospital.Model;
using hospital.Repository;
using hospital.Service;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class RoomService
    {
        private readonly IRoomRepository roomRepository;
        private readonly AppointmentRepository _appointmentRepository;
        private readonly ScheduledBasicRenovationService _basicRenovation;

        public RoomService(IRoomRepository roomRepository, AppointmentRepository appointmentRepository, ScheduledBasicRenovationService basicRenovation)
        {
            this.roomRepository = roomRepository;
            _appointmentRepository = appointmentRepository;
            _basicRenovation = basicRenovation;
        }

        public void Create(Room room)
        {
            Console.WriteLine("Added new user");
            roomRepository.Create(room);
        }

        public Room FindRoomById(string id)
        {
            return roomRepository.FindById(id);
        }

        public Room FindRoomByName(string name)
        {
            return roomRepository.FindByName(name);
        }

        public Room FindRoomByPurpose(string purpose)
        {
            return roomRepository.FindByPurpose(purpose);
        }
        public List<Room> FindRoomsByPurpose(string purpose)
        {
            return roomRepository.FindRoomsByPurpose(purpose);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentType(string type)
        {
            return roomRepository.FindRoomsByEquipmentType(type);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentQuantity(int quantity)
        {
            return roomRepository.FindRoomsByEquipmentQuantity(quantity);
        }

        public ObservableCollection<Room> FindRoomsByEquipmentTypeAndQuantity(string type, int quantity)
        {
            return roomRepository.FindRoomsByEquipmentTypeAndQuantity(type, quantity);
        }
        public ref ObservableCollection<Room> FindAll()
        {
            return ref roomRepository.FindAll();
        }

        public bool UpdateById(string id, Room room)
        {
            return roomRepository.UpdateById(id, room);
        }

        public bool DeleteById(string id)
        {
            return roomRepository.DeleteById(id);
        }

        public ObservableCollection<Room> FindAllOperationRooms()
        {
            ObservableCollection<Room> roomsForOperation = new ObservableCollection<Room>();
            foreach (Room room in FindAll())
            {
                if (room.purpose == "operation")
                {
                    roomsForOperation.Add(room);
                }
            }
            return roomsForOperation;
        }
        public Room FindRoomForOperationByTime(DateTime dateTime)
        {
            foreach (Room room in FindAllOperationRooms())
            {
                bool isBussy = false;
                foreach (Appointment appointment in _appointmentRepository.FindAll())
                {
                    if (dateTime == appointment.StartTime && appointment.RoomId == room.id)
                    {
                        isBussy = true;
                        break;
                    }
                }
                if (!isBussy && IsRenovation(room.id, dateTime))
                {
                    return room;
                }
            }
            return null;
        }

        public bool IsRenovation(string roomId, DateTime dateTime)
        {
            List<ScheduledBasicRenovation> renovationList = _basicRenovation.FindAll();
            bool canMake = true;
            foreach (ScheduledBasicRenovation renovation in renovationList)
            {
                if (renovation._Room.id == roomId && renovation._Interval._Start <= dateTime && renovation._Interval._End >= dateTime)
                {
                    canMake = false;
                }
            }
            return canMake;
        }
    }
}