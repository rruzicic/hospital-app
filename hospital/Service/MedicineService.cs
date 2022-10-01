using Model;
using Repository;
using System.Collections.ObjectModel;

namespace Service
{
    public class MedicineService
    {
        private readonly MedicineRepository medicineRepository;
        public MedicineService(MedicineRepository _repo)
        {
            medicineRepository = _repo;
        }
        public bool Create(Medicine medicine)
        {
            return medicineRepository.Create(medicine);
        }
        public ObservableCollection<Medicine> FindAll()
        {
            return medicineRepository.FindAll();
        }
        public Medicine FindById(string id)
        {
            return medicineRepository.FindById(id);
        }

        public Medicine FindByName(string name)
        {
            return medicineRepository.FindByName(name);
        }
        public bool UpdateById(string id, Medicine medicine)
        {
            return medicineRepository.UpdateById(id, medicine);
        }
        public bool DeleteById(string id)
        {
            return medicineRepository.DeleteById(id);
        }

    }
}
