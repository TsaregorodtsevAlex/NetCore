using NetCoreCQRS.Commands;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class UpdateTestEntityCommand: BaseCommand
    {
        public void Execute(TestEntity testEntity)
        {
            var testRepository = GetRepository<TestEntity>();
            testRepository.Update(testEntity);
            SaveChanges();
        }
    }
}
