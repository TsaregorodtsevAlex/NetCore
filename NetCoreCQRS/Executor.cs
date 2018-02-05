using NetCoreDataAccess;
using NetCoreDI;

namespace NetCoreCQRS
{
    public class Executor : IExecutor
    {
        private readonly BaseDbContext _context;

        public Executor(BaseDbContext context)
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
    }
}