using Cargo.SqlSugar.Repositorys;
using Cargo.SqlSugar.Services;

namespace Cargo.Ui.ViewModels
{

    public class AddProcessDialogViewModel : BindableBase, IDialogAware
    {
        #region  字段、属性
        public string Title => "添加工序弹窗";
        ProcessService db = new ProcessService();
        public BaseRepository<WorkPlace> sdb = new BaseRepository<WorkPlace>();


        /// <summary>
        /// 输入的姓名
        /// </summary>
        private string _inputname;
        public string InputName
        {
            get { return _inputname; }
            set { SetProperty(ref _inputname, value); }
        }

        /// <summary>
        /// 选中的工位名称
        /// </summary>
        private WorkPlace _workStationItem = new WorkPlace();

        public WorkPlace WorkStationItem
        {
            get { return _workStationItem; }
            set { SetProperty(ref _workStationItem, value); }
        }


        /// <summary>
        /// 输入的工序名称
        /// </summary>
        private string _processName;

        public string ProcessName
        {
            get { return _processName; }
            set { SetProperty(ref _processName, value); }
        }



        private string _role;

        public string Role
        {
            get { return _role; }
            set { SetProperty(ref _role, value); }
        }

        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }
        private ObservableCollection<ComboBoxList> _roleList;

        public ObservableCollection<ComboBoxList> RoleList
        {
            get { return _roleList; }
            set { SetProperty(ref _roleList, value); }
        }

        /// <summary>
        /// 所属工位
        /// </summary>
        private ObservableCollection<WorkPlace> _workStationList = new ObservableCollection<WorkPlace>();

        public ObservableCollection<WorkPlace> WorkStationList
        {
            get { return _workStationList; }
            set { SetProperty(ref _workStationList, value); }
        }
        #endregion

        public AddProcessDialogViewModel()
        {

            RoleList = new ObservableCollection<ComboBoxList>(Enum.GetValues(typeof(ComboBoxList)).Cast<ComboBoxList>());
            sdb.GetList().ForEach(x => WorkStationList.Add(x));         //使用仓储SimpleClient，List转ObservableCollection
        }



        public event Action<IDialogResult> RequestClose;
        public event Action action;
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            action();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("RefreshValue"))
            {
                action = parameters.GetValue<Action>("RefreshValue");

            }
            //action();
        }
        #region   命令

        private DelegateCommand<string> _saveCmd;
        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));
        private void ExecuteSaveCmd(string parameter)
        {
            try
            {
                int id = sdb.GetSingle(x => x.WorkPlaceName == WorkStationItem.WorkPlaceName).WorkPlaceId;
                ProcessModel processModel = new ProcessModel()
                {
                    ProcessName = ProcessName,
                    UserName = InputName,
                    CreateTime = DateValue,
                    WorkPlaceId = id,
                    WorkPlaceName = WorkStationItem.WorkPlaceName
                };

                if (string.IsNullOrEmpty(processModel.ProcessName) || string.IsNullOrEmpty(processModel.UserName) || string.IsNullOrEmpty(processModel.WorkPlaceName))
                {
                    MessageBox.Show("请填写所有必填字段。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    // 执行保存操作
                    db.AddProcessModel(processModel);
                }
            }
            catch (Exception ex)
            {
                // 处理 SqlSugar 抛出的异常，可能是由于字段约束导致的数据插入错误
                MessageBox.Show("请填写所有必填字段。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }


        private DelegateCommand<string> _closeCmd;
        public DelegateCommand<string> CloseCmd =>
            _closeCmd ?? (_closeCmd = new DelegateCommand<string>(ExecuteCloseCmd));

        private void ExecuteCloseCmd(string parameter)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }
        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        #endregion

        #region  方法

        private void AddCurrentData()
        {

        }

        #endregion

    }
}
