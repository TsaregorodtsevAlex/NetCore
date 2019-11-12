using Microsoft.AspNetCore.Mvc;
using NetCoreCQRS.Integration.Test.Consumers;
using NetCoreDataBus;
using System.Threading.Tasks;

namespace NetCoreCQRS.Integration.Test.Controllers
{
	[Route("api/netCoreCQRS")]
	[ApiController]
	public class NetCoreCQRSController : ControllerBase
	{
		readonly IBusPublisher _busPublisher;
		public NetCoreCQRSController(IBusPublisher busPublisher)
		{
			_busPublisher = busPublisher;
		}

		[HttpPost]
		[Route("init")]
		public async Task InitTest(int count)
		{
			for (int i = 0; i < 30; i++)
			{
				await _busPublisher.PublishAsync(new AddPointsEventDto { Count = count });
			}
		}
	}
}