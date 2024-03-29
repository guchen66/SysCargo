﻿
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions
{
    /// <summary>
    /// RegisterSingleton和RegisterManySingleton
    /// </summary>
    public static class RegisterOnceExtensions
    {
        public static IContainerRegistry RegisterSingletonOnce<TFrom, TTo>(this IContainerRegistry containerRegistry)
            where TTo : TFrom
        {
            if (!containerRegistry.IsRegistered<TFrom>())
            {
                containerRegistry.RegisterSingleton<TFrom, TTo>();
            }

            return containerRegistry;
        }

        public static IContainerRegistry RegisterManySingletonOnce<TImpl>(this IContainerRegistry containerRegistry, params Type[] services)
        {
            if (!containerRegistry.IsRegistered<TImpl>() && !services.Any(s => containerRegistry.IsRegistered(s)))
            {
                containerRegistry.RegisterManySingleton<TImpl>(services);
            }

            return containerRegistry;
        }
    }
}
