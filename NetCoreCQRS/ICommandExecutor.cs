using System;
using System.Threading.Tasks;

namespace NetCoreCQRS
{
    public interface ICommandExecutor<TCommand>
    {
        void Process(Action<TCommand> action);
        TResult Process<TResult>(Func<TCommand, TResult> func);
        Task<TResult> Process<TResult>(Func<TCommand, Task<TResult>> func);

        void ProcessWithTransaction(Action<TCommand> action);
        TResult ProcessWithTransaction<TResult>(Func<TCommand, TResult> func);
        Task<TResult> ProcessWithTransaction<TResult>(Func<TCommand, Task<TResult>> func);
    }
}