using Newtonsoft.Json;
using PB.Domain.Enums;
using PB.Domain.Shared.Commands;
using System.ComponentModel.DataAnnotations;

namespace PB.Domain.Commands.PhoneCommands.Requests
{
    public class CreatePhoneCommandRequest : Command<CreatePhoneCommandRequest>
    {
        public CreatePhoneCommandRequest(Guid id, string ddd, string phoneNumber, EPhoneType phoneType)
        {
            ClientId = id;
            Ddd = ddd;
            PhoneNumber = phoneNumber;
            PhoneType = phoneType;
        }

        [JsonIgnore]
        public Guid ClientId { get; set; }

        [Required]
        public string Ddd { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public EPhoneType PhoneType { get; set; }
    }
}