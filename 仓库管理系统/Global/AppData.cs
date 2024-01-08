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
                new ModuleInfos() { IconFont = "\xe626", Title = "首页" },
                new ModuleInfos() { IconFont = "\xe77a", Title = "设置" },
                new ModuleInfos() { IconFont = "\xe77a", Title = "用户信息" },
                new ModuleInfos() { IconFont = "\xe50a", Title = "仓库汇总" },
                new ModuleInfos() { IconFont = "\xe77a", Title = "工位信息" },
                new ModuleInfos() { IconFont = "\xe502", Title = "工序信息" },
                new ModuleInfos() { IconFont = "\xe669", Title = "智能报警" },

            };
        }      
    }
}
