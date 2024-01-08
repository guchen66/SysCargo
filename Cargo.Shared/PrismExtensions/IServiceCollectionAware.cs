using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IServiceCollectionAware
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        IContainerRegistry RegisterServices(Action<IServiceCollection> registerServices);
    }
}
