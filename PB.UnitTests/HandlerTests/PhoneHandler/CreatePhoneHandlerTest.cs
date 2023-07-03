using Moq;
using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Entities;
using PB.Domain.Enums;
using PB.Domain.Handlers.PhoneHandlers;
using PB.Domain.Repositories;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.UnitTests.HandlerTests.PhoneHandler
{
    [Trait("Handler", "Create_Phone")]
    public class PhoneHandlerTest
    {
        private readonly Mock<IHandler> _handler;
        private readonly Mock<IDomainNotificationHandler<DomainNotification>> _domainNotifications;
        private readonly Mock<IPhoneRepository> _phoneRepository;

        public PhoneHandlerTest()
        {
            _handler = new Mock<IHandler>();
            _domainNotifications = new Mock<IDomainNotificationHandler<DomainNotification>>();
            _phoneRepository = new Mock<IPhoneRepository>();

        }

        [Theory(DisplayName = "Valid phone creation")]
        [InlineData(EPhoneType.Mobile)]
        [InlineData(EPhoneType.Landline)]
        public async Task CreatePhone_ValidEntity_ShouldReturnSuccessTrue(EPhoneType type)
        {
            var command = new CreatePhoneCommandRequest(Guid.NewGuid(), "123", "123456789", type);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Create(It.IsAny<Phone>()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            _phoneRepository.Verify(x => x.Create(It.IsAny<Phone>()), Times.Once);
            Assert.True(result.Success);
        }

        [Theory(DisplayName = "Invalid phone ddd")]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("12")]
        public async Task CreatePhone_InvalidDdd_ShouldReturnSuccessFalse(string ddd)
        {
            var command = new CreatePhoneCommandRequest(Guid.NewGuid(), ddd, "123456789", EPhoneType.Mobile);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Create(It.IsAny<Phone>()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Invalid phone number")]
        public async Task CreatePhone_InvalidPhoneNumber_ShouldReturnSuccessFalse()
        {
            var command = new CreatePhoneCommandRequest(Guid.NewGuid(), "123", "", EPhoneType.Mobile);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Create(It.IsAny<Phone>()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Invalid phone type")]
        public async Task CreatePhone_InvalidPhoneType_ShouldReturnSuccessFalse()
        {
            var command = new CreatePhoneCommandRequest(Guid.NewGuid(), "123", "123123", (EPhoneType)2);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Create(It.IsAny<Phone>()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }
    }
}