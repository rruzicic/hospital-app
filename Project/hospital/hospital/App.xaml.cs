﻿using Controller;
using Repository;
using Service;
using System.Windows;

namespace hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public RoomController roomController { get; set; }
        public PatientController patientController { get; set; }

        public AppointmentController appointmentController { get; set; }
        
        public App() {
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);

            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, doctorRepository);
            appointmentController = new AppointmentController(appointmentService);

        }
    }
}
