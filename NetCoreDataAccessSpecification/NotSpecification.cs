using System;
using System.Linq;
using System.Linq.Expressions;

namespace NetCoreDataAccessSpecification
{
    public class NotSpecification<TEntity> : Specification<TEntity>
    {
        private readonly Specification<TEntity> _specification;

        public NotSpecification(Specification<TEntity> specification)
        {
            _specification = specification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();
            var andExpression = Expression.Not(expression.Body);

            return Expression.Lambda<Func<TEntity, bool>>(andExpression, expression.Parameters.Single());
        }
    }
}