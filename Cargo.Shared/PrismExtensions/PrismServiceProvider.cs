﻿using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions
{
    internal class PrismServiceProvider : IServiceProvider
    {
        private IContainerProvider _container { get; }

        public PrismServiceProvider(IContainerExtension container)
            : this((IContainerProvider)container) { }

        public PrismServiceProvider(IContainerRegistry container)
            : this(container as IContainerProvider) { }

        public PrismServiceProvider(IContainerProvider container)
        {
            _container = container;
        }

        object IServiceProvider.GetService(Type serviceType) => _container.Resolve(serviceType);
    }
}
