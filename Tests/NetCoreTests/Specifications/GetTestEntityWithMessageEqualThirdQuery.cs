using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Specifications
{
    public class GetTestEntityWithMessageEqualThirdQuery : BaseQuery
    {
        public List<TestEntity> Execute()
        {
            var testEntityRepository = Uow.GetRepository<TestEntity>();
            var testEntities = testEntityRepository
                .AsQueryable()
                .Where(new TestEntityMessageEqualsThirdMessageSpecification().ToExpression())
                .ToList();

            return testEntities;
        }
    }
}
