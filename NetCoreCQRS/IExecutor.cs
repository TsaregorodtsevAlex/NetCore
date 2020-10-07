using System;
using System.Threading.Tasks;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Queries;

namespace NetCoreCQRS
{
	public interface IExecutor
	{
		IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery;
		ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand;
		ICommandChainExecutor CommandChain();

		TResult WithTransaction<TResult>(Func<TResult> func);
		void WithTransaction(Action action);

		Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> func);
		Task WithTransactionAsync<TResult>(Func<Task> func);
	}

	public interface IExecutor<TContext>
	{
		IQueryExecutor<TQuery> GetQuery<TQuery>() where TQuery : BaseQuery<TContext>;
		ICommandExecutor<TCommand> GetCommand<TCommand>() where TCommand : BaseCommand<TContext>;
		ICommandChainExecutor CommandChain();

		TResult WithTransaction<TResult>(Func<TResult> func);
		void WithTransaction(Action action);

		Task<TResult> WithTransactionAsync<TResult>(Func<Task<TResult>> func);
		Task WithTransactionAsync<TResult>(Func<Task> func);
	}
}