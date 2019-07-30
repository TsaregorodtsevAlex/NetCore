using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreDataBus
{
    public static class DataBusServiceCollectionExtensions
    {
        private static IRabbitMqHost _host;

        public static IServiceCollection AddDataBusConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var userName = configuration["RABBITMQ_USERNAME"];
            var password = configuration["RABBITMQ_PASSWORD"];
            var hostUrl = configuration["RABBITMQ_HOSTURL"];

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                _host = cfg.Host(new Uri(hostUrl), h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });
            });
            serviceCollection.AddSingleton<IPublishEndpoint>(bus);
            serviceCollection.AddSingleton<ISendEndpointProvider>(bus);
            serviceCollection.AddSingleton<IBus>(bus);

            bus.Start();

            return serviceCollection;
        }

        public static IServiceCollection RegisterConsumerWithRetry<TConsumer, TContract>(this IServiceCollection serviceCollection, int retryCount, int intervalMin)
            where TConsumer : class, IConsumer, new() where TContract: class
        {
            var queueName = $"{Assembly.GetAssembly(typeof(TConsumer)).GetName().Name}_{typeof(TContract)}";

            _host.ConnectReceiveEndpoint(queueName, cfg =>
                {
                    cfg.Consumer<TConsumer>(x =>
                        {
                            x.UseRetry(configurator =>
                            {
                                configurator.Interval(retryCount, TimeSpan.FromMinutes(intervalMin));
                            });

                            x.Message<TContract>(m => m.UsePartitioner(1, context => context.MessageId.Value));
                        });

                    cfg.AutoDelete = false;
                    cfg.Durable = true;
                });

            return serviceCollection;
        }

        public static IServiceCollection RegisterConsumer<TConsumer, TContract>(this IServiceCollection serviceCollection)
            where TConsumer : class, IConsumer, new()
        {
            var queueName = $"{Assembly.GetAssembly(typeof(TConsumer)).GetName().Name}_{typeof(TContract)}";

            _host.ConnectReceiveEndpoint(queueName, cfg =>
            {
                cfg.Consumer<TConsumer>();
                cfg.AutoDelete = false;
                cfg.Durable = true;
            });

            return serviceCollection;
        }
    }
}
