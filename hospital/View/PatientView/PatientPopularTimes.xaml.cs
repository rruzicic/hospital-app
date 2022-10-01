using System.Windows.Controls;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientPopularTimes.xaml
    /// </summary>
    public partial class PatientPopularTimes : Page
    {
        public PatientPopularTimes()
        {
            InitializeComponent();
            DataContext = new PopularTimesViewModel();
        }
    }
}
