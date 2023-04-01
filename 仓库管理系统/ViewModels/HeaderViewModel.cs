using ContentModule.Views;
using MahApps.Metro.Controls.Dialogs;
using Moudles.Common;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar.DbAccess.Services;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using 仓库管理系统.Shell.Views;
using System.Windows;

namespace 仓库管理系统.ViewModels
{
    public class HeaderViewModel : BindableBase
    {
        public SimpleClient<User> sdb = new SimpleClient<User>(DatabaseService.CreateClient());


        public HeaderViewModel()
        {

        }
        //TextBox初始为Empty
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<User> gridModelList = new ObservableCollection<User>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<User> GridModelList//和前台要对应
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        void ExecuteQueryCmd()
        {
            var dataList = sdb.GetList().Where(it => it.Id.ToString().Contains(Search)
            || it.Name.Contains(Search)
            || it.Password.Contains(Search));
            GridModelList = new ObservableCollection<User>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
            }
        }


    }
}
