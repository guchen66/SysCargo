
using Cargo.Shared.Dtos;
using Mapster;
using MessageBox = System.Windows.MessageBox;

namespace 仓库管理系统.Shell.ViewModels
{
    public class UserInfoViewModel : BaseViewModel
    {
        #region 属性、字段

        private readonly IDialogCoordinator _dialogCoordinator;

        private ObservableCollection<UserDto> gridModelList;
        public ObservableCollection<UserDto> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        private DataBaseProvider<User> db = new DataBaseProvider<User>();
        
        #endregion

        #region 命令

        public ICommand InitingCommand {  get; set; }
        public ICommand QueryUserCommand { get; set; }
        public ICommand AddUserCommand { get; set; }
        public ICommand UpdateUserCommand { get; set; }
        public ICommand DelUserCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion
     
        List<User> dataList = new List<User>();

        private DispatcherTimer _timer;
        private bool _isDelaying;
        public UserInfoViewModel(IContainerProvider provider):base(provider) 
        {

            _dialogCoordinator = provider.Resolve<IDialogCoordinator>();

            InitingCommand = new DelegateCommand(ExecuteIniting);
            QueryUserCommand = new DelegateCommand(ExecuteQueryUser);
            AddUserCommand = new DelegateCommand(ExecuteAddUser);
            UpdateUserCommand = new DelegateCommand<int?>(ExecuteUpdateUser);
            DelUserCommand = new DelegateCommand<int?>(ExecuteDelUser);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
          //  GridModelList = new ObservableCollection<UserDto>();
        }

        #region 方法

        /// <summary>
        /// 界面初始化
        /// </summary>
        private async void ExecuteIniting()
        {
            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            try
            {
                var users = await db.Queryable.Includes(x => x.Role).ToListAsync();

                GridModelList = new ObservableCollection<UserDto>(
                users.Select(x =>
                {
                    var dto = x.Adapt<UserDto>();
                    dto.RoleName = x.Role?.RoleName ?? string.Empty;
                    return dto;
                }));
            }
            catch (Exception ex)
            {
                // 错误处理，例如记录日志或显示错误提示
                ShowProgressDialogAsync();
            }
        }
        public async void ShowProgressDialogAsync()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "请稍等", "操作失败...");
            controller.SetIndeterminate();

            // 执行长时间运行的操作
            await Task.Delay(2000);

            await controller.CloseAsync();
        }
        /// <summary>
        /// 查询User
        /// </summary>
        private async void ExecuteQueryUser()
        {
            var dataList =await db.Queryable.Includes(x=>x.Role).Where(it => it.Id.ToString().Contains(SearchContent)
                                                  || it.Name.Contains(SearchContent)
                                                  || it.Password.Contains(SearchContent)).ToListAsync();
            GridModelList = new ObservableCollection<UserDto>();
            /* if (dataList != null)
             {
                 dataList.ForEach(o => GridModelList.Add(o));
             }*/

            if (dataList != null)
            {
                dataList.ForEach(x =>
                {
                    var dto = x.Adapt<UserDto>();
                    dto.RoleName = x.Role?.RoleName ?? string.Empty;
                    //  return dto;
                    GridModelList.Add(dto);
                });


            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void ExecuteAddUser()
        {
            DialogParameters paramters = new DialogParameters();

            paramters.Add("RefreshValue", new Action(Refresh));
            // _dialogService.ShowDialog("AddUserDialogView");
            DialogService.ShowDialog("AddUserDialog", paramters, arg =>
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

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        private void ExecuteUpdateUser(int? id)
        {
            var dataList = db.Queryable.Where(it => it.Id == id).ToList();
            DialogParameters paramters = new DialogParameters();
            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));

            DialogService.ShowDialog("UpdateUserDialog", paramters, r =>
            {
                /* if (r.Result == ButtonResult.Yes)
                 {
                     //刷新
                     //AddUser = GetLessUserGrad();
                 }*/
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        private async void ExecuteDelUser(int? ids)
        {
            var model = db.Queryable.Where(it => it.Id == ids);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否删除该用户?", "删除用户", MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                    db.DeleteById(ids);
                    Refresh();
                }
                if (result == MessageDialogResult.Negative)
                {
                    Refresh();
                }
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        private void ExecuteRefresh()
        {
            var dataList = db.Queryable.Includes(x=>x.Role).Where(it => it.Name == SearchContent);
            GridModelList.Clear();// = new ObservableCollection<User>();
            if (dataList != null)
            {
                //  db.Queryable.ForEach(x => GridModelList.Add(x));
                dataList.ForEach(x =>
                {
                    var dto = x.Adapt<UserDto>();
                    dto.RoleName = x.Role?.RoleName ?? string.Empty;
                    //  return dto;
                    GridModelList.Add(dto);
                });

            }
        }

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
            _isDelaying = false;

            //执行搜索操作
            //   ExecuteQueryCmd();
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
            /*int pageSize = 15; //每页展示的数据数量
            int startIndex = (CurrentPage - 1) * pageSize;
            int endIndex = CurrentPage * pageSize;

            //获取当前页的数据
            GridModelList.Clear();
            for (int i = startIndex; i < endIndex && i < dataList.Count; i++)
            {
                GridModelList.Add(dataList[i]);
            }

            //计算总页数
            TotalPages = (int)Math.Ceiling((double)dataList.Count / pageSize);*/
        }

        //处理用户输入的方法
        public void HandleInput(string input)
        {
            //根据用户输入执行相应的操作
          /*  switch (input)
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
            }*/
        }

        //用来刷新界面
        public void Refresh()
        {
            //var dataList = db.Queryable.ToList(it => it.Name == Search);
            var dataList = db.Queryable.Includes(x => x.Role).Where(it => it.Name == SearchContent);
            GridModelList.Clear();// = new ObservableCollection<User>();
            if (dataList != null)
            {
                dataList.ForEach(x =>
                {
                    var dto = x.Adapt<UserDto>();
                    dto.RoleName = x.Role?.RoleName ?? string.Empty;
                    //  return dto;
                    GridModelList.Add(dto);
                });

            }
        }


        private async void DoRefresh()
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

        #endregion
    }
}
