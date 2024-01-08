
namespace 仓库管理系统.Mvvm
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
