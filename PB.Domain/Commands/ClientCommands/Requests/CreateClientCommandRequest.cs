using PB.Domain.Commands.PhoneCommands.Requests;
using PB.Domain.Shared.Commands;
using System.ComponentModel.DataAnnotations;

namespace PB.Domain.Commands.ClientCommands.Requests
{
    public class CreateClientCommandRequest : Command<CreateClientCommandRequest>
    {
        public CreateClientCommandRequest(string fullName,
                                          string email,
                                          List<CreatePhoneCommandRequest> phones)
        {
            FullName = fullName;
            Email = email;
            Phones = phones;
        }

        [Required(ErrorMessage = "Fullname is required")]
        public string FullName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }

        [Required]
        [MinLength(1)]
        public List<CreatePhoneCommandRequest> Phones { get; set; }
    }
}
