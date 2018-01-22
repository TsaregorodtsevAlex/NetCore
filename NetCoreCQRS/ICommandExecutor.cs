using System;

namespace NetCoreCQRS
{
    public interface ICommandExecutor<TCommand>
    {
        void Process(Action<TCommand> action);
    }
}