using Model;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class NotificationController
    {
        private readonly NotificationService notificationService;

        public NotificationController(NotificationService _service) { notificationService = _service; }

        public void Create(Notification notification)
        {
            notificationService.Create(notification);
        }

        public ObservableCollection<Notification> FindAll()
        {
            return notificationService.FindAll();
        }
        public bool Delete(Notification n)
        {
            return notificationService.Delete(n);
        }
    }
}
