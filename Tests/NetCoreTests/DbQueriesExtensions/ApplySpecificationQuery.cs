using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS.Queries;
using NetCoreDataAccess.Externsions;
using NetCoreTests.DbDataAccess;
using NetCoreTests.Specifications;

namespace NetCoreTests.DbQueriesExtensions
{
    public class ApplySpecificationQuery : BaseQuery
    {
        public List<string> Execute()
        {
            var testEntityRepository = Uow.GetRepository<TestEntity>();
            var testEntities = testEntityRepository
                .AsQueryable()
                .ApplySpecificationIf(new TestEntityMessageEqualsThirdMessageSpecification(), () => true)
                .Select(t => t.Message)
                .ToList();

            return testEntities;
        }
    }
}
