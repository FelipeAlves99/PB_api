﻿using Moq;
using PB.Domain.Commands.ClientCommands.Requests;
using PB.Domain.Entities;
using PB.Domain.Handlers.ClientHandlers;
using PB.Domain.Repositories;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.UnitTests.HandlerTests.ClientHandler
{
    [Trait("Handler", "Update_Client")]
    public class DeleteClientHandlerTest
    {
        private readonly Mock<IHandler> _handler;
        private readonly Mock<IDomainNotificationHandler<DomainNotification>> _domainNotifications;
        private readonly Mock<IClientRepository> _clientRepository;

        public DeleteClientHandlerTest()
        {
            _handler = new Mock<IHandler>();
            _domainNotifications = new Mock<IDomainNotificationHandler<DomainNotification>>();
            _clientRepository = new Mock<IClientRepository>();
        }

        [Fact(DisplayName = "Valid client delete")]
        public async Task DeleteClient_ValidEntity_ShouldReturnSuccessTrue()
        {
            var command = new DeleteClientCommandRequest("valid@email");
            _handler.Setup(x => x.SendCommand(command));
            _clientRepository
                .Setup(x => x.Update(It.IsAny<Client>()));
            _clientRepository
                .Setup(x => x.GetByEmail(command.Email))
                .ReturnsAsync(new Client("Valid name", command.Email));

            var result = await new ClientCommandHandler(_handler.Object, _domainNotifications.Object, _clientRepository.Object)
                .Handle(command, CancellationToken.None);

            _clientRepository.Verify(x => x.Update(It.IsAny<Client>()), Times.Once);
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Client not found")]
        public async Task DeleteClient_NotFound_ShouldReturnSuccessFalse()
        {
            var command = new DeleteClientCommandRequest("valid@email");
            _handler.Setup(x => x.SendCommand(command));
            _clientRepository
                .Setup(x => x.GetByEmail(command.Email))
                .ReturnsAsync((Client)null);

            var result = await new ClientCommandHandler(_handler.Object, _domainNotifications.Object, _clientRepository.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }
    }
}
