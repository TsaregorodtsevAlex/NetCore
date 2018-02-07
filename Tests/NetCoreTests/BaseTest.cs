using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCQRS;
using NetCoreDataAccess;
using NetCoreDataAccess.UnitOfWork;
using NetCoreDataBus;
using NetCoreDI;
using NetCoreTests.Commands;
using NetCoreTests.DbDataAccess;
using NetCoreTests.Queries;
using NUnit.Framework;

namespace NetCoreTests
{
    [TestFixture]
    public class BaseTest
    {
        protected IServiceCollection ServiceCollection;
        protected ServiceProvider ServiceProvider;

        [OneTimeSetUp]
        public void SetUp()
        {
            ServiceCollection = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<TestDbContext>(opt => opt.UseInMemoryDatabase("Add_writes_to_database")
                    .ConfigureWarnings(config=>config.Ignore(InMemoryEventId.TransactionIgnoredWarning)))
                .AddScoped<BaseDbContext, TestDbContext>()
                .AddTransient<IExecutor, Executor>()
                .AddTransient<IAmbientContext, AmbientContext>()
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IObjectResolver, ObjectResolver>()
                .AddDataBusConfiguration();

            AddCommandsToServiceCollection();
            AddQueriesToServiceCollection();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
            var _ = new AmbientContext(ServiceProvider);
        }

        private void AddCommandsToServiceCollection()
        {
            ServiceCollection
                .AddTransient<CreateTestEntityCommand>()
                .AddTransient<CreateTestEntityCommandAsync>();
        }

        private void AddQueriesToServiceCollection()
        {
            ServiceCollection.AddTransient<GetTestEntityQuery>();
        }
    }
}
