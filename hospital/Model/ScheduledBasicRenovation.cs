using Model;

namespace hospital.Model
{
    public class ScheduledBasicRenovation
    {
        private string id;
        private Room room;
        private TimeInterval interval;
        private string description;

        public ScheduledBasicRenovation(string id, Room room, TimeInterval interval, string description)
        {
            this.id = id;
            this.room = room;
            this.interval = interval;
            this.description = description;
        }

        public string _Id
        {
            get => id;
            set => id = value;
        }

        public Room _Room
        {
            get => room;
            set => room = value;
        }

        public TimeInterval _Interval
        {
            get => interval;
            set => interval = value;
        }

        public string _Description
        {
            get => description;
            set => description = value;
        }
    }
}
