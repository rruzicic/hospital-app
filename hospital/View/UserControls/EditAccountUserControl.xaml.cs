﻿using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for EditAccountUserControl.xaml
    /// </summary>
    public partial class EditAccountUserControl : UserControl
    {
        public PatientController pc;
        public UserController uc;
        private readonly bool[] isCorrected = new bool[8];
        public ObservableCollection<Model.Role> Roles { get; set; }
        public EditAccountUserControl()
        {
            InitializeComponent();
            DataContext = this;
            App app = Application.Current as App;
            pc = app.patientController;
            uc = app.userController;
            resetCorected();
            Roles = new ObservableCollection<Model.Role>(Enum.GetValues(typeof(Model.Role)).Cast<Model.Role>().ToList());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }
        private string getGender()
        {
            if (radioMale.IsChecked == true)
            {
                return "Male";
            }
            else
            {
                return "Female";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isCorrected[0] & isCorrected[1] & isCorrected[2] & isCorrected[3] & isCorrected[4] & isCorrected[5] & isCorrected[6] & isCorrected[7])
                {
                    pc.UpdateByUsername(pc.EditPatient.Username, new Patient(txtUsername.Text, txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtId.Text, txtPhone.Text, txtDate.Text, getGender(), isBlocked()));
                    uc.UpdateByUsername(pc.EditPatient.Username, new User(txtUsername.Text, getRole(cmbRole.Text), isBlocked()));
                    Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Input first name"))
                {
                    errFristname.Text = ex.Message;
                }

                if (ex.Message.Equals("Input surname"))
                {
                    errLastname.Text = ex.Message;
                }
                if (ex.Message.Equals("Username already exists !"))
                {
                    errUsername.Text = ex.Message;
                }
            }
        }


        public void resetCorected()
        {
            for (int i = 0; i < 8; i++)
            {
                isCorrected[i] = true;
            }
        }

        private bool isBlocked()
        {
            if (cbBlocked.IsChecked == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private Model.Role getRole(string txt)
        {
            switch (txt)
            {
                case "Patient":
                    return Model.Role.Patient;
                    break;
                case "Doctor":
                    return Model.Role.Doctor;
                    break;
                case "Secretary":
                    return Model.Role.Secretary;
                    break;
                default:
                    return Model.Role.Manager;
                    break;
            }
        }
        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtFirstName.Text.Any(char.IsDigit))
            {
                errFristname.Text = "Mustn't be number";
                isCorrected[0] = false;
            }
            else if (txtFirstName.Text.Trim().Equals(""))
            {
                errFristname.Text = "Must be filled";
                isCorrected[0] = false;
            }
            else
            {
                errFristname.Text = "";
                isCorrected[0] = true;
            }
        }

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLastName.Text.Any(char.IsDigit))
            {
                errLastname.Text = "Mustn't be number";
                isCorrected[1] = false;
            }
            else if (txtLastName.Text.Trim().Equals(""))
            {
                errLastname.Text = "Must be filled";
                isCorrected[1] = false;
            }
            else
            {
                errLastname.Text = "";
                isCorrected[1] = true;
            }
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPhone.Text.Trim().Equals(""))
            {
                errPhone.Text = "Must be filled";
                isCorrected[2] = false;
            }
            else
            {
                errPhone.Text = "";
                isCorrected[2] = true;
            }
        }

        private void txtId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtId.Text.Trim().Equals(""))
            {
                errId.Text = "Must be filled";
                isCorrected[3] = false;
            }
            else
            {
                errId.Text = "";
                isCorrected[3] = true;
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtEmail.Text.Contains("@"))
            {
                errEmail.Text = "Must have @";
                isCorrected[4] = false;
            }
            else if (txtEmail.Text.Trim().Equals(""))
            {
                errEmail.Text = "Must be filled";
                isCorrected[4] = false;
            }
            else
            {
                errEmail.Text = "";
                isCorrected[4] = true;
            }
        }

        private void txtUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUsername.Text.Trim().Equals(""))
            {
                errUsername.Text = "Must be filled";
                isCorrected[5] = false;
            }
            else
            {
                errUsername.Text = "";
                isCorrected[5] = true;
            }
        }

        private void txtDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtDate.Text.Trim().Equals(""))
            {
                errDate.Text = "Must be filled";
                isCorrected[7] = false;
            }
            else
            {
                errDate.Text = "";
                isCorrected[7] = true;
            }

        }
    }
}
