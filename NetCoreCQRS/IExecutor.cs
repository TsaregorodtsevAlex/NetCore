using NetCoreCQRS.Commands;
using NetCoreCQRS.Handlers;
using NetCoreCQRS.Queries;

namespace NetCoreCQRS
{
    public interface IExecutor
    {
        IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery;
        ICommandExecutor<TCommand> GetCommand<TCommand>();
        ICommandChainExecutor CommandChain();
        IHandlerExecutor<THandler> GetHandler<THandler>();
    }

    public interface IExecutor<TContext>
    {
	    IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery<TContext>;
	    ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand<TContext>;
		ICommandChainExecutor CommandChain();
	    IHandlerExecutor<THandler> GetHandler<THandler>();
    }
}