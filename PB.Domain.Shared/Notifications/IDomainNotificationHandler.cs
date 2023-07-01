using MediatR;

namespace PB.Domain.Shared.Notifications
{
    public interface IDomainNotificationHandler<T> : INotificationHandler<T>
        where T : DomainNotification
    {
        ICollection<DomainNotification> GetNotifications();

        bool HasNotifications() { return GetNotifications().Any(); }

        void ClearNotifications() { GetNotifications().Clear(); }
    }
}