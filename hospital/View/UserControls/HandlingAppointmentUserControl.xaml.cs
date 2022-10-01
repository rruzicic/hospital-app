using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace hospital.View.UserControls
{
    public partial class HandlingAppointmentUserControl : UserControl
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        private readonly ObservableCollection<Appointment> comingAppointmnet = new ObservableCollection<Appointment>();
        private readonly PatientController pc;
        private readonly AppointmentManagementController ac;
        private readonly DoctorController dc;
        private readonly AvailableAppointmentController aac;
        public HandlingAppointmentUserControl()
        {
            InitializeComponent();
            DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            foreach (Appointment appointment in ac.GetAppointments())
            {
                if (DateTime.Now < appointment.StartTime)
                {
                    comingAppointmnet.Add(appointment);
                }
            }

            Appointments = comingAppointmnet;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            makeAppointmentUserControl.Visibility = Visibility.Visible;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAppointment.SelectedItem != null)
            {
                editAppointmentUsercontrol.cmbUsername.Text = ((Appointment)dateGridHandlingAppointment.SelectedItem).PatientUsername;
                editAppointmentUsercontrol.date.SelectedDate = ((Appointment)dateGridHandlingAppointment.SelectedItem).StartTime;
                editAppointmentUsercontrol.txtNewTime.Value = ((Appointment)dateGridHandlingAppointment.SelectedItem).StartTime;
                editAppointmentUsercontrol.Visibility = Visibility.Visible;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dateGridHandlingAppointment.SelectedItem != null)
            {
                ac.DeleteAppointment(((Appointment)dateGridHandlingAppointment.SelectedItem).Id);
                Appointments.Remove(((Appointment)dateGridHandlingAppointment.SelectedItem));
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (tbx != null)
            {
                List<Appointment> filteredList = Appointments.Where(x => x.PatientUsername.ToLower().Contains(tbx.Text.ToLower()) || x.DoctorUsername.ToLower().Contains(tbx.Text.ToLower()) || x.StartTime.ToString().Contains(tbx.Text.ToLower()) || x.Duration.ToString().Contains(tbx.Text.ToLower()) || x.RoomId.ToLower().Contains(tbx.Text.ToLower())).ToList();
                dateGridHandlingAppointment.ItemsSource = null;
                dateGridHandlingAppointment.ItemsSource = filteredList;
            }
            else
            {
                dateGridHandlingAppointment.ItemsSource = Appointments;
            }
        }
    }
}
