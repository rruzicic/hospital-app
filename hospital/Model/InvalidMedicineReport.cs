namespace Model
{
    public class InvalidMedicineReport
    {
        private int id;
        private string medicineId;
        private string note;

        public InvalidMedicineReport() { }
        public InvalidMedicineReport(string medicineId, string note, int id)
        {
            this.medicineId = medicineId;
            this.note = note;
            this.id = id;
        }
        public int Id
        {
            get => id;
            set => id = value;
        }
        public string MedicineId
        {
            get => medicineId;
            set => medicineId = value;
        }
        public string Note
        {
            get => note;
            set => note = value;
        }
    }
}
