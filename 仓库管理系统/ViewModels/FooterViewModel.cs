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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using 仓库管理系统.Shell.Views;

namespace 仓库管理系统.ViewModels
{
    public class FooterViewModel : BindableBase,INavigationAware
    {

        IRegionNavigationJournal _navigationJournal;//导航日志，上一页，下一页
        IRegionManager _regionManager;//区域管理
        IDialogService _dialogService;
        IEventAggregator _eventAggregator;
        private IRegionNavigationJournal _journal;
        public FooterViewModel()
        {

        }
        public FooterViewModel(IDialogService dialogService,
                                        IRegionManager regionManager, IRegionNavigationJournal navigationJournal,
                                        IEventAggregator eventAggregator, IRegionNavigationJournal journal)
        {
            _dialogService = dialogService;
            _regionManager = regionManager;
            _navigationJournal = navigationJournal;
            _eventAggregator = eventAggregator;
            _journal = journal;
            // regionManager.RegisterViewWithRegion("ContentRegion", typeof(HomeView));

        }



        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(DoGoBack));

        public void DoGoBack()
        {
            //返回有这种，其他的应该也有类似的,
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
            //_journal.GoBack(); // 失败因为各个模块之间没有通信
        }

        private DelegateCommand _forWardCommand;
        public DelegateCommand ForWardCommand =>
            _forWardCommand ?? (_forWardCommand = new DelegateCommand(DoForWard));

        public void DoForWard()
        {
            _regionManager.Regions["ContentRegion"].NavigationService.Journal.GoForward();
           // _journal.GoForward();
        }


        #region 鼠标左键移动

        private DelegateCommand<string> _dragMoveCommand;
        public DelegateCommand<string> DragMoveCommand =>
            _dragMoveCommand ?? (_dragMoveCommand = new DelegateCommand<string>(ExecuteDragMoveCommand, CanExecuteDragMoveCommand));

        private void ExecuteDragMoveCommand(string parameter)
        {
            Application.Current.MainWindow.DragMove();

        }

        private bool CanExecuteDragMoveCommand(string parameter)
        {
            return true;
        }
        #endregion
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
