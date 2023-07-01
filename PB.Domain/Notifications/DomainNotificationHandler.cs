using PB.Domain.Shared.Notifications;

namespace PB.Domain.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;

        public DomainNotificationHandler(Notifications notifications)
        {
            _notifications = notifications.NotificationItems;
        }

        public ICollection<DomainNotification> GetNotifications()
            => _notifications.OrderBy(n => n.Key).ToList();

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }
    }
}
