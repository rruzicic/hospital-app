using Model;
using System.Collections.Generic;

namespace FileHandler
{
    public class MedicalRecordFileHandler
    {
        private readonly string path = @"../../Resources/Data/MedicalRecordData.txt";

        public List<MedicalRecord> Read()
        {
            try
            {
                string serializedPatients = System.IO.File.ReadAllText(path);
                List<MedicalRecord> patients = Newtonsoft.Json.JsonConvert.DeserializeObject<List<MedicalRecord>>(serializedPatients);
                return patients;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<MedicalRecord> patients)
        {
            string serializedPatients = Newtonsoft.Json.JsonConvert.SerializeObject(patients);
            System.IO.File.WriteAllText(path, serializedPatients);
        }

    }
}
