using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 仓库管理系统.Shell.ViewModels
{
    public class CountViewModel:BindableBase
    {
        #region  属性、字段

        private DateTime _startTime;

        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty<DateTime>(ref _startTime, value); }
        }
        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty<DateTime>(ref _endTime, value); }
        }
        #endregion
        public CountViewModel()
        {
            QueryCommand = new DelegateCommand(ExecuteQuery);
            ImportCommand = new DelegateCommand(ExecuteImport);
            ExportCommand = new DelegateCommand(ExecuteExport);
        }
        #region  命令

        public ICommand QueryCommand { get; set; }
        public ICommand ImportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        #endregion

        #region  方法
        
        private void ExecuteQuery()
        {
            if (StartTime==null)
            {

            }
        }

        private void ExecuteImport()
        {

        }

        private void ExecuteExport()
        {

        }
        #endregion
    }
}
