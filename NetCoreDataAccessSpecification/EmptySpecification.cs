using System;
using System.Linq.Expressions;

namespace NetCoreDataAccessSpecification
{
    public class EmptySpecification<TEntity> : Specification<TEntity>
    {
        private EmptySpecification()
        {
        }

        public static Specification<TEntity> Create()
        {
            return new EmptySpecification<TEntity>();
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return s => true;
        }
    }
}