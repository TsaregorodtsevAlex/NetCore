using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCoreCQRS.Commands
{
    public class CommandExecutor<TCommand> : ICommandExecutor<TCommand>
    {
        readonly TCommand _command;

        public CommandExecutor(TCommand command)
        {
            _command = command;
        }

        public void Process(Action<TCommand> commandAction)
        {
            commandAction(_command);
        }

        public TResult Process<TResult>(Func<TCommand, TResult> commandFunc)
        {
            return commandFunc(_command);
        }

        public async Task<TResult> Process<TResult>(Func<TCommand, Task<TResult>> commandFunc)
        {
            return await commandFunc(_command);
        }
    }
}