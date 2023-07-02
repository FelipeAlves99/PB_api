using PB.Domain.Queries.PhoneQueries;

namespace PB.Domain.Queries.ClientQueries
{
    public record GetClientQuery(Guid id, string FullName, string Email, List<GetPhoneQuery> Phones);
}