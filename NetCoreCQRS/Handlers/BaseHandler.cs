using NetCoreDI;

namespace NetCoreCQRS.Handlers
{
    public class BaseHandler
    {
        private IExecutor _executor;
        protected IExecutor Executor => _executor ?? (_executor = AmbientContext.Current.Resolver.ResolveObject<IExecutor>());
    }
}
