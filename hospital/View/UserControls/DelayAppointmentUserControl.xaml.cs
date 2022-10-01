﻿using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.UserControls
{
    public partial class DelayAppointmentUserControl : UserControl
    {
        private readonly PatientController pc;
        private readonly AppointmentManagementController ac;
        private readonly DoctorController dc;
        private readonly NotificationController nc;
        private readonly ScheduledBasicRenovationController sbrc;
        private readonly RecommendedAppointmentController rc;
        private readonly AvailableAppointmentController aac;
        public ObservableCollection<Patient> Patients { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }
        public DelayAppointmentUserControl()
        {
            InitializeComponent();
            DataContext = this;
            date.DisplayDateStart = DateTime.Today;
            newDate.DisplayDateStart = DateTime.Today;
            App app = Application.Current as App;
            pc = app.patientController;
            nc = app.notificationController;
            sbrc = app.scheduledBasicRenovationController;
            ac = app.appointmentController;
            dc = app.doctorController;
            rc = app.recommendedAppointmentController;
            aac = app.availableAppointmentController;
            Patients = pc.FindAll();
            Doctors = dc.GetDoctors();
        }

        private readonly Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });



        private bool isValidate()
        {
            bool[] isCorrected = new bool[2];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }
            if (newDate.Text.Equals(""))
            {
                errNewDate.Text = "Choose one date";
                isCorrected[0] = false;
            }
            else
            {
                errNewDate.Text = "";
                isCorrected[0] = true;
            }
            //newtime
            /*if (txtNewTime.Text.Equals(""))
            {
                errNewTime.Text = "Must be filled";
                isCorrected[4] = false;
            }
            else
            {
                errNewTime.Text = "";
                isCorrected[4] = true;
            }*/


            return (isCorrected[0] && isCorrected[1]);
        }

        private void cmbUsername_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            errUsername.Text = "";
        }


        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source == date)
            {
                if (date.Text.Equals(""))
                {
                    errDate.Text = "Choose one date";
                }
                else
                {
                    errDate.Text = "";
                }
            }

            if (e.Source == newDate)
            {
                if (newDate.Text.Equals(""))
                {
                    errNewDate.Text = "Choose one date";
                }
                else
                {
                    errNewDate.Text = "";
                }
            }
        }

        private void time_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Source == txtTime)
            {
                errTime.Text = "";
            }

            if (e.Source == txtNewTime)
            {
                errNewTime.Text = "";
            }
        }

        private void btnRecTwo_Click(object sender, RoutedEventArgs e)
        {
            rc.tryChangeAppointment(CurrentAppointment, (DateTime)newDate.SelectedDate, rc.RecommendedTwo.StartTime.ToString().Split(' ')[1]);
            Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            newDate.Text = "";
            txtNewTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            errNewDate.Text = "";
            errNewTime.Text = "";
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnRecOne_Click(object sender, RoutedEventArgs e)
        {
            rc.tryChangeAppointment(CurrentAppointment, (DateTime)newDate.SelectedDate, rc.RecommendedOne.StartTime.ToString().Split(' ')[1]);
            Visibility = Visibility.Collapsed;
            cmbUsername.Text = "";
            date.Text = "";
            txtTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            newDate.Text = "";
            txtNewTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            errNewDate.Text = "";
            errNewTime.Text = "";
            errDate.Text = "";
            btnRecOne.Visibility = Visibility.Collapsed;
            btnRecTwo.Visibility = Visibility.Collapsed;
        }

        private void btnShowRec_Click(object sender, RoutedEventArgs e)
        {
            if (isValidate())
            {
                btnShowRec.Visibility = Visibility.Collapsed;
                notFree.Text = "";
                if (((DateTime)txtNewTime.Value).Hour.Equals("6") && ((DateTime)txtNewTime.Value).Minute.Equals("30"))
                {
                    btnRecOne.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnRecOne.Visibility = Visibility.Visible;
                }

                if (((DateTime)txtNewTime.Value).Hour.Equals("7") && ((DateTime)txtNewTime.Value).Minute.Equals("00"))
                {
                    btnRecTwo.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnRecTwo.Visibility = Visibility.Visible;
                }

                ObservableCollection<Appointment> apointments = aac.GetFreeAppointmentsByDateAndDoctor((DateTime)newDate.SelectedDate, CurrentAppointment.DoctorUsername, cmbUsername.Text);
                rc.FindFreeForward(apointments, (DateTime)txtNewTime.Value);
                rc.FindFreeBack(apointments, (DateTime)txtNewTime.Value);
                recOne.Text = "Doctor: " + dc.GetByUsername(rc.RecommendedOne.DoctorUsername) + "\n" + rc.RecommendedOne.StartTime;
                recTwo.Text = "Doctor: " + dc.GetByUsername(rc.RecommendedTwo.DoctorUsername) + "\n" + rc.RecommendedTwo.StartTime;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
        public Appointment CurrentAppointment { get; set; }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("AJDE");
            if (isValidate())
            {
                bool sucess = false;
                Console.WriteLine("GROBARI");
                ObservableCollection<Appointment> appointments = ac.GetAppointmentByPatient(cmbUsername.Text);
                string dates = date.ToString().Split(' ')[0];
                bool exists = false;

                //da li je uopste uneta promena nekakva
                /* if (date.Text.Equals(newDate.Text) && (DateTime) txtNewTime.Value.Equals(txtTime.Value))
                 {
                     notFree.Text = "No change occured";
                 } */

                foreach (Appointment appointment in appointments)
                {
                    string hoursFromAppointment = appointment.StartTime.ToString().Split(' ')[1].Split(':')[0];
                    string minutsFromAppointment = appointment.StartTime.ToString().Split(' ')[1].Split(':')[1];
                    string dateFromAppointment = appointment.StartTime.ToString().Split(' ')[0];
                    Console.WriteLine(hoursFromAppointment + ":" + minutsFromAppointment + " / " + dateFromAppointment);
                    //pogresno si izvedao gde sta setujes kad otvaras prozor pa pravi problem
                    //Console.WriteLine(((DateTime)txtTime.Value).Hour + ":" + ((DateTime)txtTime.Value).Minute + " / " + dates);
                    if (hoursFromAppointment.Equals(((DateTime)txtTime.Value).Hour) && minutsFromAppointment.Equals(((DateTime)txtTime.Value).Minute) && dates.Equals(dateFromAppointment))
                    {
                        exists = true;
                        CurrentAppointment = appointment;
                        Console.WriteLine("USAO SAM ODJE");
                        List<ScheduledBasicRenovation> renovationList = sbrc.FindAll();
                        bool canMake = true;
                        foreach (ScheduledBasicRenovation renovation in renovationList)
                        {
                            if (renovation._Room.id == dc.GetByUsername(appointment.DoctorUsername).OrdinationId && renovation._Interval._Start <= (DateTime)newDate.SelectedDate && renovation._Interval._End >= (DateTime)newDate.SelectedDate)
                            {
                                notifier.ShowError("Invalid time because of renovations");
                                canMake = false;
                            }
                        }

                        if (canMake)
                        {
                            sucess = rc.tryChangeAppointment(appointment, (DateTime)newDate.SelectedDate, txtNewTime.Value.ToString().Split(' ')[1]);
                            if (sucess)
                            {
                                notifier.ShowSuccess("Appointment has been moved successfully.");
                                Visibility = Visibility.Collapsed;
                                return;
                            }
                            else
                            {
                                btnShowRec.Visibility = Visibility.Visible;
                                notFree.Text = "Appointment is not free";
                                break;
                            }
                        }
                    }
                }
                if (!exists)
                {
                    notFree.Text = "Appointment not exists";
                    return;
                }
            }
        }
    }
}
