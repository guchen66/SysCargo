
namespace 仓库管理系统.Shell.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }
        private void OpenDefaultEmailApp(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "http://mail.kstopa.com.cn/");
            }
            catch (Exception ex)
            {
                // 处理异常情况，例如没有默认邮件应用程序或发生其他错误
                Console.WriteLine(ex.Message);
            }
        }
    }
}
