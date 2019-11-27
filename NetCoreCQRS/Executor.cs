using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Queries;
using System;

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

		public ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand
		{
			var command = (TCommand)_provider.GetService(typeof(TCommand));
			command.SetContext(_context);
			return new CommandExecutor<TCommand>(command, _context);
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
		readonly IServiceProvider _provider;

		public Executor(TContext context, IServiceProvider provider)
		{
			_context = context;
			_provider = provider;
		}

		public ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand<TContext>
		{
			var command = (TCommand)_provider.GetService(typeof(TCommand));
			command.SetContext(_context);
			return new CommandExecutor<TCommand>(command, _context);
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