using MahApps.Metro.Controls;
using SqlSugar.DbAccess.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 仓库管理系统.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : MetroWindow
    {
        public LoginView()
        {
            InitializeComponent();

            //SugarGlobal.Initialized(); //初始化数据库自动生成表
        }
    }
}
