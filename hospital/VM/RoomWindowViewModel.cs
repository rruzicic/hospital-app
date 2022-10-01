using Controller;
using hospital.View;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace hospital.VM
{
    public class RoomWindowViewModel : INotifyPropertyChanged
    {
        public ICommand EditCommand => new EditCommand();
        public ICommand AddCommand => new AddCommand();

        public ICommand DeleteCommand => new DeleteCommand();
        public ICommand SearchCommand => new SearchCommand(this);
        public ObservableCollection<Room> rooms;
        private readonly RoomController roomController;

        private string quantitySearch = "";
        private string typeSearch = "";

        public string QuantitySearch
        {
            get => quantitySearch;
            set
            {
                quantitySearch = value;
                //Filter();
                OnPropertyChanged("QuantitySearch");
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {

                    rooms = value;

                }
            }
        }

        public string TypeSearch
        {
            get => typeSearch;
            set
            {
                typeSearch = value;
                //Filter();
                OnPropertyChanged("TypeSearch");
            }
        }

        public RoomWindowViewModel()
        {
            App app = Application.Current as App;
            roomController = app.roomController;
            Rooms = roomController.FindAll();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void Filter()
        {
            Console.WriteLine(quantitySearch + " " + typeSearch);
            if (quantitySearch.Equals("") && !typeSearch.Equals(""))
            {
                Rooms = roomController.FindRoomsByEquipmentType(typeSearch);
            }
            else if (!quantitySearch.Equals("") && typeSearch.Equals(""))
            {
                Rooms = roomController.FindRoomsByEquipmentQuantity(int.Parse(quantitySearch));
            }
            else if (!quantitySearch.Equals("") && !typeSearch.Equals(""))
            {
                Rooms = roomController.FindRoomsByEquipmentTypeAndQuantity(typeSearch, int.Parse(quantitySearch));
            }
            else
            {
                Rooms = roomController.FindAll();
            }
            OnPropertyChanged("Rooms");
        }

    }
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Room selected = parameter as Room;
            if (selected != null)
            {
                EditRoomWindow editWindow = new EditRoomWindow(selected);
                editWindow.Show();
            }
        }
    }

    public class AddCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            new AddRoomWindow().Show();
        }
    }

    public class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Room selected = parameter as Room;
            if (selected != null)
            {
                DeleteRoomWindow deleteWindow = new DeleteRoomWindow(selected);
                deleteWindow.Show();
            }
        }
    }

    public class SearchCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly RoomWindowViewModel viewModel;

        public SearchCommand(RoomWindowViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Filter();
        }
    }
}
