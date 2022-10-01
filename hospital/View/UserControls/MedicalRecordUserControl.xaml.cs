using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View.UserControls
{
    /// <summary>
    /// Interaction logic for MedicalRecordUserControl.xaml
    /// </summary>
    public partial class MedicalRecordUserControl : UserControl
    {
        public MedicalRecordUserControl()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPhone.Clear();
            txtId.Clear();
            txtDoctor.Clear();
            txtRecordId.Clear();
            txtDate.Clear();
            txtBlood.Clear();
            txtNote.Clear();
            listAllergens.ItemsSource = new List<string>();

        }
    }
}
