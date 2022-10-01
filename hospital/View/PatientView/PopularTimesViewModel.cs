using hospital.DTO;
using System.Collections.Generic;
using System.Windows;

namespace hospital.View.PatientView
{

    public class PopularTimesViewModel
    {
        public List<ChartDataDTO> Data { get; set; }

        public PopularTimesViewModel()
        {
            App app = Application.Current as App;
            Data = app.appointmentController.GetPopularTimes();
        }
    }
}
