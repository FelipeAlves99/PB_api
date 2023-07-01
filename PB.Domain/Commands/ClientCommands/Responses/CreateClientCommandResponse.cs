using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.ClientCommands.Responses
{
    public record CreateClientCommandResponse(Guid Id) : ICommandResultData;
}