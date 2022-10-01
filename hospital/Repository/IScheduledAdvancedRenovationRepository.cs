using hospital.Model;
using Model;
using System.Collections.Generic;

namespace hospital.Repository
{
    public interface IScheduledAdvancedRenovationRepository
    {
        void Create(ScheduledAdvancedRenovation renovation);
        ScheduledAdvancedRenovation FindById(string id);
        List<ScheduledAdvancedRenovation> FindAll();
        bool UpdateById(string id, ScheduledAdvancedRenovation renovation);
        bool DeleteById(string id);
        List<ScheduledAdvancedRenovation> FindForSpecifiedRoom(Room room);
        void LoadRenovationData();
        void WriteRenovationData();
    }
}
