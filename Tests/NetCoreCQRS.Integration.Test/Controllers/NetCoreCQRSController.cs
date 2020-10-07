using System;
using Microsoft.AspNetCore.Mvc;
using NetCoreCQRS.Integration.Test.Consumers;
using NetCoreDataBus;
using System.Threading.Tasks;
using NetCoreCQRS.Integration.Test.CQRSCommands;
using NetCoreCQRS.Integration.Test.Model;

namespace NetCoreCQRS.Integration.Test.Controllers
{
	[Route("api/netCoreCQRS")]
	[ApiController]
	public class NetCoreCQRSController : ControllerBase
	{
		readonly IBusPublisher _busPublisher;

		readonly IExecutor<NetCoreCQRSDbContext> _executor;
		
		public NetCoreCQRSController(IBusPublisher busPublisher, IExecutor<NetCoreCQRSDbContext> executor)
		{
			_busPublisher = busPublisher;
			_executor = executor;
		}

		[HttpPost]
		[Route("init")]
		public async Task InitTest(int count)
		{
			for (int i = 0; i < count; i++)
			{
				await _busPublisher.PublishAsync(new AddPointsEventDto { Count = count });
			}
		}
	}
}