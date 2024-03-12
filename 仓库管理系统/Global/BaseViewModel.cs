using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统.Global
{
    public class BaseViewModel:BindableBase
    {
        public IRegionNavigationJournal NavigationJournal;  //导航日志，上一页，下一页
        public IRegionManager RegionManager;                //区域管理
        public IEventAggregator EventAggregator;            //事件处理
        public IDialogService DialogService { get; set; }   //页面跳转
        public IContainerProvider Provider { get; set; }    //服务容器
        public AppData appData { get; set; } = AppData.Instance;
        public BaseViewModel(IContainerProvider provider)
        {
            Provider = provider;
            NavigationJournal=Provider.Resolve<IRegionNavigationJournal>();
            RegionManager=Provider.Resolve<IRegionManager>();
            EventAggregator =Provider.Resolve<IEventAggregator>();
            DialogService=Provider.Resolve<IDialogService>();
             
           
        }
        
    }
}
