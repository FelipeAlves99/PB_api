using MediatR;
using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Notifications;

namespace PB.Domain.Shared.Handlers
{
    public class Handler : IHandler
    {
        private readonly IMediator _mediator;

        public Handler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task ThrowNotification<T>(T notification) where T : DomainNotification
            => _mediator.Publish(notification);

        public Task<ICommandResult> SendCommand<T>(T command) where T : Command<T>
        {
            var result = _mediator.Send(command);
            return result;
        }
    }
}