using System.Threading.Tasks;
using FluentAssertions;
using NetCoreTests.Commands;
using NetCoreTests.DbDataAccess;
using NetCoreTests.Handlers;
using NetCoreTests.Queries;
using NUnit.Framework;

namespace NetCoreTests
{
    [TestFixture]
    public class ExecutorTests : BaseTest
    {
        [Test]
        public void Executor_AddAndGetTestEntity_ReturnExistedTestEntities_Success()
        {
            var executor = GetExecutor();

            executor.GetCommand<CreateTestEntityCommand>().Process(command => command.Execute(TestEntity.Default));
            var testEntities = executor.GetQuery<GetTestEntityQuery>().Process(query => query.Execute());

            executor.Should().NotBeNull();
            testEntities.Should().NotBeNull();
            testEntities.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task Executor_ProcessCommandAsync_CorrectProcess()
        {
            var executor = GetExecutor();

            var testEntityId = await executor.GetCommand<CreateTestEntityCommandAsync>().Process(command => command.ExecuteAsync());

            testEntityId.Should().BePositive();
        }

        [Test]
        public void Executor_GetSumOfTwoNumbersHandler_ReturnSum_Success()
        {
            var executor = GetExecutor();

            var sum = executor.GetHandler<GetSumOfTwoNumbersHandler>().Process(h => h.Handle(1, 2));

            sum.Should().Be(3);
        }
    }
}
