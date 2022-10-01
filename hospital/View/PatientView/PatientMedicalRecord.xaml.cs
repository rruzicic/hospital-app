using Controller;
using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecord.xaml
    /// </summary>
    public partial class PatientMedicalRecord : Page
    {
        private readonly App app;
        private readonly PatientController pc;
        private readonly UserController uc;
        private readonly MedicalRecordsController mrc;

        public PatientMedicalRecord()
        {
            InitializeComponent();
            DataContext = new MedicalRecordViewModel();
            /*app = Application.Current as App;
            pc = app.patientController;
            uc = app.userController;
            mrc = app.medicalRecordsController;

            Patient currentPatient = pc.FindById(uc.CurentLoggedUser.Username);
            tbName.Text = currentPatient.FirstName + " " + currentPatient.LastName;
            tbGender.Text = currentPatient.Gender;
            tbDateOfBirth.Text = currentPatient.DateOfBirth.ToString();
            tbAlergens.Text = mrc.FindById(currentPatient.RecordId).Alergies;
            tbNote.Text = mrc.FindById(currentPatient.RecordId).Note;*/
        }

    }
}
