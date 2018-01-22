using System;

namespace NetCoreCQRS
{
    public class QueryExecutor<TQuery> : IQueryExecutor<TQuery>
    {
        private readonly TQuery _query;

        public QueryExecutor(TQuery query)
        {
            _query = query;
        }

        public TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func)
        {
            return func(_query);
        }
    }

}