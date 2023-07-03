using Moq;
using PB.Domain.Commands.ClientCommands.Requests;
using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Entities;
using PB.Domain.Handlers.ClientHandlers;
using PB.Domain.Repositories;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.UnitTests.HandlerTests.ClientHandler
{
    [Trait("Handler", "Create_Client")]
    public class ClientHandlerTest
    {
        private readonly Mock<IHandler> _handler;
        private readonly Mock<IDomainNotificationHandler<DomainNotification>> _domainNotifications;
        private readonly Mock<IClientRepository> _clientRepository;

        public ClientHandlerTest()
        {
            _handler = new Mock<IHandler>();
            _domainNotifications = new Mock<IDomainNotificationHandler<DomainNotification>>();
            _clientRepository = new Mock<IClientRepository>();

        }

        [Fact(DisplayName = "Valid client creation")]
        public async Task CreateClient_ValidEntity_ShouldReturnSuccessTrue()
        {
            var command = new CreateClientCommandRequest("Full name", "test@email", new List<CreatePhoneCommandRequest>());
            _handler.Setup(x => x.SendCommand(command));
            _clientRepository.Setup(x => x.Create(It.IsAny<Client>()));

            var result = await new ClientCommandHandler(_handler.Object, _domainNotifications.Object, _clientRepository.Object)
                .Handle(command, CancellationToken.None);

            _clientRepository.Verify(x => x.Create(It.IsAny<Client>()), Times.Once);
            Assert.True(result.Success);
        }

        [Theory(DisplayName = "Invalid client command")]
        [InlineData("")]
        [InlineData("InvalidName")]
        public async Task CreateClient_InvalidEntityName_ShouldReturnIsValidFalse(string name)
        {
            var command = new CreateClientCommandRequest(name, "test@email", new List<CreatePhoneCommandRequest>());
            _handler.Setup(x => x.SendCommand(command));
            _clientRepository.Setup(x => x.Create(It.IsAny<Client>()));

            var result = await new ClientCommandHandler(_handler.Object, _domainNotifications.Object, _clientRepository.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Theory(DisplayName = "Invalid client email")]
        [InlineData("")]
        [InlineData("invalidEmail")]
        public async Task CreateClient_InvalidEntityEmail_ShouldReturnIsValidFalse(string email)
        {
            var command = new CreateClientCommandRequest("Valid name", email, new List<CreatePhoneCommandRequest>());
            _handler.Setup(x => x.SendCommand(command));
            _clientRepository.Setup(x => x.Create(It.IsAny<Client>()));

            var result = await new ClientCommandHandler(_handler.Object, _domainNotifications.Object, _clientRepository.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }
    }
}