using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.ClientCommands.Requests
{
    public class DeleteClientCommandRequest : Command<DeleteClientCommandRequest>
    {
        public DeleteClientCommandRequest(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}