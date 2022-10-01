using Model;
using System.Collections.Generic;

namespace hospital.Model
{
    public class ScheduledAdvancedRenovation : ScheduledBasicRenovation
    {
        public List<Room> rooms { get; set; }
        public string flag { get; set; }
        public ScheduledAdvancedRenovation(string id, Room room, TimeInterval interval, string description, List<Room> rooms, string flag) : base(id, room, interval, description)
        {
            this.rooms = rooms;
            this.flag = flag;
        }

    }
}
