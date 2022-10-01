using Controller;
using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class SelectMultipleFromListViewCommand : IDemoCommand
    {
        private readonly ListView roomListView;
        private readonly RoomController roomController;

        public SelectMultipleFromListViewCommand(ListView roomListView, RoomController roomController)
        {
            this.roomListView = roomListView;
            this.roomController = roomController;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                roomListView.SelectedItems.Add(roomController.FindAll()[10]);
                roomListView.SelectedItems.Add(roomController.FindAll()[11]);
            });
        }
    }
}
