using NetCoreCQRS.Commands;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class DeleteAllTestEntitiesCommand : BaseCommand
    {
        public void Execute()
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            var testEntities = testRepository.GetAll();

            foreach (var testEntity in testEntities)
            {
                testRepository.Delete(testEntity);
            }

            Uow.SaveChanges();
        }
    }
}