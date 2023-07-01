using PB.Domain.Entities;

namespace PB.Domain.Repositories
{
    public interface IPhoneRepository
    {
        Task Create(Phone phone);

        Task Update(Phone phone);

        Task Delete(Phone phone);

        Task<ICollection<Phone>> GetAll(Guid clientId);

        Task<Phone> Get(Guid id);

        Task<Phone> GetByClientId(Guid clientId);
    }
}