﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorTherapyWindow.xaml
    /// </summary>
    public partial class DoctorTherapyWindow : Window
    {
        public IEnumerable<int> intervals = new List<int>() { 1, 2, 3, 4, 6, 8, 12 };
        public IEnumerable<int> hours = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };

        private readonly DoctorController dc;
        private readonly PatientController pc;
        private readonly MedicalRecordsController mrc;
        private readonly UserController uc;
        private readonly MedicineController mc;

        private readonly Doctor loggedInDoctor;
        private Patient selectedPatient;
        private Medicine selectedMedicine;
        private int interval;
        public DoctorTherapyWindow()
        {
            InitializeComponent();
            DataContext = this;
            App app = Application.Current as App;
            dc = app.doctorController;
            pc = app.patientController;
            mrc = app.medicalRecordsController;
            uc = app.userController;
            mc = app.medicineController;

            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);
            cmbPatients.ItemsSource = loggedInDoctor.myPatients;
            cmbMedicine.ItemsSource = mc.FindAll();
            cmbInterval.ItemsSource = intervals;
            cmbStartHour.ItemsSource = hours;
        }

        private void cmbPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPatient = pc.FindById((string)cmbPatients.SelectedItem);
        }

        private void cmbMedicine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMedicine = mc.FindById((cmbMedicine.SelectedItem as Medicine).Id);
        }

        private void cmbInterval_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            interval = int.Parse(cmbInterval.SelectedItem.ToString());
        }
        private DateTime generateStartTime()
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue && cmbStartHour.SelectedIndex != -1)
            {
                int startHour = int.Parse(cmbStartHour.SelectedItem.ToString());
                DateTime selectedDate = (DateTime)dpStartDate.SelectedDate;
                DateTime startTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, startHour, 0, 0);
                if (startTime > DateTime.Now && dpEndDate.SelectedDate.Value > startTime)
                {
                    return startTime;
                }
            }
            return DateTime.MinValue; //ako vreme ne odgovara
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool canTakeMedicine = mrc.CheckAllergies(selectedPatient.RecordId, selectedMedicine);
            DateTime startTime = generateStartTime();
            if (cmbPatients.SelectedIndex != -1 && cmbMedicine.SelectedIndex != -1 && cmbInterval.SelectedIndex != -1
                && canTakeMedicine && startTime != DateTime.MinValue)
            {
                Therapy newTherapy = new Therapy(startTime, dpEndDate.SelectedDate.Value, interval, selectedMedicine);
                mrc.AddTheraphy(selectedPatient.RecordId, newTherapy);
                Close();
            }

        }

    }
}
