using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.PrismExtensions.Internal
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public abstract class ContainerExtensionAttribute : Attribute
    {
        public IContainerExtension GetContainer()
        {
            var container = Init();
            ContainerLocator.SetContainerExtension(() => container);
            return ContainerLocator.Current;
        }

        protected abstract IContainerExtension Init();
    }
}
