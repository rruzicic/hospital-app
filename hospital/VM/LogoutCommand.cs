using Controller;
using hospital.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace hospital.VM
{
    public class LogoutCommand : ICommand
    {
        private Window Main;
        private readonly UserController userController;

        public LogoutCommand()
        {
            RetrieveMainFrame();
            App app = Application.Current as App;
            userController = app.userController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            userController.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            mw.Show();
            Main.Close();
        }

        public event EventHandler CanExecuteChanged;

        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    Main = (ManagerMainWindow)win;
                }
            }
        }
    }
}
