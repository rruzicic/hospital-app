using Model;

namespace hospital.Model
{
    public class ScheduledRelocation
    {
        private string id;
        private Room fromRoom;
        private Room toRoom;
        private string typeOfEquipment;
        private int quantity;
        private TimeInterval relocation;

        public ScheduledRelocation(string id, Room froomRoom, Room toRoom, string typeOfEquipment, int quantity, TimeInterval relocation)
        {
            this.id = id;
            fromRoom = froomRoom;
            this.toRoom = toRoom;
            this.typeOfEquipment = typeOfEquipment;
            this.quantity = quantity;
            this.relocation = relocation;
        }

        public string _Id
        {
            get => id;
            set => id = value;
        }

        public Room _FromRoom
        {
            get => fromRoom;
            set => fromRoom = value;
        }

        public Room _ToRoom
        {
            get => toRoom;
            set => toRoom = value;
        }

        public string _TypeOfEquipment
        {
            get => typeOfEquipment;
            set => typeOfEquipment = value;
        }
        public int _Quantity
        {
            get => quantity;
            set => quantity = value;
        }
        public TimeInterval _Relocation
        {
            get => relocation;
            set => relocation = value;
        }


    }
}
