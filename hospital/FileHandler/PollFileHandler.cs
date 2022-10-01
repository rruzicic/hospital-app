using hospital.Model;
using System.Collections.Generic;

namespace hospital.FileHandler
{
    public class PollFileHandler
    {
        private readonly string path = @"../../Resources/Data/PollResultData.txt";

        public List<PollBlueprint> Read()
        {
            try
            {
                string serializedPolls = System.IO.File.ReadAllText(path);
                List<PollBlueprint> polls = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PollBlueprint>>(serializedPolls);
                return polls;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<PollBlueprint> polls)
        {
            string serializedPolls = Newtonsoft.Json.JsonConvert.SerializeObject(polls);
            System.IO.File.WriteAllText(path, serializedPolls);
        }
    }
}
