
namespace 仓库管理系统.Shell.ViewModels
{

    public class TotalViewModel : BindableBase
    {

        CargoService db = new CargoService();
       
        private readonly IDialogService _dialogService;
        
        private IDialogCoordinator _dialogCoordinator;
      //  IEnumerable<Cargo> dataList=null;
        public TotalViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            //仓储查询结果是List，转成ObservableCollection
            db.GetAllCargoModels().ForEach(x => GridModelList.Add(x));

        }

        private ObservableCollection<CargoModel> gridModelList = new ObservableCollection<CargoModel>();//已经封装好的集合列表，提供实时刷新，当做有通知的List<Student>
        public ObservableCollection<CargoModel> GridModelList//和前台要对应
        {
            get{ return gridModelList;}
            set{ gridModelList = value;RaisePropertyChanged();}
        }


        //查询全部
        public ObservableCollection<CargoModel> SelectAll()
        {
            List<CargoModel> cargos = new List<CargoModel>();
            if (cargos != null)
            {
                GridModelList.Clear();
                cargos.ForEach(x => GridModelList.Add(x));
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
            
            var dataList = db.GetAllCargoModels().ToList().Where(it => it.Id.ToString().Contains(Search)
            || it.Name.Contains(Search)||it.UserName.Contains(Search)
            );
            GridModelList = new ObservableCollection<CargoModel>();
            if (dataList != null)
            {
                dataList.ToList().ForEach(o => GridModelList.Add(o));

            }
        }


        
        //新增
        private DelegateCommand _addCommand;
        public DelegateCommand AddCommand =>
            _addCommand ?? (_addCommand = new DelegateCommand(ExecuteAddCmd));

        void ExecuteAddCmd()
        {
            DialogParameters paramters = new DialogParameters();
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("AddCargoDialog", paramters,r =>
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
            var dataList=db.GetAllCargoModels().Where(it=>it.Id==id);
            DialogParameters paramters = new DialogParameters();

            paramters.Add("dataList", dataList);

            paramters.Add("RefreshValue", new Action(Refresh));

           
            _dialogService.ShowDialog("UpdateCargoDialog", paramters, r =>
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
            File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\Cargo.Ui\DownLoad\Total.json", json);

            string path = @"E:\VS Workspace\Apply\仓库管理系统\Cargo.Ui\DownLoad\Total.json";
            if (File.Exists(path))
            {
                MessageBox.Show("文件下载成功！");
            }
        }

        //删除
        private DelegateCommand<int?> _deleteCommand { get; set; }
        public DelegateCommand<int?> DeleteCommand
        {
            get => _deleteCommand ?? (_deleteCommand = new DelegateCommand<int?>(ExecuteDeleCommand));
            set => _deleteCommand = value;
        }



        private async void ExecuteDeleCommand(int? ids)
        {
            var model = db.GetAllCargoModels().Where(it => it.Id == ids);

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
                    db.DeleteCargoModel((int)ids);
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

        

        //用来刷新界面
        public void Refresh()
        {
            var dataList = db.GetAllCargoModels().Where(it => it.Name == Search);
           
            GridModelList = new ObservableCollection<CargoModel>();
            if (dataList != null)
            {
                db.GetAllCargoModels().ForEach(x => GridModelList.Add(x));
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

    }
}

