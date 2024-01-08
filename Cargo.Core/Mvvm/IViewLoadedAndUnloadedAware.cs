using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Core.Mvvm
{
    public interface IViewLoadedAndUnloadedAware
    {
        void OnLoaded();

        void OnUnloaded();
    }

    public interface IViewLoadedAndUnloadedAware<in TView>
    {
        void OnLoaded(TView view);

        void OnUnloaded(TView view);
    }
}
