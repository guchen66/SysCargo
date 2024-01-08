
namespace Cargo.Ui.ViewModels
{
    public class UpdateProcessDialogViewModel : BindableBase, IDialogAware
    {

        #region 字段、属性
        public string Title => "修改工序弹窗";
        public SimpleClient<WorkPlace> sdb = new SimpleClient<WorkPlace>(DatabaseService.GetClient());

        /// <summary>
        /// 工序的自增Id
        /// </summary>
        private int _currentId;
        public int CurrentId
        {
            get { return _currentId; }
            set { SetProperty(ref _currentId, value); }
        }

        /// <summary>
        /// 所属工位
        /// </summary>
        private string _workStationName;
        public string WorkStationName
        {
            get { return _workStationName; }
            set { SetProperty(ref _workStationName, value); }
        }

        /// <summary>
        /// 所属全部工位
        /// </summary>
        private ObservableCollection<WorkPlace> _workStationList = new ObservableCollection<WorkPlace>();

        public ObservableCollection<WorkPlace> WorkStationList
        {
            get { return _workStationList; }
            set { SetProperty(ref _workStationList, value); }
        }

        /// <summary>
        /// 此工序的操作员
        /// </summary>
        private string _operateName;

        public string OperateName
        {
            get { return _operateName; }
            set { SetProperty(ref _operateName, value); }
        }

        /// <summary>
        /// 工序的名称
        /// </summary>
        private string _orocessName;

        public string ProcessName
        {
            get { return _orocessName; }
            set { SetProperty(ref _orocessName, value); }
        }

        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }

        #endregion
        ProcessService db = new ProcessService();
        public UpdateProcessDialogViewModel()
        {

            sdb.GetList().ForEach(x => WorkStationList.Add(x));         //使用仓储SimpleClient，List转ObservableCollection

        }



        public event Action<IDialogResult> RequestClose;
        public event Action action;
        IEnumerable<ProcessModel> processes = null;//= new List<User>();
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


            if (parameters.ContainsKey("dataList"))
            {
                processes = parameters.GetValue<IEnumerable<ProcessModel>>("dataList");
                foreach (var process in processes)
                {
                    CurrentId = process.Id;
                    WorkStationName = process.WorkPlaceName;
                    OperateName = process.UserName;
                    ProcessName = process.ProcessName;
                    DateValue = process.CreateTime;

                }

            }
            if (parameters.ContainsKey("RefreshValue"))
            {
                action = parameters.GetValue<Action>("RefreshValue");

            }

            //action();
        }


        private DelegateCommand<int?> _saveCmd;
        public DelegateCommand<int?> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<int?>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(int? id)
        {

            var processList = db.GetAllProcessModels().Where(x => x.Id == id);
            var model = processList.FirstOrDefault(o => o.Id == id);

            //两种方法都可以
            if (model != null)
            {
                model.WorkPlaceName = WorkStationName;
                model.ProcessName = ProcessName;
                model.UserName = OperateName;
                model.CreateTime = DateValue;

            }
            db.UpdateProcessModel(model);



            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

            /*if (Convert.ToBoolean(parameter) == db.AddUser(user))
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                HandyControl.Controls.Dialog.Show(new ErrorDialog());
            }*/
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

    }
}
