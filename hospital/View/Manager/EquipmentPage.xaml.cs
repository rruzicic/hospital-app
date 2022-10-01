using Controller;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EquipmentPage.xaml
    /// </summary>
    public partial class EquipmentPage : Page
    {
        private readonly UserController uc;
        public EquipmentPage()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
            FillDataGrid();
        }

        private void Show_Relocation_Window_Click(object sender, RoutedEventArgs e)
        {
            new EquipmentRelocationWindow().Show();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
        }

        private void FillDataGrid()
        {
            List<DamiEquipment> equipment = new List<DamiEquipment>
            {
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50),
                new DamiEquipment("DentCo", "Dentist chair", 50)
            };
            equipmentDataGrid.ItemsSource = equipment;
        }
    }

    public class DamiEquipment
    {
        public string manufacturer { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }

        public DamiEquipment(string name, string type, int quantity)
        {
            manufacturer = name;
            this.type = type;
            this.quantity = quantity;
        }
    }
}
