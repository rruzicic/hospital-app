using FileHandler;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository
{
    public class VacationRequestRepository
    {
        public ObservableCollection<VacationRequest> vacationRequests;
        public VacationRequestFileHandler vacationRequestFileHandler;
        public VacationRequestRepository()
        {
            vacationRequestFileHandler = new VacationRequestFileHandler();
            List<VacationRequest> deserializedList = vacationRequestFileHandler.Read();
            if (deserializedList != null)
            {
                vacationRequests = new ObservableCollection<VacationRequest>(vacationRequestFileHandler.Read());
            }
            else
            {
                vacationRequests = new ObservableCollection<VacationRequest>();
            }
        }
        public bool Create(VacationRequest vacationRequest)
        {
            vacationRequest.Id = generateId();
            vacationRequests.Add(vacationRequest);
            vacationRequestFileHandler.Write(vacationRequests.ToList());
            return true;
        }
        private int generateId()
        {
            int maxId = 0;
            foreach (VacationRequest v in FindAll())
            {
                if (v.Id > maxId)
                {
                    maxId = v.Id;
                }
            }
            return maxId + 1;
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return vacationRequests;
        }
        public VacationRequest FindById(int id)
        {
            foreach (VacationRequest vacationRequest in vacationRequests)
            {
                if (vacationRequest.Id == id)
                {
                    return vacationRequest;
                }
            }
            return null;
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            foreach (VacationRequest vacationRequest in vacationRequests)
            {
                if (vacationRequest.DoctorId == doctorId)
                {
                    return vacationRequest;
                }
            }
            return null;
        }
        public void UpdateStatus(int requestId, Status status)
        {
            FindById(requestId).Status = status;
            vacationRequestFileHandler.Write(vacationRequests.ToList());
        }
        public bool DeleteById(int id)
        {
            vacationRequests.Remove(FindById(id));
            vacationRequestFileHandler.Write(vacationRequests.ToList());
            return true;
        }
    }
}
