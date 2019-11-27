
using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess.Repository;

namespace NetCoreCQRS.Queries
{
	public class BaseQuery
	{
		DbContext DbContext;
		public void SetContext(DbContext dbContext)
		{
			DbContext = dbContext;
		}
		public Repository<T> GetRepository<T>() where T : class
		{
			var repository = new Repository<T>(DbContext);
			return repository;
		}
		public CommonRepository GetRepository()
		{
			var repository = new CommonRepository(DbContext);
			return repository;
		}
	}

	public class BaseQuery<TContext>
	{
		public TContext Context;
		public void SetContext(TContext context)
		{
			Context = context;
		}
	}
}
