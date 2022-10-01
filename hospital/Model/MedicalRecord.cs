using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class MedicalRecord : INotifyPropertyChanged
    {
        private string note;
        private string alergies;
        private string doctorUsername;

        private List<Therapy> therapy;
        private BloodType bloodType;
        private string patientUsername;
        private int recordId;

        public MedicalRecord(string patientUsername, string _allergens, string _choosen, BloodType bt, string note)
        {
            PatientUsername = patientUsername;
            Note = note;
            Alergies = _allergens;
            DoctorUsername = _choosen;
            BloodType = bt;
        }
        public MedicalRecord(string patientsUsername, int id)
        {
            PatientUsername = patientsUsername;
            RecordId = id;
        }
        public MedicalRecord() { }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public int RecordId { get => recordId; set { recordId = value; OnPropertyChanged(""); } }
        public string Note { get => note; set { note = value; OnPropertyChanged(""); } }
        public string Alergies { get => alergies; set { alergies = value; OnPropertyChanged(""); } }
        public string DoctorUsername { get => doctorUsername; set { doctorUsername = value; OnPropertyChanged(""); } }
        public string PatientUsername { get => patientUsername; set { patientUsername = value; OnPropertyChanged(""); } }
        public BloodType BloodType { get => bloodType; set { bloodType = value; OnPropertyChanged(""); } }

        public List<Therapy> Therapy
        {
            get
            {
                if (therapy == null)
                {
                    therapy = new List<Therapy>();
                }

                return therapy;
            }
            set
            {
                therapy = value;
                OnPropertyChanged("");
            }
        }
    }
}