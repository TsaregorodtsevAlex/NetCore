using Microsoft.Extensions.DependencyInjection;
using NetCoreCQRS.Queries;
using System.Linq;

namespace NetCoreCQRS.Integration.Test.Extensions
{
	public static class QueriesDependencyInjectionExtensions
	{
		public static IServiceCollection AddCQRSQueries(this IServiceCollection serviceCollection)
		{
			serviceCollection.Scan(scan => scan
				.FromAssemblyOf<AssemblyPointer>()
				.AddClasses(classes => classes.AssignableTo<BaseQuery<NetCoreCQRSDbContext>>())
				.AsSelf()
				.WithTransientLifetime());

			return serviceCollection;
		}
	}
}
