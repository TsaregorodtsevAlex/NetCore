using System;

namespace NetCoreCQRS
{
    public interface IQueryExecutor<TQuery>
    {
        TQueryResult Process<TQueryResult>(Func<TQuery, TQueryResult> func);
    }
}