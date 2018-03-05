using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRS.Queries
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

        public async ValueTask<TQueryResult> ProcessAsync<TQueryResult>(Func<TQuery, ValueTask<TQueryResult>> queryFunc)
        {
            var queryResults = await queryFunc(_query);
            return queryResults;
        }

        public async ValueTask<IEnumerable<TMapResult>> ProcessAsync<TQueryResult, TMapResult>(Func<TQuery, ValueTask<IEnumerable<TQueryResult>>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc)
        {
            var queryResults = await queryFunc(_query);
            return queryResults.Select(queryResultMapFunc);
        }
    }

}