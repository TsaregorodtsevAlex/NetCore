using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace NetCoreCQRS.Integration.Test.Extensions
{
	public static class ConsumersServiceCollectionExtensions
	{
		public static IServiceCollection RegisterConsumers(this IServiceCollection collection)
		{
			collection.Scan(scan => scan
				.FromAssemblyOf<AssemblyPointer>()
				.AddClasses(classes => classes.AssignableTo<IConsumer>())
				.AsSelf()
				.WithTransientLifetime());

			return collection;
		}
	}
}
