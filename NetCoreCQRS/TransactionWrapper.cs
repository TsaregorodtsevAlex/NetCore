using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetCoreCQRS
{
	internal class TransactionWrapper
	{
		private DbContext _context;
		private IDbContextTransaction _transaction;

		internal TransactionWrapper(DbContext context)
		{
			_context = context;
		}

		public void ProcessTransaction(Action action)
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("Transaction in process");
			}

			try
			{
				using (_transaction = _context.Database.BeginTransaction())
				{
					action();
					_transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				_transaction.Rollback();
				throw;
			}
		}

		public TResult ProcessTransaction<TResult>(Func<TResult> func)
		{
			if (_transaction != null)
			{
				throw new InvalidOperationException("Transaction in process");
			}

			try
			{
				using (_transaction = _context.Database.BeginTransaction())
				{
					var result = func();
					_transaction.Commit();
					return result;
				}
			}
			catch (Exception ex)
			{
				_transaction.Rollback();
				throw;
			}
		}

		public async Task<TResult> ProcessTransactionAsync<TResult>(Func<Task<TResult>> func)
		{
			try
			{
				TResult funcResult;
				using (_transaction = _context.Database.BeginTransaction())
				{
					funcResult = await func();
					_transaction.Commit();
				}
				return funcResult;
			}
			catch (Exception ex)
			{
				_transaction.Rollback();
				throw;
			}
		}

		public async Task ProcessTransactionAsync(Func<Task> func)
		{
			try
			{
				
				using (_transaction = _context.Database.BeginTransaction())
				{
					await func();
					_transaction.Commit();
				}
			}
			catch (Exception ex)
			{
				_transaction.Rollback();
				throw;
			}
		}
	}
}
