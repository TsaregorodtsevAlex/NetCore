using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using NetCoreDataAccess;

namespace NetCoreCQRS
{
    public class CommandExecutor<TCommand> : ICommandExecutor<TCommand>
    {
        readonly TCommand _command;
        readonly BaseDbContext _context;
        IDbContextTransaction _transaction;

        public CommandExecutor(TCommand command, BaseDbContext context)
        {
            _command = command;
            _context = context;
        }

        public void Process(Action<TCommand> action)
        {
            action(_command);
        }

        public TResult Process<TResult>(Func<TCommand, TResult> func)
        {
            return func(_command);
        }

        public async Task<TResult> Process<TResult>(Func<TCommand, Task<TResult>> func)
        {
            return await func(_command);
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

        public TResult ProcessWithTransaction<TResult>(Func<TCommand, TResult> func)
        {
            try
            {
                TResult funcResult;
                using (_transaction = _context.Database.BeginTransaction())
                {
                    funcResult = func(_command);
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

        public async Task<TResult> ProcessWithTransaction<TResult>(Func<TCommand, Task<TResult>> func)
        {
            try
            {
                TResult funcResult;
                using (_transaction = _context.Database.BeginTransaction())
                {
                    funcResult = await func(_command);
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