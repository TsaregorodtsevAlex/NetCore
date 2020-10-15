using System;
using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess.Repository;
using System.Threading.Tasks;

namespace NetCoreCQRS.Commands
{
	public class BaseCommand
	{
		DbContext Context;

		/// <summary>
		/// Вызывается после выполнения команди или транзакции
		/// </summary>
		public Action SuccessAction { get; protected set; }

		public void SetContext(DbContext dbContext)
		{
			Context = dbContext;
		}

		public Repository<T> GetRepository<T>() where T : class
		{
			var repository = new Repository<T>(Context);
			return repository;
		}
		public CommonRepository GetRepository()
		{
			var repository = new CommonRepository(Context);
			return repository;
		}

		public int SaveChanges()
		{
			return Context.SaveChanges();
		}
		public async Task<int> SaveChangesAsync()
		{
			return await Context.SaveChangesAsync();
		}
	}

	public class BaseCommand<TDbContext>
	{
		public TDbContext Context;

		public void SetContext(TDbContext dbContext)
		{
			Context = dbContext;
		}
	}
}
