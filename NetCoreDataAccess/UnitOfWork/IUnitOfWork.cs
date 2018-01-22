using NetCoreDataAccess.Repository;

namespace NetCoreDataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<T> GetRepository<T>() where T : class;
        void SaveChanges();
    }
}