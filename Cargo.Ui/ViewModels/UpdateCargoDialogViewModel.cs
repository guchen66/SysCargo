﻿
namespace Cargo.Ui.ViewModels
{
    public class UpdateCargoDialogViewModel : BindableBase, IDialogAware
    {

        CargoService db = new CargoService();
        public UpdateCargoDialogViewModel()
        {



        }

        public string Title => "修改Cargo弹窗";

        public event Action<IDialogResult> RequestClose;
        public event Action action;
        IEnumerable<CargoModel> cargos = null;//= new List<User>();
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
                cargos = parameters.GetValue<IEnumerable<CargoModel>>("dataList");
                foreach (var cargo in cargos)
                {
                    CurrentId = cargo.Id;
                    Name = cargo.Name;
                    InputTypeName = cargo.MaterialType;
                    InputAmount = cargo.Amount;
                    InputPrice = cargo.Price;
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
            set { SetProperty(ref _currentId, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _type;

        public string InputTypeName
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        private int? _amount;

        public int? InputAmount
        {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private decimal _price;

        public decimal InputPrice
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
        private string _tag;

        public string InputTag
        {
            get { return _tag; }
            set { SetProperty(ref _tag, value); }
        }


        private DateTime _dateValue = DateTime.Now;
        public DateTime DateValue
        {
            get { return _dateValue; }
            set { SetProperty(ref _dateValue, value); }
        }
        private int? _userId;

        public int? InputUserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        private string _username;

        public string InputUserName
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private DelegateCommand<int?> _saveCmd;
        public DelegateCommand<int?> SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand<int?>(ExecuteSaveCmd));

        private void ExecuteSaveCmd(int? id)
        {

            var cargoList = db.GetAllCargoModels().Where(x => x.Id == id);
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
            db.UpdateCargoModel(model);

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