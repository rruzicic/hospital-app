using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorMakeNewAppointmentForOtherDoctors.xaml
    /// </summary>
    public partial class DoctorMakeNewAppointmentForOtherDoctors : Window
    {
        private readonly DoctorController dc;
        private readonly AppointmentManagementController ac;
        private readonly PatientController pc;
        private readonly RoomController rc;
        private readonly UserController uc;
        private readonly ScheduledBasicRenovationController sbrc;
        private readonly AvailableAppointmentController aac;

        private readonly Doctor loggedInDoctor;
        private Doctor selectedDoctor;
        private Patient selectedPatient;
        public DoctorMakeNewAppointmentForOtherDoctors()
        {
            InitializeComponent();
            App app = Application.Current as App;
            dc = app.doctorController;
            ac = app.appointmentController;
            pc = app.patientController;
            rc = app.roomController;
            uc = app.userController;
            sbrc = app.scheduledBasicRenovationController;
            aac = app.availableAppointmentController;

            cmbPatients.ItemsSource = pc.FindAll();
            cmbDoctor.ItemsSource = dc.GetDoctors();
            cmbOpRoom.ItemsSource = rc.FindAll(); // dodati proveru da izlistava samo "operation" sale
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);

            cbOperation.IsEnabled = false;
            DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPatients.SelectedIndex != -1 && cmbDoctor.SelectedIndex != -1 && date.SelectedDate == null)
            {
                DataContext = this;
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                selectedDoctor = (Doctor)cmbDoctor.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDoctor(selectedDoctor.Username, "");
            }
            if (cmbPatients.SelectedIndex != -1 && cmbDoctor.SelectedIndex != -1 && date.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)date.SelectedDate;
                selectedPatient = (Patient)cmbPatients.SelectedItem;
                selectedDoctor = (Doctor)cmbDoctor.SelectedItem;
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDateAndDoctor(selectedDate, selectedDoctor.Username, cmbPatients.Text);
            }
        }
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedIndex != -1 && selectedPatient != null && selectedDoctor != null)
            {
                Appointment selectedAppointment = assembleAppointment();

                bool canMake = checkRenovations(selectedAppointment);
                if (canMake)
                {
                    ac.CreateAppointment(selectedAppointment);
                    Close();
                }
            }
        }
        private bool checkRenovations(Appointment selectedAppointment)
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
        private Appointment assembleAppointment()
        {
            Appointment selectedAppointment = (Appointment)appointmentTable.SelectedItem;
            selectedAppointment.PatientUsername = selectedPatient.Username;
            selectedAppointment.Description = tbDescription.Text;
            if (cbOperation.IsChecked == true && cmbOpRoom.SelectedIndex != -1 && selectedDoctor.Specialization != Specialization.General)
            {
                selectedAppointment.RoomId = ((Room)cmbOpRoom.SelectedItem).id;
            }
            else
            {
                selectedAppointment.RoomId = selectedDoctor.OrdinationId;
            }

            return selectedAppointment;
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

        private void cmbDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDoctor = (Doctor)cmbDoctor.SelectedItem;
            appointmentTable.ItemsSource = null;
            if (selectedDoctor.Specialization != Specialization.General)
            {
                cbOperation.IsEnabled = true;
            }
            else
            {
                cbOperation.IsEnabled = false;
            }
        }

        private void cmbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appointmentTable.ItemsSource = null;
        }
    }
}
