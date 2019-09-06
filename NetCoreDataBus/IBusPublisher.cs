using System.Threading.Tasks;

namespace NetCoreDataBus
{
	public interface IBusPublisher
	{
		void Publish<T>(T message) where T : class;
		Task PublishAsync<T>(T message) where T : class;
	}
}
