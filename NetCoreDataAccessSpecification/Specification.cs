using System;
using System.Linq.Expressions;

namespace NetCoreDataAccessSpecification
{
    public abstract class Specification<TEntity>
        where TEntity: class, new()
    {
        public bool IsSatisfiedBy(TEntity entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<TEntity, bool>> ToExpression();

        public Specification<TEntity> Add(Specification<TEntity> rightSpecification)
        {
            return new AndSpecification<TEntity>(this, rightSpecification);
        }

        public Specification<TEntity> Or(Specification<TEntity> rightSpecification)
        {
            return new OrSpecification<TEntity>(this, rightSpecification);
        }

        public Specification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }
    }
}
