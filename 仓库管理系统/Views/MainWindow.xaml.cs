using MahApps.Metro.Controls;
using SqlSugar.DbAccess.Db;
using System.Windows;

namespace 仓库管理系统.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow :  MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //Ctrl Alt J打开对象浏览器
           // SugarGlobal.Initialized(); //初始化数据库自动生成表
        }
    }
}
