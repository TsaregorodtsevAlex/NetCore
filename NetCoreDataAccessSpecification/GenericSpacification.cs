using System;
using System.Linq.Expressions;

namespace NetCoreDataAccessSpecification
{
    public class GenericSpacification<TEntity>
    {
        public Expression<Func<TEntity, bool>> Expression { get; set; }

        public GenericSpacification(Expression<Func<TEntity, bool>> expression)
        {
            Expression = expression;
        }

        public bool IsSatisfiedBy(TEntity entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}
