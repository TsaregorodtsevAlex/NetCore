using System;
using System.Collections.Generic;

namespace NetCoreCQRS
{
    public interface IQueryExecutor<out TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc);

        IEnumerable<TMapResult> Process<TQueryResult, TMapResult>(Func<TQuery, IEnumerable<TQueryResult>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc);
    }
}