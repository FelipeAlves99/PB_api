using MediatR;
using PB.Domain.Shared.Commands;

namespace PB.Domain.Shared.Notifications
{
    public interface IDomainNotification : INotification, ICommandResultData
    { }
}