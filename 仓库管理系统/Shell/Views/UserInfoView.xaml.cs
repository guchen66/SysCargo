
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
            System.IO.File.WriteAllText(@"E:\VS Workspace\Apply\仓库管理系统\Cargo.Ui\DownLoad\User.json", json);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            down.Items.Filter = FilterMethod;
        }

        private bool FilterMethod(object obj)
        {
            var user = (User)obj;

            return user.Name.Contains(FilterTextBox.Text, StringComparison.OrdinalIgnoreCase);


        }
    }
}
