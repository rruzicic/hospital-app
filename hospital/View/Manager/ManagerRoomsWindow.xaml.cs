using hospital.VM;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerRoomsWindow.xaml
    /// </summary>
    public partial class ManagerRoomsWindow : Window
    {

        public ManagerRoomsWindow()
        {
            DataContext = new RoomWindowViewModel();
            InitializeComponent();
        }
    }
}
