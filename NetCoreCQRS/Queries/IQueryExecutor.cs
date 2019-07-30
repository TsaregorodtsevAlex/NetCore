using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRS.Queries
{
    public interface IQueryExecutor<out TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc);

        IEnumerable<TMapResult> Process<TQueryResult, TMapResult>(Func<TQuery, ICollection<TQueryResult>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc);

        ValueTask<TQueryResult> Process<TQueryResult>(Func<TQuery, ValueTask<TQueryResult>> queryFunc);

        ValueTask<IEnumerable<TMapResult>> Process<TQueryResult, TMapResult>(Func<TQuery, ValueTask<ICollection<TQueryResult>>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc);
    }
}