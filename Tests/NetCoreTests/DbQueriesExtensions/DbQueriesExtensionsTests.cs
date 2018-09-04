using System.Collections.Generic;
using NetCoreTests.Commands;
using NetCoreTests.DbDataAccess;
using NUnit.Framework;

namespace NetCoreTests.DbQueriesExtensions
{
    [TestFixture]
    public class DbQueriesExtensionsTests : BaseTest
    {
        public const string FirstEntityMessage = "first message";
        public const string SecondEntityMessage = "second message";
        public const string ThirdMessage = "third message";

        [SetUp]
        public void DbQueriesExtensions_SetUp()
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
    }
}
