using FluentValidation.Results;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;
using PB.Domain.Shared.UnitOfWork;

namespace PB.Domain.Shared.Commands
{
    public class CommandHandler
    {
        private readonly IHandler _handler;
        private readonly IDomainNotificationHandler<DomainNotification> _notificationHandler;
        private readonly IUnitOfWork _unitOfWork;

        public CommandHandler(IHandler handler, IDomainNotificationHandler<DomainNotification> notificationHandler, IUnitOfWork unitOfWork)
        {
            _handler = handler;
            _notificationHandler = notificationHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CommitAsync()
        {
            if (_notificationHandler.HasNotifications())
                return await Task.FromResult(false);

            if (await _unitOfWork.CommitAsync())
                return await Task.FromResult(true);

            await _handler.ThrowNotification(new DomainNotification("Commit", "Ocorreu um erro ao salvar os dados"));

            return await Task.FromResult(false);
        }

        public void NotifyErrors(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _handler.ThrowNotification(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }
    }
}
