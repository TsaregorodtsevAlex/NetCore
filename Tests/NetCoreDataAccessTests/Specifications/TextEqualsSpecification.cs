using NetCoreDataAccessSpecification;
using System;
using System.Linq.Expressions;

namespace NetCoreDataAccessTests.Specifications
{
    public class TextEqualsSpecification : Specification<SpectificationTestEntity>
    {
        public override Expression<Func<SpectificationTestEntity, bool>> ToExpression()
        {
            return (entity) => entity.Id == 1;
        }
    }
}
