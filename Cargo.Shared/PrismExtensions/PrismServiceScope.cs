using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions
{
    /// <summary>
    /// 提供一个作用域
    /// </summary>
    public class PrismServiceScope : IServiceScope
    {
        private IScopedProvider _scopedProvider;

        public PrismServiceScope(IScopedProvider scopedProvider)
        {
            _scopedProvider = scopedProvider;
            ServiceProvider = new PrismServiceProvider(scopedProvider.CurrentScope);
        }

        public IServiceProvider ServiceProvider { get; }

        public void Dispose()
        {
            if (_scopedProvider != null)
            {
                _scopedProvider.Dispose();
                _scopedProvider = null;
            }
        }
    }
}
