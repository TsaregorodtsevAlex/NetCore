using MassTransit;
using NetCoreCQRS.Integration.Test.CQRSCommands;
using NetCoreCQRS.Integration.Test.CQRSQueries;
using NetCoreCQRS.Integration.Test.Model;
using System;
using System.Threading.Tasks;

namespace NetCoreCQRS.Integration.Test.Consumers
{
	public class AddPointConsumer : IConsumer<IAddPointsEvent>
	{
		readonly IExecutor<NetCoreCQRSDbContext> _executor;
		public AddPointConsumer(IExecutor<NetCoreCQRSDbContext> executor)
		{
			_executor = executor;
		}
		public async Task Consume(ConsumeContext<IAddPointsEvent> context)
		{
			var currentCount = 0;
			while (context.Message.Count > currentCount)
			{
				currentCount++;

				var point = new Point
				{
					Comment = "",
					DateTimeOffset = DateTimeOffset.Now.UtcDateTime,
					Value = currentCount
				};

				await _executor.GetCommand<CreatePointCommand>().Process(r => r.Execute(point));

				await _executor.GetQuery<GetPointsQuery>().Process(r => r.Execute());

				//GC.Collect(2, GCCollectionMode.Optimized, true);
			}
		}
	}
}
