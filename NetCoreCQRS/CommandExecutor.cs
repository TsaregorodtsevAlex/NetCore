using System;
using Microsoft.EntityFrameworkCore.Storage;
using NetCoreDataAccess;

namespace NetCoreCQRS
{
    public class CommandExecutor<TCommand> : ICommandExecutor<TCommand>
    {
        private readonly TCommand _command;
        private readonly BaseDbContext _context;
        private IDbContextTransaction _transaction;

        public CommandExecutor(TCommand command, BaseDbContext context)
        {
            _command = command;
            _context = context;
        }

        public void Process(Action<TCommand> action)
        {
            action(_command);
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
            catch
            {
                //todo: set error log
                _transaction.Rollback();
            }
        }
    }
}