using hospital.Service;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Controller
{
    public class AvailableAppointmentController
    {
        private readonly AvailableAppointmentService _availableAppointmentService;
        public AvailableAppointmentController(AvailableAppointmentService availableAppointmentService)
        {
            _availableAppointmentService = availableAppointmentService;
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDoctor(string doctorUsername, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDoctor(doctorUsername, patientUsername);
        }

        public ObservableCollection<Appointment> GetFreeAppointmentsByDate(DateTime date, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDate(date, patientUsername);
        }
        public ObservableCollection<Appointment> GetFreeAppointmentsByDateAndDoctor(DateTime date, string username, string patientUsername)
        {
            return _availableAppointmentService.GetFreeAppointmentsByDateAndDoctor(date, username, patientUsername);
        }
    }
}
