using Automatonymous;
using GreenPipes;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration.Saga;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetCoreDataBus
{
	public static class DataBusServiceCollectionExtensions
	{
		static bool _useQuartz;

		public static IRabbitMqBusFactoryConfigurator AddDataBusConfiguration(this IRabbitMqBusFactoryConfigurator cfg, IServiceCollection services, IConfiguration configuration)
		{
			var userName = configuration["RABBITMQ_USERNAME"];
			var password = configuration["RABBITMQ_PASSWORD"];
			var hostUrl = configuration["RABBITMQ_HOSTURL"];
			_useQuartz = string.IsNullOrEmpty(configuration["RABBITMQ_QUARTZ_QUEUE_NAME"]) == false;
			var quartzQueueName = configuration["RABBITMQ_QUARTZ_QUEUE_NAME"];

			var host = cfg.Host(new Uri(hostUrl), h =>
			{
				h.Username(userName);
				h.Password(password);
			});

			if (_useQuartz)
			{
				cfg.UseMessageScheduler(new Uri($"rabbitmq://{host.Settings.Host}/{quartzQueueName}"));
			} 

			return cfg;
		}

		public static IRabbitMqBusFactoryConfigurator RegisterConsumerWithRetry<TConsumer, TContract>(this IRabbitMqBusFactoryConfigurator cfg, IServiceProvider provider, int retryCount, int intervalMin, int concurrencyLimit = 0)
			where TConsumer : class, IConsumer
			where TContract : class
		{
			var queueName = $"{typeof(TConsumer).FullName}_{typeof(TContract)}";

			cfg.ReceiveEndpoint(queueName, e =>
			{
				if (_useQuartz)
				{
					e.UseScheduledRedelivery(r => r.Intervals(
						TimeSpan.FromMinutes(5),
						TimeSpan.FromMinutes(15),
						TimeSpan.FromMinutes(30),
						TimeSpan.FromMinutes(60),
						TimeSpan.FromMinutes(120),
						TimeSpan.FromMinutes(240)));
				}

				e.UseMessageRetry(configurator =>
				{
					configurator.Interval(retryCount, TimeSpan.FromMinutes(intervalMin));
				});

				e.ConfigureConsumer<TConsumer>(provider, ccfg =>
				{
					if (concurrencyLimit > 0)
					{
						ccfg.UseConcurrencyLimit(concurrencyLimit);
						ccfg.UseConcurrentMessageLimit(concurrencyLimit);
					}
					else
					{
						ccfg.Message<TContract>(m => m.UsePartitioner(1, context => context.MessageId.Value));
					}
				});

			});

			cfg.AutoDelete = false;
			cfg.Durable = true;

			return cfg;
		}

		public static IRabbitMqBusFactoryConfigurator RegisterSaga<TStateMachineInstance>(this IRabbitMqBusFactoryConfigurator cfg,
			MassTransitStateMachine<TStateMachineInstance> sagaStateMachine,
			EntityFrameworkSagaRepository<TStateMachineInstance> sagaRepository)
			where TStateMachineInstance : class, SagaStateMachineInstance, new()
		{
			var queueName = $"{typeof(TStateMachineInstance).FullName}";

			cfg.ReceiveEndpoint(queueName, e =>
			{
				e.UseConcurrencyLimit(1);
				e.StateMachineSaga(sagaStateMachine, sagaRepository);
			});

			return cfg;
		}

		public static IServiceCollection RegisterDataBusPublisher(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IBusPublisher, BusPublisher>();

			return serviceCollection;
		}
		public static IServiceCollection RegisterDataBusPublisherStub(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IBusPublisher, BusPublisherStub>();

			return serviceCollection;
		}
	}
}
