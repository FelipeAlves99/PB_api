using PB.Domain.Commands.ClientCommands.Responses;
using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Entities;
using PB.Domain.Repositories;
using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.Domain.Handlers.PhoneHandlers
{
    public class PhoneCommandHandler : CommandHandler,
        ICommandHandler<CreatePhoneCommandRequest>
    {
        private readonly IHandler _handler;
        private readonly IDomainNotificationHandler<DomainNotification> _notificationHandler;
        private readonly IPhoneRepository _phoneRepository;

        public PhoneCommandHandler(IHandler handler,
                                   IPhoneRepository phoneRepository,
                                   IDomainNotificationHandler<DomainNotification> notificationHandler) : base(handler)
        {
            _handler = handler;
            _phoneRepository = phoneRepository;
            _notificationHandler = notificationHandler;
        }

        public async Task<ICommandResult> Handle(CreatePhoneCommandRequest request, CancellationToken cancellationToken)
        {
            var phone = new Phone(request.Ddd, request.PhoneNumber, request.PhoneType, request.ClientId);

            if (phone.IsValid() is false)
            {
                NotifyErrors(phone.ValidationResult);
                return await Task.FromResult(new CommandResult<ICollection<DomainNotification>>("Failed to register the client", false, _notificationHandler.GetNotifications()));
            }

            await _phoneRepository.Create(phone);
            return await Task.FromResult(new CommandResult<CreateClientCommandResponse>("Client regitered successfully", true, new CreateClientCommandResponse(phone.Id)));
        }
    }
}
