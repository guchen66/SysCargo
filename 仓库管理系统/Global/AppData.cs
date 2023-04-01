using MaterialDesignColors;
using Prism.Commands;
using Prism.Mvvm;
using SqlSugar;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 仓库管理系统.Models;

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
        public SimpleClient<User> sdb = new (DatabaseService.CreateClient());

        public List<User> GetTreeListProducts()
        {
            return sdb.GetList().ToList();
        }

        public ObservableCollection<ModuleInfo> Modules { get; set; }
       
        public MenuTitle MenuTitle { get; set; }
        private void Init()
        {
            Modules = new()
            {
                new ModuleInfo() { IconFont = "\xe626", Title = "首页" },
                new ModuleInfo() { IconFont = "\xe77a", Title = "设置" },
                new ModuleInfo() { IconFont = "\xe77a", Title = "用户信息" },
                new ModuleInfo() { IconFont = "\xe50a", Title = "仓库汇总" },
                new ModuleInfo() { IconFont = "\xe77a", Title = "入库信息" },
                new ModuleInfo() { IconFont = "\xe502", Title = "出库信息" },
                new ModuleInfo() { IconFont = "\xe669", Title = "智能报警" },

            };

        }
    }
}
