using DryIoc;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SqlSugar;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar.DbAccess.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 仓库管理系统.Global;
using 仓库管理系统.Views;

namespace 仓库管理系统.ViewModels
{
    public class LoginViewModel:BindableBase
    {
        public AppData appData { get; set; }=AppData.Instance;
        public SimpleClient<User> sdb = new SimpleClient<User>(DatabaseService.CreateClient());
       
       
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
           

            var user = sdb.GetList().FirstOrDefault(item => item.Name == appData.CurrentUser.Name
            && item.Password == appData.CurrentUser.Password);

            
            if (user == null)
            {
                MessageBox.Show("用户名和密码错误");
            }
            else
            {
                
                //MainWindow mainWindow = new MainWindow();
                 win.DialogResult = true;
                 win.Close();
               // mainWindow.Show();
                /*var mainWindow = _container.Resolve<MainWindow>();
                RegionManager.SetRegionManager(mainWindow, _regionManager);
                mainWindow.Show();
                win.Close();*/
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
    }
}
