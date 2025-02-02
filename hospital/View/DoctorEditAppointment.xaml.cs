﻿using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointment.xaml
    /// </summary>
    public partial class DoctorEditAppointment : Window
    {
        private readonly AppointmentManagementController ac;
        private readonly RoomController rc;
        private readonly ScheduledBasicRenovationController sbrc;
        private readonly AvailableAppointmentController aac;

        private readonly Appointment selectedAppointment;
        public DoctorEditAppointment()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ac = app.appointmentController;
            rc = app.roomController;
            aac = app.availableAppointmentController;
            sbrc = app.scheduledBasicRenovationController;
            DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorAppointmentsWindow))
                {
                    selectedAppointment = (window as DoctorAppointmentsWindow).Table.SelectedItem as Appointment;
                    tbPatient.Text = selectedAppointment.PatientUsername; // changed because of changes in the model 
                    date.SelectedDate = selectedAppointment.StartTime;
                    tbDescription.Text = selectedAppointment.Description;
                    if (rc.FindRoomById(selectedAppointment.RoomId) != null && rc.FindRoomById(selectedAppointment.RoomId)._Purpose == "operation")
                    {
                        cmbOpRoom.ItemsSource = rc.FindRoomsByPurpose("operation");
                        cmbOpRoom.SelectedItem = rc.FindRoomById(selectedAppointment.RoomId);
                        cbOperation.IsChecked = true;
                        cmbOpRoom.IsEnabled = true;
                    }
                }
            }

        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (date.SelectedDate != null)
            {
                appointmentTable.ItemsSource = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, selectedAppointment.DoctorUsername, tbPatient.Text);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedItem != null)
            {
                Appointment updatedAppointment = (Appointment)appointmentTable.SelectedItem;
                updatedAppointment.Description = tbDescription.Text;
                if (cmbOpRoom.SelectedIndex != -1)
                {
                    updatedAppointment.RoomId = ((Room)cmbOpRoom.SelectedItem).id;
                }

                bool canMake = checkRenovations(updatedAppointment);
                if (canMake)
                {
                    ac.UpdateAppointment(selectedAppointment, updatedAppointment);
                    Close();
                }
            }
        }
        private bool checkRenovations(Appointment updatedAppointment)
        {
            List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
            foreach (ScheduledBasicRenovation renovation in renovationList)
            {
                if (renovation._Room.id == selectedAppointment.RoomId && renovation._Interval._Start < updatedAppointment.StartTime && renovation._Interval._End > updatedAppointment.StartTime)
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
