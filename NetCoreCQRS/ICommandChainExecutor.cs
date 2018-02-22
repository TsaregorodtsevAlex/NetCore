using System;

namespace NetCoreCQRS
{
    public interface ICommandChainExecutor
    {
        ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> commandAction) where TCommand : BaseCommand;
        void ExecuteAll();
        void ExecuteAllWithTransaction();
    }
}