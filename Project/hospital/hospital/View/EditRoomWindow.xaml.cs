﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for EditRoomWindow.xaml
    /// </summary>
    public partial class EditRoomWindow : Window, INotifyPropertyChanged
    {

        private readonly ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        public Room selectedRoom;
   

      
        public EditRoomWindow() 
        {
            InitializeComponent();
            

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ReadOnlyObservableCollection<Room> Rooms { get { return new ReadOnlyObservableCollection<Room>(this.rooms); } }

        public Room SelectedRoom {
            get { return this.selectedRoom; }
            set {
                this.selectedRoom = value;
                this.OnPropertyChanged("SelectedItem");
            }
        }


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
