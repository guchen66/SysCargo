
namespace Cargo.Ui.ViewModels
{
    public class UpdateWorkPlaceDialogViewModel : BindableBase, IDialogAware
    {

        #region 字段、属性
        public string Title => "修改工位弹窗";
        public SimpleClient<WorkPlace> sdb = new SimpleClient<WorkPlace>(DatabaseService.GetClient());

        /// <summary>
        /// 工位的自增Id
        /// </summary>
        private int _currentId;
        public int CurrentId
        {
            get { return _currentId; }
            set { SetProperty(ref _currentId, value); }
        }

        /// <summary>
        /// 工位Id
        /// </summary>
        private int _workPlaceId;

        public int WorkPlaceId
        {
            get { return _workPlaceId; }
            set { SetProperty(ref _workPlaceId, value); }
        }


        /// <summary>
        /// 工位名称
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

        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }

        #endregion
        ProcessService db = new ProcessService();
        WorkStationService db2 = new WorkStationService();
        public UpdateWorkPlaceDialogViewModel()
        {

            sdb.GetList().ForEach(x => WorkStationList.Add(x));         //使用仓储SimpleClient，List转ObservableCollection

        }



        public event Action<IDialogResult> RequestClose;
        public event Action action;
        IEnumerable<WorkPlace> workStations = null;//= new List<User>();
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
                workStations = parameters.GetValue<IEnumerable<WorkPlace>>("dataList");
                foreach (var workStation in workStations)
                {
                    CurrentId = workStation.Id;
                    WorkPlaceId = workStation.WorkPlaceId;
                    WorkStationName = workStation.WorkPlaceName;
                    DateValue = workStation.CreateTime;

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

            var workList = db2.GetAllWorkPlacesNames().Where(x => x.Id == id);
            var model = workList.FirstOrDefault(o => o.Id == id);

            //两种方法都可以
            if (model != null)
            {
                model.WorkPlaceId = WorkPlaceId;
                model.WorkPlaceName = WorkStationName;
                model.CreateTime = DateValue;

            }
            db2.UpdateWorkPlace(model);



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
