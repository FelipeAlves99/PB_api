using PB.Domain.Commands.ClientCommands.Requests;
using PB.Domain.Commands.ClientCommands.Responses;
using PB.Domain.Entities;
using PB.Domain.Repositories;
using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.Domain.Handlers.ClientHandlers
{
    public class ClientCommandHandler : CommandHandler,
        ICommandHandler<CreateClientCommandRequest>,
        ICommandHandler<UpdateClientCommandRequest>,
        ICommandHandler<DeleteClientCommandRequest>
    {
        private readonly IHandler _handler;
        private readonly IDomainNotificationHandler<DomainNotification> _notificationHandler;
        private readonly IClientRepository _clientRepository;

        public ClientCommandHandler(IHandler handler,
                                    IDomainNotificationHandler<DomainNotification> notificationHandler,
                                    IClientRepository clientRepository) : base(handler)
        {
            _handler = handler;
            _notificationHandler = notificationHandler;
            _clientRepository = clientRepository;
        }

        public async Task<ICommandResult> Handle(CreateClientCommandRequest request, CancellationToken cancellationToken)
        {
            var client = new Client(request.FullName, request.Email);

            if (client.IsValid() is false)
            {
                NotifyErrors(client.ValidationResult);
                return await Task.FromResult(new CommandResult<ICollection<DomainNotification>>("Failed to register the client", false, _notificationHandler.GetNotifications()));
            }

            await _clientRepository.Create(client);
            return await Task.FromResult(new CommandResult<CreateClientCommandResponse>("Client regitered successfully", true, new CreateClientCommandResponse(client.Id)));
        }

        public async Task<ICommandResult> Handle(UpdateClientCommandRequest request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.Get(request.Id);

            if (client is null)
            {
                await _handler.ThrowNotification(new DomainNotification("ClientNotFound", "Client not found"));
                return await Task.FromResult(new CommandResult<ICollection<DomainNotification>>("Failed to update the client", false, _notificationHandler.GetNotifications()));
            }

            client.Update(request.Email);

            if (client.IsValid() is false)
            {
                NotifyErrors(client.ValidationResult);
                return await Task.FromResult(new CommandResult<ICollection<DomainNotification>>("Failed to update the client", false, _notificationHandler.GetNotifications()));
            }

            await _clientRepository.Update(client);

            return await Task.FromResult(new CommandResult<UpdateClientCommandResponse>("Client updated successfully", true, new UpdateClientCommandResponse(client.Id, client.Email)));
        }

        public async Task<ICommandResult> Handle(DeleteClientCommandRequest request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByEmail(request.Email);

            if (client is null)
            {
                await _handler.ThrowNotification(new DomainNotification("ClientNotFound", "Client not found"));
                return await Task.FromResult(new CommandResult<ICollection<DomainNotification>>("Failed to delete the client", false, _notificationHandler.GetNotifications()));
            }

            client.SetIsDeleted();

            await _clientRepository.Update(client);

            return await Task.FromResult(new CommandResult<object>("Client deleted successfully", true, new()));
        }
    }
}
