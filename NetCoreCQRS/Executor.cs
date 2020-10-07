using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Queries;
using System;
using System.Threading.Tasks;

namespace NetCoreCQRS
{
	public class Executor : IExecutor
	{
		readonly DbContext _context;
		readonly IServiceProvider _provider;
		public Executor(DbContext context, IServiceProvider provider)
		{
			_context = context;
			_provider = provider;
		}

		public void WithTransaction(Action action)
		{
			var wrapper = new TransactionWrapper(_context);
			wrapper.ProcessTransaction(action);
		}

		public TResult WithTransaction<TResult>(Func<TResult> func)
		{
			var wrapper = new TransactionWrapper(_context);
			return wrapper.ProcessTransaction(func);
		}

		public async Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> func)
		{
			var wrapper = new TransactionWrapper(_context);
			return await wrapper.ProcessTransactionAsync(func);
		}

		public async Task WithTransactionAsync<TResult>(Func<Task> func)
		{
			var wrapper = new TransactionWrapper(_context);
			await wrapper.ProcessTransactionAsync(func);
		}

		public ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand
		{
			var command = (TCommand)_provider.GetService(typeof(TCommand));
			command.SetContext(_context);

			return new CommandExecutor<TCommand>(command);
		}

		public IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery
		{
			var query = (TQuery)_provider.GetService(typeof(TQuery));
			query.SetContext(_context);

			return new QueryExecutor<TQuery>(query);
		}

		public ICommandChainExecutor CommandChain()
		{
			return new CommandChainExecutor(_context, _provider);
		}
	}

	public class Executor<TContext> : IExecutor<TContext> where TContext : DbContext
	{
		readonly TContext _context;

		IServiceProvider _provider;
		public Executor(TContext context, IServiceProvider provider)
		{
			_context = context;
			_provider = provider;
		}

		public void WithTransaction(Action action)
		{
			var wrapper = new TransactionWrapper(_context);
			wrapper.ProcessTransaction(action);
		}

		public TResult WithTransaction<TResult>(Func<TResult> func)
		{
			var wrapper = new TransactionWrapper(_context);
			return wrapper.ProcessTransaction(func);
		}

		public async Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> func)
		{
			var wrapper = new TransactionWrapper(_context);
			return await wrapper.ProcessTransactionAsync(func);
		}

		public async Task WithTransactionAsync<TResult>(Func<Task> func)
		{
			var wrapper = new TransactionWrapper(_context);
			await wrapper.ProcessTransactionAsync(func);
		}

		public ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand<TContext>
		{
			var command = (TCommand)_provider.GetService(typeof(TCommand));
			command.SetContext(_context);
			return new CommandExecutor<TCommand>(command);
		}

		public IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery<TContext>
		{
			var query = (TQuery)_provider.GetService(typeof(TQuery));
			query.SetContext(_context);
			return new QueryExecutor<TQuery>(query);
		}

		public ICommandChainExecutor CommandChain()
		{
			return new CommandChainExecutor(_context, _provider);
		}
	}
}