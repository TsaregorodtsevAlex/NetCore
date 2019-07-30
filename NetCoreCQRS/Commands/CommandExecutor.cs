using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCoreCQRS.Commands
{
    public class CommandExecutor<TCommand> : ICommandExecutor<TCommand>
    {
        readonly TCommand _command;
        readonly DbContext _context;
        IDbContextTransaction _transaction;

        public CommandExecutor(TCommand command, DbContext context)
        {
            _command = command;
            _context = context;
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

        public void ProcessWithTransaction(Action<TCommand> action)
        {
            try
            {
                using (_transaction = _context.Database.BeginTransaction())
                {
                    action(_command);
                    _transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                //todo: set error log
                _transaction.Rollback();
                throw ex;
            }
        }

        public TResult ProcessWithTransaction<TResult>(Func<TCommand, TResult> commandFunc)
        {
            try
            {
                TResult funcResult;
                using (_transaction = _context.Database.BeginTransaction())
                {
                    funcResult = commandFunc(_command);
                    _transaction.Commit();
                }
                return funcResult;
            }
            catch (Exception ex)
            {
                //todo: set error log
                _transaction.Rollback();
                throw ex;
            }
        }

        public async Task<TResult> ProcessWithTransactionAsync<TResult>(Func<TCommand, Task<TResult>> commandFunc)
        {
            try
            {
                TResult funcResult;
                using (_transaction = _context.Database.BeginTransaction())
                {
                    funcResult = await commandFunc(_command);
                    _transaction.Commit();
                }
                return funcResult;
            }
            catch (Exception ex)
            {
                //todo: set error log
                _transaction.Rollback();
                throw ex;
            }
        }
    }
}