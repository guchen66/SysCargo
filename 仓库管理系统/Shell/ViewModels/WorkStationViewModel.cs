
namespace 仓库管理系统.Shell.ViewModels
{
    public class WorkStationViewModel : BindableBase
    {
        #region 属性、字段

        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;

        private ObservableCollection<WorkPlace> gridModelList;
        public ObservableCollection<WorkPlace> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 命令

        public ICommand QueryWorkStationCommand { get; set; }
        public ICommand AddWorkStationCommand { get; set; }
        public ICommand UpdateWorkStationCommand { get; set; }
        public ICommand DelWorkStationCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion

        WorkStationService db = new WorkStationService();
        public SimpleClient<ProcessModel> sdb = new SimpleClient<ProcessModel>(DatabaseService.GetClient());
        ProcessService db2 = new ProcessService();
       
        public WorkStationViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;

            QueryWorkStationCommand = new DelegateCommand(ExecuteQuery);
            AddWorkStationCommand = new DelegateCommand(ExecuteAdd);
            UpdateWorkStationCommand = new DelegateCommand<int?>(ExecuteUpdate);
            DelWorkStationCommand = new DelegateCommand<int?>(ExecuteDel);
            RefreshCommand=new DelegateCommand(ExecuteRefresh);
            GridModelList = new ObservableCollection<WorkPlace>();
            db.GetAllWorkPlaces().ForEach(x => GridModelList.Add(x));
            this.Refresh();
        }

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void ExecuteQuery()
        {
            var dataList = db.GetAllWorkPlaces().ToList().Where(it => it.Id.ToString().Contains(Search) || it.WorkPlaceName.Contains(Search));
            GridModelList = new ObservableCollection<WorkPlace>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        private void ExecuteAdd()
        {
            DialogParameters paramters = new DialogParameters();
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("AddWorkStationDialog", paramters, r =>
            {
                /*if (r.Result == ButtonResult.Yes)
                {
                    Refresh();
                }*/
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void ExecuteUpdate(int? id)
        {
            var dataList = db.GetAllWorkPlaces().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();
            paramters.Add("dataList", dataList);
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("UpdateWorkPlaceDialog", paramters, r =>
            {

            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        private async void ExecuteDel(int? ids)
        {
            var model = db.GetAllWorkPlaces().Where(it => it.Id == ids);

            if (model != null)
            {
                var settings = new MetroDialogSettings
                {
                    ColorScheme = MetroDialogColorScheme.Accented,
                    AnimateShow = false,
                    AnimateHide = false,
                    AffirmativeButtonText = "确认"
                };
                var result = await this._dialogCoordinator.ShowMessageAsync(this, "是否删除该工位?", "删除工位", MessageDialogStyle.AffirmativeAndNegative, settings);
                if (result == MessageDialogResult.Affirmative)
                {
                    //刷新
                    db.DeleteWorkPlace((int)ids);
                    /* var msg= MessageBox.Show("是否连同对应工序一起删除。", "删除工序", MessageBoxButton.OK, MessageBoxImage.Error);

                     if (msg.HasFlag(MessageBoxResult.OK))
                     {
                         db2.DeleteWorkStationList((int)ids);
                     }
                     else
                     {
                         return;
                     }*/
                    Refresh();
                    //通知工序表界面实时刷新
                    // _eventAggregator.GetEvent<WorkPlaceDeletedEvent>().Publish((int)ids);
                    _eventAggregator.GetEvent<WorkStationDelEvent>().Publish();
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
            DoRefresh();

        }
        //查询全部
        public ObservableCollection<WorkPlace> SelectAll()
        {
            List<WorkPlace> stations = new List<WorkPlace>();
            if (stations != null)
            {
                GridModelList.Clear();
                stations.ForEach(x => GridModelList.Add(x));
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

        //用来刷新界面
        public void Refresh()
        {
            var dataList = db.GetAllWorkPlaces().Where(it => it.WorkPlaceName == Search);

            GridModelList = new ObservableCollection<WorkPlace>();
            if (dataList != null)
            {
                db.GetAllWorkPlaces().ForEach(x => GridModelList.Add(x));
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
    }
}
