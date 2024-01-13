using Cargo.Core.Providers;
using Cargo.Shared.Dtos;
using Cargo.SqlSugar.Providers;
using Cargo.SqlSugar.Services;
using Mapster;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace Cargo.Ui.ViewModels
{
    public class AddUserDialogViewModel : BindableBase, IDialogAware
    {

        #region 字段、属性

        public string Title => "添加用户弹窗";
        public event Action<IDialogResult> RequestClose;
        public event Action action;
        private DataBaseProvider<User> db = new DataBaseProvider<User>();
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

        private string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { SetProperty(ref _roleName, value); }
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

        #endregion

        public AddUserDialogViewModel()
        {
            RoleList = new ObservableCollection<ComboBoxList>(Enum.GetValues(typeof(ComboBoxList)).Cast<ComboBoxList>());

            SaveCommand =new DelegateCommand<string>(ExecuteSave);
            CancelCommand = new DelegateCommand<string>(ExecuteCancel);
        }
        #region 命令

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion
        #region 方法

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
        }

        private void ExecuteSave(string parameter)
        {
            EncryptProvider encrypt = new EncryptProvider();
            UserDto user = new UserDto()
            {
                Name = InputName,
                Password = encrypt.SetAESEncrypt(InputPassword),
                CreateTime = DateValue,
                RoleName = RoleName
            };
            User user2 = new User();
         //   user2.RoleId = user.Id;
            var dto = user2.Adapt<UserDto>();
         //   dto.Id = user2.Id;
            dto.RoleName = user2.Role?.RoleName ?? string.Empty;
           
            db.Insert(user2);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }


        private void ExecuteCancel(string parameter)
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }
        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        #endregion       
    }
}
