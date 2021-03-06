using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreCQRS.Integration.Test.Consumers;
using NetCoreCQRS.Integration.Test.Extensions;
using NetCoreDataAccess.UnitOfWork;
using NetCoreDataBus;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace NetCoreCQRS.Integration.Test
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<NetCoreCQRSDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("NetCoreCQRSDB")), 10)
				.AddTransient<NetCoreCQRSDbContext>()
				.AddTransient<IExecutor<NetCoreCQRSDbContext>, Executor<NetCoreCQRSDbContext>>()
				.AddTransient<IUnitOfWork, UnitOfWork>();

			services
				.AddCQRSCommands()
				.AddCQRSQueries();


			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "NetCoreCQRS.Integration.Test", Version = "v1" });
			});

			services.AddMassTransit(x =>
			{
				x.AddConsumer<AddPointConsumer>();

				x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
				{
					cfg
						.AddDataBusConfiguration(services, Configuration);
					cfg
						.RegisterConsumerWithRetry<AddPointConsumer, IAddPointsEvent>(provider, 1, 1, 1);
				}));
			});
			services.RegisterDataBusPublisher();

			services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
			services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
			services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBusControl busControl, IServiceProvider provider)
		{
			busControl.Start();

			provider.GetService<NetCoreCQRSDbContext>().Database.Migrate();

			var swaggerBasePath = string.Empty;
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				swaggerBasePath = "/wialon";
				app.UseHsts();
			}

			app.UseSwagger(c =>
			{
				c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
				{
					swaggerDoc.BasePath = swaggerBasePath;
				});
			});

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint($"{swaggerBasePath}/swagger/v1/swagger.json", "Wialon Service");
			});

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
