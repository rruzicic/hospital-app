using hospital.VM;
using Model;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DeleteRoomWindow.xaml
    /// </summary>
    public partial class DeleteRoomWindow : Window
    {
        public DeleteRoomWindow(Room room)
        {
            DataContext = new DeleteRoomWindowViewModel(room);
            InitializeComponent();
        }

    }
}
