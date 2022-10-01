using hospital.VM;
using System.Windows.Controls;

namespace hospital.View.Manager
{
    /// <summary>
    /// Interaction logic for ReviewPage.xaml
    /// </summary>
    public partial class ReviewPage : Page
    {
        public ReviewPage()
        {
            DataContext = new ReviewPageViewModel();
            InitializeComponent();
        }

    }
}
