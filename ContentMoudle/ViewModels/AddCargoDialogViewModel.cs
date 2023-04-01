using ContentModule.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SqlSugar.DbAccess.Model.Models;
using SqlSugar.DbAccess.Providers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentModule.ViewModels
{
    public class AddCargoDialogViewModel : BindableBase, IDialogAware
    {
        public DataBaseProvider<User> db = new DataBaseProvider<User>();
        public AddCargoDialogViewModel()
        {
            RoleList = new ObservableCollection<ComboBoxList>(Enum.GetValues(typeof(ComboBoxList)).Cast<ComboBoxList>());

        }

        public string Title => "添加用户弹窗";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }


        private ObservableCollection<ComboBoxList> _roleList;

        public ObservableCollection<ComboBoxList> RoleList
        {
            get { return _roleList; }
            set { SetProperty(ref _roleList, value); }
        }

        private DelegateCommand _saveCmd;
        public DelegateCommand SaveCmd =>
            _saveCmd ?? (_saveCmd = new DelegateCommand(ExecuteSaveCmd));

        void ExecuteSaveCmd()
        {
            //User user = new User();
            // db.SaveData();
        }



    }
}
