using System;
using System.Threading.Tasks;
using MassTransit;

namespace NetCoreDataBus
{
	public static class BusExtensions
	{
		public static async Task<Guid> SendScheduledMessage<TMessage>(this IBus bus, TMessage message, DateTime scheduledTime)
		{
			
			var sendResult =  await bus.ScheduleSend(new Uri($"rabbitmq://{bus.Address.Host}/{typeof(TMessage).Namespace}:{nameof(TMessage)}"), scheduledTime, message);
			return sendResult.TokenId;
		}

		public static async Task CancelScheduledMessage(this IBus bus, Guid tokenId)
		{
			await bus.CancelScheduledSend(tokenId);
		}
	}
}
