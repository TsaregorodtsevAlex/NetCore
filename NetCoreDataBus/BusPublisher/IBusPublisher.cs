using System.Threading.Tasks;

namespace NetCoreDataBus.BusPublisher
{
	public interface IBusPublisher
	{
		void Publish<T>(T message) where T : class;
		Task PublishAsync<T>(T message) where T : class;
	}
}
