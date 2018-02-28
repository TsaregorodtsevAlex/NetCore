using System;
using System.Linq;
using System.Linq.Expressions;
using NetCoreDataAccessSpecification;

namespace NetCoreDataAccess.Externsions
{
    public static class DbQueriesExtensions
    {
        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> queryable, Specification<T> specification)
        {
            return queryable.Where(specification.ToExpression());
        }

        public static IQueryable<T> ApplySpecificationIf<T>(this IQueryable<T> queryable, Specification<T> specification, Func<bool> predicate)
        {
            if (predicate())
            {
                return queryable.Where(specification.ToExpression());
            }

            return queryable;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> filterExpression, Func<bool> predicate)
        {
            if (predicate())
            {
                return queryable.Where(filterExpression);
            }

            return queryable;
        }
    }
}
