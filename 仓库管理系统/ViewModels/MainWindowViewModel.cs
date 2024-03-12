
namespace 仓库管理系统.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public SimpleClient<User> sdb = new SimpleClient<User>();

        public MainWindowViewModel(IContainerProvider provider):base(provider)  
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomeView));
            RegionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof(HeaderView));
            RegionManager.RegisterViewWithRegion(RegionNames.FooterRegion, typeof(FooterView));
            /*_eventAggregator.GetEvent<MyEvent2>().Subscribe(DoGoBack);
            _eventAggregator.GetEvent<MyEvent3>().Subscribe(DoForWard);*/
            SelectedViewCommand = new DelegateCommand<string>(ExecuteSelected);
        }

        public ICommand SelectedViewCommand { get; set; }

       
        public void ExecuteSelected(string menuTitle)
        {
            //switch ((MenuTitle)Enum.Parse(typeof(MenuTitle), menuTitle))
            switch (menuTitle)
            {
                case "首页":
                    Navigate("HomeView");
                    break;
                case "用户信息":
                    Navigate("User");
                    break;
                case "仓库汇总":
                    Navigate("Total");
                    break;
                case "工位信息":
                    Navigate("WorkStationView");
                    break;
                case "工序信息":
                    Navigate("ProcessView");
                    break;
                case "统计数据":
                    Navigate("CountView");
                    break;
                case "智能报警":
                    Navigate("Alarm");
                    break;
                case "设置":
                    Navigate("SetView");
                    break;
            }

        }
     
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                RegionManager.RequestNavigate(RegionNames.ContentRegion, navigatePath, arg =>
                {
                    NavigationJournal = arg.Context.NavigationService.Journal;
                });
        }      
    }
}
