
namespace 仓库管理系统.ViewModels
{
    public class FooterViewModel : BaseViewModel, INavigationAware
    {
        public FooterViewModel(IContainerProvider provider) : base(provider)
        {

            // regionManager.RegisterViewWithRegion("ContentRegion", typeof(HomeView));

        }



        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(DoGoBack));

        public void DoGoBack()
        {
            //返回有这种，其他的应该也有类似的,
            RegionManager.Regions["ContentRegion"].NavigationService.Journal.GoBack();
          //  NavigationJournal.GoBack(); // 失败因为各个模块之间没有通信
        }

        private DelegateCommand _forWardCommand;
        public DelegateCommand ForWardCommand =>
            _forWardCommand ?? (_forWardCommand = new DelegateCommand(DoForWard));

        public void DoForWard()
        {
            RegionManager.Regions["ContentRegion"].NavigationService.Journal.GoForward();
          //  NavigationJournal.GoForward();
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
           // _journal = navigationContext.NavigationService.Journal;
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
