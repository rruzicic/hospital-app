using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class MeetingController
    {
        private readonly MeetingService _meetingService;
        private readonly NotificationService _notificationService;
        public MeetingController(MeetingService _mservice, NotificationService _nservice) { _meetingService = _mservice; _notificationService = _nservice; }
        public void Create(Meeting meeting)
        {
            _meetingService.Create(meeting);
            MakeNotification(meeting);
        }
        private void MakeNotification(Meeting meeting)
        {
            foreach (string tmp in meeting.Doctors)
            {
                _notificationService.Create(new Notification(tmp, "New meeting is scheduled."));
            }
        }

        public ObservableCollection<Meeting> FindAll()
        {
            return _meetingService.FindAll();
        }

        public Meeting FindById(int id)
        {
            return _meetingService.FindById(id);
        }

        public bool DeleteById(int id)
        {
            return _meetingService.DeleteById(id);
        }
    }
}
