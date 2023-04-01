using ContentMoudle.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using 仓库管理系统.Shell.Views;

namespace 仓库管理系统.Shell.ViewModels
{
    public class SetViewModel:BindableBase
    {
        private DelegateCommand<string> _openSetCommand
            ;
        public DelegateCommand<string> OpenSetCommand =>
            _openSetCommand ?? (_openSetCommand = new DelegateCommand<string>(DoOpenSet));

        public void DoOpenSet(string name)
        {
            switch (name)
            {
                case "SetName": //打开设置界面
                    Window win = new SetdetailView("设置1");
                    win.Title = "设置1";
                    win.ShowDialog();
                    break;

                

               

            }
        }
    }
}
