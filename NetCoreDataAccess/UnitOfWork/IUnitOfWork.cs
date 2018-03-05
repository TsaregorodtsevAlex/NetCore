using System.Threading.Tasks;
using NetCoreDataAccess.Repository;

namespace NetCoreDataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        CommonRepository GetRepository();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}