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
    [Trait("Handler", "Update_Phone")]
    public class UpdatePhoneHandlerTest
    {
        private readonly Mock<IHandler> _handler;
        private readonly Mock<IDomainNotificationHandler<DomainNotification>> _domainNotifications;
        private readonly Mock<IPhoneRepository> _phoneRepository;

        public UpdatePhoneHandlerTest()
        {
            _handler = new Mock<IHandler>();
            _domainNotifications = new Mock<IDomainNotificationHandler<DomainNotification>>();
            _phoneRepository = new Mock<IPhoneRepository>();
        }

        [Theory(DisplayName = "Valid phone update")]
        [InlineData(EPhoneType.Mobile)]
        [InlineData(EPhoneType.Landline)]
        public async Task UpdatePhone_ValidEntity_ShouldReturnSuccessTrue(EPhoneType type)
        {
            var command = new UpdatePhoneCommandRequest(Guid.NewGuid(), "123", "123456789", type);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Update(It.IsAny<Phone>()));
            _phoneRepository
                .Setup(x => x.Get(command.Id))
                .ReturnsAsync(new Phone("123", "123456", type, Guid.NewGuid()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            _phoneRepository.Verify(x => x.Update(It.IsAny<Phone>()), Times.Once);
            Assert.True(result.Success);
        }

        [Theory(DisplayName = "Invalid phone ddd")]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("12")]
        public async Task UpdatePhone_InvalidDdd_ShouldReturnSuccessFalse(string ddd)
        {
            var command = new UpdatePhoneCommandRequest(Guid.NewGuid(), ddd, "123456789", EPhoneType.Mobile);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Update(It.IsAny<Phone>()));
            _phoneRepository
                .Setup(x => x.Get(command.Id))
                .ReturnsAsync(new Phone(ddd, "123456", EPhoneType.Mobile, Guid.NewGuid()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Invalid phone number")]
        public async Task UpdatePhone_InvalidPhoneNumber_ShouldReturnSuccessFalse()
        {
            var command = new UpdatePhoneCommandRequest(Guid.NewGuid(), "123", "", EPhoneType.Mobile);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Update(It.IsAny<Phone>()));
            _phoneRepository
                .Setup(x => x.Get(command.Id))
                .ReturnsAsync(new Phone("123", "", EPhoneType.Mobile, Guid.NewGuid()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Phone not found")]
        public async Task UpdatePhone_NotFound_ShouldReturnSuccessFalse()
        {
            var command = new UpdatePhoneCommandRequest(Guid.NewGuid(), "123", "123456789", EPhoneType.Mobile);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Get(command.Id))
                .ReturnsAsync((Phone)null);

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Invalid phone type")]
        public async Task UpdatePhone_InvalidPhoneType_ShouldReturnSuccessFalse()
        {
            var command = new UpdatePhoneCommandRequest(Guid.NewGuid(), "123", "123123", (EPhoneType)2);
            _handler.Setup(x => x.SendCommand(command));
            _phoneRepository
                .Setup(x => x.Update(It.IsAny<Phone>()));
            _phoneRepository
                .Setup(x => x.Get(command.Id))
                .ReturnsAsync(new Phone("123", "", (EPhoneType)2, Guid.NewGuid()));

            var result = await new PhoneCommandHandler(_handler.Object, _phoneRepository.Object, _domainNotifications.Object)
                .Handle(command, CancellationToken.None);

            Assert.False(result.Success);
        }
    }
}