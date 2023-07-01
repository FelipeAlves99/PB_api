using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.PhoneCommands.Requests
{
    public class UpdateRangePhoneCommandRequest : Command<UpdateRangePhoneCommandRequest>
    {
        public UpdateRangePhoneCommandRequest(Guid clientId, List<UpdatePhoneCommandRequest> phones)
        {
            ClientId = clientId;
            Phones = phones;
        }

        public Guid ClientId { get; set; }

        public List<UpdatePhoneCommandRequest> Phones { get; set; }
    }
}