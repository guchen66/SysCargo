using Cargo.Shared.PrismExtensions.Internal;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions.Internal
{
    public static class PrismIocExtensions
    {
        public static IContainer GetContainer(this IContainerProvider containerProvider)
        {
            return ((IContainerExtension<IContainer>)containerProvider).Instance;
        }

        public static IContainer GetContainer(this IContainerRegistry containerRegistry)
        {
            return ((IContainerExtension<IContainer>)containerRegistry).Instance;
        }

        public static bool IsRegistered<T>(this IContainerProvider containerProvider)
        {
            return ((IContainerExtension<IContainerProvider>)containerProvider).Instance.IsRegistered<T>();
        }
    }


}
