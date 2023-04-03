using ContentModule.Helpers;
using ContentModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentModule.ViewModels
{
    public class AddCargoDialogViewModel : BindableBase, IDialogAware
    {

        public Action action { get; set; }
        CargoService db = new CargoService(new DatabaseService());
        public AddCargoDialogViewModel()
        {

        }

        public string Title => "添加Cargo弹窗";

        public event Action<IDialogResult> RequestClose;

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
            //action();
        }

        private string _name;

        public string InputName
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }
        private string _type;

        public string InputTypeName
        {
            get { return _type; }
            set { SetProperty<string>(ref _type, value); }
        }

        private int? _amount;

        public int? InputAmount
        {
            get { return _amount; }
            set { SetProperty<int?>(ref _amount, value); }
        }

        private decimal? _price;

        public decimal? InputPrice
        {
            get { return _price; }
            set { SetProperty<decimal?>(ref _price, value); }
        }
        private string _tag;

        public string InputTag
        {
            get { return _tag; }
            set { SetProperty<string>(ref _tag, value); }
        }


        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty<DateTime>(ref _dateValue, value); }
        }
        private int? _userId;

        public int? InputUserId
        {
            get { return _userId; }
            set { SetProperty<int?>(ref _userId, value); }
        }

        private string _username;

        public string InputUserName
        {
            get { return _username; }
            set { SetProperty<string>(ref _username, value); }
        }
       
        private DelegateCommand _saveCmd;
        public DelegateCommand SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand(ExecuteSaveCmd));

        void ExecuteSaveCmd()
        {
            Cargo cargo = new Cargo()
            {
                Name=InputName,
                MaterialType=InputTypeName,
                Amount=(int)InputAmount,
                Price=(decimal)InputPrice,
                Tag=InputTag,
                CreateTime=DateValue,
                UserId=(int)InputUserId,
                UserName=InputUserName,
            };

            db.AddCargo(cargo);
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }
        private DelegateCommand<string> _closeCmd;
        public DelegateCommand<string> CloseCmd =>
            _closeCmd ?? (_closeCmd = new DelegateCommand<string>(ExecuteCloseCmd));

        private void ExecuteCloseCmd(string parameter)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.No));
        }
        //触发窗体关闭事件
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }


    }
}
