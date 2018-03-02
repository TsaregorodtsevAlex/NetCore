using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NetCoreDataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        private DbSet<T> DbSet => _context.Set<T>();

        public Repository(DbContext context)
        {
            _context = context;
        }

        public T GetById(object keyValue)
        {
            return DbSet.Find(keyValue);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.AsEnumerable();
        }

        public IQueryable<T> AsQueryable()
        {
            return DbSet;
        }

        public void Create(T item)
        {
            DbSet.Add(item);
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
        }

        public void Update(T item)
        {
            DbSet.Update(item);
        }
    }
}