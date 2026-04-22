using Microsoft.EntityFrameworkCore.Storage;
using webapi.Application.Services;
using webapi.Infrastructure.Database;

namespace webapi.Infrastructure.Services
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly MyAppDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWorks(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransaction()
        {
            if (_transaction != null) return;

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task FinishTransaction()
        {
            try
            {
                await _context.SaveChangesAsync();

                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransaction();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransaction()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
