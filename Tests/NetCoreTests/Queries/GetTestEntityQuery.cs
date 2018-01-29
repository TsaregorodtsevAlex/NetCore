using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityQuery : BaseCommand
    {
        public List<TestEntity> Execute()
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            return Enumerable.ToList<TestEntity>(testRepository.GetAll());
        }
    }
}
