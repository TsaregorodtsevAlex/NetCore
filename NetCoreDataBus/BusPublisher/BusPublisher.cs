using MassTransit;
using System;
using System.Threading.Tasks;

namespace NetCoreDataBus.BusPublisher
{
	public class BusPublisher : IBusPublisher
	{
		private readonly IBus _bus;
		private readonly ILogger _logger;

		public BusPublisher(IBus bus, ILogger logger)
		{
			_bus = bus;
			_logger = logger;
		}

		public void Publish<T>(T message) where T : class
		{
			try
			{
				_logger.Information("Starting publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				_bus.Publish(message);
				_logger.Information("Ending publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);
			}
		}

		public async Task PublishAsync<T>(T message) where T : class
		{
			try
			{
				_logger.Information("Starting async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
				await _bus.Publish(message);
				_logger.Information("Ending async publishing the event {EventName} via publisher {Publisher}", typeof(T).Name, nameof(BusPublisher));
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while async publishing the event via publisher {Publisher}. Event: {Event}, Name: {EventName}", nameof(BusPublisher), message, typeof(T).Name);
			}
		}
	}
}
