using System;
using System.Linq.Expressions;
using NetCoreDataAccessSpecification;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Specifications
{
    public class TestEntityMessageEqualsThirdMessageSpecification : Specification<TestEntity>
    {
        public override Expression<Func<TestEntity, bool>> ToExpression()
        {
            return testEntity => testEntity.Message == SpecificationTests.ThirdMessage;
        }
    }
}
