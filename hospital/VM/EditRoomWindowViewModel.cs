﻿using Controller;
using hospital.View;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace hospital.VM
{
    public class EditRoomWindowViewModel : INotifyPropertyChanged
    {
        public ICommand EditRoomCommand => new EditRoomCommand(this);
        public ICommand CancelEditRoomCommand => new CancelEditRoomCommand();
        private string name;
        private string id;
        private string purpose;
        private string floor;
        private int floorIndex;
        public List<Equipment> Equipment { get; set; }
        public int FloorIndex
        {
            get => floorIndex;
            set
            {
                Console.WriteLine("Hit");
                floorIndex = value;
                Floor = (floorIndex + 1).ToString();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Purpose
        {
            get => purpose;
            set
            {
                purpose = value;
                OnPropertyChanged("Purpose");
            }
        }

        public string Floor
        {
            get => floor;
            set
            {
                Console.WriteLine("stigao" + value);
                floor = value;
                OnPropertyChanged("Floor");
            }
        }

        private readonly RoomController roomController;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public EditRoomWindowViewModel(Room room)
        {
            Equipment = new List<Equipment>();
            App app = Application.Current as App;
            roomController = app.roomController;
            Name = room._Name;
            Id = room.id;
            Purpose = room._Purpose;
            Floor = room.floor.ToString();
            FloorIndex = room.floor - 1;
            Equipment = room.equipment;
        }

        public void EditRoom()
        {
            try
            {
                if (!int.TryParse(floor, out int res))
                {
                    MessageBox.Show("Not valid input for floor", "Error");
                    return;
                }

                Room newRoom = new Room(name, purpose, res, id);
                roomController.UpdateById(id, newRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class EditRoomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly EditRoomWindowViewModel viewModel;

        public EditRoomCommand(EditRoomWindowViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.EditRoom();
            new CancelEditRoomCommand().Execute("");
        }
    }

    public class CancelEditRoomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private EditRoomWindow window;

        public CancelEditRoomCommand()
        {
            RetrieveEditRoomWindow();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            window.Close();
        }

        private void RetrieveEditRoomWindow()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(EditRoomWindow))
                {
                    window = ((EditRoomWindow)win);
                }
            }
        }
    }
}
