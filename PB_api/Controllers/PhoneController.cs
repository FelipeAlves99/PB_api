using Microsoft.AspNetCore.Mvc;
using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Commands.PhoneCommands.Responses;
using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;
using PB.Domain.Shared.UnitOfWork;

namespace PB.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneController : ControllerBase
    {
        private readonly IHandler _handler;
        private readonly IDomainNotificationHandler<DomainNotification> _notificationHandler;
        private readonly IUnitOfWork _unitOfWork;

        public PhoneController(IHandler handler, IDomainNotificationHandler<DomainNotification> notificationHandler, IUnitOfWork unitOfWork)
        {
            _handler = handler;
            _notificationHandler = notificationHandler;
            _unitOfWork = unitOfWork;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateRangePhoneCommandRequest request)
        {
            var finalResult = new UpdateRangePhoneCommandResponse(new List<UpdatePhoneCommandResponse>());

            foreach (var phone in request.Phones)
            {
                var result = await _handler.SendCommand(new UpdatePhoneCommandRequest(phone.Id, phone.Ddd, phone.PhoneNumber, phone.PhoneType));

                if (result.Success is false)
                    return BadRequest(result);

                var commandResult = result as CommandResult<UpdatePhoneCommandResponse>;

                finalResult.Phones.Add(commandResult.Data);
            }

            if (await CommitAsync() is false)
                return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to update the client's email", false, _notificationHandler.GetNotifications()));

            return Ok(new CommandResult<UpdateRangePhoneCommandResponse>("Client's phones updated successfully", true, finalResult));
        }

        private async Task<bool> CommitAsync()
        {
            if (_notificationHandler.HasNotifications())
                return await Task.FromResult(false);
            if (await _unitOfWork.CommitAsync())
                return await Task.FromResult(true);
            await _handler.ThrowNotification(new DomainNotification("Commit", "An error occured while saving your data"));

            return await Task.FromResult(false);
        }
    }
}