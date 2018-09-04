using System;
using System.Linq;
using System.Linq.Expressions;

namespace NetCoreDataAccessSpecification
{
    public class OrSpecification<TEntity> : Specification<TEntity>
        where TEntity : class, new()
    {
        private readonly Specification<TEntity> _leftSpecification;
        private readonly Specification<TEntity> _rightSpecification;

        public OrSpecification(Specification<TEntity> leftSpecification, Specification<TEntity> rightSpecification)
        {
            _leftSpecification = leftSpecification;
            _rightSpecification = rightSpecification;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var leftExpression = _leftSpecification.ToExpression();
            var rightExpression = _rightSpecification.ToExpression();

            var andExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

            return Expression.Lambda<Func<TEntity, bool>>(andExpression, leftExpression.Parameters.Single());
        }
    }
}