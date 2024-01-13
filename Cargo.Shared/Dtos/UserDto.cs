using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Shared.Dtos
{
    public class UserDto:BindableBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { SetProperty<int>(ref _id, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }
        private string _roleName;

        public string RoleName
        {
            get { return _roleName; }
            set { SetProperty<string>(ref _roleName, value); }
        }
        private DateTime _createTime;

        public DateTime CreateTime
        {
            get { return _createTime; }
            set { SetProperty<DateTime>(ref _createTime, value); }
        }
    }
}
