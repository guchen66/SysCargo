
using MiniExcelLibs;

namespace 仓库管理系统.Shell.ViewModels
{

    public class TotalViewModel : BindableBase
    {
        #region 属性、字段

        private readonly IDialogService _dialogService;
        private readonly IDialogCoordinator _dialogCoordinator;
        CargoService db = new CargoService();

        private ObservableCollection<CargoModel> gridModelList;
        public ObservableCollection<CargoModel> GridModelList
        {
            get { return gridModelList; }
            set { gridModelList = value; RaisePropertyChanged(); }
        }
        #endregion

        #region 命令

        public ICommand QueryCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DownLoadCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        #endregion      
       
        public TotalViewModel(IDialogService dialogService, IDialogCoordinator dialogCoordinator)
        {

            _dialogService = dialogService;
            _dialogCoordinator = dialogCoordinator;
            GridModelList = new ObservableCollection<CargoModel>();
            db.GetAllCargoModels().ForEach(x => GridModelList.Add(x));

            QueryCommand = new DelegateCommand(ExecuteQuery);
            AddCommand = new DelegateCommand(ExecuteAdd);
            UpdateCommand = new DelegateCommand<int?>(ExecuteUpdate);
            DeleteCommand = new DelegateCommand<int?>(ExecuteDelete);
            DownLoadCommand = new DelegateCommand<string>(ExecuteDownLoad);
            RefreshCommand = new DelegateCommand(ExecuteRefresh);
           
        }
        #region 方法
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

        /// <summary>
        /// 查询
        /// </summary>
        private void ExecuteQuery()
        {
            var dataList = db.GetAllCargoModels().ToList().Where(it => it.Id.ToString().Contains(Search) || it.Name.Contains(Search) || it.UserName.Contains(Search));
            GridModelList = new ObservableCollection<CargoModel>();
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
            _dialogService.ShowDialog("AddCargoDialog", paramters, r =>
            {
                //触发回调
                /*if (r.Result == ButtonResult.Yes)
                {
                    Refresh();
                }*/
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        private void ExecuteUpdate(int? id)
        {
            var dataList = db.GetAllCargoModels().Where(it => it.Id == id);
            DialogParameters paramters = new DialogParameters();
            paramters.Add("dataList", dataList);
            paramters.Add("RefreshValue", new Action(Refresh));
            _dialogService.ShowDialog("UpdateCargoDialog", paramters, r =>
            {

            });
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="search"></param>
        private async void ExecuteDownLoad(string search)
        {
            var fileName = $"{DateTime.Now:yyyyMMddHHmmss}-Cargo.xlsx";
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), fileName);
            var dataList = db.GetAllCargoModels().ToList();
            MiniExcel.SaveAs(filePath, RewriteTitle(dataList));
            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "确认",
                AnimateHide = true,
                AnimateShow = true,
                ColorScheme = MetroDialogColorScheme.Accented
            };

            await _dialogCoordinator.ShowMessageAsync(this, "提示", "操作成功！", MessageDialogStyle.Affirmative, settings);
        }

        private IEnumerable<Dictionary<string,object>> RewriteTitle(List<CargoModel> cargoModels)
        {
            foreach (var item in cargoModels)
            {
                var dict = new Dictionary<string,object>();
                dict.Add("物资Id",item.Id);
                dict.Add("物资名称", item.Name);
                dict.Add("物资类型", item.MaterialType);
                dict.Add("物资单位", item.Amount);
                dict.Add("价格", item.Price);
                dict.Add("备注", item.Tag);
                dict.Add("创建日期", item.CreateTime);
                dict.Add("操作员Id", item.UserId);
                dict.Add("操作员", item.UserName);

                yield return dict;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        private async void ExecuteDelete(int? ids)
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

        /// <summary>
        /// 刷新
        /// </summary>
        private void ExecuteRefresh()
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

        #endregion
    }
}

