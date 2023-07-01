using PB.Domain.Enums;
using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.PhoneCommands.Responses
{
    public class UpdatePhoneCommandResponse : ICommandResultData
    {
        public UpdatePhoneCommandResponse(Guid id, string ddd, string phoneNumber, EPhoneType phoneType)
        {
            Id = id;
            Ddd = ddd;
            PhoneNumber = phoneNumber;
            PhoneType = phoneType;
        }

        public Guid Id { get; set; }

        public string Ddd { get; set; }

        public string PhoneNumber { get; set; }

        public EPhoneType PhoneType { get; set; }
    }
}
