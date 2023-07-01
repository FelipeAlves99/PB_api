using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Notifications;

namespace PB.Domain.Shared.Handlers
{
    public interface IHandler
    {
        /// <summary>
        /// Handle a Notification
        /// </summary>
        /// <typeparam name="T">DomainNotification Object</typeparam>
        /// <param name="notification">DomainNotifications to be read</param>
        /// <returns></returns>
        Task ThrowNotification<T>(T notification) where T : DomainNotification;

        /// <summary>
        /// Handle a command
        /// </summary>
        /// <typeparam name="T">Command<T> Object</typeparam>
        /// <param name="command">Command to be iterated</param>
        /// <returns></returns>
        Task<ICommandResult> SendCommand<T>(T command) where T : Command<T>;
    }
}
