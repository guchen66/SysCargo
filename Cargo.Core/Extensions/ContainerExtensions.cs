using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Extensions
{
    // 使用特性定义需要注册的类
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class RegisterServiceAttribute : Attribute { }

    public static class ContainerExtensions
    {
        public static void AddRegisteredServices(this IServiceCollection services, Assembly assembly)
        {
            // 获取所有带有 [RegisterService] 特性的类
            var types = assembly.GetTypes()
                .Where(t => t.IsClass && t.GetCustomAttribute<RegisterServiceAttribute>() != null);

            // 对于每个类，以其自身实现的接口或者本身类型进行注册
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Length > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        services.Add(new ServiceDescriptor(@interface, type, ServiceLifetime.Transient));
                    }
                }
                else
                {
                    services.Add(new ServiceDescriptor(type, type, ServiceLifetime.Transient));
                }
            }
        }
    }
}
