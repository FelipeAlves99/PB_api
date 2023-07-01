using PB.Domain.Shared.UnitOfWork;
using PB.Infra.Data.Context;

namespace PB.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposedValue;
        private readonly PbContext _context;

        public UnitOfWork(PbContext context)
        {
            _context = context;
            _disposedValue = false;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}