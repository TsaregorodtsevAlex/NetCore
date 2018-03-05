using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreDataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        Task<T> GetByIdAsync(object keyValue);

        IEnumerable<T> GetAll();
        IQueryable<T> AsQueryable();

        void Create(T item);
        Task CreateAsync(T item);

        void CreateRange(IEnumerable<T> item);
        Task CreateRangeAsync(IEnumerable<T> item);

        void Update(T item);
        void UpdateRange(IEnumerable<T> item);

        void Delete(T item);
        void DeleteRange(IEnumerable<T> items);
    }
}