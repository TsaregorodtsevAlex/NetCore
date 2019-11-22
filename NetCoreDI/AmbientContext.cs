using System;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDataAccess.UnitOfWork;
using NetCoreLocalization;

namespace NetCoreDI
{
	[Obsolete("AmbientContext, заменен на использование IServiceProvider и больше не используется")]
    public class AmbientContext : IAmbientContext
    {
        private readonly IServiceProvider _serviceProvider;

        private static AmbientContext _current;

        public AmbientContext(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _current = this;
        }

        public static AmbientContext Current => _current;

        public IUnitOfWork UnitOfWork => _serviceProvider.GetService<IUnitOfWork>();
        public ILocalizationService Localization => _serviceProvider.GetService<ILocalizationService>();
        public IObjectResolver Resolver => _serviceProvider.GetService<IObjectResolver>();
    }
}