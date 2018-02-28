using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRS.Queries
{
    public interface IQueryExecutor<out TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc);

        IEnumerable<TMapResult> Process<TQueryResult, TMapResult>(Func<TQuery, IEnumerable<TQueryResult>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc);

        ValueTask<IEnumerable<TMapResult>> ProcessAsync<TQueryResult, TMapResult>(Func<TQuery, ValueTask<IEnumerable<TQueryResult>>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc);
    }
}