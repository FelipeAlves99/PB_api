using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.PhoneCommands.Responses
{
    public record CreatePhoneCommandResponse(Guid Id) : ICommandResultData;
}