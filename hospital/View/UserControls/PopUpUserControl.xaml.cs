using Controller;
using System.Windows;
using System.Windows.Controls;
namespace hospital.View.UserControls
{
    public partial class PopUpUserControl : UserControl
    {
        private readonly UserController uc;
        public PopUpUserControl()
        {
            InitializeComponent();
            App app = Application.Current as App;
            uc = app.userController;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            uc.CurentLoggedUser = null;
            MainWindow mw = new MainWindow();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            mw.Show();
        }
    }
}
