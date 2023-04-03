using SqlSugar.DbAccess.Model.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 仓库管理系统.Shell.Views
{
    /// <summary>
    /// UserInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoView : UserControl
    {
        public UserInfoView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取DataGrid的数据源
            var data = (ObservableCollection<User>)down.ItemsSource;

            //序列化为Json字符串
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            //保存文件
            System.IO.File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\ContentMoudle\DownLoad\User.json", json);

        }
    }
}
