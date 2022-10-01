﻿using Controller;
using hospital.DTO;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientTherapies.xaml
    /// </summary>
    public partial class PatientTherapies : Page
    {
        public PatientTherapies()
        {
            InitializeComponent();

            App app = Application.Current as App;
            PatientController pc = app.patientController;
            ScheduleAppointmentCollection sac = new ScheduleAppointmentCollection();

            foreach (TherapyDTO t in pc.FindCurrentMonthTherapies(app.userController.CurentLoggedUser.Username))
            {
                ScheduleAppointment sa = new ScheduleAppointment
                {
                    StartTime = t.Date,
                    EndTime = t.Date.AddMinutes(30),
                    IsAllDay = false,
                    Subject = t.Name,
                    AppointmentBackground = new SolidColorBrush(Color.FromRgb(6, 158, 47))
                };
                sac.Add(sa);
            }
            therapyCalendar.ItemsSource = sac;
        }

        private void btnPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            //btnPDF.Visibility = Visibility.Hidden;
            printDialog.PrintVisual(this, "report");
            //btnPDF.Visibility = Visibility.Visible;
        }
    }
}
