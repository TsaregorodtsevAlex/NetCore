using System;
using System.Collections.Generic;

namespace NetCoreCQRS
{
    public interface IQueryExecutor<out TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc);

        IEnumerable<TQueryRawResult> Process<TQueryResult, TQueryRawResult>(Func<TQuery, IEnumerable<TQueryResult>> queryFunc, Func<TQueryResult, TQueryRawResult> queryResultMapFunc);
    }
}