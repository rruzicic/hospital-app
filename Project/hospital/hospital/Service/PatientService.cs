using hospital;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Service
{
    public class PatientService
    {
        private readonly PatientRepository patientRepository;
        private readonly MedicalRecordsRepository medicalRecordsRepository;
        private readonly UserRepository userRepository;
        private  ObservableCollection<MedicalRecord> medicalRecords;
        

        public PatientService(PatientRepository _repo,MedicalRecordsRepository mec,UserRepository uc)
        { 
            patientRepository = _repo;
            medicalRecordsRepository = mec;
            userRepository = uc;
        }
        public bool Create(Patient patient)
        {
            int id = GetNewId();
            patient.RecordId = id;
            medicalRecordsRepository.Create(new MedicalRecord(patient.Username, id));
            return patientRepository.Create(patient);
        }

        public ObservableCollection<Patient> FindAll()
        {
            return patientRepository.FindAll();
        }

        public Patient FindById(string id)
        {
            return patientRepository.FindById(id);
        }

        public bool DeleteById(string id)
        {
            medicalRecordsRepository.DeleteById(FindById(id).RecordId);
            userRepository.DeleteByUsername(id);
            return patientRepository.DeleteById(id);
        }

        public bool UpdateById(string id, Patient patient)
        {
            return patientRepository.UpdateById(id, patient);
        }


        public int GetNewId()
        {
            medicalRecords = medicalRecordsRepository.FindAll();
            if (medicalRecords.Count == 0)
                return 333;
            else
                return medicalRecords[medicalRecords.Count - 1].RecordId + 1;
        }
    }
}