using NetCoreCQRS.Commands;
using NetCoreCQRS.Handlers;
using NetCoreCQRS.Queries;

namespace NetCoreCQRS
{
    public interface IExecutor
    {
        IQueryExecutor<TQuery> GetQuery<TQuery>();
        ICommandExecutor<TCommand> GetCommand<TCommand>();
        ICommandChainExecutor CommandChain();
        IHandlerExecutor<THandler> GetHandler<THandler>();
    }
}