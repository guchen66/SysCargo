
using MessageBox = System.Windows.MessageBox;

namespace Cargo.Ui.ViewModels
{
    public class AddUserDialogViewModel : BindableBase, IDialogAware
    {
        //public DataBaseProvider<User> db = new DataBaseProvider<User>();

        UserService db = new UserService();
        public AddUserDialogViewModel()
        {

            RoleList = new ObservableCollection<ComboBoxList>(Enum.GetValues(typeof(ComboBoxList)).Cast<ComboBoxList>());

        }

        public string Title => "添加用户弹窗";

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


        private string _name;

        public string InputName
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _password;

        public string InputPassword
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
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



        private DelegateCommand<string> _saveCmd;
        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(string parameter)
        {
            MD5Helper mD5Helper = new MD5Helper();
            User user = new User()
            {
                Name = InputName,
                Password = mD5Helper.GetMD5Provider(InputPassword, InputName),
                CreateTime = DateValue,
                Role = Role
            };

            db.AddUser(user);
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
