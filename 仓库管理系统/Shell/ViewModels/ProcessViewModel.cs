
namespace 仓库管理系统.Shell.ViewModels
{
    public class ProcessViewModel : BindableBase//, IConfirmNavigationRequest
    {

        ProcessService db = new ProcessService();
       
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private IDialogCoordinator _dialogCoordinator;
        //  IEnumerable<ProcessModel> dataList=null;
        public ProcessViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator,IEventAggregator eventAggregator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            //仓储查询结果是List，转成ObservableCollection
           
            db.GetAllProcessModels().ForEach(x => GridModelList.Add(x));
            
            //可以使用事件管理器，也可以使用导航跳转时进行刷新
            _eventAggregator.GetEvent<WorkStationDelEvent>().Subscribe(Refresh);

        }
      
        private ObservableCollection<ProcessModel> gridModelList = new ObservableCollection<ProcessModel>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<ProcessModel> GridModelList//和前台要对应
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }


        //查询全部
        public ObservableCollection<ProcessModel> SelectAll()
        {
            List<ProcessModel> process = new List<ProcessModel>();
            if (process != null)
            {
                GridModelList.Clear();
                process.ForEach(x => GridModelList.Add(x));
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

            var dataList = db.GetAllProcessModels().ToList().Where(it => it.Id.ToString().Contains(Search)
            || it.ProcessName.Contains(Search)||it.WorkPlaceName.Contains(Search)
            );
            GridModelList = new ObservableCollection<ProcessModel>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));

            }
        }

        #region 命令

        //新增
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCmd));

        void ExecuteAddCmd()
        {
            DialogParameters paramters = new DialogParameters();
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("AddProcessDialog", paramters, r =>
            {
                /*if (r.Result == ButtonResult.Yes)
                {
                    Refresh();
                }*/
            });
        }


        //修改
        private DelegateCommand<int?> _updateCommand;
        public DelegateCommand<int?> UpdateCommand =>
            _updateCommand ?? (_updateCommand = new DelegateCommand<int?>(ExecuteUpdateCmd));

        private void ExecuteUpdateCmd(int? id)
        {
            var dataList = db.GetAllProcessModels().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));


            _dialogService.ShowDialog("UpdateProcessDialog", paramters, r =>
            {

            });
        }

        //下载
        private DelegateCommand<string> _downloadCommand;
        public DelegateCommand<string> DownLoadCommand =>
            _downloadCommand ?? (_downloadCommand = new DelegateCommand<string>(ExecuteDownLoadCommand));

        private void ExecuteDownLoadCommand(string search)
        {

            //var dataList = sdb.GetList();
            // DataTable dt = FileData.ListToDataTable(dataList);

            var data = GridModelList;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\Total.json", json);

            string path = @"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\Total.json";
            if (File.Exists(path))
            {
                MessageBox.Show("文件下载成功！");
            }
        }

        //删除  跟据主键Id删除对应工序
        private DelegateCommand<int?> _deleteCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand<int?>(ExecuteDeleCommand));
            set => _deleteCommand = value;
        }
        private async void ExecuteDeleCommand(int? ids)
        {
            var model = db.GetAllProcessModels().Where(it => it.Id == ids);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否删除该工序?", "删除工序", MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                    db.DeleteProcessModel((int)ids);
                   
                    Refresh();
                }
                if (result == MessageDialogResult.Negative)
                {
                    Refresh();

                }
            }
        }


        /// <summary>
        /// 跟据删除工位，删除指定工序
        /// </summary>
        /// <param name="ids"></param>
        private  void ExecuteDeleByWorkPlace(int ids)
        {
            var model = db.GetAllProcessModels().Where(it => it.WorkPlaceId == ids);

            if (model != null)
            {
                db.DeleteProcessModel(ids);
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


        //用来刷新界面
        public void Refresh()
        {
            var dataList = db.GetAllProcessModels().Where(it => it.ProcessName == Search);

            GridModelList = new ObservableCollection<ProcessModel>();
           
            if (dataList != null)
            {
                db.GetAllProcessModels().ForEach(x => GridModelList.Add(x));
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
            Search = string.Empty;
            this.Refresh();
            await Task.Delay(3000); // Wait for 3 seconds
            await controller.CloseAsync();
        }
        #endregion

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;
           

            continuationCallback(result);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// 在导航到达时刷新界面
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Refresh();
        }

    }
}
