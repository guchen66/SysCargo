﻿using ContentModule.Helpers;
using ContentModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContentModule.ViewModels
{
    public class UpdateUserDialogViewModel : BindableBase, IDialogAware
    {
        
        UserService db = new UserService();
        public UpdateUserDialogViewModel()
        {

           

        }

        public string Title => "修改User弹窗";

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
                    CurrentId = user.Id;
                    InputName = user.Name;
                    InputPassword = user.Password;
                    Role = user.Role;
                    DateValue = user.CreateTime;

                }

            }
            if (parameters.ContainsKey("RefreshValue"))
            {
                action = parameters.GetValue<Action>("RefreshValue");

            }

            //action();
        }

        private int _currentId;

        public int CurrentId
        {
            get { return _currentId; }
            set { SetProperty<int>(ref _currentId, value); }
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

        private string _roleList;

        public string RoleList
        {
            get { return _roleList; }
            set { SetProperty<string>(ref _roleList, value); }
        }
        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty<DateTime>(ref _dateValue, value); }
        }

        private DelegateCommand<int?> _saveCmd;
        public DelegateCommand<int?> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<int?>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(int? id)
        {
           /* User user = new User()
            {
                Name = InputName,
                Password = InputPassword,
                CreateTime = DateValue,
                Role = Role
            };*/
           var userList= db.GetAllUsers().Where(x => x.Id ==id);
           var model = userList.FirstOrDefault(o => o.Id == id);
            MD5Helper mD5Helper = new MD5Helper();
            //两种方法都可以
            /*if (model != null)
            {
                model.Name=InputName;
                model.Password = InputPassword;
                model.CreateTime = DateValue;
                model.Role = Role;
            }
            db.UpdateUser(model);*/
            if (userList != null)
            {
                foreach (var user in userList)
                {
                    user.Name = InputName;
                    user.Password = mD5Helper.GetMD5Provider2(InputPassword, InputName);
                    user.Role=Role;
                    user.CreateTime=DateValue;
                    db.UpdateUser(user);
                }
            }

            
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
