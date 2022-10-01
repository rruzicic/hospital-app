using hospital.VM;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for AddRoomWindow.xaml
    /// </summary>
    public partial class AddRoomWindow : Window
    {
        public AddRoomWindow()
        {
            DataContext = new AddRoomWindowViewModel();
            InitializeComponent();
        }
    }
}
