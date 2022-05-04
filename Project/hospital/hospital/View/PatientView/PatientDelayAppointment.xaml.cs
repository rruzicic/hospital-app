﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for PatientDelayAppointment.xaml
    /// </summary>
    public partial class PatientDelayAppointment : Window
    {
        private AppointmentController ac;
        private Appointment selectedAppointment;
        private UserController uc;

        public PatientDelayAppointment(Appointment a)
        {
            InitializeComponent();
            /*foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(PatientHomeWindow))
                {
                    selectedAppointment = (window as PatientHomeWindow).appointmentTable.SelectedItem as Appointment;
                    tbxDoctor.Text = selectedAppointment.DoctorUsername;
                    oldDate.SelectedDate = selectedAppointment.StartTime;
                }
            */
            selectedAppointment = a;
            tbxDoctor.Text = selectedAppointment.DoctorUsername;
            oldDate.SelectedDate = selectedAppointment.StartTime;
            App app = Application.Current as App;
            uc = app.userController;
            ac = app.appointmentController;
            newDate.DisplayDateStart = DateTime.Now > selectedAppointment.StartTime.AddDays(-4) ? DateTime.Now : selectedAppointment.StartTime.AddDays(-4);
            newDate.DisplayDateEnd = selectedAppointment.StartTime.AddDays(4);
            DataContext = this;
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (newDate.SelectedDate != null)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, selectedAppointment.DoctorUsername,uc.CurentLoggedUser.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if(appointmentTable.SelectedItem != null)
            {
                ac.UpdateAppointment(selectedAppointment, (Appointment)appointmentTable.SelectedItem);
                Close();
            }
        }
    }
}
