using Cargo.Core.Providers;
using Cargo.Shared.Dtos;
using Cargo.SqlSugar.Providers;
using Cargo.SqlSugar.Services;
using System.Windows.Input;

namespace Cargo.Ui.ViewModels
{
    public class UpdateUserDialogViewModel : BindableBase, IDialogAware
    {
        #region 属性、字段

        public string Title => "修改User弹窗";

        public event Action<IDialogResult> RequestClose;
        public event Action action;
        List<User> users = null;
        public DataBaseProvider<User> db = new DataBaseProvider<User>();

        private int _currentId;

        public int CurrentId
        {
            get { return _currentId; }
            set { SetProperty(ref _currentId, value); }
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
        private UserDto userDto;
        public UserDto UserDto
        {
            get { return userDto; }
            set { SetProperty(ref userDto, value); }
        }

        private ObservableCollection<ComboBoxList> _roleList;

        public ObservableCollection<ComboBoxList> RoleList
        {
            get { return _roleList; }
            set { SetProperty(ref _roleList, value); }
        }

        private ObservableCollection<User> _userList;

        public ObservableCollection<User> UserList
        {
            get { return _userList; }
            set { SetProperty(ref _userList, value); }
        }
        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }
        #endregion

        public UpdateUserDialogViewModel()
        {
          
            RoleList = new ObservableCollection<ComboBoxList>(Enum.GetValues(typeof(ComboBoxList)).Cast<ComboBoxList>());
            SaveCommand = new DelegateCommand<int?>(ExecuteSave);
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
            if (parameters.ContainsKey("dataList"))
            {
                users = parameters.GetValue<List<User>>("dataList");
                foreach (var user in users)
                {
                    CurrentId = user.Id;
                    InputName = user.Name;
                    InputPassword = user.Password;
                  //  Role = user.Role;
                    DateValue = user.CreateTime;
                }
            }
            if (parameters.ContainsKey("RefreshValue"))
            {
                action = parameters.GetValue<Action>("RefreshValue");

            }
        }

        private void ExecuteSave(int? id)
        {
            var userList = db.Queryable.Where(x => x.Id == id).ToList();
            var model = userList.FirstOrDefault(o => o.Id == id);
            EncryptProvider encrypt = new EncryptProvider();

            if (userList != null)
            {
                foreach (var user in userList)
                {
                    user.Name = InputName;
                    user.Password = encrypt.SetAESEncrypt(InputPassword);
                 //   user.Role = Role;
                    user.CreateTime = DateValue;
                    db.Update(user);
                }
            }
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
