﻿using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService doctorService;

        public DoctorController(DoctorService _service)
        {
            doctorService = _service;
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return doctorService.GetDoctors();
        }

        public Doctor getByName(string name)
        {
            return doctorService.getByName(name);
        }


        public Doctor GetByUsername(string username)
        {
            return doctorService.GetByUsername(username);
        }

        public void addPatientToDoctorsList(string patientId, string doctorUsername)
        {
            doctorService.addPatientToDoctorsList(patientId, doctorUsername);
        }

        public void addOrdinationToDoctor(string doctorUsername, string ordinationId)
        {
            doctorService.addOrdinationToDoctor(doctorUsername, ordinationId);
        }
    }
}
