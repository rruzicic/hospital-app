using Model;
using System.Collections.Generic;

namespace hospital.FileHandler
{
    public class UserFileHandler
    {
        private readonly string path = @"../../Resources/Data/UserData.txt";


        public List<User> Read()
        {
            try
            {
                string serializedUsers = System.IO.File.ReadAllText(path);
                List<User> users = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(serializedUsers);
                return users;
            }
            catch
            {
                return null;
            }
        }

        public void Write(List<User> users)
        {
            string serializedUsers = Newtonsoft.Json.JsonConvert.SerializeObject(users);
            System.IO.File.WriteAllText(path, serializedUsers);
        }
    }
}
