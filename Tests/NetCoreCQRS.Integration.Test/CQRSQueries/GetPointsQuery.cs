using Microsoft.EntityFrameworkCore;
using NetCoreCQRS.Integration.Test.Model;
using NetCoreCQRS.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRS.Integration.Test.CQRSQueries
{
	public class GetPointsQuery : BaseQuery<NetCoreCQRSDbContext>
	{
		public async Task Execute()
		{
			var datetimeTo = DateTimeOffset.Now.UtcDateTime;
			var datetimeFrom = datetimeTo.AddSeconds(-2);

			await Context.Points.AsNoTracking()
				.Where(r => r.DateTimeOffset >= datetimeFrom && r.DateTimeOffset <= datetimeTo)
				.ToListAsync();			
		}
	}
}
