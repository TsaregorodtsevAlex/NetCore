using System.Collections.Generic;
using System.Linq;

namespace NetCoreDataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IEnumerable<T> GetAll();
        IQueryable<T> AsQueryable();
        void Create(T item);
        void Update(T item);
        void Delete(T item);
    }
}