using PB.Domain.Shared.Commands;

namespace PB.Domain.Commands.PhoneCommands.Responses
{
    public class UpdateRangePhoneCommandResponse : ICommandResultData
    {
        public UpdateRangePhoneCommandResponse(List<UpdatePhoneCommandResponse> phones)
        {
            Phones = phones;
        }

        public List<UpdatePhoneCommandResponse> Phones { get; set; }
    }
}