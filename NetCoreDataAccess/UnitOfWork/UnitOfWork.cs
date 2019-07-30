using NetCoreDataAccess.Repository;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCoreDataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private  DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public Repository<T> GetRepository<T>() where T : class
        {
            var repository = new Repository<T>(_context);
            return repository;
        }

        public CommonRepository GetRepository()
        {
            var repository = new CommonRepository(_context);
            return repository;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _context?.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}