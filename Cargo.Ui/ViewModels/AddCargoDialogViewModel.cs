
namespace Cargo.Ui.ViewModels
{
    public class AddCargoDialogViewModel : BindableBase, IDialogAware
    {

        public Action action { get; set; }
        CargoService db = new CargoService();
        public AddCargoDialogViewModel()
        {

        }

        public string Title => "添加Cargo弹窗";

        public event Action<IDialogResult> RequestClose;

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

        private string _name;

        public string InputName
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _type;

        public string InputTypeName
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private int? _amount;

        public int? InputAmount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private decimal? _price;

        public decimal? InputPrice
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        private string _tag;

        public string InputTag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }


        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }
        private int? _userId;

        public int? InputUserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private string _username;

        public string InputUserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private DelegateCommand _saveCmd;
        public DelegateCommand SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand(ExecuteSaveCmd));

        void ExecuteSaveCmd()
        {

            try
            {
                CargoModel cargo = new CargoModel()
                {
                    Name = InputName,
                    MaterialType = InputTypeName,
                    Amount = (int)InputAmount,
                    Price = (decimal)InputPrice,
                    Tag = InputTag,
                    CreateTime = DateValue,
                    UserId = (int)InputUserId,
                    UserName = InputUserName,
                };
                if (string.IsNullOrEmpty(cargo.Name) || string.IsNullOrEmpty(cargo.UserName) || string.IsNullOrEmpty(cargo.MaterialType)
                    || string.IsNullOrEmpty(cargo.Amount.ToString()))
                {
                    MessageBox.Show("请填写所有必填字段。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    // 执行保存操作
                    db.AddCargoModel(cargo);
                }
            }
            catch (Exception ex)
            {
                // 处理 SqlSugar 抛出的异常，可能是由于字段约束导致的数据插入错误
                MessageBox.Show("请填写所有必填字段。", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }



            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }
        private DelegateCommand<string> _closeCmd;
        public DelegateCommand<string> CloseCmd =>
            _closeCmd ?? (_closeCmd = new DelegateCommand<string>(ExecuteCloseCmd));

        private void ExecuteCloseCmd(string parameter)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.No));
        }
        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }


    }
}
