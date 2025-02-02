﻿using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class MedicineController
    {
        private readonly MedicineService medicineService;

        public MedicineController(MedicineService _service)
        {
            medicineService = _service;
        }

        public bool Create(Medicine medicine)
        {
            return medicineService.Create(medicine);
        }

        public ObservableCollection<Medicine> FindAll()
        {
            return medicineService.FindAll();
        }

        public Medicine FindById(string id)
        {
            return medicineService.FindById(id);
        }

        public Medicine FindByName(string name)
        {
            return medicineService.FindByName(name);
        }

        public bool UpdateById(string id, Medicine medicine)
        {
            return medicineService.UpdateById(id, medicine);
        }

        public bool DeleteById(string id)
        {
            return medicineService.DeleteById(id);
        }

    }
}