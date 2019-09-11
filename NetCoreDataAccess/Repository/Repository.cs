using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess.Interfaces;

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

        public async Task<T> GetByIdAsync(object keyValue)
        {
            return await DbSet.FindAsync(keyValue);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.AsEnumerable();
        }

        public IQueryable<T> AsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        public IQueryable<T> AsQueryable()
        {
            return DbSet;
        }

        public void Create(T item)
        {
            SetCreatedData(item);
            DbSet.Add(item);
        }

        public async Task CreateAsync(T item)
        {
            SetCreatedData(item);
            await DbSet.AddAsync(item);
        }

        public void CreateRange(IEnumerable<T> item)
        {
            SetCreatedData(item);
            DbSet.AddRange(item);
        }

        public async Task CreateRangeAsync(IEnumerable<T> item)
        {
            SetCreatedData(item);
            await DbSet.AddRangeAsync(item);
        }

        public void Delete(T item)
        {
            DbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            DbSet.RemoveRange(items);
        }

        public void Update(T item)
        {
	        _context.Entry(item).State = EntityState.Modified;
            SetModifaedData(item);
            DbSet.Update(item);
        }

        public void UpdateRange(IEnumerable<T> item)
        {
            SetModifaedData(item);
            DbSet.UpdateRange(item);
        }

        private void SetCreatedData(IEnumerable<T> items)
        {
            foreach (var item in items.Where(r => r is ICreateEntityAudit))
            {
                ((ICreateEntityAudit)item).CreateDate = DateTime.UtcNow;
                SetModifaedData(item);
            }
        }

        private void SetCreatedData(T item)
        {
            if (item is ICreateEntityAudit)
            {
                ((ICreateEntityAudit)item).CreateDate = DateTime.UtcNow;
                SetModifaedData(item);
            }
        }

        private void SetModifaedData(IEnumerable<T> items)
        {
            foreach (var item in items.Where(r => r is IModifyEntityAudit))
            {
                ((IModifyEntityAudit)item).UpdateDate = DateTime.UtcNow;
            }
        }

        private void SetModifaedData(T item)
        {
            if (item is IModifyEntityAudit)
            {
                ((IModifyEntityAudit)item).UpdateDate = DateTime.UtcNow;
            }
        }
    }
}