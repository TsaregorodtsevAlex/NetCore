using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NetCoreTests.Commands;
using NetCoreTests.DbDataAccess;
using NUnit.Framework;

namespace NetCoreTests.Specifications
{
    [TestFixture]
    public class SpecificationTests : BaseTest
    {
        public const string FirstEntityMessage = "first message";
        public const string SecondEntityMessage = "second message";
        public const string ThirdMessage = "third message";

        [SetUp]
        public void Specifications_SetUp()
        {
            ClearInMemoryDb();

            var executor = GetExecutor();
            var testEntities = new List<TestEntity>
            {
                TestEntity.Constuct(FirstEntityMessage),
                TestEntity.Constuct(SecondEntityMessage),
                TestEntity.Constuct(ThirdMessage)
            };

            foreach (var testEntity in testEntities)
            {
                executor.GetCommand<CreateTestEntityCommand>().Process(c => c.Execute(testEntity));
            }
        }

        [Test]
        public void Specification_ProcessEntitiesList_Success()
        {
            var executor = GetExecutor();

            var testEntities = executor.GetQuery<GetTestEntityWithMessageEqualThirdQuery>().Process(e => e.Execute());

            testEntities.Should().NotBeNull();
            testEntities.Count.Should().Be(1);
            testEntities.First().Message.Should().Be(ThirdMessage);
        }
    }
}
