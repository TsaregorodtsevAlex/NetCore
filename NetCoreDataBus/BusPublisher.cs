using MassTransit;
using Serilog;
using System;
using System.Threading.Tasks;

namespace NetCoreDataBus
{
	public class BusPublisher : IBusPublisher
	{
		private readonly IBus _bus;

		public BusPublisher(IBus bus)
		{
			_bus = bus;
		}

		public void Publish<T>(T message) where T : class
		{
			try
			{
				Log.Debug("Starting publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				_bus.Publish(message);
				Log.Debug("Ending publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);

				throw;
			}
		}

		public async Task PublishAsync<T>(T message) where T : class
		{
			try
			{
				Log.Debug("Starting async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				await _bus.Publish(message);
				Log.Debug("Ending async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while async publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);

				throw;
			}
		}

		public async Task<Guid> SendScheduledMessageAsync<TMessage>(TMessage message, DateTime scheduledTime) where TMessage : class
		{
			try
			{
				return await _bus.SendScheduledMessage(message, scheduledTime);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"An error occurred while SendScheduledMessage the message: {message}, scheduledTime: {scheduledTime}");

				throw;
			}
		}

		public async Task CancelScheduledMessageAsync(Guid tokenId)
		{
			try
			{
				await _bus.CancelScheduledSend(tokenId);
			}
			catch (Exception ex)
			{
				Log.Error(ex, $"An error occurred while CancelScheduledSend the tokenId: {tokenId}");

				throw;
			}
		}
	}
}
