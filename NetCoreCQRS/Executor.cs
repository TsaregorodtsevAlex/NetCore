using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Handlers;
using NetCoreCQRS.Queries;
using NetCoreDI;

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

        public IQueryExecutor<TQuery> GetQuery<TQuery>()
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
}