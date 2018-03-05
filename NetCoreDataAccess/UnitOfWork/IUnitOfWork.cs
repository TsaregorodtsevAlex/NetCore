using System.Threading.Tasks;
using NetCoreDataAccess.Repository;

namespace NetCoreDataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        void SaveChanges();
        Task SaveChangesAsync();
    }
}