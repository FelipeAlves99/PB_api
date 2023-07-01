using PB.Domain.Shared.Notifications;

namespace PB.Domain.Notifications
{
    public class Notifications
    {
        public Notifications()
        {
            NotificationItems = new List<DomainNotification>();
        }

        public List<DomainNotification> NotificationItems { get; set; }
    }
}