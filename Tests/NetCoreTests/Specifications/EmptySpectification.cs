using NetCoreDataAccessSpecification;
using System;
using System.Linq.Expressions;

namespace NetCoreTests.Specifications
{
    public class EmptySpectification<TEnity> : Specification<TEnity>
        where TEnity : class, new()
    {
        public override Expression<Func<TEnity, bool>> ToExpression()
        {
            return (entity) => true;
        }
    }
}
