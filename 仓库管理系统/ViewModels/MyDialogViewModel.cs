using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统.ViewModels
{
   
    public class MyDialogViewModel : BindableBase, IDialogAware
    {
        public MyDialogViewModel()
        {

        }
        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }
        //弹框打开时触发，用来接收传递参数
        public void OnDialogOpened(IDialogParameters parameters)
        {
           // var title = parameters.GetValue<string>("value");
        }

        //确定写在构造器
        //public DelegateCommand SaveCommand { get; set; }//要用公共类，否则不生效

        //也可以写在外面
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(DoSaveCmd));

        private void DoSaveCmd()
        {
            // DialogParameters param = new DialogParameters();
            // param.Add("value", Title);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }
    }
}
