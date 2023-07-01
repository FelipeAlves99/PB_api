using PB.Domain.Shared.Commands;
using System.ComponentModel.DataAnnotations;

namespace PB.Domain.Commands.ClientCommands.Requests
{
    public class UpdateClientCommandRequest : Command<UpdateClientCommandRequest>
    {
        public UpdateClientCommandRequest(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Email { get; set; }
    }
}