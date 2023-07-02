using PB.Domain.Entities;

namespace PB.Domain.Repositories
{
    public interface IClientRepository
    {
        Task Create(Client client);

        Task Update(Client client);

        Task Delete(Client client);

        Task<ICollection<Client>> GetAll();

        Task<Client> Get(Guid id);

        Task<Client> GetByEmail(string email);

        Task<Client> GetByPhoneNumber(string ddd, string phoneNumber);
    }
}