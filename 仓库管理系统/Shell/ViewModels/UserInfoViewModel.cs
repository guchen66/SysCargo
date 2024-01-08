
using MessageBox = System.Windows.MessageBox;

namespace 仓库管理系统.Shell.ViewModels
{
    public class UserInfoViewModel : BindableBase
    {
       
        public SimpleClient<User> sdb = new SimpleClient<User>(DatabaseService.GetClient());
        public DataBaseProvider<User> db = new DataBaseProvider<User>();
        private readonly IDialogService _dialogService;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IEventAggregator _eventAggregator;  //事件管理器发布订阅消息
        List<User> dataList = new List<User>();

        private DispatcherTimer _timer;
        private bool _isDelaying;
        public UserInfoViewModel(IDialogService dialogService,
            IDialogCoordinator dialogCoordinator, IEventAggregator eventAggregator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            //仓储查询结果是List，转成ObservableCollection
            sdb.GetList().ForEach(x=>GridModelList.Add(x));
           // db.Queryable.ToList().ForEach(x => GridModelList.Add(x));
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
      /*  public ObservableCollection<User> SelectAll()
        {
            List<User> users = new List<User>();
            if (users != null)
            {
                GridModelList.Clear();
                users.ForEach(x => GridModelList.Add(x));
            }
            return GridModelList;
        }*/

        //TextBox初始为Empty
        private string searchContent = string.Empty;

        public string SearchContent
        {
            get { return searchContent; }
            set 
            {
                searchContent = value; RaisePropertyChanged();
                if (_timer == null)
                {
                    _timer = new DispatcherTimer();
                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += _timer_Tick;
                }
                if (!_isDelaying)
                {
                    _isDelaying = true;
                    _timer.IsEnabled = false;
                    _timer.Start();
                }
            }
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            //取消上一次操作
            _timer.Stop();
            _isDelaying=false;

            //执行搜索操作
            ExecuteQueryCmd();
        }


        //查询
        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand(ExecuteQueryCmd));

        private void ExecuteQueryCmd()
        {
            var dataList = sdb.GetList().Where(it=>it.Id.ToString().Contains(SearchContent)
            ||it.Name.Contains(SearchContent)
            ||it.Password.Contains(SearchContent));
            GridModelList = new ObservableCollection<User>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
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
            _dialogService.ShowDialog("AddUserDialog", paramters, arg =>
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

            _dialogService.ShowDialog("UpdateUserDialog", paramters, r =>
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

        private void ExecuteDownLoadCommand(string search)
        {
            
            //var dataList = sdb.GetList();
           // DataTable dt = FileData.ListToDataTable(dataList);

            var data = GridModelList;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\User.json", json);

            string path = @"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\User.json";
            if (File.Exists(path))
            {
                MessageBox.Show("文件下载成功！");
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
            var dataList = sdb.GetList().Where(it => it.Name == SearchContent);
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
            SearchContent = string.Empty;
            
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
