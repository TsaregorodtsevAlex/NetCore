using System.Threading.Tasks;
using NetCoreDataAccess.Repository;
using System;

namespace NetCoreDataAccess.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        Repository<T> GetRepository<T>() where T : class;
        CommonRepository GetRepository();
        void SaveChanges();
        Task SaveChangesAsync();
	}
}