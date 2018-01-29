using NetCoreCQRS;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class CreateTestEntityCommand: BaseCommand
    {
        public void Execute()
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            testRepository.Create(TestEntity.Default);
            Uow.SaveChanges();
        }
    }
}
