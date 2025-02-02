using FileHandler;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class DoctorRepository
    {
        public DoctorFileHandler doctorFileHandler;
        public ObservableCollection<Doctor> doctors;

        public DoctorRepository()
        {
            doctorFileHandler = new DoctorFileHandler();

            List<Doctor> deserializedList = doctorFileHandler.Read();
            if (deserializedList != null)
            {
                doctors = new ObservableCollection<Doctor>(doctorFileHandler.Read());
            }
            else
            {
                doctors = new ObservableCollection<Doctor>();

            }

            //doctors = new ObservableCollection<Doctor>();
            /*Doctor d1 = new Doctor("Mitar", "Miric");
            d1.Username = "miromir";
            d1.Specialization = Specialization.general;
            doctors.Add(d1);
            Doctor d2 = new Doctor("Jovan", "Jovanovic");
            d2.Username = "jovanov";
            d2.Specialization = Specialization.general;
            doctors.Add(d2);
            Doctor d3 = new Doctor("Bosko", "Ristovic");
            d3.Username = "Skabo";
            doctors.Add(d3);*/

            //doctorFileHandler.Write(doctors.ToList()); //ovo za sad ovde stoji
        }
        public ObservableCollection<Doctor> FindAll()
        {
            return doctors;
        }
        public Doctor getByName(string firsname, string lastname)
        {
            foreach (Doctor doctor in doctors)
            {
                if (doctor.Name.Equals(firsname) && doctor.Surname.Equals(lastname))
                {
                    return doctor;
                }
            }
            return null;
        }

        public Doctor FindByUsername(string username)
        {
            foreach (Doctor d in doctors)
            {
                if (d.Username.Equals(username))
                {
                    return d;
                }
            }
            return null;
        }

        public void addPatientToDoctorsList(string patientId, string doctorUsername)
        {
            Doctor d = FindByUsername(doctorUsername);
            d.myPatients.Add(patientId);
            doctorFileHandler.Write(doctors.ToList());
        }

        public void addOrdinationToDoctor(string doctorUsername, string ordinationId)
        {
            Doctor d = FindByUsername(doctorUsername);
            d.OrdinationId = ordinationId;
            doctorFileHandler.Write(doctors.ToList());
        }

    }
}