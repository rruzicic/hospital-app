using Model;
using Repository;
using System.Collections.ObjectModel;

namespace Service
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;

        public NotificationService(NotificationRepository _repo)
        {
            _notificationRepository = _repo;
        }

        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public ObservableCollection<Notification> FindAll()
        {
            return _notificationRepository.FindAll();
        }

        public bool Delete(Notification n)
        {
            return _notificationRepository.Delete(n);
        }
    }
}
