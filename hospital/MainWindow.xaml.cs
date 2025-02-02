﻿using Controller;
using hospital.View;
using Model;
using System.Windows;

namespace hospital
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            UserController uc;
            App app = Application.Current as App;
            uc = app.userController;

            User u = uc.SendDate(txtUsername.Text, txtPassword.Password);
            if (u == null)
            {
                labIncorect.Text = "The username or password you've entered is incorrect.";
                txtPassword.Password = "";
            }
            else if (u.IsBlocked)
            {
                labIncorect.Text = "User is blocked.";
                txtPassword.Password = "";
            }
            else
            {
                labIncorect.Text = "";


                if (u.Role == Model.Role.Doctor)
                {
                    new DoctorHomeWindow().Show();
                    Close();
                }
                else if (u.Role == Model.Role.Secretary)
                {
                    new SecretaryHomeWindow().Show();
                    Close();
                }
                else if (u.Role == Model.Role.Manager)
                {
                    new ManagerMainWindow().Show();
                    Close();
                }
                else if (u.Role == Model.Role.Patient)
                {
                    new PatientHomeWindow().Show();
                    Close();
                }
            }

        }
    }
}
