using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCQRS;
using NetCoreDataAccess;
using NetCoreDataAccess.UnitOfWork;
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
                .AddDbContext<TestDbContext>(opt => opt.UseInMemoryDatabase("Add_writes_to_database"))
                .AddScoped<BaseDbContext, TestDbContext>()
                .AddTransient<IExecutor, Executor>()
                .AddTransient<IAmbientContext, AmbientContext>()
                .AddTransient(context => new AmbientContext(ServiceProvider))
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddTransient<IObjectResolver, ObjectResolver>();

            AddCommandsToServiceCollection();
            AddQueriesToServiceCollection();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        private void AddCommandsToServiceCollection()
        {
            ServiceCollection.AddTransient<CreateTestEntityCommand>();
        }

        private void AddQueriesToServiceCollection()
        {
            ServiceCollection.AddTransient<GetTestEntityQuery>();
        }
    }
}
