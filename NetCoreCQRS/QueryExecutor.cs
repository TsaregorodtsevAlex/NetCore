using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreCQRS
{
    public class QueryExecutor<TQuery> : IQueryExecutor<TQuery>
    {
        private readonly TQuery _query;

        public QueryExecutor(TQuery query)
        {
            _query = query;
        }

        public TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc)
        {
            return queryFunc(_query);
        }

        public IEnumerable<TMapResult> Process<TQueryResult, TMapResult>(Func<TQuery, IEnumerable<TQueryResult>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc)
        {
            return queryFunc(_query).Select(queryResultMapFunc);
        }
    }

}