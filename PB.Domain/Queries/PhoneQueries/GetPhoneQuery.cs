using PB.Domain.Enums;

namespace PB.Domain.Queries.PhoneQueries
{
    public record GetPhoneQuery(Guid Id, string Ddd, string PhoneNumber, EPhoneType PhoneType, Guid ClientId);
}