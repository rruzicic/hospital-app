using hospital.Repository;
using System.Collections.Generic;

namespace hospital.Service
{
    public class IngridientsService
    {
        private readonly IngridientsRepository ingridientsRepository;

        public IngridientsService(IngridientsRepository ingridientsRepository)
        {
            this.ingridientsRepository = ingridientsRepository;
        }

        public List<string> FindAll()
        {
            return ingridientsRepository.FindAll();
        }

        public void Create(string ingridient)
        {
            ingridientsRepository.Create(ingridient);
        }
    }
}
