using Prism.Mvvm;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Commands;
using Prism.Services.Dialogs;
using MahApps.Metro.Controls.Dialogs;
using SqlSugar.DbAccess.Services;
using ContentModule.Views;
using Prism.Events;
using 仓库管理系统.Global;

namespace 仓库管理系统.Shell.ViewModels
{
    public class UserInfoViewModel : BindableBase
    {
        public SimpleClient<User> sdb = new SimpleClient<User>(DatabaseService.CreateClient());


        public DataBaseProvider<User> db = new DataBaseProvider<User>();
        private readonly IDialogService _dialogService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;  //事件管理器发布订阅消息
        List<User> dataList = new List<User>();

        public UserInfoViewModel(IDialogService dialogService,
            IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            //仓储查询结果是List，转成ObservableCollection
            sdb.GetList().ForEach(x=>GridModelList.Add(x));
            //db.Queryable.ToList().ForEach(x => GridModelList.Add(x));
        }

        private ObservableCollection<User> gridModelList = new ObservableCollection<User>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<User> GridModelList//和前台要对应
        {
            get
            {
                return gridModelList;
            }
            set
            {
                gridModelList = value;
                RaisePropertyChanged();
            }//两种属性通知都可
        }


        //查询全部
        public ObservableCollection<User> SelectAll()
        {
            List<User> users = new List<User>();
            if (users != null)
            {
                GridModelList.Clear();
                users.ForEach(x => GridModelList.Add(x));
            }
            return GridModelList;
        }

        //TextBox初始为Empty
        private string search = string.Empty;

        public string Search
        {
            get { return search; }
            set { search = value; RaisePropertyChanged(); }
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        void ExecuteQueryCmd()
        {
            var dataList = sdb.GetList().Where(it=>it.Id.ToString().Contains(Search)
            ||it.Name.Contains(Search)
            ||it.Password.Contains(Search));
            GridModelList = new ObservableCollection<User>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
            }
        }


        private ObservableCollection<User> _addUser;
        public ObservableCollection<User> AddUser
        {
            get { return _addUser ?? (_addUser = new ObservableCollection<User>()); }
            set
            {
                SetProperty(ref _addUser, value);
            }
        }


        //新增
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCmd));
      
        private void ExecuteAddCmd()
        {

            DialogParameters paramters = new DialogParameters();

            paramters.Add("RefreshValue", new Action(Refresh));
            // _dialogService.ShowDialog("AddUserDialogView");
            _dialogService.ShowDialog("AddUserDialogView", paramters, arg =>
            {
               /* if (arg.Result == ButtonResult.OK)
                {

                   // HandyControl.Controls.Dialog.Show(new ErrorDialog());
                    Refresh();
                }
                else
                {
                    HandyControl.Controls.Dialog.Show(new ErrorDialog());
                }*/
            });
        }

        //修改
        private DelegateCommand<int?> _updateCommand;
        public DelegateCommand<int?> UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand<int?>(ExecuteUpdateCmd));

        private void ExecuteUpdateCmd(int? id)
        {
            var dataList=sdb.GetList().Where(it => it.Id==id);
            DialogParameters paramters = new DialogParameters();
            
           
            paramters.Add("dataList", dataList);
           
            paramters.Add("RefreshValue", new Action(Refresh));

            _dialogService.ShowDialog("UpdateUserDialogView", paramters, r =>
            {
               /* if (r.Result == ButtonResult.Yes)
                {
                    //刷新
                    //AddUser = GetLessUserGrad();
                }*/
            });
        }


        //通过SimpleClient写通过id删除
        private DelegateCommand<int?> _deleteCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand<int?>(ExecuteDeleCommand));
            set => _deleteCommand = value;
        }

        
        
        private async void ExecuteDeleCommand(int? ids)
        {
            var model = sdb.GetList().Where(it => it.Id == ids);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否删除该用户?", "删除用户", MessageDialogStyle.AffirmativeAndNegative,settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                    sdb.DeleteById(ids);
                    Refresh();
                }
                if (result==MessageDialogResult.Negative)
                {
                    Refresh();
                }
            }

           
        }

        //下载
        private DelegateCommand<string> _downLoadCommand { get; set; }
        public DelegateCommand<string> DownLoadCommand
        {
            get => _downLoadCommand ?? (_downLoadCommand = new DelegateCommand<string>(ExecuteDownLoadCommand));
            set => _downLoadCommand = value;
        }

        private async void ExecuteDownLoadCommand(string search)
        {
            var model = sdb.GetList().Where(it => it.Name==Search);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否下载表单?", "下载", MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                   // sdb.Delete(model);
                    Refresh();
                }
                if (result == MessageDialogResult.Negative)
                {
                    Refresh();
                }
            }

        }

        //刷新
        private DelegateCommand _refreshCommand;
        public DelegateCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExecuteRefreshCmd));

        private void ExecuteRefreshCmd()
        {
            DoRefresh();
           
        }
       
        //当前页码的属性
        private int _currentPage = 1;
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);
                UpdateData();
            }
        }

        //总页数的属性
        private int _totalPages = 1;
        public int TotalPages
        {
            get { return _totalPages; }
            set { SetProperty(ref _totalPages, value); }
        }

        //更新数据的方法
        private void UpdateData()
        {
            int pageSize = 15; //每页展示的数据数量
            int startIndex = (CurrentPage - 1) * pageSize;
            int endIndex = CurrentPage * pageSize;

            //获取当前页的数据
            GridModelList.Clear();
            for (int i = startIndex; i < endIndex && i < dataList.Count; i++)
            {
                GridModelList.Add(dataList[i]);
            }

            //计算总页数
            TotalPages = (int)Math.Ceiling((double)dataList.Count / pageSize);
        }

        //处理用户输入的方法
        public void HandleInput(string input)
        {
            //根据用户输入执行相应的操作
            switch (input)
            {
                case "add":
                    User newData = new User();
                    //设置新数据的属性
                    db.Insert(newData);
                    dataList.Add(newData);
                    UpdateData();
                    break;
                case "delete":
                    if (GridModelList.Count > 0)
                    {
                        User dataToDelete = GridModelList[0];
                        db.Delete(dataToDelete);
                        dataList.Remove(dataToDelete);
                        UpdateData();
                    }
                    break;
                case "update":
                    if (GridModelList.Count > 0)
                    {
                        User dataToUpdate = GridModelList[0];
                        //修改数据的属性
                        db.Update(dataToUpdate);
                        UpdateData();
                    }
                    break;
            }
        }

        //用来刷新界面
        public void Refresh()
        {
            //var dataList = db.Queryable.ToList(it => it.Name == Search);
            var dataList = sdb.GetList().Where(it => it.Name == Search);
            GridModelList.Clear();// = new ObservableCollection<User>();
            if (dataList != null)
            {
                sdb.GetList().ForEach(x => GridModelList.Add(x));
            }
        }


        private  async void DoRefresh()
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "取消",
                ColorScheme = MetroDialogColorScheme.Accented,
                AnimateShow = true,
                AnimateHide = false,

            };
            var controller = await this._dialogCoordinator.ShowProgressAsync(this, "请稍等", "正在刷新数据...", settings: mySettings);

            controller.SetIndeterminate();
            Search = string.Empty;
            
            this.Refresh();
            await Task.Delay(3000); // Wait for 3 seconds
            await controller.CloseAsync();
        }

        private void OnDialogClosed(string result)
        {
           // _eventAggregator.GetEvent<DialogColsedEvent>().Publish(result);
        }

    }
}
