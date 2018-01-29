using System;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDataAccess.UnitOfWork;

namespace NetCoreDI
{
    public class AmbientContext : IAmbientContext
    {
        readonly IServiceProvider _serviceProvider;

        private static AmbientContext _current { get; set; }

        public AmbientContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _current = this;
        }

        public static AmbientContext Current => _current;

        public IUnitOfWork UnitOfWork => _serviceProvider.GetService<IUnitOfWork>();

        public TObject ResolveObject<TObject>()
        {
            return _serviceProvider.GetService<TObject>();
        }
    }
}