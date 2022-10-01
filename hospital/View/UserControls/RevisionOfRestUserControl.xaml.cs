using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace hospital.View.UserControls
{
    public partial class RevisionOfRestUserControl : UserControl
    {
        private readonly VacationRequestController _vacationRequestController;
        private readonly DoctorController _doctorController;
        private ObservableCollection<VacationRequest> _vacationRequests;
        private VacationRequest _currentSelected;
        public RevisionOfRestUserControl()
        {
            DataContext = this;
            App app = Application.Current as App;
            _vacationRequestController = app.vacationRequestController;
            _doctorController = app.doctorController;
            _vacationRequests = _vacationRequestController.FindAll();
            InitializeComponent();
        }
        public ObservableCollection<VacationRequest> VacationRequests
        {
            get => _vacationRequests;
            set => _vacationRequests = value;
        }
        private void dateGridHandlingRest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _currentSelected = (VacationRequest)dateGridHandlingRest.SelectedItem;
            if (_currentSelected != null)
            {
                if (_currentSelected.Status == Status.approved || _currentSelected.Status == Status.rejected)
                {
                    btnApprove.IsEnabled = false;
                    btnReject.IsEnabled = false;
                }
                else
                {
                    btnApprove.IsEnabled = true;
                    btnReject.IsEnabled = true;
                }
                txtDate.Text = _currentSelected.StartDate.ToString().Split(' ')[0] + "-" + _currentSelected.EndDate.ToString().Split(' ')[0];
                txtMotive.Text = _currentSelected.Note;
                header.Header = _doctorController.GetByUsername(_currentSelected.DoctorId);
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            _vacationRequestController.FinishRequest("approve", _currentSelected.Id);
            dateGridHandlingRest.Items.Refresh();
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            _vacationRequestController.FinishRequest("reject", _currentSelected.Id);
            dateGridHandlingRest.Items.Refresh();
        }
        private void ResetFields()
        {
            txtDate.Text = "";
            txtMotive.Text = "";
            txtReason.Text = "";
        }
    }
}
