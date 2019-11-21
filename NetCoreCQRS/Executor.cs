using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Handlers;
using NetCoreCQRS.Queries;
using NetCoreDI;
using System;

namespace NetCoreCQRS
{
    public class Executor : IExecutor
    {
        private readonly DbContext _context;

        public Executor(DbContext context)
        {
            _context = context;
        }

        public ICommandExecutor<TCommand> GetCommand<TCommand>()
        {
            var command = AmbientContext.Current.Resolver.ResolveObject<TCommand>();
            return new CommandExecutor<TCommand>(command, _context);
        }

        public IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery: BaseQuery
        {
            var query = AmbientContext.Current.Resolver.ResolveObject<TQuery>();
            return new QueryExecutor<TQuery>(query);
        }

        public ICommandChainExecutor CommandChain()
        {
            return new CommandChainExecutor(_context);
        }

        public IHandlerExecutor<THandler> GetHandler<THandler>()
        {
            var handler = AmbientContext.Current.Resolver.ResolveObject<THandler>();
            return new HandlerExecutor<THandler>(handler);
        }
    }



    public class Executor<TContext> : IExecutor<TContext> where TContext : DbContext
    {
	    private readonly TContext _context;
		IServiceProvider _provider;

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
		    var query = Activator.CreateInstance<TQuery>();
			query.SetContext(_context);
		    return new QueryExecutor<TQuery>(query);
	    }

	    public ICommandChainExecutor CommandChain()
	    {
		    return new CommandChainExecutor(_context);
	    }

	    public IHandlerExecutor<THandler> GetHandler<THandler>()
	    {
		    var handler = Activator.CreateInstance<THandler>();
			return new HandlerExecutor<THandler>(handler);
	    }
    }
}