using hospital.VM;
using Model;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    /// 
    public partial class EditRoomWindow : Window
    {
        public EditRoomWindow(Room room)
        {
            DataContext = new EditRoomWindowViewModel(room);
            InitializeComponent();
        }
    }
}
