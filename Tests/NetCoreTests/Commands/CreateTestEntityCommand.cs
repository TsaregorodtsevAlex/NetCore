using NetCoreCQRS.Commands;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class CreateTestEntityCommand: BaseCommand
    {
        public void Execute(TestEntity testEntity)
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            testRepository.Create(testEntity);
            Uow.SaveChanges();
        }
    }
}
