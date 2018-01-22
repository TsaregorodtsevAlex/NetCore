namespace NetCoreCQRS
{
    public interface IExecutor
    {
        IQueryExecutor<TQuery> GetQuery<TQuery>();
        ICommandExecutor<TCommand> GetCommand<TCommand>();
        ICommandChainExecutor CommandChain();
    }
}