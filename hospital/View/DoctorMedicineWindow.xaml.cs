using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorMedicineWindow.xaml
    /// </summary>
    public partial class DoctorMedicineWindow : Window
    {
        public ObservableCollection<Medicine> medicineList { get; set; }
        private readonly MedicineController mc;
        private readonly InvalidMedicineReportController imrc;
        public DoctorMedicineWindow()
        {
            InitializeComponent();
            DataContext = this;
            App app = Application.Current as App;
            mc = app.medicineController;
            imrc = app.invalidMedicineReportController;
            medicineList = mc.FindAll();
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if (medicineTable.SelectedIndex != -1 && tbNote.Text != "")
            {
                Medicine selectedMedicine = (Medicine)medicineTable.SelectedItem;
                InvalidMedicineReport newReport = new InvalidMedicineReport(selectedMedicine.Id, tbNote.Text, -1);
                imrc.Create(newReport);
                MessageBox.Show("Report sent!");
            }
        }
    }
}
