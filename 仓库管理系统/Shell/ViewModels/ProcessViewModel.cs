
namespace 仓库管理系统.Shell.ViewModels
{
    public class ProcessViewModel : BindableBase//, IConfirmNavigationRequest
    {
        #region 属性、字段

        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;
        ProcessService db = new ProcessService();

        private ObservableCollection<ProcessModel> gridModelList;
        public ObservableCollection<ProcessModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        #endregion

        

       
        
        public ProcessViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator,IEventAggregator eventAggregator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            _eventAggregator = eventAggregator;
            GridModelList = new ObservableCollection<ProcessModel>();
            db.GetAllProcessModels().ForEach(x => GridModelList.Add(x));
            
            //可以使用事件管理器，也可以使用导航跳转时进行刷新
            _eventAggregator.GetEvent<WorkStationDelEvent>().Subscribe(Refresh);
            QueryCommand = new DelegateCommand(ExecuteQuery);
            AddCommand = new DelegateCommand(ExecuteAdd);
            UpdateCommand = new DelegateCommand<int?>(ExecuteUpdate);
            DeleteCommand = new DelegateCommand<int?>(ExecuteDelete);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
        }
        #region 命令

        public ICommand QueryCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion

        #region 方法


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


        private void ExecuteQuery()
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

        //新增

        private void ExecuteAdd()
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
 
        private void ExecuteUpdate(int? id)
        {
            var dataList = db.GetAllProcessModels().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));


            _dialogService.ShowDialog("UpdateProcessDialog", paramters, r =>
            {

            });
        }

        //删除  跟据主键Id删除对应工序

        private async void ExecuteDelete(int? ids)
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

        private void ExecuteRefresh()
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

        #endregion
    }
}
