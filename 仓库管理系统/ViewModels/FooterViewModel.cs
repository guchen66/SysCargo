using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统.ViewModels
{
    public class FooterViewModel : BindableBase
    {

        IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
        IRegionManager _regionManager;//区域管理
        IDialogService _dialogService;
        IEventAggregator _eventAggregator;

        public FooterViewModel()
        {

        }
        public FooterViewModel(IDialogService dialogService,
                                        IRegionManager regionManager, IRegionNavigationJournal navigationJournal,
                                        IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _navigationJournal = navigationJournal;
            _eventAggregator = eventAggregator;
            // regionManager.RegisterViewWithRegion("ContentRegion", typeof(HomeView));



        }

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(DoGoBack));

        public void DoGoBack()
        {
            //返回有这种，其他的应该也有类似的
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
        }

        private DelegateCommand _forWardCommand;
        public DelegateCommand ForWardCommand =>
            _forWardCommand ?? (_forWardCommand = new DelegateCommand(DoForWard));

        public void DoForWard()
        {
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoForward();

        }




    }
}
