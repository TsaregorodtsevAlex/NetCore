using System;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreDataBus
{
    public static class DataBusServiceCollectionExtensions
    {
        public static IServiceCollection AddDataBusConfiguration(this IServiceCollection serviceCollection)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(DataBusConfiguration.HostUrl), h =>
                {
                    h.Username(DataBusConfiguration.UserName);
                    h.Password(DataBusConfiguration.Password);
                });
            });

            serviceCollection.AddSingleton<IPublishEndpoint>(bus);
            serviceCollection.AddSingleton<ISendEndpointProvider>(bus);
            serviceCollection.AddSingleton<IBus>(bus);

            bus.Start();

            return serviceCollection;
        }
    }
}