using System.Timers;

namespace hospital.Service
{
    public class SystemTimer
    {
        public static Timer timer;
        private readonly ScheduledAdvancedRenovationService scheduledAdvancedRenovationService;
        private readonly ScheduledBasicRenovationService scheduledBasicRenovationService;
        private readonly ScheduledRelocationService scheduledRelocationService;

        public SystemTimer(ScheduledAdvancedRenovationService scheduledAdvancedRenovationService, ScheduledBasicRenovationService scheduledBasicRenovationService,
            ScheduledRelocationService scheduledRelocationService)
        {
            this.scheduledAdvancedRenovationService = scheduledAdvancedRenovationService;
            this.scheduledBasicRenovationService = scheduledBasicRenovationService;
            this.scheduledRelocationService = scheduledRelocationService;
            SetTimer();
        }


        private void SetTimer()
        {
            timer = new Timer(3000);
            timer.Elapsed += FireScheduledTask;
            timer.AutoReset = true;
            timer.Enabled = true;
        }


        private void FireScheduledTask(object source, ElapsedEventArgs e)
        {
            scheduledAdvancedRenovationService.RenovationTracker();
            scheduledBasicRenovationService.RenovationTracker();
            scheduledRelocationService.RelocationTracker();
        }
    }

}
