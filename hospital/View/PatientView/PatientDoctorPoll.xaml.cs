﻿using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDoctorPoll.xaml
    /// </summary>
    public partial class PatientDoctorPoll : Page
    {
        private readonly App app;
        private readonly AppointmentManagementController ac;
        private readonly PollController pbc;
        private readonly UserController uc;
        public List<PollQuestion> Poll { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public PatientDoctorPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            pbc = app.pollBlueprintController;
            uc = app.userController;
            ac = app.appointmentController;
            Appointments = ac.GetPastAppointmentsByPatient(uc.CurentLoggedUser.Username);
            DataContext = this;
            Poll = pbc.GetDoctorPollQuestions();
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidated())
            {
                pbc.SavePoll(FillPoll());
            }
        }

        private bool IsValidated()
        {
            if (CheckIfFilled())
            {
                if (cbAppointments.SelectedItem != null)
                {
                    Appointment appointment = (Appointment)cbAppointments.SelectedItem;
                    if (!pbc.AppointmentPollAlreadyFilled(appointment.Id))
                    {
                        return true;
                    }
                    else
                    {
                        lbWarning.Content = "You have already answered the poll for this appointment!";
                        return false;
                    }
                }
                else
                {
                    lbWarning.Content = "Please choose an appointment!";
                    return false;
                }
            }
            else
            {
                lbWarning.Content = "Please answer all of the questions!";
                return false;
            }
        }

        private PollBlueprint FillPoll()
        {
            PollBlueprint poll = pbc.GetDoctorPollBlueprint();
            foreach (object listIterator in lbPoll.Items)
            {
                PollQuestion question = (PollQuestion)listIterator;
                ListBoxItem item = (ListBoxItem)lbPoll.ItemContainerGenerator.ContainerFromItem(listIterator);
                ContentPresenter presenter = FindVisualChild<ContentPresenter>(item);
                DataTemplate dataTemplate = presenter.ContentTemplate;
                if (dataTemplate != null)
                {
                    int grade;
                    bool oneChecked = (bool)((RadioButton)dataTemplate.FindName("rbOne", presenter)).IsChecked;
                    bool twoChecked = (bool)((RadioButton)dataTemplate.FindName("rbTwo", presenter)).IsChecked;
                    bool threeChecked = (bool)((RadioButton)dataTemplate.FindName("rbThree", presenter)).IsChecked;
                    bool fourChecked = (bool)((RadioButton)dataTemplate.FindName("rbFour", presenter)).IsChecked;
                    bool fiveChecked = (bool)((RadioButton)dataTemplate.FindName("rbFive", presenter)).IsChecked;

                    if (oneChecked)
                    {
                        grade = 1;
                    }
                    else if (twoChecked)
                    {
                        grade = 2;
                    }
                    else if (threeChecked)
                    {
                        grade = 3;
                    }
                    else if (fourChecked)
                    {
                        grade = 4;
                    }
                    else
                    {
                        grade = 5;
                    }

                    foreach (PollCategory category in poll.Categories)
                    {
                        foreach (PollQuestion pollQuestion in category.PollQuestions)
                        {
                            if (pollQuestion.Id == question.Id)
                            {
                                pollQuestion.Grade = grade;
                            }
                        }
                    }
                }
            }
            Appointment appointment = (Appointment)cbAppointments.SelectedItem;
            poll.Username = uc.CurentLoggedUser.Username;
            poll.AppointmentId = appointment.Id;
            return poll;
        }
        private bool CheckIfFilled()
        {
            foreach (object listIterator in lbPoll.Items)
            {
                ListBoxItem item = (ListBoxItem)lbPoll.ItemContainerGenerator.ContainerFromItem(listIterator);
                ContentPresenter presenter = FindVisualChild<ContentPresenter>(item);
                DataTemplate dataTemplate = presenter.ContentTemplate;
                if (dataTemplate != null)
                {
                    bool oneChecked = (bool)((RadioButton)dataTemplate.FindName("rbOne", presenter)).IsChecked;
                    bool twoChecked = (bool)((RadioButton)dataTemplate.FindName("rbTwo", presenter)).IsChecked;
                    bool threeChecked = (bool)((RadioButton)dataTemplate.FindName("rbThree", presenter)).IsChecked;
                    bool fourChecked = (bool)((RadioButton)dataTemplate.FindName("rbFour", presenter)).IsChecked;
                    bool fiveChecked = (bool)((RadioButton)dataTemplate.FindName("rbFive", presenter)).IsChecked;
                    if (!oneChecked && !twoChecked && !threeChecked && !fourChecked && !fiveChecked)
                    {
                        return false;
                    }

                }
            }
            return true;
        }
    }
}
