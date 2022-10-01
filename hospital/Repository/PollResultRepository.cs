using hospital.FileHandler;
using hospital.Model;
using Repository;
using System.Collections.Generic;

namespace hospital.Repository
{
    public class PollResultRepository
    {
        private readonly List<PollBlueprint> pollResults;
        public PollFileHandler pollFileHandler;
        private readonly AppointmentRepository appointmentRepository;

        public PollResultRepository(AppointmentRepository appointmentRepository)
        {
            pollFileHandler = new PollFileHandler();

            List<PollBlueprint> deserializedList = pollFileHandler.Read();
            if (deserializedList == null)
            {
                pollResults = new List<PollBlueprint>();
            }
            else
            {
                pollResults = new List<PollBlueprint>(pollFileHandler.Read());
            }
            this.appointmentRepository = appointmentRepository;
        }

        public void AddResult(PollBlueprint result)
        {
            pollResults.Add(result);
            pollFileHandler.Write(pollResults);
        }

        public List<PollBlueprint> GetAllResults()
        {
            return null;
        }

        public List<PollBlueprint> GetHospitalPollResults()
        {
            List<PollBlueprint> retVal = new List<PollBlueprint>();
            foreach (PollBlueprint poll in pollResults)
            {
                if (poll.Type == PollType.HOSPITAL_POLL)
                {
                    retVal.Add(poll);
                }
            }
            return retVal;
        }

        public List<PollBlueprint> GetDoctorPollResults()
        {
            List<PollBlueprint> retVal = new List<PollBlueprint>();
            foreach (PollBlueprint poll in pollResults)
            {
                if (poll.Type == PollType.DOCTOR_POLL)
                {
                    retVal.Add(poll);
                }
            }
            return retVal;
        }

        public List<PollBlueprint> FindPollResultsForDoctor(string id)
        {
            List<PollBlueprint> results = new List<PollBlueprint>();
            foreach (PollBlueprint poll in GetDoctorPollResults())
            {
                if (appointmentRepository.FindById(poll.AppointmentId).DoctorUsername == id)
                {
                    results.Add(poll);
                }
            }
            return results;
        }
    }
}
