
namespace 仓库管理系统.Shell.ViewModels
{
    public class SetViewModel:BindableBase
    {
        private DelegateCommand<string> _openSetCommand;
        private IDialogCoordinator _dialogCoordinator;

        public SetViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator=dialogCoordinator;
        }
        public DelegateCommand<string> OpenSetCommand =>
            _openSetCommand ?? (_openSetCommand = new DelegateCommand<string>(DoOpenSet));

        public void DoOpenSet(string name)
        {
            switch (name)
            {
                case "设置": //打开设置界面
                    Window win = new SetdetailView("登录");
                    win.Title = "登录";
                    win.ShowDialog();
                    break;

                case "Login": //打开登录弹窗
                    ShowLoginDialog();
                    break;

                case "Progress": //打开进度弹窗
                    ShowProgressDialogAsync();
                    break;
                case "Custom": //打开自定义弹窗
                    ShowCustomDialogAsync();
                    break;
                case "Input": //打开对话弹窗
                    ShowInputDialogAsync();
                    break;
                case "Message": //打开消息弹窗
                    ShowMessageDialogAsync();
                    break;
            }
        }


       
        public async void ShowLoginDialog()
        {
            var settings = new LoginDialogSettings
            {
                NegativeButtonVisibility = Visibility.Visible,
                NegativeButtonText = "取消",
                AffirmativeButtonText = "登录"
            };

            var result = await _dialogCoordinator.ShowLoginAsync(this, "登录", "请输入用户名和密码", settings);
            if (result == null)
            {
                // 用户取消了登录操作
            }
            else if (!string.IsNullOrEmpty(result.Username) && !string.IsNullOrEmpty(result.Password))
            {
                // 用户输入了用户名和密码，执行登录操作
            }
            else
            {
                // 用户未输入完整的用户名和密码
            }
        }

        public async void ShowProgressDialogAsync()
        {
            var controller = await _dialogCoordinator.ShowProgressAsync(this, "请稍等", "正在进行操作...");
            controller.SetIndeterminate();

            // 执行长时间运行的操作
            await Task.Delay(3000);

            await controller.CloseAsync();
        }


        public async void ShowCustomDialogAsync()
        {
           /* var customDialog = new MyCustomDialog();
            var result = await _dialogCoordinator.ShowMetroDialogAsync(this, customDialog);

            // 等待用户关闭自定义对话框
            await customDialog.WaitUntilUnloadedAsync();

            if ((bool)result)
            {

                // 用户点击了“确认”按钮
            }
            else
            {
                // 用户点击了“取消”按钮
            }*/
        }


        public async void ShowInputDialogAsync()
        {
            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "确认",
                NegativeButtonText = "取消",
                ColorScheme= MetroDialogColorScheme.Inverted,
                AnimateHide = true,
                AnimateShow = true
            };

            var result = await _dialogCoordinator.ShowInputAsync(this, "请输入", "请输入您的姓名：", settings);

            if (!string.IsNullOrEmpty(result))
            {
                // 用户输入了姓名
            }
            else
            {
                // 用户取消了输入操作
            }
        }
        public async void ShowMessageDialogAsync()
        {
            var settings = new MetroDialogSettings
            {
                AffirmativeButtonText = "确认",
                AnimateHide = true,
                AnimateShow = true,
                ColorScheme = MetroDialogColorScheme.Accented
            };

            await _dialogCoordinator.ShowMessageAsync(this, "提示", "操作成功！", MessageDialogStyle.Affirmative, settings);
        }
        /*AffirmativeAndNegative,
                AffirmativeAndNegativeAndSingleAuxiliary,
                AffirmativeAndNegativeAndDoubleAuxiliary*/

    }
}
