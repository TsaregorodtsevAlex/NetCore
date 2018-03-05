using System;
using System.Threading.Tasks;

namespace NetCoreCQRS.Commands
{
    public interface ICommandExecutor<out TCommand>
    {
        void Process(Action<TCommand> commandAction);
        TResult Process<TResult>(Func<TCommand, TResult> commandFunc);
        Task<TResult> Process<TResult>(Func<TCommand, Task<TResult>> commandFunc);

        void ProcessWithTransaction(Action<TCommand> action);
        TResult ProcessWithTransaction<TResult>(Func<TCommand, TResult> commandFunc);
        Task<TResult> ProcessWithTransactionAsync<TResult>(Func<TCommand, Task<TResult>> commandFunc);
    }
}