﻿using hospital.Model;
using System.Collections.Generic;

namespace hospital.FileHandler
{
    public class ScheduledBasicRenovationFIleHandler
    {
        private readonly string path = @"../../Resources/Data/ScheduledBasicRenovationData.txt";


        public List<ScheduledBasicRenovation> Read()
        {
            string serializedRenovations = System.IO.File.ReadAllText(path);
            List<ScheduledBasicRenovation> renovations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ScheduledBasicRenovation>>(serializedRenovations);
            return renovations;
        }

        public void Write(List<ScheduledBasicRenovation> renovations)
        {
            string serializedRenovations = Newtonsoft.Json.JsonConvert.SerializeObject(renovations);
            System.IO.File.WriteAllText(path, serializedRenovations);
        }
    }
}
