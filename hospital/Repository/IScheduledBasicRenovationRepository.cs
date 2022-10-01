using hospital.Model;
using Model;
using System.Collections.Generic;

namespace hospital.Repository
{
    public interface IScheduledBasicRenovationRepository
    {
        void Create(ScheduledBasicRenovation renovation);
        ScheduledBasicRenovation FindById(string id);
        List<ScheduledBasicRenovation> FindAll();
        bool UpdateById(string id, ScheduledBasicRenovation renovation);
        bool DeleteById(string id);
        List<ScheduledBasicRenovation> FindForSpecifiedRoom(Room room);
        void LoadRenovationData();
        void WriteRenovationData();

    }
}
