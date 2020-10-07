using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS.Queries;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityByIdQuery : BaseQuery
    {
        public TestEntity Execute(int id)
        {
            var testRepository = GetRepository<TestEntity>();
            return testRepository.GetAll().FirstOrDefault(x => x.Id == id);
        }
    }
}