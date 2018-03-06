using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCoreDataAccess.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly DbContext _context;

        public CommonRepository(DbContext context)
        {
            _context = context;
        }

        public Type GetEntityClrType(string entityName)
        {
            var entityType = _context.Model.GetEntityTypes().SingleOrDefault(e => e.ClrType.Name == entityName);
            var clrType = entityType?.ClrType;
            return clrType;
        }

        public void Create<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public T GetById<T>(T entity, object key) where T : class
        {
            return _context.Find<T>(typeof(T), key);
        }

        public async Task<T> GetByIdAsync<T>(T entity, object key) where T : class
        {
            return await _context.FindAsync<T>(typeof(T), key);
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public IQueryable<T> AsQueriable<T>(T entity) where T : class
        {
           return  _context.Set<T>();
        }
    }
}