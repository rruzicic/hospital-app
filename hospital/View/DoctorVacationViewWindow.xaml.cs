using Controller;
using Model;
using System.Windows;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorVacationViewWindow.xaml
    /// </summary>
    public partial class DoctorVacationViewWindow : Window
    {
        private readonly VacationRequestController vc;

        private readonly VacationRequest selectedVacation;
        public DoctorVacationViewWindow()
        {
            InitializeComponent();
            App app = Application.Current as App;
            vc = app.vacationRequestController;
            DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorVacationWindow))
                {
                    selectedVacation = (window as DoctorVacationWindow).tableRequests.SelectedItem as VacationRequest;
                    lbStatus.Content = selectedVacation.Status;
                    tbNote.Text = selectedVacation.Note;
                }
            }

        }
    }
}
