using System.Threading.Tasks;

namespace NetCoreDataBus
{
	public class BusPublisherStub : IBusPublisher
	{
		public void Publish<T>(T message) where T : class { }
		public Task PublishAsync<T>(T message) where T : class
		{
			return Task.CompletedTask;
		}
	}
}
