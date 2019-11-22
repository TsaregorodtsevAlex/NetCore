using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NetCoreCQRS.Commands
{
	public class CommandChainExecutor : ICommandChainExecutor
	{
		readonly DbContext _context;
		readonly Dictionary<BaseCommand, Action<BaseCommand>> _commandsChain;
		readonly IServiceProvider _provider;

		public CommandChainExecutor(DbContext context, IServiceProvider provider)
		{
			_context = context;
			_provider = provider;
			_commandsChain = new Dictionary<BaseCommand, Action<BaseCommand>>();
		}

		public ICommandChainExecutor AddCommand<TCommand>(Action<TCommand> commandAction) where TCommand : BaseCommand
		{
			var command = (TCommand)_provider.GetService(typeof(TCommand));
			command.SetContext(_context);
			var act = new Action<BaseCommand>(o => commandAction((TCommand)o));
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