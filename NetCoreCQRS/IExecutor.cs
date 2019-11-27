using NetCoreCQRS.Commands;
using NetCoreCQRS.Queries;

namespace NetCoreCQRS
{
	public interface IExecutor
	{
		IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery;
		ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand;
		ICommandChainExecutor CommandChain();
	}

	public interface IExecutor<TContext>
	{
		IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery<TContext>;
		ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand<TContext>;
		ICommandChainExecutor CommandChain();
	}
}