﻿using ContentModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentModule.ViewModels
{
    public class UpdateUserDialogViewModel : BindableBase, IDialogAware
    {
        
        UserService db = new UserService(new DatabaseService());
        public UpdateUserDialogViewModel()
        {

           

        }

        public string Title => "添加用户弹窗";

        public event Action<IDialogResult> RequestClose;
        public event Action action;
        IEnumerable<User> users=null;//= new List<User>();
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
                users = parameters.GetValue<IEnumerable<User>>("dataList");
                foreach (var user in users)
                {
                     InputName=user.Name;
                     InputPassword=user.Password;
                     Role=user.Role;
                     DateValue = user.CreateTime;
                     
                }
                
            }
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
            set { SetProperty<string>(ref _name, value); }
        }

        private string _password;

        public string InputPassword
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        private string _role;

        public string Role
        {
            get { return _role; }
            set { SetProperty<string>(ref _role, value); }
        }

        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty<DateTime>(ref _dateValue, value); }
        }
       


        private DelegateCommand<string> _saveCmd;
        public DelegateCommand<string> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<string>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(string parameter)
        {
            User user = new User()
            {
                Name = InputName,
                Password = InputPassword,
                CreateTime = DateValue,
                Role = Role
            };
            
            db.UpdateUser(user);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

            /*if (Convert.ToBoolean(parameter) == db.AddUser(user))
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                HandyControl.Controls.Dialog.Show(new ErrorDialog());
            }*/
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