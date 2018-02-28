using System.Threading.Tasks;
using NetCoreCQRS.Commands;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Commands
{
    public class CreateTestEntityCommandAsync : BaseCommand
    {
        public async ValueTask<int> ExecuteAsync()
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            var testEntity = TestEntity.Default;
            testRepository.Create(testEntity);
            Uow.SaveChanges();
            return await Task.FromResult(testEntity.Id);
        }
    }
}