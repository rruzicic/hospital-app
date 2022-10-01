using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for DoctorMakeNewAppointment.xaml
    /// </summary>
    public partial class DoctorMakeNewAppointment : Window
    {
        private readonly DoctorController dc;
        private readonly AppointmentManagementController ac;
        private readonly PatientController pc;
        private readonly RoomController rc;
        private readonly UserController uc;
        private readonly ScheduledBasicRenovationController sbrc;
        private readonly AvailableAppointmentController aac;

        public Doctor loggedInDoctor;
        public Patient selectedPatient;
        public DoctorMakeNewAppointment()
        {
            InitializeComponent();
            App app = Application.Current as App;
            dc = app.doctorController;
            ac = app.appointmentController;
            pc = app.patientController;
            rc = app.roomController;
            uc = app.userController;
            aac = app.availableAppointmentController;

            sbrc = app.scheduledBasicRenovationController;

            cmbPatients.ItemsSource = pc.FindAll();
            cmbOpRoom.ItemsSource = rc.FindRoomsByPurpose("operation");
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            if (loggedInDoctor.Specialization == Specialization.General)
            {
                cbOperation.IsEnabled = false;
            }

            DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatients.SelectedIndex != -1 && date.SelectedDate == null)
            {
                DataContext = this;
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDoctor(loggedInDoctor.Username, "");
            }
            if (cmbPatients.SelectedIndex != -1 && date.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)date.SelectedDate;
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDateAndDoctor(selectedDate, loggedInDoctor.Username, cmbPatients.Text);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1 && selectedPatient != null)
            {
                Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
                selectedAppointment.PatientUsername = selectedPatient.Username;
                selectedAppointment.Description = tbDescription.Text;
                if (cbOperation.IsChecked == true && cmbOpRoom.SelectedIndex != -1 && loggedInDoctor.Specialization != Specialization.General)
                {
                    selectedAppointment.RoomId = ((Room)cmbOpRoom.SelectedItem).id;
                }
                else
                {
                    selectedAppointment.RoomId = loggedInDoctor.OrdinationId;
                }

                bool canMake = checkRenovation(selectedAppointment);
                if (canMake)
                {
                    ac.CreateAppointment(selectedAppointment);
                    Close();
                }
            }
        }
        public bool checkRenovation(Appointment selectedAppointment)
        {
            List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
            foreach (ScheduledBasicRenovation renovation in renovationList)
            {
                if (renovation._Room.id == selectedAppointment.RoomId && renovation._Interval._Start < selectedAppointment.StartTime && renovation._Interval._End > selectedAppointment.StartTime)
                {
                    MessageBox.Show("Invalid time because of renovations");
                    return false;
                }
            }
            return true;
        }

        private void cbOperation_Checked(object sender, RoutedEventArgs e)
        {
            if (cbOperation.IsChecked == true)
            {
                cmbOpRoom.IsEnabled = true;
            }

        }

        private void cbOperation_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbOperation.IsChecked == false)
            {
                cmbOpRoom.IsEnabled = false;
            }
        }
    }
}
