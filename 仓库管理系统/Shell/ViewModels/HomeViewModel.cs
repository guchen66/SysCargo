﻿
using System.Windows.Navigation;

namespace 仓库管理系统.Shell.ViewModels
{

    /* 除了prism:RegionManager.RegionContext，
     * Prism框架还可以通过以下方式实现模块之间的通信：
     * 1. 事件聚合器（EventAggregator）：
     * 可以让不同的ViewModel之间通过发布和订阅事件来进行通信。
     * 2. 服务容器（ServiceLocator）：
     * 可以让ViewModel通过获取同一服务容器中注册的服务实例来进行通信。
     * 3. 依赖注入（Dependency Injection）：
     * 可以通过注册和注入对象来实现ViewModel之间的通信。
     * 4. 导航（Navigation）：
     * 可以通过导航过程中传递参数来实现ViewModel之间的通信。
     * 5. 共享模型（Shared Model）：
     * 可以通过共享一个数据模型（DTO或实体类）来实现ViewModel之间的通信*/
    public class HomeViewModel : BaseViewModel, INavigationAware
    {
        #region  属性、字段

        private readonly IRegionManager _regionManager;
        private readonly IRegionNavigationJournal _journal;
        private readonly IRegionNavigationService _navigationService;
        #endregion

        #region  命令

        public ICommand BackDeskTopCommand {  get; set; }
        #endregion

        public HomeViewModel(IContainerProvider provider):base(provider)
        {
            _navigationService=provider.Resolve<IRegionNavigationService>();
            BackDeskTopCommand = new DelegateCommand(ShowDesktop);
        }

        #region  方法

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Do nothing
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _navigationService.Journal.GoBack();
            /* _journal = navigationContext.NavigationService.Journal;
             if (_journal != null && _journal.CanGoBack)
            {
                _journal.GoBack();
            }
            */
        }

        private void ShowDesktop()
        {
            // 执行显示桌面的逻辑，例如使用 Shell
            var shell = new Shell32.Shell();
            shell.MinimizeAll();
        }
        #endregion
    }
}
