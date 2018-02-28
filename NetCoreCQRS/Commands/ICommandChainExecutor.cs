using System;

namespace NetCoreCQRS.Commands
{
    public interface ICommandChainExecutor
    {
        ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> commandAction) where TCommand : BaseCommand;
        void ExecuteAll();
        void ExecuteAllWithTransaction();
    }
}