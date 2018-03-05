using System;
using System.Threading.Tasks;

namespace NetCoreDataAccess.Repository
{
    public interface ICommonRepository
    {
        Type GetEntityClrType(string entityName);
        T GetById<T>(T entity, object key) where T : class;
        Task<T> GetByIdAsync<T>(T entity, object key) where T : class;
        void Create<T>(T entity) where T : class;
        Task CreateAsync<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
    }
}