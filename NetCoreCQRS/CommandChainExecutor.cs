using System;
using System.Collections.Generic;
using NetCoreDataAccess;
using NetCoreDI;

namespace NetCoreCQRS
{
    public class CommandChainExecutor : ICommandChainExecutor
    {
        readonly BaseDbContext _context;
        private readonly Dictionary<BaseCommand, Action<BaseCommand>> _commandsChain;

        public CommandChainExecutor(BaseDbContext context)
        {
            _context = context;
            _commandsChain = new Dictionary<BaseCommand, Action<BaseCommand>>();
        }

        public ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> action) where TCommand : BaseCommand
        {
            var command = AmbientContext.Current.Resolver.ResolveObject<TCommand>();
            var act = new Action<BaseCommand>(o => action((TCommand)o));
            _commandsChain.Add(command, act);
            return this;
        }

        public void ExecuteAll()
        {
            foreach (var chainItem in _commandsChain)
            {
                var command = chainItem.Key;
                var action = chainItem.Value;
                action(command);
            }
        }

        public void ExecuteAllWithTransaction()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                ExecuteAll();
                transaction.Commit();
            }
        }
    }
}