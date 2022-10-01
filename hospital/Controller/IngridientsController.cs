using hospital.Service;
using System.Collections.Generic;

namespace hospital.Controller
{
    public class IngridientsController
    {
        private readonly IngridientsService ingridientsService;

        public IngridientsController(IngridientsService ingridientsService)
        {
            this.ingridientsService = ingridientsService;
        }

        public List<string> FindAll()
        {
            return ingridientsService.FindAll();
        }

        public void Create(string ingridient)
        {
            ingridientsService.Create(ingridient);
        }
    }
}
