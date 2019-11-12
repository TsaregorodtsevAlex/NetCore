using Microsoft.Extensions.DependencyInjection;
using NetCoreCQRS.Commands;
using System.Linq;

namespace NetCoreCQRS.Integration.Test.Extensions
{
	public static class CommandsDependencyInjectionExtensions
	{
		public static IServiceCollection AddCQRSCommands(this IServiceCollection serviceCollection)
		{
			serviceCollection.Scan(scan => scan
				.FromAssemblyOf<AssemblyPointer>()
				.AddClasses(classes => classes.AssignableTo<BaseCommand<NetCoreCQRSDbContext>>())
				.AsSelf()
				.WithTransientLifetime());

			return serviceCollection;
		}
	}
}
