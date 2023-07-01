using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.ClientCommands.Responses
{
    public class UpdateClientCommandResponse : ICommandResultData
    {
        public UpdateClientCommandResponse(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
