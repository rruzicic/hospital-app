﻿using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for ManagerRoomsWindow.xaml
    /// </summary>
    public partial class ManagerRoomsWindow : Window
    {

        //public ObservableCollection<Room> rooms { get; set; }
        private RoomController roomController;

        public ManagerRoomsWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            roomController = app.roomController;
            dataGridRooms.ItemsSource =  roomController.FindAll();
        }

        private void Add_Room_Click(object sender, RoutedEventArgs e)
        {
            new AddRoomWindow().Show();
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            int selectedRow = dataGridRooms.SelectedIndex;
            if (e.Key == Key.Enter && selectedRow != -1)
            {
                Room room = (Room)dataGridRooms.SelectedItem;
                EditRoomWindow editWindow = new EditRoomWindow(room);
                
                editWindow.roomName.Text = room.name;
                editWindow.roomId.Text = room.id;
                editWindow.roomPurpose.Text = room.purpose;
                editWindow.roomFloor.Text = room.floor.ToString();
                foreach (Equipment eq in room.equipment)
                {
                    editWindow.equipmentListView.Items.Add(eq.type + " " + eq.quantity);
                }
                editWindow.Show();
            }
            else if (e.Key == Key.Delete && selectedRow != -1)
            {
                new DeleteRoomWindow().Show();
            }
        }
    }
}
