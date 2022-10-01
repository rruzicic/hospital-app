using hospital.ViewModel;
using System.Windows.Controls;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for MedicinePage.xaml
    /// </summary>
    public partial class MedicinePage : Page
    {
        public MedicinePage()
        {
            DataContext = new MedicinePageViewModel();
            InitializeComponent();
        }
    }
}
