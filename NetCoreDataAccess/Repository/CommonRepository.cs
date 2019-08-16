using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess.Interfaces;

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
            SetCreatedData(entity);
            _context.Add(entity);
        }

        public T GetById<T>(T entity, object key) where T : class
        {
            return _context.Find<T>(key);
        }

        public async Task<T> GetByIdAsync<T>(T entity, object key) where T : class
        {
            return await _context.FindAsync<T>(key);
        }

        public async Task CreateAsync<T>(T entity) where T : class
        {
            SetCreatedData(entity);
            await _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            SetModifaedData(entity);
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
        
        private void SetCreatedData<T>(T item) where T : class
        {
            if (item is ICreateEntityAudit)
            {
                ((ICreateEntityAudit)item).CreateDate = DateTime.UtcNow;
                SetModifaedData(item);
            }
        }

        private void SetModifaedData<T>(T item) where T : class
        {
            if (item is IModifyEntityAudit)
            {
                ((IModifyEntityAudit)item).UpdateDate = DateTime.UtcNow;
            }
        }
    }
}