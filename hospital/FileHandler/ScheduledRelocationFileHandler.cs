using hospital.Model;
using System.Collections.Generic;

namespace hospital.FileHandler
{
    public class ScheduledRelocationFileHandler
    {
        private readonly string path = @"../../Resources/Data/ScheduledRelocationData.txt";


        public List<ScheduledRelocation> Read()
        {
            string serializedScheduledRelocations = System.IO.File.ReadAllText(path);
            List<ScheduledRelocation> scheduledRelocations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScheduledRelocation>>(serializedScheduledRelocations);
            return scheduledRelocations;
        }

        public void Write(List<ScheduledRelocation> scheduledRelocations)
        {
            string serializedScheduledRelocations = Newtonsoft.Json.JsonConvert.SerializeObject(scheduledRelocations);
            System.IO.File.WriteAllText(path, serializedScheduledRelocations);
        }
    }
}
