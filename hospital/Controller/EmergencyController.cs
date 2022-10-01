using Model;
using Service;
using System.Collections.Generic;

namespace Controller
{
    public class EmergencyController
    {
        private readonly EmergencyService _emergencyService;

        public EmergencyController(EmergencyService emergencyService)
        {
            _emergencyService = emergencyService;
        }

        public void TryMakeEmergencyAppointment(string patientUsername, Specialization requiredSpecialization, bool isOperation)
        {
            _emergencyService.TryMakeEmergencyAppointment(patientUsername, requiredSpecialization, isOperation);
        }
        public List<Appointment> FindSuggestedAppointments()
        {
            return _emergencyService.FindSuggestedAppointments();
        }
        public List<Appointment> FindAppointmentsForCancelation()
        {
            return _emergencyService.FindAppointmentsForCancelation();
        }
        public void MoveAppointemntAndMakeNotification(Appointment oldAppointment, Appointment newAppoitnemnt)
        {
            _emergencyService.MoveAppointemntAndMakeNotification(oldAppointment, newAppoitnemnt);
        }
    }
}
