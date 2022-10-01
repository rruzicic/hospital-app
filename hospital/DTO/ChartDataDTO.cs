using System;

namespace hospital.DTO
{
    public class ChartDataDTO
    {
        public DateTime Time { get; set; }
        public int NumberOfAppointments { get; set; }

        public ChartDataDTO(DateTime time)
        {
            Time = time;
            NumberOfAppointments = 1;
        }
    }
}
