using Controller;
using hospital.View.UserControls;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorAppointmentsWindow.xaml
    /// </summary>
    public partial class DoctorAppointmentsWindow : Window
    {
        public ObservableCollection<Appointment> Appointments { get; set; }
        private readonly AppointmentManagementController ac;
        private readonly UserController uc;
        private readonly DoctorController dc;

        private readonly Doctor loggedInDoctor;
        public DoctorAppointmentsWindow()
        {
            InitializeComponent();
            DataContext = this;
            App app = Application.Current as App;
            ac = app.appointmentController;
            uc = app.userController;
            dc = app.doctorController;
            //Appointments = ac.GetAppointmentByDoctor(uc.CurentLoggedUser.Username);
            Appointments = ac.GetAppointments();
            loggedInDoctor = dc.GetByUsername(uc.CurentLoggedUser.Username);

            ICollectionView appointmentsView = CollectionViewSource.GetDefaultView(ac.GetAppointments());
            appointmentsView.Filter = DoctorFilter;
        }

        private bool DoctorFilter(object item)
        {
            Appointment appointment = item as Appointment;
            return appointment.DoctorUsername.Equals(loggedInDoctor.Username);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {
                new DoctorEditAppointment().Show();
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMakeNewAppointment().Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {
                Appointment toDelete = (Appointment)Table.SelectedItem;
                ac.DeleteAppointment(toDelete.Id);
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex != -1)
            {
                new DoctorViewInfoWindow().Show();
            }
        }

        private void btnOtherDoctor_Click(object sender, RoutedEventArgs e)
        {
            new DoctorMakeNewAppointmentForOtherDoctors().Show();
        }
    }
}
