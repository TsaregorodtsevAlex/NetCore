﻿using System;
using System.Reflection;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MassTransit.BusConfigurators;

namespace NetCoreDataBus
{
    public static class DataBusServiceCollectionExtensions
    {
        private static IRabbitMqHost _host;

        private static bool _useQuartz;


		public static IServiceCollection AddDataBusConfiguration(this IServiceCollection serviceCollection, IConfiguration configuration)
		{
			var userName = configuration["RABBITMQ_USERNAME"];
            var password = configuration["RABBITMQ_PASSWORD"];
            var hostUrl = configuration["RABBITMQ_HOSTURL"];
            _useQuartz = string.IsNullOrEmpty(configuration["RABBITMQ_QUARTZ_QUEUE_NAME"]) == false;
            var quartzQueueName = configuration["RABBITMQ_QUARTZ_QUEUE_NAME"];

			var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                _host = cfg.Host(new Uri(hostUrl), h =>
                {
                    h.Username(userName);
                    h.Password(password);
                });

                if (_useQuartz)
                {
	                cfg.UseMessageScheduler(new Uri($"rabbitmq://{_host.Settings.Host}/{quartzQueueName}"));
				}
            });
            serviceCollection.AddSingleton<IPublishEndpoint>(bus);
            serviceCollection.AddSingleton<ISendEndpointProvider>(bus);
            serviceCollection.AddSingleton<IBus>(bus);

            bus.Start();

            return serviceCollection;
        }

        public static IServiceCollection RegisterConsumerWithRetry<TConsumer, TContract>(this IServiceCollection serviceCollection, int retryCount, int intervalMin, int concurrencyLimit = 0)
            where TConsumer : class, IConsumer, new() where TContract: class
        {
            var queueName = $"{typeof(TConsumer).FullName}_{typeof(TContract)}";

            _host.ConnectReceiveEndpoint(queueName, cfg =>
                {
                    cfg.Consumer<TConsumer>(x =>
                        {
	                        if (_useQuartz)
	                        {
		                        x.UseScheduledRedelivery(r => r.Intervals(TimeSpan.FromMinutes(5),
			                        TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(60),
			                        TimeSpan.FromMinutes(120), TimeSpan.FromMinutes(240)));
	                        }


	                        x.UseMessageRetry(configurator =>
                            {
                                configurator.Interval(retryCount, TimeSpan.FromMinutes(intervalMin));
                            });

                            x.Message<TContract>(m => m.UsePartitioner(1, context => context.MessageId.Value));

                            if(concurrencyLimit > 0)
                            {
                                x.UseConcurrencyLimit(concurrencyLimit);
                                x.UseConcurrentMessageLimit(concurrencyLimit);
                            }
                        });

                    cfg.AutoDelete = false;
                    cfg.Durable = true;
                });

            return serviceCollection;
        }

        public static IServiceCollection RegisterConsumer<TConsumer, TContract>(this IServiceCollection serviceCollection)
            where TConsumer : class, IConsumer, new()
        {
            var queueName = $"{typeof(TConsumer).FullName}_{typeof(TContract)}";

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
