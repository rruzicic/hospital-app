using System.Windows.Controls;

namespace hospital.View.Manager
{
    public class SelectFromListBoxCommand : IDemoCommand
    {
        private readonly ListView roomRenovationListView;
        private readonly int index;
        public SelectFromListBoxCommand(ListView roomRenovationListView, int index)
        {
            this.roomRenovationListView = roomRenovationListView;
            this.index = index;
        }
        public void execute()
        {
            App.Current.Dispatcher.Invoke(delegate
            {
                roomRenovationListView.SelectedIndex = index;
            });
        }
    }
}
