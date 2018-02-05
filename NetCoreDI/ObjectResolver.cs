using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetCoreDI
{
    public class ObjectResolver : IObjectResolver
    {
        readonly IServiceProvider _serviceProvider;

        public ObjectResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TObject ResolveObject<TObject>()
        {
            return _serviceProvider.GetService<TObject>();
        }
    }
}