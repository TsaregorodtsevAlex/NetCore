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
using NetCoreDI;
using Swashbuckle.AspNetCore.Swagger;

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
				.AddTransient<DbContext, NetCoreCQRSDbContext>()
				.AddTransient<IExecutor<NetCoreCQRSDbContext>, Executor<NetCoreCQRSDbContext>>()
				.AddTransient<IAmbientContext, AmbientContext>()
				.AddTransient<IUnitOfWork, UnitOfWork>()
				.AddTransient<IObjectResolver, ObjectResolver>();

			services
				.AddCQRSCommands()
				.AddCQRSQueries();

			services.AddDataBusConfiguration(Configuration)
				.RegisterDataBusPublisher()
				.RegisterConsumers();

			services
				.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "NetCoreCQRS.Integration.Test", Version = "v1" });
			});

			var serviceProvider = services.BuildServiceProvider();

			var _ = new AmbientContext(serviceProvider);

			using (var db = _.Resolver.ResolveObject<NetCoreCQRSDbContext>())
			{
				db.Database.Migrate();
			}

			serviceProvider
				.RegisterConsumerWithRetry<AddPointConsumer, IAddPointsEvent>(1, 1, 5);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
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