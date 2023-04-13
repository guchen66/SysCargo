using ContentModule;
using ControlzEx.Theming;
using Dm;
using MahApps.Metro.Controls.Dialogs;
using Moudles.Common;
using NLog;
using NLog.Config;
using NLog.Targets;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SqlSugar.DbAccess.Db;
using System.Windows;
using 仓库管理系统.Shell.Views;
using 仓库管理系统.ViewModels;
using 仓库管理系统.Views;
using LogLevel = NLog.LogLevel;

namespace 仓库管理系统
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// 
    /// Prism的运行步骤，
    /// InitializeShell在创建Shell之后运行，用来确保Shell可以显示，将其设为主窗口
    /// </summary>
    public partial class App:PrismApplication
    {

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //SugarGlobal.Initialized(); //初始化数据库自动生成表
            // ThemeManager.Current.ChangeTheme(this, "Dark.Green");
            // 设置日志级别
            LogManager.Configuration = new LoggingConfiguration();
            var target = new FileTarget { FileName = "log.txt" };
            LogManager.Configuration.AddTarget("file", target);
            LogManager.Configuration.AddRule(LogLevel.Debug, LogLevel.Fatal, "file");

            logger.Info("Application has started.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            logger.Info("Application has exited.");
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
            containerRegistry.RegisterForNavigation<AlarmView>("Alarm");  //报警信息
        }

        //新建类库，通过模块化传入用户控件
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //注册模块就行
            moduleCatalog.AddModule<ContentModuleModule>();
            base.ConfigureModuleCatalog(moduleCatalog);
        }


        //第一步，创建容器注册服务，注册模块，注册导航，注册事件聚合器
        /*protected override void OnInitialized()
        {
            base.OnInitialized();   
        }*/
    }
}
