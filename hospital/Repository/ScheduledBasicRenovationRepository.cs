using hospital.FileHandler;
using hospital.Model;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace hospital.Repository
{
    public class ScheduledBasicRenovationRepository : IScheduledBasicRenovationRepository
    {
        private readonly List<ScheduledBasicRenovation> renovations;
        private readonly ScheduledBasicRenovationFIleHandler renovationFileHandler;

        public ScheduledBasicRenovationRepository()
        {
            renovations = new List<ScheduledBasicRenovation>();
            renovationFileHandler = new ScheduledBasicRenovationFIleHandler();
        }

        public void Create(ScheduledBasicRenovation renovation)
        {
            renovations.Add(renovation);
            renovationFileHandler.Write(renovations.ToList());
        }

        public ScheduledBasicRenovation FindById(string id)
        {
            foreach (ScheduledBasicRenovation renovation in renovations)
            {
                if (renovation._Id.Equals(id))
                {
                    return renovation;
                }
            }
            return null;
        }

        public List<ScheduledBasicRenovation> FindAll()
        {
            return renovations;
        }

        public bool UpdateById(string id, ScheduledBasicRenovation renovation)
        {
            foreach (ScheduledBasicRenovation ren in renovations)
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
            ScheduledBasicRenovation renovation = FindById(id);
            renovations.Remove(renovation);
            renovationFileHandler.Write(renovations.ToList());
            return true;
        }

        public List<ScheduledBasicRenovation> FindForSpecifiedRoom(Room room)
        {
            List<ScheduledBasicRenovation> scheduledBasicRenovation = new List<ScheduledBasicRenovation>();
            foreach (ScheduledBasicRenovation renovation in renovations)
            {
                if (renovation._Room.id.Equals(room.id))
                {
                    scheduledBasicRenovation.Add(renovation);
                }
            }
            return renovations;
        }

        public void LoadRenovationData()
        {
            if (renovationFileHandler.Read() != null)
            {
                foreach (ScheduledBasicRenovation ren in renovationFileHandler.Read())
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
