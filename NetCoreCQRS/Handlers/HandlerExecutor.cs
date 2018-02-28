using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreCQRS.Handlers
{
    public class HandlerExecutor<THandler>: IHandlerExecutor<THandler>
    {
        private readonly THandler _handler;

        public HandlerExecutor(THandler handler)
        {
            _handler = handler;
        }

        public THandlerResult Process<THandlerResult>(Func<THandler, THandlerResult> handlerFunc)
        {
            return handlerFunc(_handler);
        }

        public IEnumerable<TMapResult> Process<THandlerResult, TMapResult>(Func<THandler, IEnumerable<THandlerResult>> handlerFunc, Func<THandlerResult, TMapResult> handlerResultMapFunc)
        {
            return handlerFunc(_handler).Select(handlerResultMapFunc);
        }

        public async ValueTask<IEnumerable<TMapResult>> ProcessAsync<THandlerResult, TMapResult>(Func<THandler, ValueTask<IEnumerable<THandlerResult>>> handlerFunc, Func<THandlerResult, TMapResult> handlerResultMapFunc)
        {
            var handlerResults = await handlerFunc(_handler);
            return handlerResults.Select(handlerResultMapFunc);
        }
    }
}
