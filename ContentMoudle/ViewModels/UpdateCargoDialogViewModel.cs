using ContentModule.Helpers;
using HandyControl.Controls;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ContentModule.ViewModels
{
    public class UpdateCargoDialogViewModel : BindableBase, IDialogAware
    {

        CargoService db = new CargoService(new DatabaseService());
        public UpdateCargoDialogViewModel()
        {



        }

        public string Title => "修改Cargo弹窗";

        public event Action<IDialogResult> RequestClose;
        public event Action action;
        IEnumerable<Cargo> cargos = null;//= new List<User>();
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
                cargos = parameters.GetValue<IEnumerable<Cargo>>("dataList");
                foreach (var cargo in cargos)
                {
                    CurrentId = cargo.Id;
                    Name = cargo.Name;
                    InputTypeName = cargo.MaterialType;
                    InputAmount=cargo.Amount;
                    InputPrice=cargo.Price;
                    InputTag = cargo.Tag;
                    DateValue = cargo.CreateTime;
                    InputUserId = cargo.UserId;
                    InputUserName = cargo.UserName;


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

        private string _name ;

        public string Name
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }
        private string _type ;

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

        private decimal _price;

        public decimal InputPrice
        {
            get { return _price; }
            set { SetProperty<decimal>(ref _price, value); }
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

        private DelegateCommand<int?> _saveCmd;
        public DelegateCommand<int?> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<int?>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(int? id)
        {
           
            var cargoList = db.GetAllCargos().Where(x => x.Id == id);
            var model = cargoList.FirstOrDefault(o => o.Id == id);
           
            if (model != null)
            {
                model.Name = Name;
                model.MaterialType = InputTypeName;
                model.Amount = InputAmount;
                model.Price = InputPrice;
                model.CreateTime = DateValue;
                model.UserId = InputUserId;
                model.UserName = InputUserName;
             
            }
            db.UpdateCargo(model);

            RaiseRequestClose(new DialogResult(ButtonResult.OK));
           
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
