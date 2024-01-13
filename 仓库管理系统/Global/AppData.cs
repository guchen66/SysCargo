
namespace 仓库管理系统.Global
{
    public class  AppData:BindableBase
    {
        public static readonly AppData Instance = new Lazy<AppData>(()=>new AppData()).Value;

        private string systemName="仓库管理系统1.0";
        public string SystemName
        {
            get { return systemName; }
            set { systemName = value; RaisePropertyChanged(); }
        }

        private User user =new ();
        public User CurrentUser
        {
            get { return user; }
            set { user = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }

        public AppData()
        {
            Init();
        }
        public SimpleClient<User> sdb = new ();

        public List<User> GetTreeListProducts()
        {
            return sdb.GetList().ToList();
        }

        public ObservableCollection<ModuleInfos> Modules { get; set; }
  
        private void Init()
        {
            Modules = new()
            {
                new ModuleInfos() { IconFont = "\xe623", Title = "首页" },
                new ModuleInfos() { IconFont = "\xe64a", Title = "用户信息" },
                new ModuleInfos() { IconFont = "\xe66c", Title = "仓库汇总" },
                new ModuleInfos() { IconFont = "\xe60b", Title = "工位信息" },
                new ModuleInfos() { IconFont = "\xe685", Title = "工序信息" },
                new ModuleInfos() { IconFont = "\xe60a", Title = "统计数据" },
                new ModuleInfos() { IconFont = "\xe872", Title = "智能报警" },
                new ModuleInfos() { IconFont = "\xe892", Title = "设置" },

            };
        }      
    }
}
