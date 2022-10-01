using Controller;
using Model;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientCalendar.xaml
    /// </summary>
    public partial class PatientCalendar : Page
    {
        public PatientCalendar()
        {
            InitializeComponent();
            App app = Application.Current as App;
            AppointmentManagementController ac = app.appointmentController;
            PatientController pc = app.patientController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach (Appointment a in ac.GetAppointmentByPatient(app.userController.CurentLoggedUser.Username))
            {
                ScheduleAppointment sa = new ScheduleAppointment
                {
                    StartTime = a.StartTime,
                    EndTime = a.StartTime.AddMinutes(30),
                    IsAllDay = false
                };
                sac.Add(sa);


            }
            appointmentCalendar.ItemsSource = sac;

        }
    }
}
