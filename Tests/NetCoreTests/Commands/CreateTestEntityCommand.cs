using NetCoreCQRS.Commands;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class CreateTestEntityCommand: BaseCommand
    {
        public void Execute(TestEntity testEntity)
        {
            var testRepository = GetRepository<TestEntity>();
            testRepository.Create(testEntity);
            SaveChanges();
        }
    }
}
