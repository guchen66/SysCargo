
using Cargo.Core.Providers;
using Cargo.Shared.Dtos;

namespace 仓库管理系统.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        #region  字段
      //  public AppData appData { get; set; }=AppData.Instance;
        public SimpleClient<User> sdb = new SimpleClient<User>();
        UserService db = new UserService();
        private SignUpArgs _signUpArgs;
        private bool _isRememberPassword;
        private bool _isAutoSignIn;

        #endregion

        #region  属性

        /// <summary>
        /// 记住密码
        /// </summary>
        public bool IsRememberPassword
        {
            get => _isRememberPassword;
            set { if (SetProperty(ref _isRememberPassword, value) && !value) IsAutoSignIn = false; }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        public bool IsAutoSignIn
        {
            get => _isAutoSignIn;
            set { if (SetProperty(ref _isAutoSignIn, value) && value) IsRememberPassword = true; }
        }
        #endregion

        #region 命令

        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion

        public LoginViewModel(IContainerProvider provider) : base(provider)
        {
            this.appData.CurrentUser.Name = "admin";
            this.appData.CurrentUser.Password = "1";
            
            LoginCommand = new DelegateCommand<Window>(DoLogin);
            CancelCommand= new DelegateCommand(DoCancel);
        }

        #region 方法

        /// <summary>
        /// 带参登录
        /// </summary>
        private void DoLogin(Window win)
        {
            EncryptProvider encrypt = new EncryptProvider();
            string encryPwd = encrypt.SetAESEncrypt(appData.CurrentUser.Password);
            var user = db.GetUserList().FirstOrDefault(item => item.Name == appData.CurrentUser.Name && item.Password == encryPwd);
            if (user == null)
            {
                MessageBox.Show("用户名和密码错误");
            }
            else
            {
                if (UserExtension.Vailde(this.appData.CurrentUser))
                {
                    win.DialogResult = true;
                    win.Close();
                }
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        private void DoCancel()
        {
            //App.Current.Shutdown();
            Application.Current.Shutdown();
        }

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
        #endregion

    }
}
