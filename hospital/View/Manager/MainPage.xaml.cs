﻿using Controller;
using hospital.View.Manager;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly Frame Main;
        private readonly ManagerMainWindow mainWindow;
        private readonly UserController uc;
        public MainPage()
        {
            InitializeComponent();
            foreach (Window win in Application.Current.Windows)
            {
                if (win is ManagerMainWindow)
                {
                    Main = ((ManagerMainWindow)win).Main;
                    mainWindow = (ManagerMainWindow)win;
                }
            }
            App app = Application.Current as App;
            uc = app.userController;
        }

        private void Room_Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerRoomsWindow().Show();
        }

        private void ManagerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Equipment_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new EquipmentPage();
        }

        private void Renovation_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new RoomRenovationPage();
        }



        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            mainWindow.Close();
        }

        private void Medication_Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MedicinePage();
        }

        private void Review_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new ReviewPage();
        }
    }


}
