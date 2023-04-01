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

namespace 仓库管理系统.Shell.Views
{
    /// <summary>
    /// SetdetailView.xaml 的交互逻辑
    /// </summary>
    public partial class SetdetailView : Window
    {
        public SetdetailView()
        {
            InitializeComponent();
        }
        //窗口重载
        public SetdetailView(string title)
        {
            InitializeComponent();
            
        }
    }
}
