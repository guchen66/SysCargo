
using ILogger = Cargo.Core.Log.ILogger;
using Cargo.Core.Log;
using 仓库管理系统.ViewModels;
using Cargo.Ui;

namespace 仓库管理系统
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// Prism的运行步骤，
    /// InitializeShell在创建Shell之后运行，用来确保Shell可以显示，将其设为主窗口
    /// </summary>
    public partial class App : PrismApplication
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnStartup(StartupEventArgs e)
        {
            
            base.OnStartup(e);
            //Ctrl Alt J打开对象浏览器
            //  SugarGlobal.Initialized(); //初始化数据库自动生成表
            // ThemeManager.Current.ChangeTheme(this, "Dark.Green");
            // 设置日志级别
           
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            
        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();

        }

        protected override void InitializeShell(System.Windows.Window shell)
        {
            if (Container.Resolve<LoginView>().ShowDialog() == false)
            {
                Application.Current?.Shutdown();
            }
            else
            {
                base.InitializeShell(shell);
            }

        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {           
            //注册MahMapps.Metro控件的对话框，方面使用
            containerRegistry.Register<IDialogCoordinator, DialogCoordinator>();

            //注册对话框弹窗
            containerRegistry.RegisterDialog<MyDialogView, MyDialogViewModel>();

            containerRegistry.RegisterForNavigation<UserInfoView>("User");  //用户信息
            containerRegistry.RegisterForNavigation<OutboundView>("Outbound");  //出库信息
            containerRegistry.RegisterForNavigation<StorageView>();  //入库信息
            containerRegistry.RegisterForNavigation<TotalView>("Total");  //库存总量
            containerRegistry.RegisterForNavigation<WorkStationView>();  //工位信息
            containerRegistry.RegisterForNavigation<ProcessView>();  //工序信息
            containerRegistry.RegisterForNavigation<AlarmView>("Alarm");  //报警信息
            containerRegistry.RegisterForNavigation<SetView>();

            containerRegistry.RegisterSingleton<ILogger, NLogLogger>();
            containerRegistry.RegisterSingleton<LoggerHelper>();

        }

        //新建类库，通过模块化传入用户控件
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //注册模块就行
            moduleCatalog.AddModule<UiModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }
        protected override void OnInitialized()
        {
            //注册WatchDog
            //Container.Resolve<WatchLog>();
            //他妈的一个星期了，艹
            // var regionManager = containerProvider.Resolve<IRegionManager>();
            base.OnInitialized();
            var regionManager = Container.Resolve<IRegionManager>();

            regionManager.RequestNavigate("ContentRegion", "HomeView");
          //  regionManager.RequestNavigate("HeaderRegion", "HeaderView");
          //  regionManager.RequestNavigate("FooterRegion", "FooterView");
        }
        /* protected override IContainerExtension CreateContainerExtension()
         {
             var serviceCollection = new ServiceCollection();
             serviceCollection.AddLogging(configure =>
             {
                 configure.ClearProviders();
                 configure.SetMinimumLevel(LogLevel.Trace);
                 configure.AddNLog();
             });

             return new DryIocContainerExtension(new Container(CreateContainerRules())
                 .WithDependencyInjectionAdapter(serviceCollection));
         }*/

        /* protected override void ConfigureContainer(IContainerExtension containerBuilder)
         {
             // 使用你自己的容器实现来设置 ContainerLocator
             ContainerLocator.SetContainerExtension(() => new DryIocContainerExtension(containerBuilder));
         }*/

        protected override IContainerExtension CreateContainerExtension()
        {
            return base.CreateContainerExtension();
        }

        protected override DryIoc.Rules CreateContainerRules()
        {
            return base.CreateContainerRules();
        }
    }
}
