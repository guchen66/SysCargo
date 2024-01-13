using Cargo.SqlSugar.Services;

namespace Cargo.Ui.ViewModels
{
    public class AddWorkStationDialogViewModel : BindableBase, IDialogAware
    {
        //public DataBaseProvider<User> db = new DataBaseProvider<User>();
        #region 字段、属性
        WorkStationService db = new WorkStationService();

        public string Title => "添加工位弹窗";

        /// <summary>
        /// 工位Id
        /// </summary>
        private int? _workPlaceId;

        public int? WorkPlaceId
        {
            get { return _workPlaceId; }
            set { SetProperty(ref _workPlaceId, value); }
        }

        /* private int _workPlaceId;

         public int WorkPlaceId
         {
             get { return _workPlaceId; }
             set { SetProperty<int>(ref _workPlaceId, value); }
         }*/
        /// <summary>
        /// 选中的工位名称
        /// </summary>
        private string _workStationName;

        public string WorkStationName
        {
            get { return _workStationName; }
            set { SetProperty(ref _workStationName, value); }
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
        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }

        #endregion

        public AddWorkStationDialogViewModel()
        {

            // sdb.GetList().ForEach(x => WorkStationList.Add(x));         //使用仓储SimpleClient，List转ObservableCollection

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
        private DelegateCommand<string> _saveCmd;
        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(string parameter)
        {
            try
            {
                WorkPlace workStationModel = new WorkPlace()
                {
                    WorkPlaceId = (int)WorkPlaceId,
                    CreateTime = DateValue,
                    WorkPlaceName = WorkStationName
                };

                if (string.IsNullOrEmpty(workStationModel.WorkPlaceName) || workStationModel.CreateTime == DateTime.MinValue)
                {
                    MessageBox.Show("请填写所有必填字段。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    // 执行保存操作
                    db.AddWorkPlace(workStationModel);
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

    }
}
