using NetCoreDataAccess;
using NetCoreDI;

namespace NetCoreCQRS
{
    public class Executor : IExecutor
    {
        private readonly BaseDbContext _context;
        private readonly IAmbientContext _ambientContext;

        public Executor(BaseDbContext context, IAmbientContext serviceProvider)
        {
            _context = context;
            _ambientContext = serviceProvider;
        }

        public ICommandExecutor<TCommand> GetCommand<TCommand>()
        {
            var command = _ambientContext.ResolveObject<TCommand>();
            return new CommandExecutor<TCommand>(command, _context);
        }

        public IQueryExecutor<TQuery> GetQuery<TQuery>()
        {
            var query = _ambientContext.ResolveObject<TQuery>();
            return new QueryExecutor<TQuery>(query);
        }

        public ICommandChainExecutor CommandChain()
        {
            return new CommandChainExecutor(_context, _ambientContext);
        }
    }
}