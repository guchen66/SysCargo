﻿using DryIoc;
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
        public SimpleClient<User> sdb = new SimpleClient<User>();
        UserService db = new UserService();

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
            string encryPwd = Encrypt.GetMD5Provider(appData.CurrentUser.Password,appData.CurrentUser.Name);
           

            var user = db.GetAllUsers().FirstOrDefault(item => item.Name == appData.CurrentUser.Name
            && item.Password == encryPwd);

            //
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
