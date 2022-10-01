using hospital.Model;
using System.Collections.Generic;

namespace hospital.FileHandler
{
    public class ScheduledAdvancedRenovationFileHandler
    {
        private readonly string path = @"../../Resources/Data/ScheduledAdvancedRenovationData.txt";
        public List<ScheduledAdvancedRenovation> Read()
        {
            string serializedRenovations = System.IO.File.ReadAllText(path);
            List<ScheduledAdvancedRenovation> renovations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScheduledAdvancedRenovation>>(serializedRenovations);
            return renovations;
        }

        public void Write(List<ScheduledAdvancedRenovation> renovations)
        {
            string serializedRenovations = Newtonsoft.Json.JsonConvert.SerializeObject(renovations);
            System.IO.File.WriteAllText(path, serializedRenovations);
        }
    }
}
