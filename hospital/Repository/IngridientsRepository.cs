using hospital.FileHandler;
using System.Collections.Generic;

namespace hospital.Repository
{
    public class IngridientsRepository
    {
        private readonly IngridientsFileHandler ingridientsFileHandler;
        private readonly List<string> ingridients;

        public IngridientsRepository()
        {
            ingridientsFileHandler = new IngridientsFileHandler();
            ingridients = new List<string>();
        }

        public List<string> FindAll()
        {
            return ingridients;
        }

        public void Create(string ingridient)
        {
            ingridients.Add(ingridient);
            WriteIngridientsData();
        }
        public void LoadIngridientsData()
        {
            if (ingridientsFileHandler.Read() != null)
            {
                foreach (string ingridient in ingridientsFileHandler.Read())
                {
                    ingridients.Add(ingridient);
                }
            }
        }

        public void WriteIngridientsData()
        {
            ingridientsFileHandler.Write(ingridients);
        }
    }
}
