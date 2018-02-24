﻿using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCQRS;
using NetCoreTests.Commands;
using NetCoreTests.DbDataAccess;
using NetCoreTests.Queries;
using NUnit.Framework;

namespace NetCoreTests
{
    [TestFixture]
    public class ExecutorTests: BaseTest
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
    }
}
