using Controller;
using Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for AddGuestAccUserControl.xaml
    /// </summary>
    public partial class AddGuestAccUserControl : UserControl
    {
        public PatientController pc;
        public AddGuestAccUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pc = app.patientController;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
        }


        private bool isValidate()
        {
            bool[] isCorrected = new bool[3];

            for (int i = 0; i < 2; i++)
            {
                isCorrected[i] = true;
            }
            if (txtFirstname.Text.Trim().Equals(""))
            {
                errFirstname.Text = "Input first name";
                isCorrected[0] = false;
            }
            else if (txtFirstname.Text.Any(char.IsDigit))
            {
                errFirstname.Text = "number isn't allowed";
                isCorrected[0] = false;
            }
            else
            {
                errFirstname.Text = "";
                isCorrected[0] = true;
            }

            //Surname validation
            if (txtLastname.Text.Trim().Equals(""))
            {
                errLastname.Text = "Input surname";
                isCorrected[1] = false;
            }
            else if (txtLastname.Text.Any(char.IsDigit))
            {
                errLastname.Text = "number isn't allowed";
                isCorrected[1] = false;
            }
            else
            {
                errLastname.Text = "";
                isCorrected[1] = true;
            }
            //usernam validate
            if (txtUsername.Text.Trim().Equals(""))
            {
                errUsername.Text = "Input username";
                isCorrected[2] = false;
            }
            else
            {
                errUsername.Text = "";
                isCorrected[2] = true;
            }
            return (isCorrected[0] && isCorrected[1] && isCorrected[2]);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValidate())
                {
                    pc.Create(new Patient(txtFirstname.Text, txtLastname.Text, txtUsername.Text));
                    Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Username already exists !"))
                {
                    txtUsername.BorderBrush = Brushes.Red;
                    errUsername.Text = ex.Message;
                }
            }
        }
    }
}
