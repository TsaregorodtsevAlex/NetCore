using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS.Queries;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Specifications
{
    public class GetTestEntityWithMessageEqualThirdQuery : BaseQuery
    {
        public List<TestEntity> Execute()
        {
            var testEntityRepository = GetRepository<TestEntity>();
            var testEntities = testEntityRepository
                .AsQueryable()
                .Where(new TestEntityMessageEqualsThirdMessageSpecification().ToExpression())
                .ToList();

            return testEntities;
        }
    }
}
