﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRS.Queries
{
    public class QueryExecutor<TQuery> : IQueryExecutor<TQuery> where TQuery: class
    {
        private TQuery _query;

        public QueryExecutor(TQuery query)
        {
            _query = query;
        }

        public TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> queryFunc)
        {
            var result = queryFunc(_query);
            _query = null;
			return result;
        }

        public IEnumerable<TMapResult> Process<TQueryResult, TMapResult>(Func<TQuery, ICollection<TQueryResult>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc)
        {
            var result = queryFunc(_query).Select(queryResultMapFunc);
            _query = null;
			return result;
        }

        public async ValueTask<TQueryResult> Process<TQueryResult>(Func<TQuery, ValueTask<TQueryResult>> queryFunc)
        {
            var queryResults = await queryFunc(_query);
            _query = null;
			return queryResults;
        }

        public async ValueTask<IEnumerable<TMapResult>> Process<TQueryResult, TMapResult>(Func<TQuery, ValueTask<ICollection<TQueryResult>>> queryFunc, Func<TQueryResult, TMapResult> queryResultMapFunc)
        {
            var queryResults = await queryFunc(_query);
            var result = queryResults.Select(queryResultMapFunc);
            _query = null;
            return result;
        }
    }

}