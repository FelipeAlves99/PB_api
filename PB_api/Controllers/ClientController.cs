using Microsoft.AspNetCore.Mvc;
using PB.Domain.Commands.ClientCommands.Requests;
using PB.Domain.Commands.ClientCommands.Responses;
using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Shared.Commands;
using PB.Domain.Shared.Handlers;
using PB.Domain.Shared.Notifications;
using PB.Domain.Shared.UnitOfWork;

namespace PB.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IHandler _handler;
        private readonly IDomainNotificationHandler<DomainNotification> _notificationHandler;
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IHandler handler, IDomainNotificationHandler<DomainNotification> notificationHandler, IUnitOfWork unitOfWork)
        {
            _handler = handler;
            _notificationHandler = notificationHandler;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommandRequest request)
        {
            var result = await _handler.SendCommand(request);

            if (result.Success is false)
                return BadRequest(result);

            var commandResult = result as CommandResult<CreateClientCommandResponse>;

            foreach (var phone in request.Phones)
                await _handler.SendCommand(new CreatePhoneCommandRequest(commandResult.Data.Id, phone.Ddd, phone.PhoneNumber, phone.PhoneType));

            if (await CommitAsync() is false)
                return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to register the client", false, _notificationHandler.GetNotifications()));

            return Created("", result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateClientCommandRequest request)
        {
            var result = await _handler.SendCommand(request);

            if (result.Success is false)
                return BadRequest(result);

            if (await CommitAsync() is false)
                return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to update the client's email", false, _notificationHandler.GetNotifications()));

            return Ok(result);
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteClient([FromRoute] string email)
        {
            var result = await _handler.SendCommand(new DeleteClientCommandRequest(email));

            if (result.Success is false)
                return BadRequest(result);

            if (await CommitAsync() is false)
                return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to delete the client", false, _notificationHandler.GetNotifications()));

            return NoContent();
        }

        //[HttpGet("{clientId}")]
        //public async Task<IActionResult> GetClient([FromRoute] Guid clientId)
        //{
        //    var result = await _handler.SendCommand(new GetClientQueryRequest(clientId));

        //    //if (result is null)
        //    //    return BadRequest();

        //    if (result.Success is false)
        //        return BadRequest(result);

        //    //foreach (var phone in request.Phones)
        //    //    await _handler.SendCommand(new CreatePhoneCommandRequest(commandResult.Data.Id, phone.Ddd, phone.PhoneNumber, phone.PhoneType));

        //    //if (await CommitAsync() is false)
        //    //    return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to register the client", false, _notificationHandler.GetNotifications()));

        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByPhone([FromBody] CreateClientCommandRequest request)
        //{
        //    var result = await _handler.SendCommand(request);

        //    if (result.Success is false)
        //        return BadRequest(result);

        //    //var commandResult = result as CommandResult<CreateClientCommandResponse>;

        //    //if (commandResult is null || commandResult.Success is false)
        //    //    return BadRequest(result);

        //    //foreach (var phone in request.Phones)
        //    //    await _handler.SendCommand(new CreatePhoneCommandRequest(commandResult.Data.Id, phone.Ddd, phone.PhoneNumber, phone.PhoneType));

        //    //if (await CommitAsync() is false)
        //    //    return BadRequest(new CommandResult<ICollection<DomainNotification>>("Failed to register the client", false, _notificationHandler.GetNotifications()));

        //    return Ok(result);
        //}

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