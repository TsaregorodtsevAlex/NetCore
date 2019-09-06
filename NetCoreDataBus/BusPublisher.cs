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
				Log.Information("Starting publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				_bus.Publish(message);
				Log.Information("Ending publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);
			}
		}

		public async Task PublishAsync<T>(T message) where T : class
		{
			try
			{
				Log.Information("Starting async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				await _bus.Publish(message);
				Log.Information("Ending async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "An error occurred while async publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);
			}
		}
	}
}
