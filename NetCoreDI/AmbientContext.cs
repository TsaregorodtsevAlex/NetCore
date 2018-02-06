﻿using System;
using Microsoft.Extensions.DependencyInjection;
using NetCoreDataAccess.UnitOfWork;

namespace NetCoreDI
{
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
        public IObjectResolver Resolver => _serviceProvider.GetService<IObjectResolver>();
    }
}