using System;
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
		public Task CancelScheduledMessageAsync(Guid tokenId)
		{
			return Task.CompletedTask;
		}
		public Task<Guid> SendScheduledMessageAsync<TMessage>(TMessage message, DateTime scheduledTime) where TMessage : class
		{
			return Task.FromResult(Guid.Empty);
		}
	}
}
