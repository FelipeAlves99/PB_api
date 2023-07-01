using FluentValidation.Results;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;

namespace PB.Domain.Shared.Commands
{
    public class CommandHandler
    {
        private readonly IHandler _handler;

        public CommandHandler(IHandler handler)
        {
            _handler = handler;
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