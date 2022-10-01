﻿using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class VacationRequestController
    {
        private readonly VacationRequestService vacationRequestService;
        public VacationRequestController(VacationRequestService _repo)
        {
            vacationRequestService = _repo;
        }
        public bool Create(VacationRequest vacationRequest)
        {
            return vacationRequestService.Create(vacationRequest);
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return vacationRequestService.FindAll();
        }
        public VacationRequest FindById(int id)
        {
            return vacationRequestService.FindById(id);
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            return vacationRequestService.FindByDoctorId(doctorId);
        }
        public bool DeleteById(int id)
        {
            return vacationRequestService.DeleteById(id);
        }
        public void FinishRequest(string resultRequest, int requestId)
        {
            vacationRequestService.FinishRequest(resultRequest, requestId);
        }
    }
}
