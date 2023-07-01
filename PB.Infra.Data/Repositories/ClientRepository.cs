using Microsoft.EntityFrameworkCore;
using PB.Domain.Entities;
using PB.Domain.Repositories;
using PB.Infra.Data.Context;

namespace PB.Infra.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly PbContext _db;
        private readonly DbSet<Client> _client;
        private bool _disposed;

        public ClientRepository(PbContext db)
        {
            _db = db;
            _client = _db.Set<Client>();
            _disposed = false;
        }

        public async Task Create(Client client)
        {
            await _client.AddAsync(client);
        }

        public Task Update(Client client)
        {
            _db.Entry(client).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task Delete(Client client)
        {
            _client.Remove(client);
            return Task.CompletedTask;
        }

        public async Task<Client> Get(Guid id)
        {
            return await _client
                .AsNoTracking()
                .Include(x => x.Phones)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<Client>> GetAll()
        {
            var result = await _client
                .AsNoTracking()
                .Include(x => x.Phones)
                .ToListAsync();
            return result;
        }
    }
}
