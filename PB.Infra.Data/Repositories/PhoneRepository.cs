using Microsoft.EntityFrameworkCore;
using PB.Domain.Entities;
using PB.Domain.Repositories;
using PB.Infra.Data.Context;

namespace PB.Infra.Data.Repositories
{
    public class PhoneRepository : IPhoneRepository
    {
        private readonly PbContext _db;
        private readonly DbSet<Phone> _phone;
        private bool _disposed;

        public PhoneRepository(PbContext db)
        {
            _db = db;
            _phone = _db.Set<Phone>();
            _disposed = false;
        }

        public async Task Create(Phone phone)
        {
            await _phone.AddAsync(phone);
        }

        public Task Update(Phone phone)
        {
            _db.Entry(phone).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task Delete(Phone phone)
        {
            _phone.Remove(phone);
            return Task.CompletedTask;
        }

        public async Task<ICollection<Phone>> GetAll()
        {
            var result = await _phone
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        public async Task<Phone> Get(Guid id)
        {
            return await _phone
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}