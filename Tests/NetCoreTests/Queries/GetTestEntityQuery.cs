using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityQuery : BaseQuery
    {
        public List<TestEntity> Execute()
        {
            var testRepository = Uow.GetRepository<TestEntity>();
            return testRepository.GetAll().ToList();
        }
    }
}
