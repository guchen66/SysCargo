
namespace 仓库管理系统.ViewModels
{
    public class LoginViewModel : BindableBase
    {

        #region  字段
        public AppData appData { get; set; }=AppData.Instance;
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


        public LoginViewModel()
        {

            this.appData.CurrentUser.Name = "admin";
            this.appData.CurrentUser.Password = "0";
        }

       


        /// <summary>
        /// 带参登录
        /// </summary>
        private DelegateCommand<Window> _loginCommand;
        public DelegateCommand<Window> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<Window>(DoLogin));
       
        private void DoLogin(Window win)
        {
            MD5Encrypt Encrypt = new MD5Encrypt();
            string encryPwd = Encrypt.GetMD5Provider(appData.CurrentUser.Password, appData.CurrentUser.Name);

            var user = db.GetAllUsers().FirstOrDefault(item => item.Name == appData.CurrentUser.Name
            && item.Password == encryPwd);

            if (user == null)
            {
                MessageBox.Show("用户名和密码错误");
            }
            else
            {
                win.DialogResult = true;
                win.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(DoCancel));

        private void DoCancel()
        {
            //App.Current.Shutdown();
            Application.Current.Shutdown();
        }

        private static bool CanSignIn(string username, string password) => !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);

       
    }
}
