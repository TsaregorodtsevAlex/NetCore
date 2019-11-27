using NetCoreCQRS.Queries;
using NetCoreDataAccessSpecification;
using NetCoreTests.DbDataAccess;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreTests.Specifications
{
	public class GetTestEntityWithMessageEqualThirdQuery : BaseQuery
	{
		public List<TestEntity> Execute()
		{
			var testEntityRepository = GetRepository<TestEntity>();

			var spec = GetSpecification();

			var expr = spec.ToExpression();
			var testEntities = testEntityRepository
				.AsQueryable()
				.Where(expr)
				.ToList();

			return testEntities;
		}

		Specification<TestEntity> GetSpecification()
		{
			var spec = EmptySpecification<TestEntity>.Create();

			spec = spec.And(new TestEntityMessageEqualsThirdMessageSpecification()); 

			return spec;
		}
	}
}
