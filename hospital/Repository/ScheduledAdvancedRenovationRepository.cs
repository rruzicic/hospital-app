﻿using hospital.FileHandler;
using hospital.Model;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace hospital.Repository
{
    public class ScheduledAdvancedRenovationRepository : IScheduledAdvancedRenovationRepository
    {
        private readonly List<ScheduledAdvancedRenovation> renovations;
        private readonly ScheduledAdvancedRenovationFileHandler renovationFileHandler;

        public ScheduledAdvancedRenovationRepository()
        {
            renovations = new List<ScheduledAdvancedRenovation>();
            renovationFileHandler = new ScheduledAdvancedRenovationFileHandler();
        }

        public void Create(ScheduledAdvancedRenovation renovation)
        {
            renovations.Add(renovation);
            renovationFileHandler.Write(renovations.ToList());
        }

        public ScheduledAdvancedRenovation FindById(string id)
        {
            foreach (ScheduledAdvancedRenovation renovation in renovations)
            {
                if (renovation._Id.Equals(id))
                {
                    return renovation;
                }
            }
            return null;
        }

        public List<ScheduledAdvancedRenovation> FindAll()
        {
            return renovations;
        }

        public bool UpdateById(string id, ScheduledAdvancedRenovation renovation)
        {
            foreach (ScheduledAdvancedRenovation ren in renovations)
            {
                if (ren._Id.Equals(id))
                {
                    ren._Room = renovation._Room;
                    ren._Interval = renovation._Interval;
                    ren._Description = renovation._Description;
                    renovationFileHandler.Write(renovations.ToList());
                    return true;
                }
            }
            return false;
        }

        public bool DeleteById(string id)
        {
            ScheduledAdvancedRenovation renovation = FindById(id);
            renovations.Remove(renovation);
            renovationFileHandler.Write(renovations.ToList());
            return true;
        }

        public List<ScheduledAdvancedRenovation> FindForSpecifiedRoom(Room room)
        {
            List<ScheduledAdvancedRenovation> scheduledAdvancedRenovations = new List<ScheduledAdvancedRenovation>();
            foreach (ScheduledAdvancedRenovation renovation in renovations)
            {
                if (renovation._Room.id.Equals(room.id) || renovation.rooms[0].id.Equals(room.id) || renovation.rooms[1].id.Equals(room.id))
                {
                    scheduledAdvancedRenovations.Add(renovation);
                }
            }
            return renovations;
        }

        public void LoadRenovationData()
        {
            if (renovationFileHandler.Read() != null)
            {
                foreach (ScheduledAdvancedRenovation ren in renovationFileHandler.Read())
                {
                    renovations.Add(ren);
                }
            }
        }

        public void WriteRenovationData()
        {
            renovationFileHandler.Write(renovations);
        }
    }
}
