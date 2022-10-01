using Controller;
using Model;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorViewInfoWindow.xaml
    /// </summary>
    public partial class DoctorViewInfoWindow : Window
    {
        private readonly PatientController pc;
        private readonly MedicalRecordsController mrc;
        private readonly Patient selectedPatient;
        private readonly Appointment selectedAppointment;
        private readonly MedicalRecord selectedMedicalRecord;
        public DoctorViewInfoWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            pc = app.patientController;
            mrc = app.medicalRecordsController;
            DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorAppointmentsWindow))
                {
                    selectedAppointment = (window as DoctorAppointmentsWindow).Table.SelectedItem as Appointment;
                    selectedPatient = pc.FindById(selectedAppointment.PatientUsername);
                    selectedMedicalRecord = mrc.FindById(selectedPatient.RecordId);
                    lbFirstName.Content = selectedPatient.FirstName;
                    lbLastName.Content = selectedPatient.LastName;
                    lbUsername.Content = selectedPatient.Username;
                    lbDateOfBirth.Content = selectedPatient.DateOfBirth;
                    lbPhoneNumber.Content = selectedPatient.PhoneNumber;
                    tbAlergies.Text = selectedMedicalRecord.Alergies;
                    tbNotes.Text = selectedMedicalRecord.Note;
                    lbBloodType.Content = selectedMedicalRecord.BloodType;
                }
            }
        }
    }
}
