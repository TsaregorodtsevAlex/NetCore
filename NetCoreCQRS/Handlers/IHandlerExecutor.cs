using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetCoreCQRS.Handlers
{
    public interface IHandlerExecutor<out THandler>
    {
        void Process<THandlerResult>(Action<THandler> handlerAction);

        THandlerResult Process<THandlerResult>(Func<THandler, THandlerResult> handlerFunc);

        IEnumerable<TMapResult> Process<THandlerResult, TMapResult>(Func<THandler, IEnumerable<THandlerResult>> handlerFunc, Func<THandlerResult, TMapResult> handlerResultMapFunc);

        ValueTask<IEnumerable<TMapResult>> ProcessAsync<THandlerResult, TMapResult>(Func<THandler, ValueTask<IEnumerable<THandlerResult>>> handlerFunc, Func<THandlerResult, TMapResult> handlerResultMapFunc);
    }
}
